using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;


namespace Presentation.Convertors
{
	class PointFToPointValueConverter : BaseConverter, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Point location = new Point();
			System.Drawing.PointF center = (System.Drawing.PointF)value;
			location.X = center.X;
			location.Y = center.Y;
			return location;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
