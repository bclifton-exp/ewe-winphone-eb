using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Expedia.Entities.Extensions
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }
}
    