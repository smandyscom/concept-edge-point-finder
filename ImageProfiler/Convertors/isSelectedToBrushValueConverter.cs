using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WindowsFormsApp2.WPF
{
	class isSelectedToBrushValueConverter : BaseConverter, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool isSelected = (bool)value;
			var color = isSelected ? Colors.Purple : Colors.Black;
			return new SolidColorBrush(color);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
