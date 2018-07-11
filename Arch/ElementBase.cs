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
    public abstract class ElementBase
    {
        /// <summary>
        /// Holding depended elements' reference
        /// Which to determine this element's profile
        /// </summary>
        List<ElementBase> __dependencies = new List<ElementBase>();

        /// <summary>
        /// updated coefficient... internal datas...etc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnDependeciesValueChanged(Object sender, EventArgs args)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementBase(List<ElementBase> dependencies)
        {
            __dependencies = dependencies;
        }

        /// <summary>
        ///Referenced coodiante system
        /// </summary>
        HierarchyTreeNode<CoordinateBase> coordinateReference = null;
        /// <summary>
        /// 
        /// </summary>
        LinkedListNode<SequenceBase> sequenceReference = null;
    }
}
