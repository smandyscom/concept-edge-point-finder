﻿using System;
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
            //throw new NotImplementedException();
            //raise next level's update
            //if not last node
            if(_sequenceReference.Next != null)
            {
                _sequenceReference.Next.Value.OnUpdate(sender, args);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementBase(List<ElementBase> dependencies)
        {
            //check if all dependcies in the same coordinate system

            _dependencies = dependencies;

            //inherit coordinate reference
            _coordinateReference = _dependencies.Last()._coordinateReference;
            OnDependeciesValueChanged(null, null);
        }

        /// <summary>
        ///Referenced coodiante system
        /// </summary>
        internal HierarchyTreeNode<CoordinateBase> _coordinateReference = null;
        /// <summary>
        /// Referenced creation generation
        /// </summary>
        internal LinkedListNode<SequenceBase> _sequenceReference = null;
    }
}
