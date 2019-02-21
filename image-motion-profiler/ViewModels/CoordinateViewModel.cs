using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using Core.Arch;

namespace WindowsFormsApp2.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CoordinateViewModel : BaseViewModel
    {
        /// <summary>
        /// The model
        /// </summary>
        internal CoordinateBase Coordinate
        {
            get => m_element as CoordinateBase;
        }
    }
}
