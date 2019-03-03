using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Presentation.ViewModels;
using Core.Derived;

namespace Presentation.Actions
{
    public class ActionLineFitted : ActionBase
    {
        ActionLineFitted():base(typeof(ViewModelLine),typeof(LineFitted))
        {

        }
    }
}
