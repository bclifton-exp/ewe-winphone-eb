namespace Expedia.Entities.Hotels
{
    public class HotelDetailTile
    {
        public HotelDetailTile() { }

        public HotelDetailTile(string imageHotelUri, string hotelName, int hotelId, string deepLinkUri, string date, int nbGuests, string price)
        {
            ImageHotelUri = imageHotelUri;
            HotelName = hotelName;
            HotelId = hotelId;
            DeepLinkUri = deepLinkUri;
            Date = date;
            NbGuests = nbGuests;
            Price = price;
        }

        public string ImageHotelUri { get; set; }

        public string HotelName { get; set; }

        public int HotelId { get; set; }

        public string DeepLinkUri { get; set; }

        public string Date { get; set; }

        public int NbGuests { get; set; }

        public string Price { get; set; }
    }
}