using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Extensions
{
    public static class ObjectExtensions
    {
        public static TResult SelectOrDefault<TInstance, TResult>(this TInstance instance, Func<TInstance, TResult> selector)
        {
            return SelectOrDefault(instance, selector, default(TResult));
        }

        public static TResult SelectOrDefault<TInstance, TResult>(this TInstance instance, Func<TInstance, TResult> selector, TResult defaultValue)
        {
            return instance == null ? defaultValue : selector(instance);
        }
    }
}
