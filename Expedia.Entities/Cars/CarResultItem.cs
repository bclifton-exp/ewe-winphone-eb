using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Cars
{
    public class CarResultItem
    {
        public class PickupTime
        {
            public string raw { get; set; }
            public string localized { get; set; }
            public int epochSeconds { get; set; }
            public int timeZoneOffsetSeconds { get; set; }
            public string localizedShortDate { get; set; }
        }

        public class DropOffTime
        {
            public string raw { get; set; }
            public string localized { get; set; }
            public int epochSeconds { get; set; }
            public int timeZoneOffsetSeconds { get; set; }
            public string localizedShortDate { get; set; }
        }

        public class Vendor
        {
            public int id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
        }

        public class PickUpLocation
        {
            public string locationType { get; set; }
            public string locationDescription { get; set; }
            public string airportInstructions { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string locationCode { get; set; }
        }

        public class DropOffLocation
        {
            public string locationType { get; set; }
            public string locationDescription { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string locationCode { get; set; }
        }

        public class Rate
        {
            public string amount { get; set; }
            public string formattedPrice { get; set; }
            public string formattedWholePrice { get; set; }
            public string currencyCode { get; set; }
        }

        public class Total
        {
            public string amount { get; set; }
            public string formattedPrice { get; set; }
            public string formattedWholePrice { get; set; }
            public string currencyCode { get; set; }
        }

        public class Fare
        {
            public string rateTerm { get; set; }
            public Rate rate { get; set; }
            public Total total { get; set; }
        }

        public class VehicleInfo
        {
            public string category { get; set; }
            public string type { get; set; }
            public string fuel { get; set; }
            public string transmission { get; set; }
            public string drive { get; set; }
            public bool hasAirConditioning { get; set; }
            public List<string> makes { get; set; }
            public int minDoors { get; set; }
            public int maxDoors { get; set; }
            public int adultCapacity { get; set; }
            public int childCapacity { get; set; }
            public int largeLuggageCapacity { get; set; }
            public int smallLuggageCapacity { get; set; }
            public string carCategoryDisplayLabel { get; set; }
            public string carCategoryDisplayFullLabel { get; set; }
        }

        public class Offer
        {
            public string productKey { get; set; }
            public Vendor vendor { get; set; }
            public bool creditCardRequiredToGuaranteeReservation { get; set; }
            public PickUpLocation pickUpLocation { get; set; }
            public DropOffLocation dropOffLocation { get; set; }
            public Fare fare { get; set; }
            public bool hasFreeCancellation { get; set; }
            public bool hasUnlimitedMileage { get; set; }
            public bool isMerchant { get; set; }
            public bool isInsuranceIncluded { get; set; }
            public VehicleInfo vehicleInfo { get; set; }
        }

        public class RootObject
        {
            public PickupTime pickupTime { get; set; }
            public DropOffTime dropOffTime { get; set; }
            public List<Offer> offers { get; set; }
            public string activityId { get; set; }
        }
    }
}
