using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NotesPCL.Models;

namespace NotesPCL.Services
{
    public interface ILocationService
    {
        Task<GeoLocation> GetCurrentLocation();
        Task<GeoLocation> GetCurrentLocation(CancellationToken cancellationToken);
        Task<GeoLocation> GetCurrentLocation(CancellationToken cancellationToken, int timeout);
    }
}
