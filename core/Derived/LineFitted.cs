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
                LinearAlgebra.FittingCategrory.Polynominal,
                1);

            //TODO , calculate m_end1, m_en2

            base.OnValueChanged(sender, args);
        }

        /// <summary>
        /// 1. Calculate projection onto line
        /// 2. 
        /// </summary>
        internal void calculateEndPoints()
        {
            IEnumerable<MatExpr> errorList = m_dependencies.Select((ElementBase p) =>
            {
                return -1 * (m_coeff * (p as PointBase).Point);
            });
            //takes (a,b) only , should be 1x2
            var subCoeff = m_coeff.SubMat(
                0, 
                m_coeff.Rows,
                0, 
                m_coeff.Cols - 1);

            //map the vector perpendicular to line corresponding to each points
            IEnumerable<Mat> subVectorList = errorList.Select((MatExpr y) =>
            {
                
                return LinearAlgebra.DataFitting(subCoeff, y, LinearAlgebra.FittingCategrory.Polynominal);
            });

            IEnumerable<Mat> projectionList = subVectorList.Select((Mat sub) =>
            {
                int index = subVectorList.ToList().IndexOf(sub);
                return (Mat)((m_dependencies[index] as PointBase).Point + sub);
            });

            //generate initial length table
            var lengthTable = projectionList.Select((Mat point) =>
            {
                return ((Mat)(point - projectionList.First())).Norm();
            });

            //find the max one
            int maxIndex = lengthTable.ToList().IndexOf(lengthTable.Max());
            if(m_end1 != null)
                m_end1 = new PointBase(new List<ElementBase> { this});
            m_end1.Point = projectionList.ToList()[maxIndex];

            //find the another end
            lengthTable = projectionList.Select((Mat point) =>
            {
                return ((Mat)(point - m_end1.Point)).Norm();
            });
            if(m_end2 != null)
                m_end2 = new PointBase(new List<ElementBase> { this });
            m_end2.Point = projectionList.ToList()[lengthTable.ToList().IndexOf(lengthTable.Max())];
        }
    }
}
