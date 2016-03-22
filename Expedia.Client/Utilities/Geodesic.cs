using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;

namespace Expedia.Client.Utilities
{
    public static class Geodesic
    {
        public static List<BasicGeoposition> ToGeodesic(BasicGeoposition start, BasicGeoposition finish)
        {
            var locs = new List<BasicGeoposition>();

            for (int i = 0; i < 1; i++)
            {
                var lat1 = start.Latitude * (Math.PI / 180);
                var lon1 = start.Longitude * (Math.PI / 180);
                var lat2 = finish.Latitude * (Math.PI / 180);
                var lon2 = finish.Longitude * (Math.PI / 180);

                var d = 2 *
                        Math.Asin(
                            Math.Sqrt(Math.Pow((Math.Sin((lat1 - lat2) / 2)), 2) +
                                      Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow((Math.Sin((lon1 - lon2) / 2)), 2)));

                for (double k = 0; k <= 32; k++)
                {
                    var f = (k / 32);
                    var A = Math.Sin((1 - f) * d) / Math.Sin(d);
                    var B = Math.Sin(f * d) / Math.Sin(d);
                    // Obtain 3D Cartesian coordinates of each point
                    var x = A * Math.Cos(lat1) * Math.Cos(lon1) + B * Math.Cos(lat2) * Math.Cos(lon2);
                    var y = A * Math.Cos(lat1) * Math.Sin(lon1) + B * Math.Cos(lat2) * Math.Sin(lon2);
                    var z = A * Math.Sin(lat1) + B * Math.Sin(lat2);
                    // Convert these to latitude/longitude
                    var lat = Math.Atan2(z, Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
                    var lon = Math.Atan2(y, x);
                    // Create a Location (convert back to degrees)
                    var loc = new BasicGeoposition
                    {
                        Latitude = lat / (Math.PI / 180),
                        Longitude = lon / (Math.PI / 180)
                    };
                    locs.Add(loc);
                }
            }
            return locs;
        }
    }
}
