using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Arch;
namespace Core.Derived
{
    class PointIntersection : PointBase
    {

        internal LineBase m_line1 = null;
        internal LineBase m_line2 = null;

        public PointIntersection(List<ElementBase> dependencies) : base(dependencies)
        {

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
