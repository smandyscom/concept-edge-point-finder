using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Arch;
namespace Core.Derived
{
    /// <summary>
    /// Dependencies as multiple points
    /// </summary>
    class LineFitted : LineBase
    {
        public LineFitted(List<ElementBase> dependencies) : base(dependencies)
        {

        }

        public override void OnValueChanged(object sender, EventArgs args)
        {
            //TODO invoke fitting methods
            //output m_end1 , m_end2

            base.OnValueChanged(sender, args);
        }
    }
}
