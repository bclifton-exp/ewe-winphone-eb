using Expedia.Entities.Cars;
using Expedia.Entities.Flights;
using Expedia.Entities.Hotels;

namespace Expedia.Client.Utilities
{
    public class SearchParamsMemory
    {
        private static SearchParamsMemory _instance;
        public SearchHotelsLocalParameters HotelParams;
        public SearchFlightsLocalParameters FlightParams;
        public SearchCarsLocalParameters CarParams;

        public static SearchParamsMemory Instance()
        {
            return _instance ?? (_instance = new SearchParamsMemory());
        }
    }
}
