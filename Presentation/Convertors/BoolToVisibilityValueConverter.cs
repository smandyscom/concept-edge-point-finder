using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Presentation.Convertors
{
	/// <summary>
	/// Ture is Visible, False is Hidden
	/// </summary>
	public class BoolToVisibilityValueConverter : BaseConverter, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var visible = (bool)value;

			Visibility visibility = visible ? Visibility.Visible : Visibility.Hidden;
			return visibility;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
