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
        ///
        /// </summary>
        public Mat Coefficient()
        {
            return m_coeff;
        }
        internal Mat m_coeff;

        internal PointBase m_end1 = null;
        internal PointBase m_end2 = null;

        public LineBase(List<ElementBase> dependencies) : base(dependencies)
        {
            //perform downcasting , lack of exception handling
            m_end1 = dependencies.First() as PointBase;
            m_end2 = dependencies.Last() as PointBase;

            OnValueChanged(this, null);
        }

        /// <summary>
        /// ouput coeffcient , ax+by+c=0
        /// Solve : [0] = [x1   y1  1]  [a]
        ///         [0]   [x2   y2  1]  [b]
        ///                             [c]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void OnValueChanged(object sender, EventArgs args)
        {
            Mat xVectors = new Mat();
            List<Mat> coord = new List<Mat>{
                m_end1.Point.Transpose(),
                m_end2.Point.Transpose()
            };
            //vertical concate
            Cv2.VConcat(coord.ToArray(), xVectors);
            m_coeff = LinearAlgebra.RightSingularVector(xVectors); ;


            base.OnValueChanged(sender, args);
        }


    }
}
