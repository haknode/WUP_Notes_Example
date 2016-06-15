using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using NotesPCL.Models;
using NotesPCL.Services;

namespace Notes.Services
{
    class WupLocationService : ILocationService
    {
        private readonly Geolocator geolocator = new Geolocator();

        public Task<GeoLocation> GetCurrentLocation()
        {
            return GetCurrentLocation(CancellationToken.None);
        }

        public Task<GeoLocation> GetCurrentLocation(CancellationToken cancellationToken)
        {
            return GetCurrentLocation(cancellationToken, Int32.MaxValue);
        }

        public async Task<GeoLocation> GetCurrentLocation(CancellationToken cancellationToken, int timeout)
        {
            try
            {
                var access = await Geolocator.RequestAccessAsync().AsTask(cancellationToken);

                if (access == GeolocationAccessStatus.Allowed)
                {
                    var getGeopositonTask = geolocator.GetGeopositionAsync().AsTask(cancellationToken);

                    if (await Task.WhenAny(getGeopositonTask, Task.Delay(timeout, cancellationToken)) == getGeopositonTask)
                    {
                        var geoposition = getGeopositonTask.Result.Coordinate.Point.Position;

                        return new GeoLocation(geoposition.Latitude, geoposition.Longitude);
                    }

                }
            }
            catch (Exception)
            {
                // ignored
            }

            //No access granted or timed out or canceld
            return null;
        }
    }
}
