﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Entities.Hotels;

namespace Expedia.Services.Interfaces
{
    public interface IHotelService
    {
        Task<HotelResults> SearchHotels(CancellationToken ct, SearchHotelsLocalParameters searchInput);
        Task<HotelInformationResponse> GetHotelInformation(CancellationToken ct, string hotelId);
        Task<HotelOfferResponse> GetHotelOffer(CancellationToken ct, HotelOfferQueryParameters parameters);
    }
}
