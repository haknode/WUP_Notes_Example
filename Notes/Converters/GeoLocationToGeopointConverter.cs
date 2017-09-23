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
            var geoLocation = value as GeoLocation;
            if (geoLocation != null && targetType == typeof(Geopoint))
            {
                if (geoLocation.IsValid)
                    return new Geopoint(new BasicGeoposition
                                        {
                                            Latitude = geoLocation.Latitude,
                                            Longitude = geoLocation.Longitude,
                                            Altitude = 0,
                                        }, AltitudeReferenceSystem.Terrain);

                return new Geopoint(new BasicGeoposition
                                    {
                                        Latitude = 0,
                                        Longitude = 0,
                                    });
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
