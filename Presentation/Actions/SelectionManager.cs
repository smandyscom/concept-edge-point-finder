using Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Actions
{
    public class SelectionManager
    {
        /// <summary>
        /// Clean : click background
        /// Add : click selectable items
        /// </summary>
        public ObservableCollection<ViewModelBase> SelectedItems { get; } =
            new ObservableCollection<ViewModelBase>();

        ///Add , raise event
        ///Clean
    }
}
