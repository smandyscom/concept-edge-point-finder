using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Arch;
using Presentation.ViewModels;

namespace Presentation.Actions
{
    /// <summary>
    /// Store/Handling user's order
    /// TODO , instanize it somewhere
    /// </summary>
    public class OrderContext
    {

        public ObservableCollection<CoordinateBase> AvailableCoordinates { get => m_availableCoordinates; }
        public CoordinateBase CurrentCoordinate
        {
            get => m_currentCoordinate;
            set
            {
                m_currentCoordinate = value;
            }
        }
        public ObservableCollection<ViewModelBase> SelectedViewModels { get => m_selectedViewModels; }

        /// <summary>
        /// One of available cooridnates
        /// </summary>
        internal CoordinateBase m_currentCoordinate = null;
        /// <summary>
        /// Collect existed available cooridnates
        /// </summary>
        internal ObservableCollection<CoordinateBase> m_availableCoordinates = new ObservableCollection<CoordinateBase>();
        /// <summary>
        /// 
        /// </summary>
        internal ObservableCollection<ViewModelBase> m_selectedViewModels = new ObservableCollection<ViewModelBase>();
    }
}
