using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Notes.Converters
{
    public class DateTimeToShortStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is DateTime) || targetType != typeof(string))
                return DependencyProperty.UnsetValue;

            return ((DateTime) value).ToString("MM. dd. yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}