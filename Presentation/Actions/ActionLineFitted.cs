using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Presentation.ViewModels;
using Core.Derived;
using System.Collections.ObjectModel;

namespace Presentation.Actions
{
    public class ActionLineFitted : ActionBase
    {
        ActionLineFitted():base(typeof(ViewModelLine),typeof(LineFitted))
        {

        }

        internal override bool m_canExecute(ObservableCollection<ViewModelBase> list)
        {
            return base.m_canExecute(list);
        }
    }
}
