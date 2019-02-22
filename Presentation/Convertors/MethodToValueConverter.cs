using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Presentation.Convertors
{
	class MethodToValueConverter : BaseConverter, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var methodName = parameter as string;
			if (value == null || methodName == null)
				return value;
			var methodInfo = value.GetType().GetMethod(methodName, new Type[0]);
			if (methodInfo == null)
				return value;
			var gets = methodInfo.Invoke(value, new object[0]);
			return gets;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
