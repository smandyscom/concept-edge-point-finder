using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Arch
{
    /// <summary>
    /// This editing sequence
    /// </summary>
    class SequenceBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected event EventHandler Update;

        public void OnUpdate(Object sender, EventArgs e)
        {
            Update(sender, e);
        }
    }
}
