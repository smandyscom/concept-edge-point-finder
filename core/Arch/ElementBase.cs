using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Arch
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
        internal List<ElementBase> m_dependencies = new List<ElementBase>();

        /// <summary>
        /// Inform next level to update
        /// </summary>
        protected event EventHandler ValueChangedEvent;

        /// <summary>
        /// updated coefficient... internal features...etc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public virtual void OnValueChanged(Object sender, EventArgs args)
        {

            //do some calculation, update internal variables
            //inform next level
            EventHandler handler = ValueChangedEvent;
            if(handler!=null)
                handler(this, args);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementBase(List<ElementBase> dependencies)
        {
            //check if all dependcies in the same coordinate system
            m_dependencies = dependencies;
            if (dependencies != null)
            {
                m_dependencies.ForEach(item => item.ValueChangedEvent += OnValueChanged);
                //inherit coordinate reference
                m_coordinateReference = m_dependencies.Last().m_coordinateReference;
            }
            
            //OnValueChanged(null, null);
        }

        /// <summary>
        ///Referenced coodiante system
        /// </summary>
        internal HierarchyTreeNode<CoordinateBase> m_coordinateReference = null;
    }
}
