using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.ViewModels;

namespace Presentation.Actions
{
    public class ActionPointFree : ActionBase
    {
        public ActionPointFree() : base(typeof(ViewModelLine),typeof(ViewModelPoint))
        { }

        /// <summary>
        /// Depends on current cooridnate only
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal override bool m_canExecute(ObservableCollection<ViewModelBase> list)
        {
            //TODO , 
            return true;
        }
    }
}
