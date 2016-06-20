using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Notes.Converters
{
    public class StringToShortStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var str = value as string;

            if (str == null || targetType != typeof(string))
                return DependencyProperty.UnsetValue;

            if (str.Length < 15)
                return str;

            return str.Substring(0, 15) + " ...";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
