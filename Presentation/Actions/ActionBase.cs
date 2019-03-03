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
    /// 1. Mode 1 , Click Action->Select items->Type satisfied->Generate new item
    /// 2. Mode 2 , Select items->Check if satisfied->Change outlook->user click action
    /// </summary>
    public class ActionBase : ICommand
    {
        public ActionBase(Type viewModelType,Type modelType)
        {
            m_viewModelType = viewModelType;
            modelType = m_modelType;
        }

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
        public bool CanExecute(object parameter)
        {
            bool result = m_canExecute(parameter as ObservableCollection<ViewModelBase>);
            if (!result)
                (parameter as ObservableCollection<ViewModelBase>).Clear();
            return result;
        }

        /// <summary>
        /// TODO , how the caller to pass parameter?
        /// https://stackoverflow.com/questions/13112557/passing-a-parameter-to-icommand
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            //treat parameter as a ObservableCollection<ViewModelBase>
            var vm = Activator.CreateInstance(m_viewModelType) as ViewModelBase;

            //turns into dependecy list
            var dependencies = (parameter as ObservableCollection<ViewModelBase>).ToList().Select((ViewModelBase x) =>
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
