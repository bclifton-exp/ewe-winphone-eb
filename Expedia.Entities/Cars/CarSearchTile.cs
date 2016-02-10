using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Cars
{
    public class CarSearchTile : TileInfoBase
    {
        public CarSearchTile() { }
        public CarSearchTile(string location, string imageUri, string dates, int nbHotelAvailable, int nbGuests, string deepLinkUri)
        {
            Location = location;
            ImageUri = imageUri;
            Dates = dates;
            NbHotelAvailable = 1;
            NbGuests = 1;
            DeepLinkUri = deepLinkUri;
        }

        public string Location { get; set; }
        public string Dates { get; set; }
        public int NbHotelAvailable { get; set; }
        public int NbGuests { get; set; }
    }
}
