using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using Core.Arch;

namespace Presentation.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewModelCoordinate : ViewModelBase
    {
        /// <summary>
        /// The model
        /// </summary>
        internal CoordinateBase Coordinate
        {
            get { return m_element as CoordinateBase; }
        }
    }
}
