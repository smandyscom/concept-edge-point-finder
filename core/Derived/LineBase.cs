using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Arch;
using Core.LA;
using OpenCvSharp;

namespace Core.Derived
{
    /// <summary>
    /// Dependencies
    /// Two points
    /// </summary>
    public class LineBase : ElementBase , IGeometricItem
    {
        public Mat Vector
        {
            get
            {
                return m_end2.Point - m_end1.Point;
            }
        }
        public double Length
        {
            get { return Vector.Norm(); }
        }

        public PointBase End1
        {
            get { return m_end1; }
        }
        public PointBase End2
        {
            get { return m_end2; }
        }

        /// <summary>
        /// ouput coeffcient , ax+by+c=0
        /// Solve : [0] = [x1   y1  1]  [a]
        ///         [0]   [x2   y2  1]  [b]
        ///                             [c]
        /// </summary>
        public Mat Coefficient()
        {
            Mat xVectors = new Mat();
            List<Mat> coord = new List<Mat>{
                m_end1.Point.Transpose(),
                m_end2.Point.Transpose()
            };
            //vertical concate
            Cv2.VConcat(coord.ToArray(), xVectors);


            return LinearAlgebra.RightSingularVector(xVectors);
        }

        internal PointBase m_end1 = null;
        internal PointBase m_end2 = null;

        public LineBase(List<ElementBase> dependencies) : base(dependencies)
        {
            //perform downcasting , lack of exception handling
            m_end1 = dependencies.First() as PointBase;
            m_end2 = dependencies.Last() as PointBase;
        }

        
    }
}
