using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public class ViewModelCanvas
    {
        /// <summary>
        /// The bag which manage all active objects
        /// </summary>
        public ObservableCollection<ViewModelBase> DrawObjects { get; set; } = 
            new ObservableCollection<ViewModelBase>();
    }
}
