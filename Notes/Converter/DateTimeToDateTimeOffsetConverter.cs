using System;
using Windows.UI.Xaml.Data;

namespace Notes.Converter
{
    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime && (targetType == typeof(DateTimeOffset) || targetType == typeof(DateTimeOffset?)))
                return new DateTimeOffset((DateTime) value);

            return DateTimeOffset.MinValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((value is DateTimeOffset || value is DateTimeOffset?) && targetType == typeof(DateTime))
                return ((DateTimeOffset) value).DateTime;

            return DateTime.MinValue;
        }
    }
}