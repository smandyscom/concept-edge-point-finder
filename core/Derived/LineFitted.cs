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
            //TODO , called twice , bad
            OnValueChanged(this, null);
        }

        public override void OnValueChanged(object sender, EventArgs args)
        {
            //TODO invoke fitting methods
            //output m_end1 , m_end2
            calculateCoeff();

            //TODO , calculate m_end1, m_en2

            //base.OnValueChanged(sender, args);
        }

        /// <summary>
        /// 1. Calculate projection onto line
        /// 2. 
        /// </summary>
        internal void calculateEndPoints()
        {
            IEnumerable<Mat> projectionList = m_dependencies.Select((ElementBase sub) =>
            {
                return LinearAlgebra.CalculateProjection(m_coeff.Transpose(), (sub as PointBase).Point);
            });

            //generate initial length table , refer to first point in the list
            var lengthTable = projectionList.Select((Mat point) =>
            {
                return ((Mat)(point - projectionList.First())).Norm();
            });

            //find the max one (farest one) , as one end
            int maxIndex = lengthTable.ToList().IndexOf(lengthTable.Max());
            if(m_end1 != null)
                m_end1 = new PointBase(new List<ElementBase> { this});
            m_end1.Point = projectionList.ToList()[maxIndex];

            //re-generate length table , refer to m_end1
            lengthTable = projectionList.Select((Mat point) =>
            {
                return ((Mat)(point - m_end1.Point)).Norm();
            });
            if(m_end2 != null)
                m_end2 = new PointBase(new List<ElementBase> { this });

            //get farest point as end point
            m_end2.Point = projectionList.ToList()[lengthTable.ToList().IndexOf(lengthTable.Max())];
        }

        internal void calculateCoeff()
        {
            Mat xVectors = new Mat();
            var coords = m_dependencies.Select((ElementBase p) =>
            {
                return (p as PointBase).Point.Transpose();
            });
            //
            Cv2.VConcat(coords.ToArray(), xVectors);
            m_coeff =
                LinearAlgebra.DataFitting(
                    xVectors,
                    Mat.Zeros(xVectors.Rows, 1, xVectors.Type()),
                    LinearAlgebra.FittingCategrory.Polynominal,
                    1);
        }
    }
}
