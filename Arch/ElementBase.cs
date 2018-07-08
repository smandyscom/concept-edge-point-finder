using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Arch
{
    /// <summary>
    /// The base class for all graphical elements
    /// </summary>
    public class ElementBase
    {
        /// <summary>
        /// Holding depended elements' reference
        /// Which to determine this element's profile
        /// </summary>
        List<ElementBase> dependencies = new List<ElementBase>();

        /// <summary>
        /// updated coefficient... internal datas...etc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnDependeciesValueChanged(Object sender, EventArgs args)
        {
        }

        ///Referenced coodiante system

        

        
        LinkedListNode<SequenceBase> sequenceReference;
    }
}
