using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace WindowsFormsApp2.Arch
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
        public enum DefinitionDependecies
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
                return __transformation[0, __transformation.Rows - 1, 0, __transformation.Cols - 1];
            }
            set
            {
                __transformation[0, __transformation.Rows - 1, 0, __transformation.Cols - 1] = value;
            }
        }
        /// <summary>
        /// Editing interface
        /// Last column
        /// </summary>
        public Mat Translation
        {
            get
            {
                return __transformation.Col[__transformation.Cols - 1];
            }
            set
            {
                __transformation.Col[__transformation.Cols - 1] = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Mat Transformation
        {
            get
            {
                return __transformation;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal Mat __transformation;
        internal DefinitionDimension __dimension;
        /// <summary>
        /// build transform matrix to parent by points set
        /// </summary>
        /// <param name="dependencies"></param>
        public CoordinateBase(List<ElementBase> dependencies) : base(dependencies)
        {
            
        }
        /// <summary>
        /// Default constructor (Root creation
        /// </summary>
        public CoordinateBase(DefinitionDimension dimension= DefinitionDimension.DIM_2D) : base(null)
        {
            __transformation = Mat.Eye((int)dimension, (int)dimension, MatType.CV_64FC1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void OnDependeciesValueChanged(object sender, EventArgs args)
        {
            switch (__dimension)
            {
                case DefinitionDimension.DIM_2D:
                    //setup origin
                    //calculate unit vector of X_END - ORIGIN as X_VECTOR
                    //calculate Y_VECTOR = (unit vector of Y_END - ORIGIN) - X_VECTOR
                    break;
                case DefinitionDimension.DIM_3D:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CoordinateBase other)
        {
            //reference equvalent
            return other._coordinateReference == this._coordinateReference;
        }


        //TODO , traverse to desitionation coordinate

        //define CoordinateBase*CoodianteBase , ? ( HTM composed

        #region "operator overload"
        public static CoordinateBase operator *(CoordinateBase left, CoordinateBase right)
        {
            //TODO , return composed type if left/right are element type
            //TODO , cacading , and holding reference

            //check if reference matched
            return new CoordinateBase()
            {
                _coordinateReference = left._coordinateReference,
                __transformation = left.__transformation * right.__transformation
            };
        }
        #endregion
    }
}
