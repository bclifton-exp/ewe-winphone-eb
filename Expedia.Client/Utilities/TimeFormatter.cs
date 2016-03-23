using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expedia.Entities.Flights;

namespace Expedia.Client.Utilities
{
    public static class TimeFormatter
    {
        //public static string CalculateDuration(string duration1, string duration2)
        //{
        //    try
        //    {
        //        var d1part1 = duration1.Split('T').Last();
        //        var d1part2 = d1part1.Split('H').Last();

        //        int hours1 = int.Parse(d1part1.Split('H').First());
        //        int minutes1;
        //        int.TryParse(d1part2.Split('M').First(), out minutes1);

        //        var d2part1 = duration2.Split('T').Last();
        //        var d2part2 = d2part1.Split('H').Last();

        //        int hours2 = int.Parse(d2part1.Split('H').First());
        //        int minutes2;
        //        int.TryParse(d2part2.Split('M').First(), out minutes2);

        //        var totalMin = minutes1 + minutes2;
        //        var totalHours = hours2 + hours1;

        //        var extraHours = totalMin / 60;

        //        totalHours += extraHours;
        //        var actualMinutes = totalMin % 60;

        //        return string.Format(totalHours + "h" + actualMinutes + "m");
        //    }
        //    catch (Exception ex)
        //    {
        //        var check = ex;
        //        throw;
        //    }

           
        //}

        public static string CalculateDuration(FlightSegment[] segments)
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

            return string.Format(totalHours + "h" + actualMinutes + "m");
        }
    }
}
