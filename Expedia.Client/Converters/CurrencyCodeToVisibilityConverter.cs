using System.Globalization;
using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;

namespace Expedia.Client.Converters
{
	public class CurrencyCodeToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, String language)
		{
			var stringValue = value as string;
			if(!string.IsNullOrWhiteSpace(stringValue) && targetType == typeof (Visibility))
			{
				try
				{
					var regionInfo = new RegionInfo(CultureInfo.CurrentUICulture.Name);
                    // Hack to avoid double MXN as the currency symbol already includes it.
                    if (regionInfo.ISOCurrencySymbol == stringValue || stringValue == "MXN")
                    {
                        return Visibility.Collapsed;
                    }
					return Visibility.Visible;
				}
				catch (Exception exception)
				{
					//this.Log().Error("Unable to use the language's GenericCulture to create a RegionInfo.", exception);
				}
			}
			return "{null}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, String language)
		{
			throw new NotSupportedException();
		}
	}
}
