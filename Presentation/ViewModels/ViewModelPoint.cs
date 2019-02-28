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
    public class ViewModelPoint : ViewModelBase
    {
        /// <summary>
        /// The model as point base
        /// </summary>
        internal PointBase Point
        {
            get { return  m_element as PointBase; }
        }
    }
}
