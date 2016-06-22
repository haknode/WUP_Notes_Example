using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesPCL.Models
{
    //custom class to store coordinates, because Geopoint is not available in PCLs
    public class GeoLocation
    {
        public GeoLocation(double latitude, double longitude) : this(latitude, longitude, true) { }

        public GeoLocation(double latitude, double longitude, bool isValid)
        {
            Latitude = latitude;
            Longitude = longitude;
            IsValid = isValid;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Boolean IsValid { get; }     //Indicates that this geo location has invalid coordinates

        public static GeoLocation InvalidGeoLocation => new GeoLocation(0, 0, false);
    }
}
