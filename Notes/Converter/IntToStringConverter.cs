using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Notes.Converter
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is int) || targetType != typeof(string))
                return "NaN";

            return ((int)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is string) || targetType != typeof(int))
                return DependencyProperty.UnsetValue;

            int outValue;

            if (int.TryParse((string)value, out outValue))
                return outValue;

            return 0;
        }
    }
}
