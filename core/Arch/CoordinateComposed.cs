using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace Core.Arch
{
    /// <summary>
    /// Composed coordinate , 
    /// holding references of coordinates (keep tracking updated coordinate base
    /// Dependecies would be CoordinateBase
    /// First element in depencie list : source
    /// Last : destination
    /// </summary>
    public class CoordinateComposed : CoordinateBase
    {
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencies"></param>
        public CoordinateComposed(List<ElementBase> dependencies) : base(dependencies)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void OnValueChanged(object sender, EventArgs args)
        {
            UpdateTransformation();
            base.OnValueChanged(sender, args);
        }
        
        /// <summary>
        /// Resolve referenced coordinates
        /// Compute out overall transformation matrix
        /// </summary>
        internal override void UpdateTransformation()
        {
            var enumerator = m_dependencies.GetEnumerator();
            var _parent = ((CoordinateBase)m_dependencies.First()).Node;

            m_transformation = Mat.Eye(4, 4, MatType.CV_64FC1);//TODO , inconsistent
            //from source to destination
            // S->D
            while (enumerator.MoveNext())
            {
                if (_parent == ((CoordinateBase)enumerator.Current).Node)
                {
                    //parent matched , aligned
                    m_transformation = m_transformation * ((CoordinateBase)enumerator.Current).Transformation;
                }
                else
                {
                    //parent not matched, implicty reversed
                    m_transformation = m_transformation * ((CoordinateBase)enumerator.Current).Transformation.Inv();
                }

                _parent = enumerator.Current.m_coordinateReference; //move forward to compare
            }
        }
    }
}
