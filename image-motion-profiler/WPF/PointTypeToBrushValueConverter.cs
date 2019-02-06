using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Windows.Data;
using WindowsFormsApp2.DrawObjects;

namespace WindowsFormsApp2.WPF
{
	class PointTypeToBrushValueConverter : BaseConverter, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var type = (PointType) value;
			var color = Colors.Black;

			switch (type)
			{
				case PointType.center:
					color = Colors.Yellow;
					break;

				case PointType.start:
					color = Colors.DarkGray;
					break;

				case PointType.end:
					color = Colors.GreenYellow;
					break;
			}

			return new SolidColorBrush(color);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
