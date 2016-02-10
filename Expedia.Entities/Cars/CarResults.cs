using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Entities.Cars;

namespace Expedia.Entities.Cars
{
    public class CarResults
    {
        public List<Offer> AllCars { get; set; } 

        public List<Offer> EconomyCars { get; set; }
        public List<Offer> MiniCars { get; set; }
        public List<Offer> CompactCars { get; set; }
        public List<Offer> MidSizeCars { get; set; }
        public List<Offer> StandardCars { get; set; }
        public List<Offer> FullSizeCars { get; set; }
        public List<Offer> PremiumCars { get; set; }
        public List<Offer> LuxuryCars { get; set; }
        public List<Offer> SpecialCars { get; set; }
        public List<Offer> FullsizeVans { get; set; }
        public List<Offer> StandardVans { get; set; }
        public List<Offer> SpecialVans { get; set; }
        public List<Offer> MiniVans { get; set; }
        public List<Offer> PremiumVans { get; set; } 
        public List<Offer> PremiumSUVs { get; set; }
        public List<Offer> RegularPickups { get; set; }
        public List<Offer> ExtendedPickups { get; set; }

        public List<CarCategoryResult> CarCategoryResults { get; set; }
    }
}
