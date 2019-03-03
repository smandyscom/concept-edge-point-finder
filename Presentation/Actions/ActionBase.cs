using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.ViewModels;

namespace Presentation.Actions
{
    /// <summary>
    /// Define the common behaviors for those action
    /// 1. Mode 1 , Click Action->Select items->Type satisfied->Generate new item
    /// 2. Mode 2 , Select items->Check if satisfied->Change outlook->user click action
    /// </summary>
    public class ActionBase
    {
        /// <summary>
        /// Check
        /// Deck with selection manager
        /// </summary>
        public virtual bool IsMeetMyNeed {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// The view model type this action would create
        /// </summary>
        internal Type m_viewModelType;
        internal Type m_modelType;

        /// <summary>
        /// The collection of pre-selected items
        /// </summary>
        static SelectionManager m_manager = new SelectionManager();

        /// <summary>
        /// Flow : 
        /// check bag
        /// turns bag into dependency list
        /// generate view model and model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal virtual void onCreateActionRaised(Object sender,EventArgs args)
        {
            var v = Activator.CreateInstance(m_viewModelType) as ViewModelBase;

            //turns into dependecy list
            var dependencies = m_manager.SelectedItems.ToList().Select((ViewModelBase x) =>
            {
                return x.m_element;
            }).ToList();

            v.m_element = Activator.CreateInstance(m_modelType,dependencies) as Core.Arch.ElementBase;

            //TODO Raise event
        }
    }
}
