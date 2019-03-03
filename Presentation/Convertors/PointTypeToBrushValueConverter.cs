using System;
using System.Globalization;
using System.Windows.Media;
using Core.Derived;
using Core.Arch;
using System.Windows.Data;

namespace Presentation.Convertors
{
	public class PointTypeToBrushValueConverter : BaseConverter, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var color =  Colors.Black;
			var type = value.GetType();

			if (type == typeof(PointBase))
			{
				color = Colors.Red;
			}
			else if (type == typeof(PointEdge))
			{
				color = Colors.Green;
			}

			return new SolidColorBrush(color);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
