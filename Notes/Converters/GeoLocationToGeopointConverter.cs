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
    class GeoLocationToGeopointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var getpoint = value as GeoLocation;
            if (getpoint != null && targetType == typeof(Geopoint))
            {
                return new Geopoint(new BasicGeoposition
                                    {
                                        Latitude = getpoint.Latitude,
                                        Longitude = getpoint.Longitude,
                                    });
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
