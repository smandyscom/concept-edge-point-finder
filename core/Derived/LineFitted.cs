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
    /// Dependencies as multiple points
    /// </summary>
    public class LineFitted : LineBase
    {
        /// <summary>
        /// Multiple points (over 3 points
        /// </summary>
        /// <param name="dependencies"></param>
        public LineFitted(List<ElementBase> dependencies) : base(dependencies)
        {
            OnValueChanged(this, null);
        }

        public override void OnValueChanged(object sender, EventArgs args)
        {
            //TODO invoke fitting methods
            //output m_end1 , m_end2
            Mat xVectors = new Mat();
            var coords = m_dependencies.Select((ElementBase p) => 
            {
                return (p as PointBase).Point.Transpose();
            });
            //
            Cv2.VConcat(coords.ToArray(), xVectors);
            m_coeff =  
                LinearAlgebra.DataFitting(xVectors,
                Mat.Zeros(xVectors.Rows, 1, xVectors.Type()),
                LinearAlgebra.FittingCategrory.Polynominal);

            base.OnValueChanged(sender, args);
        }
    }
}
