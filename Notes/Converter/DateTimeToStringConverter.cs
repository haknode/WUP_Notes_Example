using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Notes.Converter
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is DateTime) || targetType != typeof(string))
                return DependencyProperty.UnsetValue;

            return ((DateTime) value).ToString("MMMM dd, yyyy - H:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}