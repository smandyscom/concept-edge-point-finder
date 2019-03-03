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
    public class ActionLineFree : ActionBase
    {
        public ActionLineFree():base(typeof(ViewModelLine),typeof(LineBase))
        {

        }

        /// <summary>
        /// At least contains two points
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal override bool m_canExecute(ObservableCollection<ViewModelBase> list)
        {
            return list.Count((ViewModelBase x)=> 
            {
                return x.GetType().IsSubclassOf(typeof(ViewModelPoint));
            }) >= 2;
        }
    }
}
