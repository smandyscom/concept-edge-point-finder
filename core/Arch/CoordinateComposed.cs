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
            var enumerator = m_dependencies.GetEnumerator();
            var parent = ((CoordinateBase)m_dependencies.First()).Node;

            //reset
            m_transformation = Mat.Eye((int)m_dimension, (int)m_dimension, MatType.CV_64FC1);

            //from source to destination
            // S->D
            while (enumerator.MoveNext())
            {
                if (parent == ((CoordinateBase)enumerator.Current).Node)
                {
                    //parent matched , aligned
                    m_transformation = m_transformation * ((CoordinateBase)enumerator.Current).Transformation;
                }
                else
                {
                    //parent not matched, implicty reversed
                    m_transformation = m_transformation * ((CoordinateBase)enumerator.Current).Transformation.Inv();
                }

                parent = enumerator.Current.m_coordinateReference; //move forward to compare
            }

            base.OnValueChanged(sender, args);
        }       
    }
}
