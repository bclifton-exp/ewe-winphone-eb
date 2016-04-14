using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;

namespace Expedia.Client.Utilities
{
    public static class MobileMapLauncher
    {
        private static string baseUri = "bingmaps:?";

        public static void LaunchMap(double latitude, double longitude, int zoom, string pointName)
        {
            string uri = string.Format("{0}collection=point.{1:N5}_{2:N5}_{4}&cp={1:N5}~{2:N5}&lvl={3}", baseUri, latitude, longitude, zoom, pointName);

            Launch(new Uri(uri));
        }

        private static async void Launch(Uri uri)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);

            if (!success)
            {
                var msg = new MessageDialog("Failed to launch maps app.");
                await msg.ShowAsync();
            }
        }
    }
}
    