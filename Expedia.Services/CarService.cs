using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Client;
using Expedia.Entities.Cars;
using Expedia.Entities.Hotels;
using Expedia.Services.Base;
using Expedia.Services.Interfaces;
using Expedia.Services.Query;
using Newtonsoft.Json;

namespace Expedia.Services
{
    public class CarService : BaseService, ICarService
    {
        private ISettingsService _settingsService;

        public CarService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<CarResults> Search(CancellationToken ct, SearchCarsLocalParameters searchInput)
        {
            var clientSearchParameters = CreateClientQueryParameters(searchInput);

            var results = await GetCars(ct, clientSearchParameters);

            return await CreateCarResultsFromClientResponse(ct, searchInput, results);
        }

        private static CarSearchQueryParameters CreateClientQueryParameters(SearchCarsLocalParameters searchInput)
        {
            var clientSearchParameters = new CarSearchQueryParameters()
            {
                AirportCode = searchInput.AirportCode,
                DropOffTime = searchInput.DropOffTime,
                PickupTime = searchInput.PickupTime,
                PickupLocationLat = searchInput.PickupLocationLat,
                PickupLocationLon = searchInput.PickupLocationLon,
                SearchRadius = searchInput.SearchRadius,
            };

            return clientSearchParameters;
        }

        private async Task<CarSearchResponse> GetCars(CancellationToken ct, CarSearchQueryParameters queryParameters)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var request = new ApiRequest(urlBase);

            request.AppendPath(Constants.Urls.MobileCarsApiRoot);
            request.AppendPath(string.IsNullOrEmpty(queryParameters.AirportCode) ? "location?" : "airport?");

            var query = new QueryBuilder(queryParameters).ReturnQueryUri();
            request.AppendPath(query);

            var result = await ExecuteGet(request.GetFullUri(), ct);
            return result != null ? JsonConvert.DeserializeObject<CarSearchResponse>(result) : null;
        }

        private static async Task<CarResults> CreateCarResultsFromClientResponse(CancellationToken ct, SearchCarsLocalParameters searchInput, CarSearchResponse results)
        {
            if (results.offers == null || !results.offers.Any())
            {
                results.offers = new Offer[0];
            }

            foreach (var car in results.offers)
            {
                if (car.vehicleInfo.makes == null)
                {
                    car.vehicleInfo.makes = new[] { car.vehicleInfo.carCategoryDisplayLabel };
                }
                if (car.vehicleInfo.makes[0] == null)
                {
                    car.vehicleInfo.makes[0] = car.vehicleInfo.carCategoryDisplayLabel;
                }
            }

            return await Task.Factory.StartNew(() =>
            {
                return new CarResults
                {
                    AllCars = results.offers.ToList(),
                    EconomyCars = results.offers.Where(o => o.vehicleInfo.category == CarCategory.ECONOMY.ToString()).ToList(),
                    MiniCars = results.offers.Where(o => o.vehicleInfo.category == CarCategory.MINI.ToString() && o.vehicleInfo.type.Contains("CAR")).ToList(),
                    CompactCars = results.offers.Where(o => o.vehicleInfo.category == CarCategory.COMPACT.ToString() && o.vehicleInfo.type.Contains("CAR")).ToList(),
                    MidSizeCars = results.offers.Where(o => o.vehicleInfo.category == CarCategory.MIDSIZE.ToString() && o.vehicleInfo.type.Contains("CAR")).ToList(),
                    StandardCars = results.offers.Where(o => o.vehicleInfo.category == CarCategory.STANDARD.ToString() && o.vehicleInfo.type.Contains("CAR")).ToList(),
                    FullSizeCars = results.offers.Where(o => o.vehicleInfo.category == CarCategory.FULLSIZE.ToString() && o.vehicleInfo.type.Contains("CAR")).ToList(),
                    PremiumCars = results.offers.Where(o => o.vehicleInfo.category == CarCategory.PREMIUM.ToString() && o.vehicleInfo.type.Contains("CAR")).ToList(),
                    LuxuryCars = results.offers.Where(o => o.vehicleInfo.category == CarCategory.LUXURY.ToString() && o.vehicleInfo.type.Contains("CAR")).ToList(),
                    SpecialCars = results.offers.Where(o => o.vehicleInfo.category == CarCategory.SPECIAL.ToString() && (o.vehicleInfo.type.Contains("CAR") || o.vehicleInfo.type.Contains("SPECIAL"))).ToList(),
                    StandardVans = results.offers.Where(o => o.vehicleInfo.category == CarCategory.STANDARD.ToString() && o.vehicleInfo.type == "VAN").ToList(),
                    FullsizeVans = results.offers.Where(o => o.vehicleInfo.category == CarCategory.FULLSIZE.ToString() && o.vehicleInfo.type == "VAN").ToList(),
                    SpecialVans = results.offers.Where(o => o.vehicleInfo.category == CarCategory.SPECIAL.ToString() && o.vehicleInfo.type == "VAN").ToList(),
                    PremiumVans = results.offers.Where(o => o.vehicleInfo.category == CarCategory.PREMIUM.ToString() && o.vehicleInfo.type == "VAN").ToList(),
                    MiniVans = results.offers.Where(o => o.vehicleInfo.category == CarCategory.MINI.ToString() && o.vehicleInfo.type == "VAN").ToList(),
                    PremiumSUVs = results.offers.Where(o => o.vehicleInfo.category == CarCategory.PREMIUM.ToString() && o.vehicleInfo.type == "SUV").ToList(),
                    CarCategoryResults = BuildCarCategoryResults(results)
                };
            }, ct);
        }

        private static List<CarCategoryResult> BuildCarCategoryResults(CarSearchResponse results)
        {
            var carCategoriesToReturn = new List<CarCategoryResult>();

            foreach (var carCategory in Enum.GetNames(typeof(CarCategory)))
            {
                var cars = results.offers.Where(o => o.vehicleInfo.category.Contains(carCategory)).ToList();
                var possibleCarTypes = cars.Select(c => c.vehicleInfo.type).Distinct().ToList();

                foreach (var carType in possibleCarTypes)
                {
                    var carCategoryResult = new CarCategoryResult();
                    cars = results.offers.Where(o => o.vehicleInfo.category.Contains(carCategory) && o.vehicleInfo.type.Contains(carType)).ToList();

                    //var carsOffers = cars ?? cars.ToArray();
                    if (cars.Any())
                    {
                        carCategoryResult.TotalCarsInCategory = cars.Count();
                        carCategoryResult.CarCategory = cars.First().vehicleInfo.carCategoryDisplayLabel;
                        carCategoryResult.CarType = cars.First().vehicleInfo.type;
                        carCategoryResult.CategoryImageUrlPart = carCategoryResult.CarCategory + "_" + carCategoryResult.CarType.Replace("_", "");

                        carCategoryResult.LowestDailyRate = cars.OrderByDescending(c => double.Parse(c.fare.rate.amount)).Last().fare.rate.formattedPrice;
                        carCategoryResult.LowestTotalRate = cars.OrderByDescending(c => double.Parse(c.fare.rate.amount)).Last().fare.total.formattedPrice;

                        var maxBags = cars.OrderByDescending(c => c.vehicleInfo.largeLuggageCapacity).First().vehicleInfo.largeLuggageCapacity;
                        var minBags = cars.Any(c => c.vehicleInfo.largeLuggageCapacity > 0) ?
                            cars.Where(c => c.vehicleInfo.largeLuggageCapacity > 0).OrderByDescending(c => c.vehicleInfo.largeLuggageCapacity).Last().vehicleInfo.largeLuggageCapacity : 0;
                        var maxDoors = cars.OrderByDescending(c => c.vehicleInfo.maxDoors).First().vehicleInfo.maxDoors == 0 ? 2 : cars.OrderByDescending(c => c.vehicleInfo.maxDoors).First().vehicleInfo.maxDoors;
                        var minDoors = cars.Any(c => c.vehicleInfo.minDoors > 0) ?
                            cars.Where(c => c.vehicleInfo.minDoors > 0).OrderByDescending(c => c.vehicleInfo.minDoors).Last().vehicleInfo.minDoors : 2;
                        var maxPassengers = cars.OrderByDescending(c => c.vehicleInfo.adultCapacity).First().vehicleInfo.adultCapacity == 0 ? 2 : cars.OrderByDescending(c => c.vehicleInfo.adultCapacity).First().vehicleInfo.adultCapacity;
                        var minPassengers = cars.Any(c => c.vehicleInfo.adultCapacity > 0) ? cars.Where(c => c.vehicleInfo.adultCapacity > 0).OrderByDescending(c => c.vehicleInfo.adultCapacity).Last().vehicleInfo.adultCapacity : 2;

                        carCategoryResult.MinMaxPassengers = minPassengers == maxPassengers ? minPassengers.ToString() : minPassengers + "-" + maxPassengers;
                        carCategoryResult.MinMaxDoors = minDoors == maxDoors ? minDoors.ToString() : minDoors + "-" + maxDoors;
                        carCategoryResult.MinMaxBags = minBags == maxBags ? minBags.ToString() : minBags + "-" + maxBags;

                        carCategoriesToReturn.Add(carCategoryResult);
                    }
                }
            }
            return carCategoriesToReturn;
        }
    }
}
