using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.ViewModels;

namespace Presentation.Actions
{
    /// <summary>
    /// Define the common behaviors for those action
    /// 0. Mode 0 , Click Action->Pre-generate things(as the material to preview)->Final clicked->Generate item (e.g LineFree)
    /// 1. Mode 1 , Click Action->Select items->Type satisfied->Generate new item
    /// 2. Mode 2 , Select items->Check if satisfied->Change outlook->user click action
    /// </summary>
    public class ActionBase : ICommand
    {

        public ActionBase(Type viewModelType,Type modelType)
        {
            m_viewModelType = viewModelType;
            m_modelType = modelType;
        }

        //TODO , CanExecute property link with Enabled
        
            [Flags]
        internal enum CreationMode
        {
            /// <summary>
            /// Pre-generate things
            /// </summary>
            MODE_0=1,
            /// <summary>
            /// Click before selection
            /// </summary>
            MODE_1=2,
            /// <summary>
            /// Pre-select
            /// </summary>
            MODE_2=4
        };
        /// <summary>
        /// Bound attribute to switch between different creation modes
        /// </summary>
        internal CreationMode m_creationFlag;

        /// <summary>
        /// The view model type this action would create
        /// </summary>
        internal Type m_viewModelType;
        internal Type m_modelType;

        public event EventHandler ViewModelCreated;
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Check if selected thing meet request
        /// Deck with selection manager
        /// </summary>
        public virtual bool CanExecute(object parameter)
        {
            OrderContext oc = parameter as OrderContext;

            if (oc == null)
                return false; //bad parameter

            ObservableCollection<ViewModelBase> list = oc.SelectedViewModels;
            
            //Nothing cached , use creating mode 0/1
            if (list.Count == 0)
                return true;

            bool result = m_canExecute(list);
            if (!result)
                //cannot execute , clear cache
                (parameter as ObservableCollection<ViewModelBase>).Clear();
            return result;
        }

        /// <summary>
        /// TODO , how the caller to pass parameter?
        /// https://stackoverflow.com/questions/13112557/passing-a-parameter-to-icommand
        /// </summary>
        /// <param name="parameter"></param>
        public virtual void Execute(object parameter)
        {
            OrderContext oc = parameter as OrderContext;

            //treat parameter as a ObservableCollection<ViewModelBase>
            var vm = Activator.CreateInstance(m_viewModelType) as ViewModelBase;

            //turns into dependecy list
            var dependencies = (oc.SelectedViewModels).ToList().Select((ViewModelBase x) =>
            {
                return x.m_element;
            }).ToList();
            //generate Model element
            vm.m_element = Activator.CreateInstance(m_modelType,dependencies) as Core.Arch.ElementBase;

            //Raise event, pass new-created object to UserControlCanvus , put it on canvus
            ViewModelCreated?.Invoke(vm, null);

            //after used , reset
            (parameter as ObservableCollection<ViewModelBase>).Clear();
        }

        internal virtual bool m_canExecute(ObservableCollection<ViewModelBase> list)
        {
            return false;
        }
    }
}
