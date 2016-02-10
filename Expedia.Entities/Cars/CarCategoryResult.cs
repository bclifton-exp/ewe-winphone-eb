using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Entities.Cars;

namespace Expedia.Entities.Cars
{
    public class CarCategoryResult
    {
        public string CarCategory { get; set; }
        public string CarType { get; set; }
        public string CategoryImageUrlPart { get; set; }
        public int TotalCarsInCategory { get; set; }
        public string LowestDailyRate { get; set; }
        public string LowestTotalRate { get; set; }
        public string MinMaxPassengers { get; set; }
        public string MinMaxDoors { get; set; }
        public string MinMaxBags { get; set; }

        public bool IsChecked { get; set; }
    }
}
