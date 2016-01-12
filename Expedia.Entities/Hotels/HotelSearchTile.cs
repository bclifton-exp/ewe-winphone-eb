namespace Expedia.Entities.Hotels
{
    public class HotelSearchTile : TileInfoBase
    {
        public HotelSearchTile() { }
        public HotelSearchTile(string location, string imageUri, string dates, int nbHotelAvailable, int nbGuests, string deepLinkUri)
        {
            Location = location;
            ImageUri = imageUri;
            Dates = dates;
            NbHotelAvailable = nbHotelAvailable;
            NbGuests = nbGuests;
            DeepLinkUri = deepLinkUri;
        }

        public string Location { get; set; }
        public string Dates { get; set; }
        public int NbHotelAvailable { get; set; }
        public int NbGuests { get; set; }
    }
}