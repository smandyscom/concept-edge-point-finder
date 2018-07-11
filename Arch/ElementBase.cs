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
        internal List<ElementBase> _dependencies = new List<ElementBase>();

        /// <summary>
        /// updated coefficient... internal features...etc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public virtual void OnDependeciesValueChanged(Object sender, EventArgs args)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementBase(List<ElementBase> dependencies)
        {
            //check if all dependcies in the same coordinate system

            _dependencies = dependencies;

            //inherit coordinate reference
            _coordinateReference = _dependencies.First<ElementBase>()._coordinateReference;
        }

        /// <summary>
        ///Referenced coodiante system
        /// </summary>
        internal HierarchyTreeNode<CoordinateBase> _coordinateReference = null;
        /// <summary>
        /// 
        /// </summary>
        internal LinkedListNode<SequenceBase> _sequenceReference = null;
    }
}
