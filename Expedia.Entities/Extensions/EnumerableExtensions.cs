using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Expedia.Entities.Extensions
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }

        public static IEnumerable<T> Safe<T>(this IEnumerable<T> items)
        {
            return items ?? Enumerable.Empty<T>();
        }

        public static T MinOrDefault<T>(this IEnumerable<T> items)
        {
            if (items.Any())
            {
                return items.Min();
            }
            else
            {
                return default(T);
            }
        }

        public static T MaxOrDefault<T>(this IEnumerable<T> items)
        {
            if (items.Any())
            {
                return items.Max();
            }
            else
            {
                return default(T);
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    action(item);
                }
            }

            return items;
        }

        public static void ForEach(this IEnumerable enumerable, Action<object> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }
    }
}
    