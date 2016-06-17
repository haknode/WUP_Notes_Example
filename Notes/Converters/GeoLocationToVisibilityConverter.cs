using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using NotesPCL.Models;

namespace Notes.Converters
{
    class GeoLocationToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var geoLocation = value as GeoLocation;
            if (geoLocation != null && targetType == typeof(Visibility))
            {
                if (geoLocation.IsValid)
                    return Visibility.Visible;

                return Visibility.Collapsed;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
