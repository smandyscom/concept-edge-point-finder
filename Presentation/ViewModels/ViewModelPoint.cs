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
	
		public ViewModelPoint(PointBase Element) : base(Element)
		{
		}

		/// <summary>
		/// The model as point base
		/// </summary>
		public PointBase Point
        {
            get { return  m_element as PointBase; }
        }
		protected override void ElementValueChanged(object sender, EventArgs e)
		{
			RaisePropertyChanged("Point");
		}
	}
}
