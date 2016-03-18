using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Entities.Flights;

namespace Expedia.Services.Interfaces
{
    public interface IFlightService
    {
        Task<FlightSearchResponse> GetFlights(CancellationToken ct, SearchFlightsQueryParameters queryParameters);
        Task<FlightResults> SearchFlights(CancellationToken ct, SearchFlightsLocalParameters searchInput);
    }
}
