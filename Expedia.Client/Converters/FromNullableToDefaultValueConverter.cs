using System;
using System.Reflection;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class FromNullableToDefaultValueConverter : IValueConverter
    {
        public FromNullableToDefaultValueConverter()
        {
            ValueIfNull = null;
            ValueIfNotNull = null;
        }

        public object ValueIfNull { get; set; }

        public object ValueIfNotNull { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null)
            {
                throw new ArgumentException("This converter does not use any parameters");
            }

            if (value == null)
            {
                return ValueIfNull ?? GetDefaultValue(targetType);
            }
            else
            {
                return ValueIfNotNull ?? value;
            }
        }

        private static object GetDefaultValue(Type targetType)
        {
            return targetType.GetTypeInfo().IsValueType ?

                Activator.CreateInstance(targetType) :
                null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
