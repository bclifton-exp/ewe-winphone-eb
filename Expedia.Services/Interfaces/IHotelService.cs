using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Entities.Hotels;

namespace Expedia.Services.Interfaces
{
    public interface IHotelService
    {
        Task<HotelSearchResponse> GetHotels(HotelSearchQueryParameters parameters, CancellationToken ct);
    }
}
