using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;


namespace Presentation.Convertors
{
	public class MatToPointValueConverter : BaseConverter, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Point location = new Point();
			var mat = (OpenCvSharp.Mat)value;
			location.X = mat.At<double>(0);
			location.Y = mat.At<double>(1);
			return location;
			
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
