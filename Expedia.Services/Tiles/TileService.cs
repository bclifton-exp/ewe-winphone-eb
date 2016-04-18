using Windows.UI.Notifications;
using NotificationsExtensions.Tiles;

namespace Expedia.Services.Tiles
{
    public class TileService
    {
        private static TileService _instance;

        protected TileService()
        {
            
        }

        public static TileService Instance()
        {
            return _instance ?? (_instance = new TileService());
        }

        public void InitializeLiveTile()
        {
            var tileTest = new TileContent
            {
                Visual = new TileVisual
                {
                    TileMedium = new TileBinding
                    {
                        Content = new TileBindingContentPhotos
                        {
                            Images =
                            {
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/nyc_300_250.jpg"),
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/par_300_250.jpg"),
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/sfo_300_250.jpg"),
                                new TileImageSource("ms-appx:///Assets/Square150x150Logo.png"),
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/lon_300_250.jpg"),
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/mco_300_250.jpg"),
                            }
                        }
                    },
                    TileWide = new TileBinding
                    {
                        Content = new TileBindingContentPhotos
                        {
                            Images =
                            {
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/nyc_300_250.jpg"),
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/par_300_250.jpg"),
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/sfo_300_250.jpg"),
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/lon_300_250.jpg"),
                                new TileImageSource(
                                    "https://images.trvl-media.com/mobiata/mobile/destination/mco_300_250.jpg"),
                            }
                        }
                    }
                }
            };

            var notification = new TileNotification(tileTest.GetXml());

            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }
    }
}
