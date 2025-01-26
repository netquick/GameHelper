using System;
using System.Globalization;
using System.Windows.Data;

namespace GameHelper.Converters
{
    public class ResourceDictionaryUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string relativePath && parameter is string assemblyName)
            {
                return new Uri($"pack://application:,,,/{assemblyName};component/{relativePath}", UriKind.Absolute);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
