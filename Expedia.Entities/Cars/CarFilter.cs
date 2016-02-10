using System;
using System.Collections.Generic;
using System.Text;


namespace Expedia.Entities.Cars
{
    public class CarFilter
    {
        public string Id { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        //public string FormattedTitle { get { return "{0}".InvariantCultureFormat(Title); } }
        public string FormattedTitle { get { return Title; } }

        public bool IsFilterEnabled { get; set; }
        public bool IsFilterChecked { get; set; }

        public sealed class IdEqualityComparer : IEqualityComparer<CarFilter>
        {
            public bool Equals(CarFilter x, CarFilter y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }
                if (ReferenceEquals(x, null))
                {
                    return false;
                }
                if (ReferenceEquals(y, null))
                {
                    return false;
                }

                if (x.GetType() != y.GetType())
                {
                    return false;
                }

                return string.Equals(x.Id, y.Id);
            }

            public int GetHashCode(CarFilter obj)
            {
                return (obj.Id != null ? obj.Id.GetHashCode() : 0);
            }
        }
    }
}
