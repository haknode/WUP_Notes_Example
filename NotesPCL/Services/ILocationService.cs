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
        Task<GeoLocation> GetCurrentLocationAsync();
        Task<GeoLocation> GetCurrentLocationAsync(CancellationToken cancellationToken);
        Task<GeoLocation> GetCurrentLocationAsync(CancellationToken cancellationToken, int timeout);
    }
}
