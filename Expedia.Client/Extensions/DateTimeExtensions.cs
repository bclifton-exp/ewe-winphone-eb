using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Client.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        }
    }
}
