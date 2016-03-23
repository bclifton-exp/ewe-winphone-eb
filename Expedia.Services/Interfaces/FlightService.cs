using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Client;
using Expedia.Client.Utilities;
using Expedia.Entites;
using Expedia.Entities.Extensions;
using Expedia.Entities.Flights;
using Expedia.Services.Base;
using Expedia.Services.Query;
using Newtonsoft.Json;

namespace Expedia.Services.Interfaces
{
    public class FlightService : BaseService, IFlightService
    {
        private ISettingsService _settingsService;

        public FlightService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<FlightResults> SearchFlights(CancellationToken ct, SearchFlightsLocalParameters searchInput)
        {
            var isSelectingReturnFlight = !searchInput.IsOneWay && searchInput.SelectedDeparture != null && !string.IsNullOrWhiteSpace(searchInput.SelectedDeparture.LegId);

            // Search input mapping from Business to Client
            var clientParameters = new SearchFlightsQueryParameters
            {
                IsOneWay = searchInput.IsOneWay,
                DepartureAirportCode = searchInput.DepartureAirportCode,
                ArrivalAirportCode = searchInput.ArrivalAirportCode,
                DepartureDate = searchInput.DepartureDate,
                ReturnDate = searchInput.ReturnDate,
                NumberOfAdultTravelers = searchInput.AdultsCount,
                ChildTravelersAge = searchInput.ChildrenAges,
            };

            var results = await GetFlights(ct, clientParameters);

            var destinationImageQuery = results.SearchCities
                .Safe()
                .LastOrDefault()
                .SelectOrDefault(_ => new ImageQueryParameters
                {
                    DestinationCode = _.Code,
                    ImageWidth = 920,
                    ImageHeight = 310
                });

            string imageUrl = null;
            if (!string.IsNullOrWhiteSpace(destinationImageQuery.DestinationCode))
            {
                imageUrl = (await GetImage(ct, destinationImageQuery))
                    .SelectOrDefault(_ => _.ImageUrl);
            }

            var selection = new FlightResultSelection
            {
                DestinationName = results.SearchCities.Safe().LastOrDefault().SelectOrDefault(_ => _.City),
                DestinationPictureUrl = imageUrl,
                ReturnName = results.SearchCities.Safe().FirstOrDefault().SelectOrDefault(_ => _.City),
                Flight = searchInput.SelectedDeparture
            };

            var flightItems = isSelectingReturnFlight
                ? GetReturnFlights(results, searchInput.SelectedDeparture.LegId)
                : GetDepartureFlights(results);

            var airlineFilters = flightItems.Safe()
                .Where(flight => !flight.IsMultipleAirlines)
                .GroupBy(flight => flight.AirlineName)
                .Select(group => new FlightFilter
                {
                    Id = group.Key,
                    Title = "{0} ({1})".CurrentCultureFormat(group.Key, group.Count()),
                    Count = group.Count(),
                    FormattedLowestPrice = group.Safe()
                        .OrderBy(flight => flight.Price)
                        .FirstOrDefault()
                        .SelectOrDefault(flight => flight.FormattedWholePrice),
                    MatchingFlights = group.ToArray(),
                    IsFilterEnabled = true
                })
                .OrderBy(filter => filter.Id)
                .ToArray();

            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

            var stopCountFilters = flightItems.Safe()
                .GroupBy(flight =>
                    (flight.ListOfSegments
                        .Safe()
                        .Count()
                        .SelectOrDefault(
                            stopCount => (stopCount > 2
                                ? 3
                                : flight.ListOfSegments
                                    .Safe()
                                    .Count()) - 1)))
                .Select(group => new FlightFilter
                {
                    Id = group.Key.ToStringInvariant(),
                    Title = "{0} ({1})"
                        .CurrentCultureFormat(
                            group.Key > 1 ? loader.GetString("TwoStop") : group.Key > 0 ? loader.GetString("OneStop") : loader.GetString("NonStop"),
                            group.Count()),
                    Count = group.Count(),
                    FormattedLowestPrice = group.Safe()
                        .OrderBy(flight => flight.Price)
                        .FirstOrDefault()
                        .SelectOrDefault(flight => flight.FormattedWholePrice),
                    MatchingFlights = group.ToArray(),
                    IsFilterEnabled = true
                })
                .OrderBy(filter => filter.Id)
                .ToArray();

            if (!isSelectingReturnFlight)
            {
                airlineFilters = SynchronizeFiltersState(searchInput.DepartureAirlineFilters, airlineFilters);
                stopCountFilters = SynchronizeFiltersState(searchInput.DepartureStopCountFilters, stopCountFilters);
            }
            else
            {
                airlineFilters = SynchronizeFiltersState(searchInput.ReturnAirlineFilters, airlineFilters);
                stopCountFilters = SynchronizeFiltersState(searchInput.ReturnStopCountFilters, stopCountFilters);
            }

            var filteredByAirlines =
                airlineFilters.Where(filter => filter.IsFilterEnabled && filter.IsFilterChecked)
                    .SelectMany(filter => filter.MatchingFlights)
                    .ToArray();

            var excludedByAirlineFilters = filteredByAirlines.Length > 0 ? flightItems.Except(filteredByAirlines).ToArray() : new FlightResultItem[0];

            stopCountFilters.ForEach(filter =>
            {
                filter.IsFilterEnabled = filter.MatchingFlights.Except(excludedByAirlineFilters).Any();
            });

            var filteredByStopsCount =stopCountFilters.Where(filter => filter.IsFilterEnabled && filter.IsFilterChecked).SelectMany(filter => filter.MatchingFlights).ToArray();

            var excludedByStopCountFilters = filteredByStopsCount.Length > 0 ? flightItems.Except(filteredByStopsCount).ToArray() : new FlightResultItem[0];

            airlineFilters.ForEach(filter =>
            {
                filter.IsFilterEnabled = filter.MatchingFlights.Except(excludedByStopCountFilters).Any();
            });

            var flights = flightItems.Except(excludedByAirlineFilters.Concat(excludedByStopCountFilters)).OrderBy(OrderFlightsBy(searchInput.OrderBy)).ToArray();

            return new FlightResults
            {
                Selection = selection,
                Flights = flights,
                AirlineFilters = airlineFilters,
                StopCountFilters = stopCountFilters
            };
        }

        private static FlightResultItem[] GetDepartureFlights(FlightSearchResponse results)
        {
            return results.Offers
                    .Where(offer => offer.LegIds.Safe().Any())
                    .GroupBy(offer => offer.LegIds[0])
                    .Join(
                        results.Legs.Where(leg => leg.Segments.Safe().Any()),
                        distinctDepartures => distinctDepartures.Key,
                        leg => leg.LegId,
                        (offer, leg) => CreateFlightResultItem(leg, offer.First()))
                    .ToArray();
        }

        private static FlightResultItem[] GetReturnFlights(FlightSearchResponse results, string departureLegId)
        {
            return results.Offers
                    .Where(offer => offer.LegIds.Length > 1 && offer.LegIds[0] == departureLegId)
                    .GroupBy(offer => offer.LegIds[1])
                    .Join(
                        results.Legs.Where(leg => leg.Segments.Safe().Any()),
                        distinctReturns => distinctReturns.Key,
                        leg => leg.LegId,
                        (offer, leg) => CreateFlightResultItem(leg, offer.First()))
                    .ToArray();
        }

        private static FlightResultItem CreateFlightResultItem(FlightLeg leg, FlightOffer offer)
        {
            var firstSegment = leg.Segments.FirstOrDefault();
            var lastSegment = leg.Segments.LastOrDefault();

            var firstSegmentDepartureTime = firstSegment.DepartureTimeRaw;
            var lastSegmentArrivalTime = lastSegment.ArrivalTimeRaw;
            var totalDays = Math.Abs(lastSegmentArrivalTime.DayOfYear - firstSegmentDepartureTime.DayOfYear);

            var airportCodes = leg.Segments.Select(segment => segment.DepartureAirportCode).ToArray();

            var lastSegmentArrivalAirportCode = lastSegment.ArrivalAirportCode;
            var isMultipleAirlines = leg.Segments.Select(segment => segment.AirlineName).Distinct().Count() > 1;

            var duration = TimeFormatter.CalculateDuration(leg.Segments);

            return new FlightResultItem
            {
                AirlineName = isMultipleAirlines ? string.Empty : firstSegment.AirlineName,
                IsMultipleAirlines = isMultipleAirlines,
                ProductKey = offer.ProductKey,
                DepartureTime = firstSegment.DepartureTime,
                DepartureTimeRaw = firstSegment.DepartureTimeRaw,
                ArrivalTime = lastSegment.ArrivalTime,
                ArrivalTimeRaw = lastSegment.ArrivalTimeRaw,
                CurrencyCode = offer.Currency,
                FormattedPrice = offer.TotalFarePrice.FormattedPrice,
                FormattedWholePrice = offer.TotalFarePrice.FormattedWholePrice,
                Price = offer.TotalFarePrice.Amount,
                LegId = leg.LegId,
                LegDurationInDays = totalDays,
                ListOfSegments = airportCodes,
                LastSegmentArrivalAirportCode = lastSegmentArrivalAirportCode,
                Duration = duration
            };
        }

        private static Func<FlightResultItem, object> OrderFlightsBy(OrderFlightsByType orderBy)
        {
            Func<FlightResultItem, object> orderByKeySelectionFunc;
            switch (orderBy)
            {
                case OrderFlightsByType.PricesLowToHigh:
                    orderByKeySelectionFunc = item => item.Price;
                    break;
                case OrderFlightsByType.Duration:
                    orderByKeySelectionFunc = item => item.ArrivalTimeRaw - item.DepartureTimeRaw;
                    break;
                case OrderFlightsByType.ArrivalTime:
                    orderByKeySelectionFunc = item => item.ArrivalTimeRaw;
                    break;
                case OrderFlightsByType.DepartureTime:
                    orderByKeySelectionFunc = item => item.DepartureTimeRaw;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("orderBy");
            }

            return orderByKeySelectionFunc;
        }

        private static FlightFilter[] SynchronizeFiltersState(FlightFilter[] oldFilters, FlightFilter[] newFilters)
        {
            if (oldFilters == null || !oldFilters.Any())
            {
                return newFilters;
            }

            return oldFilters
                .Join(
                    newFilters,
                    filter => filter.Id,
                    filter => filter.Id,
                    (filter1, filter2) =>
                    {
                        filter2.IsFilterChecked = filter1.IsFilterChecked;
                        filter2.IsFilterEnabled = filter1.IsFilterEnabled;

                        return filter2;
                    })
                    .DefaultIfEmpty()
                    .ToArray();
        }

        public async Task<FlightSearchResponse> GetFlights(CancellationToken ct, SearchFlightsQueryParameters queryParameters)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var query = new QueryBuilder(queryParameters).ReturnQueryUri();

            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.FlightsApiRoot);
            request.AppendPath(Constants.UrlActions.Search);
            request.AppendPath(query);
            var result = await ExecuteGet(request.GetFullUri(), ct);

            return result != null ? JsonConvert.DeserializeObject<FlightSearchResponse>(result) : null;
        }

        public async Task<FlightDetailsResponse> GetDetails(CancellationToken ct, FlightDetailsQueryParameters queryParameters)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var query = new QueryBuilder(queryParameters).ReturnQueryUri();

            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.FlightsApiRoot);
            request.AppendPath(Constants.UrlActions.Details);
            request.AppendPath(query);
            var result = await ExecuteGet(request.GetFullUri(), ct);

            return result != null ? JsonConvert.DeserializeObject<FlightDetailsResponse>(result) : null;
        }

        public async Task<ImageResponse> GetImage(CancellationToken ct, ImageQueryParameters queryParameters)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var query = new QueryBuilder(queryParameters).ReturnQueryUri();

            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.FlightsApiRoot);
            request.AppendPath(Constants.UrlActions.Image);
            request.AppendPath(query);
            var result = await ExecuteGet(request.GetFullUri(), ct);

            return result != null ? JsonConvert.DeserializeObject<ImageResponse>(result) : null;
        }

        public async Task<AirportDropDownResponse> GetAirportDropDown(CancellationToken ct)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            //var query = new QueryBuilder(queryParameters).ReturnQueryUri();

            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.FlightsApiRoot);
            request.AppendPath("airportDropDown");
            //request.AppendPath(query);
            var result = await ExecuteGet(request.GetFullUri(), ct);

            return result != null ? JsonConvert.DeserializeObject<AirportDropDownResponse>(result) : null;
        }

    }
}
