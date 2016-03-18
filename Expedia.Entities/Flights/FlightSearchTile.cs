namespace Expedia.Entities.Flights
{
    public class FlightSearchTile : TileInfoBase
    {
        public FlightSearchTile() { }

        public FlightSearchTile(string arrivalAirportCode, string arrivalCity, string departureAirportCode, string departureCity, string date, int nbTraveler, int nbFlights, string imageUri, string deepLinkUri)
        {
            ArrivalAirportCode = arrivalAirportCode;
            ArrivalCity = arrivalCity;
            DepartureAirportCode = departureAirportCode;
            DepartureCity = departureCity;
            Date = date;
            NbTraveler = nbTraveler;
            NbFlights = nbFlights;
            ImageUri = imageUri;
            DeepLinkUri = deepLinkUri;
        }

        public string ArrivalAirportCode { get; set; }

        public string ArrivalCity { get; set; }

        public string DepartureAirportCode { get; set; }

        public string DepartureCity { get; set; }

        public string Date { get; set; }

        public int NbTraveler { get; set; }

        public int NbFlights { get; set; }
    }
}