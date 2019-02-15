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
    public class PointIntersection : PointBase 
    {

        internal LineBase m_line1 = null;
        internal LineBase m_line2 = null;

        /// <summary>
        /// Solve   [0] =   [a1 b1  c1] [x]
        ///         [0] =   [a2 b2  c2] [y]
        ///                             [1]
        /// </summary>
        /// </summary>
        /// <param name="dependencies"></param>
        public PointIntersection(List<ElementBase> dependencies) : base(dependencies)
        {
            m_line1 = dependencies.First() as LineBase;
            m_line2 = dependencies.Last() as LineBase;
            OnValueChanged(this, null);
        }
        

        /// <summary>
        /// Calculate the intersection of two lines
        /// May expanded to line-circle , circle-circle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void OnValueChanged(object sender, EventArgs args)
        {
            Mat xVectors = new Mat();
            List<Mat> lineCoes = new List<Mat>
            {
                m_line1.Coefficient().Transpose(),
                m_line2.Coefficient().Transpose()
            };
            //vertical concate
            Cv2.VConcat(lineCoes.ToArray(), xVectors);

            m_point = LinearAlgebra.RightSingularVector(xVectors);

            if (m_point.Get<double>(m_point.Rows - 1) != 0)
            {
                //intersection is finite
                //homogenous
                m_point /= m_point.Get<double>(m_point.Rows - 1);
                base.OnValueChanged(sender, args);
            }
            //TODO , otherwise , no solution or out-of range
        }

    }
}
