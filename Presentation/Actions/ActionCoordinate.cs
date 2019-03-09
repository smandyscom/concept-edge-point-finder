using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Presentation.ViewModels;
using Core.Arch;
using System.Collections.ObjectModel;


namespace Presentation.Actions
{
    public class ActionCoordinate : ActionBase
    {
        public ActionCoordinate() : base(typeof(ViewModelCoordinate),typeof(CoordinateBase))
        {

        }


        /// <summary>
        /// Rely on at least 3 points
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal override bool m_canExecute(ObservableCollection<ViewModelBase> list)
        {
            return list.Count((ViewModelBase x) =>
            {
                return x.GetType().IsSubclassOf(typeof(ViewModelPoint));
            }) >= 3;
        }

        ///Raise event , let somebody add new-generated cooridnate to availble cooridnates

        ///// <summary>
        ///// Sender would be Coordinate
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //internal void OnViewModelCreated(Object sender,EventArgs e)
        //{
        //    CoordinateBase cb = sender as CoordinateBase;
            
        //}
    }
}
