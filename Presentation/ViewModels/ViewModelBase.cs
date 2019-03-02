using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Core.Arch;

namespace Presentation.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
		public ViewModelBase(ElementBase Element)
		{
			m_element = Element;
			Element.ValueChangedEvent += ElementValueChanged;
		}

		protected abstract void ElementValueChanged(object sender, EventArgs e);

		/// <summary>
		/// The model element held
		/// </summary>
		protected readonly ElementBase m_element;


		public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
