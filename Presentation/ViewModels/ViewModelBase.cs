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
  
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
		/// <summary>
		/// The model element held
		/// </summary>
		protected readonly ElementBase m_element;

		public ViewModelBase(ElementBase Element)
		{
			m_element = Element;
			Element.ValueChangedEvent += ElementValueChanged;
		}

		/// <summary>
		/// All properties RaisePropertyChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ElementValueChanged(object sender, EventArgs e)
		{
			Type type = this.GetType();
			foreach (var pro in type.GetProperties())
			{
				RaisePropertyChanged(pro.Name);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
