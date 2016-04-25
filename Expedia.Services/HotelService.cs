using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Client;
using Expedia.Entites;
using Expedia.Entities.Extensions;
using Expedia.Entities.Hotels;
using Expedia.Services.Base;
using Expedia.Services.Interfaces;
using Expedia.Services.Query;
using Newtonsoft.Json;

namespace Expedia.Services
{
    public class HotelService : BaseService, IHotelService
    {
        private ISettingsService _settingsService;

        public HotelService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<HotelResults> SearchHotels(CancellationToken ct, SearchHotelsLocalParameters searchInput)
        {
            try
            {
                var clientSearchParameters = CreateClientQueryParameters(searchInput);

                var results = await GetHotels(clientSearchParameters, ct);

                var hotelResults = await CreateHotelResultsFromClientResponse(ct, searchInput, results);

                hotelResults.PriceFilters.Safe().ForEach(GetLocalizedPriceFilterTitle);

                return hotelResults;

            }
            catch (Exception ex)
            {
                return new HotelResults {Hotels = new HotelResultItem[0]};
            }
        }

        private async Task<HotelSearchResponse> GetHotels(HotelSearchQueryParameters parameters, CancellationToken ct)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var query = new QueryBuilder(parameters).ReturnQueryUri();

            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.MobileHotelsApiRoot);
            request.AppendPath(Constants.UrlActions.Search);
            request.AppendPath(query);
            var result = await ExecuteGet(request.GetFullUri(), ct);

            return result != null ? JsonConvert.DeserializeObject<HotelSearchResponse>(result) : null;
        }

        public async Task<HotelInformationResponse> GetHotelInformation(CancellationToken ct, string hotelId)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.MobileHotelsApiRoot);
            request.AppendPath(Constants.UrlActions.HotelInfo);
            request.AppendParam("hotelId", hotelId);

            var result = await ExecuteGet(request.GetFullUri(), ct);
            return result != null ? JsonConvert.DeserializeObject<HotelInformationResponse>(result) : null;
        }

        public async Task<HotelOfferResponse> GetHotelOffer(CancellationToken ct, HotelOfferQueryParameters parameters)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var query = new QueryBuilder(parameters).ReturnQueryUri();

            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.MobileHotelsApiRoot);
            request.AppendPath(Constants.UrlActions.HotelOffers);
            request.AppendPath(query);

            var result = await ExecuteGet(request.GetFullUri(), ct);
            return result != null ? JsonConvert.DeserializeObject<HotelOfferResponse>(result) : null;
        }

        private static HotelSearchQueryParameters CreateClientQueryParameters(SearchHotelsLocalParameters searchInput)
        {
            var clientSearchParameters = new HotelSearchQueryParameters
            {
                Latitude = searchInput.LocationLatitude,
                Longitude = searchInput.LocationLongitude,
                RegionId = searchInput.LocationRegionId,
                City = searchInput.LocationName,
                CheckInDate = searchInput.CheckInDate,
                CheckOutDate = searchInput.CheckOutDate,
                SortOrder = searchInput.SortBy,
                Room = (new[] { searchInput.AdultsCount }.Concat(searchInput.ChildrenAges)).ToArray(),
                ReturnOpaqueHotels = false, //YOLO
                EnableSponsoredListing = false,
                PageIndex = 0,
                ResultsPerPage = -1,
                ForceV2Search = true,
                FilterUnavailable = true
            };

            FillPriceFilters(searchInput, clientSearchParameters);
            FillStarRatingFilters(searchInput, clientSearchParameters);
            FillAmenityFilters(searchInput, clientSearchParameters);

            return clientSearchParameters;
        }

        private static async Task<HotelResults> CreateHotelResultsFromClientResponse(CancellationToken ct, SearchHotelsLocalParameters searchInput, HotelSearchResponse results)
        {
            // The hotel is null if the search yield no result
            if (results.HotelList == null)
            {
                results.HotelList = new Hotel[0];
            }

            return await Task.Factory.StartNew(() =>
            {
                // No need to synchronize the neighborhood filters, the searched regionId is already checked
                var neighborhoodFilters = GetNeighborhoodFilters(results);

                // Read the filters from the response and sync the state with the filters coming from the search input
                var priceBucketFilters = SynchronizeFiltersState(searchInput.PriceFilters, GetPriceBucketFilters(results));
                var starRatingFilters = SynchronizeFiltersState(searchInput.StarRatingFilters, GetStarRatingFilters(results));
                var amenityFilters = SynchronizeFiltersState(searchInput.AmenityFilters, GetAmenityFilters(results));
                var accessibilityFilters = SynchronizeFiltersState(searchInput.AccessibilityFilters, GetAccessibilityFilters(results));

                return new HotelResults
                {
                    SearchRegionId = results.SearchRegionId,
                    FilterUnavailableHotelRequested = results.FilterUnavailableHotelsRequested,
                    FilteredSearchMatchedNoHotels = results.FilteredSearchMatchedNoHotels,
                    NumberOfRoomsRequested = results.NumberOfRoomsRequested,
                    TotalHotelCount = results.TotalHotelCount,
                    AvailableHotelCount = results.AvailableHotelCount,

                    StarRatingFilters = starRatingFilters,
                    PriceFilters = priceBucketFilters,
                    AmenityFilters = amenityFilters,
                    AccessibilityFilters = accessibilityFilters,
                    NeighborhoodFilters = neighborhoodFilters,

                    Hotels = results.HotelList.Select(hotel => new HotelResultItem
                    {
                        HotelId = hotel.HotelId,
                        ImageUrl = !string.IsNullOrWhiteSpace(hotel.LargeThumbnailUrl) ? Constants.HotelImagesUrl + hotel.LargeThumbnailUrl.Replace("_d", "_z") : null,
                        Price = hotel.LowRateInfo != null ? hotel.LowRateInfo.PriceToShowUsers : 0,
                        StrikeThroughPrice = hotel.LowRateInfo != null ? hotel.LowRateInfo.StrikethroughPriceToShowUsers : 0,
                        HotelName = hotel.LocalizedName,
                        RateCurrencyCode = hotel.RateCurrencyCode,
                        RateCurrencySymbol = hotel.RateCurrencySymbol,
                        RoomsLeftAtThisRate = hotel.RoomsLeftAtThisRate,
                        Rating = hotel.HotelStarRating,
                        GuestRating = hotel.HotelGuestRating,
                        LocationId = hotel.LocationId,
                        Latitude = hotel.Latitude,
                        Longitude = hotel.Longitude
                    }).ToArray()
                };
            }, ct);
        }

        private static void GetLocalizedPriceFilterTitle(HotelPriceBucketFilter priceFilter)
        {
            if (priceFilter == null)
            {
                return;
            }

            var currencyCode = string.Empty;

            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

            if (priceFilter.MinPrice == 0)
            {
                string formatString = loader.GetString("LessThanPrice");
                priceFilter.Title = formatString
                    .CurrentCultureFormat(
                        currencyCode,
                        priceFilter.CurrencySymbol,
                        priceFilter.MaxPrice
                    );
            }
            else if (priceFilter.MaxPrice == 0)
            {
                string formatString = loader.GetString("GreaterThanPrice");
                priceFilter.Title = formatString
                    .CurrentCultureFormat(
                        currencyCode,
                        priceFilter.CurrencySymbol,
                        priceFilter.MinPrice
                    );
            }
            else
            {
                string formatString = loader.GetString("PriceToPrice");
                priceFilter.Title = formatString
                    .CurrentCultureFormat(
                        currencyCode,
                        priceFilter.CurrencySymbol,
                        priceFilter.MinPrice,
                        priceFilter.MaxPrice
                    );
            }
        }

        private static HotelFilter[] GetAccessibilityFilters(HotelSearchResponse results)
        {
            var accessibilityFilters = results.AmenityFilterOptions.Safe()
                .SelectOrDefault(amenitiesKeyValuePairs => amenitiesKeyValuePairs
                    .Where(amenityKeyValuePair => amenityKeyValuePair.Value.IsAccessible)
                    .Select(amenityKeyValuePair => new HotelFilter()
                    {
                        Id = amenityKeyValuePair.Key,
                        Title = amenityKeyValuePair.Value.FilterName,
                        Count = amenityKeyValuePair.Value.FilterNumOccurrences,
                        IsFilterEnabled = true
                    })
                ).ToArray();
            return accessibilityFilters;
        }

        private static HotelNeighborhoodFilter[] GetNeighborhoodFilters(HotelSearchResponse results)
        {
            var neighborhoodFilters = results.AllNeighborhoodsInSearchRegion.Safe()
                .SelectOrDefault(_ => _.Select(neighborhood => new HotelNeighborhoodFilter()
                {
                    Id = neighborhood.Id,
                    Title = neighborhood.Name,
                    IsFilterEnabled = true,
                    IsFilterChecked = neighborhood.Id == results.SearchRegionId,
                    Count = results.HotelList.Count(hotel => hotel.LocationId == neighborhood.Id)
                }).ToArray());

            return neighborhoodFilters ?? new HotelNeighborhoodFilter[0];
        }

        private static HotelStarRatingFilter[] GetStarRatingFilters(HotelSearchResponse results)
        {
            var starRatingFilters = results.StarOptions.Safe()
                .SelectOrDefault(_ => _.Where(starOption => starOption.StarRating > 0)
                    .Select(starOption =>
                    {
                        starOption.StarRating =
                            (starOption.StarRating % 10 != 0)
                                ? starOption.StarRating + 5
                                : starOption.StarRating;
                        return starOption;
                    })
                    .GroupBy(starOption => starOption.StarRating)
                    .Select(group => new HotelStarRatingFilter()
                    {
                        Count = @group.Sum(hotels => hotels.Count),
                        Title = @group.Key.ToStringInvariant(),
                        Id = @group.Key.ToStringInvariant(),
                        StarRating = @group.Key / 10,
                        IsFilterEnabled = @group.Sum(hotels => hotels.Count) > 0
                    })
                    .OrderByDescending(starOption => starOption.StarRating)
                    .ToArray());

            return starRatingFilters ?? new HotelStarRatingFilter[0];
        }

        private static HotelFilter[] GetAmenityFilters(HotelSearchResponse results)
        {
            var amenityFilters = results.AmenityFilterOptions.Safe()
                .SelectOrDefault(amenitiesKeyValuePairs => amenitiesKeyValuePairs
                    .Where(amenityKeyValuePair => !amenityKeyValuePair.Value.IsAccessible)
                    .Select(amenityKeyValuePair => new HotelFilter()
                    {
                        Id = amenityKeyValuePair.Key,
                        Title = amenityKeyValuePair.Value.FilterName,
                        IsFilterEnabled = true,
                        Count = amenityKeyValuePair.Value.FilterNumOccurrences
                    }).ToArray()
                );
            return amenityFilters ?? new HotelFilter[0];
        }

        private static HotelPriceBucketFilter[] GetPriceBucketFilters(HotelSearchResponse results)
        {
            var currency = results.HotelList.Safe()
                .SelectOrDefault(hotelList => hotelList.FirstOrDefault())
                .SelectOrDefault(_ => new { Code = _.RateCurrencyCode, Symbol = _.RateCurrencySymbol });

            var currencyCode = currency != null
                               && !string.IsNullOrWhiteSpace(currency.Code)
                ? currency.Code
                : string.Empty;

            var currencySymbol = currency != null
                                 && !string.IsNullOrWhiteSpace(currency.Symbol)
                ? currency.Symbol
                : string.Empty;

            var priceBucketFilters = results.PriceOptions.Safe()
                .SelectOrDefault(priceOptions => priceOptions
                    .Select(priceOption => new HotelPriceBucketFilter()
                    {
                        Count = priceOption.Count,
                        Id = "{0}/{1}".InvariantCultureFormat(priceOption.MinPrice, priceOption.MaxPrice),
                        MinPrice = priceOption.MinPrice,
                        MaxPrice = priceOption.MaxPrice,
                        CurrencyCode = currencyCode,
                        CurrencySymbol = currencySymbol,
                        IsFilterEnabled = priceOption.Count > 0
                    }).ToArray());

            return priceBucketFilters;
        }

        private static void FillAmenityFilters(SearchHotelsLocalParameters searchInput, HotelSearchQueryParameters clientSearchParameters)
        {
            if (searchInput.AmenityFilters.Safe().SelectOrDefault(filters => filters.Any(pf => pf.IsFilterChecked)
                || searchInput.AccessibilityFilters.Safe().SelectOrDefault(filter => filter.Any(pf => pf.IsFilterChecked))))
            {
                clientSearchParameters.FilterAmenities =
                    searchInput.AmenityFilters.Safe()
                        .SelectOrDefault(amenities => amenities
                            .Concat(searchInput.AccessibilityFilters
                                    ?? new HotelFilter[0]))
                        .Where(pf => pf.IsFilterChecked)
                        .Select(filter => int.Parse(filter.Id, CultureInfo.InvariantCulture))
                        .ToArray();
            }
        }

        private static void FillStarRatingFilters(SearchHotelsLocalParameters searchInput, HotelSearchQueryParameters clientSearchParameters)
        {
            if (!searchInput.StarRatingFilters.Safe().SelectOrDefault(_ => _.Any(pf => pf.IsFilterChecked)))
            {
                return;
            }

            var starFilterMin = searchInput.StarRatingFilters.Safe()
                .Where(pb => pb.IsFilterChecked)
                .Select(pb => pb.StarRating)
                .MinOrDefault() * 10;

            var starFilterMax = searchInput.StarRatingFilters.Safe()
                .Where(pb => pb.IsFilterChecked)
                .Select(pb => pb.StarRating)
                .MaxOrDefault() * 10;

            if (starFilterMin == starFilterMax && starFilterMin > 0)
            {
                clientSearchParameters.FilterStarRatings = starFilterMax < 50
                    ? new[] { starFilterMin, (starFilterMin + 5) }
                    : new[] { starFilterMax };
            }
            else if (starFilterMax > starFilterMin)
            {
                var filters = new int[0];
                var rating = starFilterMax;
                while (rating >= starFilterMin)
                {
                    filters = filters.Concat(new[] { rating }).ToArray();
                    rating -= 5;
                }

                clientSearchParameters.FilterStarRatings = filters;
            }
        }

        private static void FillPriceFilters(SearchHotelsLocalParameters searchInput, HotelSearchQueryParameters clientSearchParameters)
        {
            if (!searchInput.PriceFilters.Safe().SelectOrDefault(_ => _.Any(pf => pf.IsFilterChecked)))
            {
                return;
            }

            var priceFilterMin = searchInput.PriceFilters.Safe()
                .Where(pb => pb.IsFilterChecked)
                .Select(pb => pb.MinPrice)
                .MinOrDefault();

            var priceFilterMax = searchInput.PriceFilters.Safe()
                .Where(pb => pb.IsFilterChecked)
                .Select(pb => pb.MaxPrice)
                .MaxOrDefault();

            var hasNoMaxPrice = searchInput.PriceFilters.Safe().Any(pf => pf.IsFilterChecked && pf.MaxPrice == 0);

            if (hasNoMaxPrice)
            {
                clientSearchParameters.FilterPrice = new[] { priceFilterMin };
            }
            else if (priceFilterMax > priceFilterMin)
            {
                clientSearchParameters.FilterPrice = new[] { priceFilterMin, priceFilterMax };
            }
        }

        private static T[] SynchronizeFiltersState<T>(T[] oldFilters, T[] newFilters) where T : HotelFilter
        {
            if (oldFilters == null || !oldFilters.Any())
            {
                return newFilters;
            }

            var missingNewFilters = oldFilters
                .Where(
                    oldFilter => !newFilters.Contains(
                        oldFilter,
                        new HotelFilter.IdEqualityComparer()
                    )
                )
                .ToArray();
                
            missingNewFilters.ForEach(missingFilter => missingFilter.Count = 0);
            newFilters = newFilters.Concat(missingNewFilters).ToArray();

            return oldFilters
                .Join(
                    newFilters,
                    filter => filter.Id,
                    filter => filter.Id,
                    (filter1, filter2) =>
                    {
                        filter2.IsFilterChecked = filter1.IsFilterChecked;
                        filter2.IsFilterEnabled = filter2.Count > 0;

                        return filter2;
                    })
                    .DefaultIfEmpty()
                    .ToArray();
        }
    }
}
