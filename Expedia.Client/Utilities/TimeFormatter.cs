using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expedia.Entities.Flights;

namespace Expedia.Client.Utilities
{
    public static class TimeFormatter
    {
        public static string CalculateDuration(FlightSegment[] segments)
        {
            try
            {
                int totalMin = 0;
                int totalHours = 0;

                foreach (var segment in segments)
                {
                    var time1 = segment.Duration.Split('T').Last();
                    var time2 = time1.Split('H').Last();

                    int hours = int.Parse(time1.Split('H').First());
                    int minutes;
                    int.TryParse(time2.Split('M').First(), out minutes);

                    totalMin += minutes;
                    totalHours += hours;
                }

                var extraHours = totalMin / 60;

                totalHours += extraHours;
                var actualMinutes = totalMin % 60;

                var days = totalHours / 24;
                var remainingHours = totalHours % 24;

                return days > 0 ? string.Format(days + "d" + remainingHours + "h" + actualMinutes + "m") : string.Format(totalHours + "h" + actualMinutes + "m");
            }
            catch (Exception ex)
            {
                var test = ex;
                throw;
            }
            
        }
    }
}
