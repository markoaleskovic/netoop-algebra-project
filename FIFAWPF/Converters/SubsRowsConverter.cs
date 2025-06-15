using System;
using System.Globalization;
using System.Windows.Data;
using FIFAWPF.ViewModels;

namespace FIFAWPF.Converters
{
	public class SubsRowsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is AppConfig.WindowSize size)
			{
				return size switch
				{
					AppConfig.WindowSize.Small => 2,
					AppConfig.WindowSize.Medium => 3,
					_ => 1 // Large or default
				};
			}
			return 1;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}
