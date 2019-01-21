using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace Core.Arch
{
    /// <summary>
    /// As artribary point
    /// </summary>
    public class PointBase : 
        ElementBase
    {
        /// <summary>
        /// Able to be modified
        /// but need to check dimension?
        /// </summary>
        public virtual Mat Point
        {
            get { return m_point; }
            set
            {
                m_point = value.Clone(); //value copy
                OnValueChanged(this, null);
            }
        }

        /// <summary>
        /// Dimension restricted , homogenous
        /// [x]
        /// [y]
        /// [1]
        /// </summary>
        internal Mat m_point = Mat.Ones((int)DefinitionDimension.DIM_2D,1,MatType.CV_64FC1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencies"></param>
        public PointBase(List<ElementBase> dependencies) : base(dependencies)
        {
            
        }

        #region "operator overload"
        public static PointBase operator *(CoordinateBase coord, PointBase position)
        {
            //reference upward
            return new PointBase(null) {
                m_coordinateReference = coord.m_coordinateReference,
                m_point = coord.Transformation * position.m_point};
        }
        public static PointBase operator +(PointBase position1, PointBase position2)
        {
            return new PointBase(null)
            {
                m_coordinateReference = position1.m_coordinateReference,
                m_point = position1.m_point + position2.m_point
            };
        }
        public static PointBase operator -(PointBase position1, PointBase position2)
        {
            return new PointBase(null)
            {
                m_coordinateReference = position1.m_coordinateReference,
                m_point = position1.m_point - position2.m_point
            };
        }
        #endregion
    }
}
