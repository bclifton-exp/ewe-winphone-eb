using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Expedia.Services.Interfaces
{
    public interface ILocationService
    {
        Task<Geoposition> GetCurrentLocation();
    }
}
