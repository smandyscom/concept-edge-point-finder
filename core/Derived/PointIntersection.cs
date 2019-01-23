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
    class PointIntersection : PointBase , IGeometricItem
    {

        internal LineBase m_line1 = null;
        internal LineBase m_line2 = null;

        /// <summary>
        
        /// </summary>
        /// <param name="dependencies"></param>
        public PointIntersection(List<ElementBase> dependencies) : base(dependencies)
        {
                      
        }
        /// <summary>
        /// Solve   [0] =   [a1 b1  c1] [x]
        ///         [0] =   [a2 b2  c2] [y]
        ///                             [1]
        /// </summary>
        /// <returns></returns>
        public Mat Coefficient()
        {
            Mat xVectors = new Mat();
            List<Mat> lineCoes = new List<Mat>
            {
                m_line1.Coefficient(),
                m_line2.Coefficient()
            };
            //vertical concate
            Cv2.VConcat(lineCoes.ToArray(), xVectors);

            Mat result = LinearAlgebra.RightSingularVector(xVectors);

            //homogenous
            result /= result.Get<double>(result.Size().Height-1);

            return result;
        }

        /// <summary>
        /// Calculate the intersection of two lines
        /// May expanded to line-circle , circle-circle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void OnValueChanged(object sender, EventArgs args)
        {
            base.OnValueChanged(sender, args);
        }

    }
}
