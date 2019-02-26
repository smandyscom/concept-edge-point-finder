using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace Core.Arch
{
    /// <summary>
    /// /// Define the homogenous transformation dimension
    /// /// </summary>
    public enum DefinitionDimension : int
    {
            DIM_2D = 3,
            DIM_3D = 4,
    }


    /// <summary>
    /// 
    /// </summary>
    public class CoordinateBase :
        ElementBase,
        IEquatable<CoordinateBase>
    {
        /// <summary>
        /// Defines reference points meaning
        /// </summary>
        public enum DefinitionDependecies : int
        {
            ORIGIN = 0,
            X_END=1,
            Y_END=2,
            Z_END=3,
        }

        /// <summary>
        /// Editing interface
        /// Sub-matrix n-1 by n-1
        /// </summary>
        public Mat Rotation
        {
            get
            {
                return m_transformation[0, m_transformation.Rows - 1, 0, m_transformation.Cols - 1];
            }
        }
        /// <summary>
        /// Editing interface
        /// Last column
        /// </summary>
        public Mat Origin
        {
            get
            {
                return m_transformation.Col[m_transformation.Cols - 1];
            }
        }
        /// <summary>
        /// Transformation to parent
        /// </summary>
        public Mat Transformation
        {
            get
            {
                return m_transformation;
            }
        }
        /// <summary>
        /// Transformation to any family member
        /// if destination = null , means to root
        /// </summary>
        /// <returns></returns>
        public CoordinateBase Generate(HierarchyTreeNode<CoordinateBase> destination=null)
        {
            var m_path = Node.Path(destination);
            return new CoordinateComposed(m_path.ToList<ElementBase>());
        }
        /// <summary>
        /// 
        /// </summary>
        public HierarchyTreeNode<CoordinateBase> Node
        {
            get { return m_node; }
        }
        /// <summary>
        /// Transformation to parent , 3x3 for 2D , 4x4 for 3D
        /// </summary>
        internal Mat m_transformation = new Mat();
        
        internal DefinitionDimension m_dimension = DefinitionDimension.DIM_2D;
        /// <summary>
        /// My node
        /// </summary>
        internal HierarchyTreeNode<CoordinateBase> m_node = null;

        /// <summary>
        /// Depends on 3points , origin-x_end-y_end
        /// </summary>
        /// <param name="dependencies"></param>
        public CoordinateBase(List<ElementBase> dependencies) : base(dependencies)
        {
            //establish node link
            m_node = new HierarchyTreeNode<CoordinateBase>(this);
            m_node.Parent = m_coordinateReference;

            OnValueChanged(this, null);
        }
        /// <summary>
        /// Default constructor (Root creation
        /// </summary>
        public CoordinateBase(DefinitionDimension dimension= DefinitionDimension.DIM_2D) : base(null)
        {
            //establish node link
            m_node = new HierarchyTreeNode<CoordinateBase>(this);
            m_node.Parent = null;

            m_dependencies = null;
            m_transformation = Mat.Eye((int)dimension, (int)dimension, MatType.CV_64FC1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void OnValueChanged(object sender, EventArgs args)
        {
            Mat origin = (m_dependencies.First() as PointBase).Point;
            Mat xEnd = (m_dependencies[Convert.ToInt32(DefinitionDependecies.X_END)] as PointBase).Point;
            Mat yEnd = (m_dependencies[Convert.ToInt32(DefinitionDependecies.Y_END)] as PointBase).Point;

            switch (m_dimension)
            {
                case DefinitionDimension.DIM_2D:
                    //setup origin
                    //calculate unit vector of X_END - ORIGIN as X_VECTOR
                    //calculate Y_VECTOR = (unit vector of Y_END - ORIGIN) - X_VECTOR

                    Mat xVec = (xEnd - origin);
                    xVec /= xVec.Norm();

                    Mat yVec = (yEnd - origin);
                    //minus complement vector
                    var scalar = (yVec.Transpose() * xVec).ToMat();
                    var substract = (xVec * (yVec.Transpose() * xVec)).ToMat();

                    yVec -= substract;
                    yVec /= yVec.Norm();

                    //output
                    Cv2.HConcat(new Mat[] { xVec, yVec, origin }, m_transformation);

                    break;
                case DefinitionDimension.DIM_3D:
                    break;
                default:
                    break;
            }
            base.OnValueChanged(sender, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CoordinateBase other)
        {
            //reference equvalent
            return other.m_coordinateReference == this.m_coordinateReference;
        }

        //define CoordinateBase*CoodianteBase , ? ( HTM composed

        #region "operator overload"
        public static CoordinateBase operator *(CoordinateBase left, CoordinateBase right)
        {
            //TODO , return composed type if left/right are element type
            //TODO , cacading , and holding reference

            //check if reference matched
            return new CoordinateBase()
            {
                m_coordinateReference = left.m_coordinateReference,
                m_transformation = left.m_transformation * right.m_transformation
            };
        }
        #endregion
    }
}
