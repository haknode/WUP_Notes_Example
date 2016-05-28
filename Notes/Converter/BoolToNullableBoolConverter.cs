using System;
using Windows.UI.Xaml.Data;

namespace Notes.Converter
{
    public class BoolToNullableBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && targetType == typeof(bool?))
                return (bool) value;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is bool? && targetType == typeof(bool))
                return (bool) value;

            return false;
        }
    }
}