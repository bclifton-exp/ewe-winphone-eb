using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Expedia.Client.Converters
{
    public class CarCategoryToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var carCategory = value != null ? value.ToString() : "null";

            switch (carCategory)
            {
                case "Economy_TWODOORCAR":
                    return Application.Current.Resources["EconomyCarBrush"] as ImageBrush;
                case "Economy_FOURDOORCAR":
                    return Application.Current.Resources["EconomyCarBrush4D"] as ImageBrush;
                case "Compact_TWODOORCAR":
                    return Application.Current.Resources["CompactCarBrush"] as ImageBrush;
                case "Compact_FOURDOORCAR":
                    return Application.Current.Resources["CompactCarBrush4D"] as ImageBrush;
                case "Midsize_TWODOORCAR":
                    return Application.Current.Resources["MidsizeCarBrush"] as ImageBrush;
                case "Midsize_FOURDOORCAR":
                    return Application.Current.Resources["MidsizeCarBrush4D"] as ImageBrush;
                case "Standard_TWODOORCAR":
                    return Application.Current.Resources["StandardCarBrush"] as ImageBrush;
                case "Standard_FOURDOORCAR":
                    return Application.Current.Resources["StandardCarBrush4D"] as ImageBrush;
                case "Fullsize_TWODOORCAR":
                    return Application.Current.Resources["FullsizeCarBrush"] as ImageBrush;
                case "Fullsize_FOURDOORCAR":
                    return Application.Current.Resources["FullsizeCarBrush4D"] as ImageBrush;
                case "Premium_TWODOORCAR":
                    return Application.Current.Resources["PremiumCarBrush"] as ImageBrush;
                case "Premium_THREEDOORCAR":
                    return Application.Current.Resources["PremiumCarBrush3D"] as ImageBrush;
                case "Premium_FOURDOORCAR":
                    return Application.Current.Resources["PremiumCarBrush4D"] as ImageBrush;
                case "Luxury_TWODOORCAR":
                    return Application.Current.Resources["LuxuryCarBrush"] as ImageBrush;
                case "Luxury_FOURDOORCAR":
                    return Application.Current.Resources["LuxuryCarBrush4D"] as ImageBrush;
                case "Special_SPECIAL":
                    return Application.Current.Resources["SpecialCarBrush"] as ImageBrush;
                case "Special_TWODOORCAR":
                    return Application.Current.Resources["SpecialCarBrush"] as ImageBrush;
                case "Special_FOURDOORCAR":
                    return Application.Current.Resources["SpecialCarBrush"] as ImageBrush;
                case "Mini Van_VAN":
                    return Application.Current.Resources["MiniVanCarBrush"] as ImageBrush;
                case "Midsize Van_VAN":
                    return Application.Current.Resources["MidsizeVanBrush"] as ImageBrush;
                case "Standard Van_VAN":
                    return Application.Current.Resources["StandardVanBrush"] as ImageBrush;
                case "Fullsize Van_VAN":
                    return Application.Current.Resources["FullsizeVanBrush"] as ImageBrush;
                case "Premium Van_VAN":
                    return Application.Current.Resources["PremiumVanBrush"] as ImageBrush;
                case "Special Van_VAN":
                    return Application.Current.Resources["SpecialVanBrush"] as ImageBrush;
                case "Mini_TWODOORCAR":
                    return Application.Current.Resources["MiniCarBrush"] as ImageBrush;
                case "Mini_FOURDOORCAR":
                    return Application.Current.Resources["MiniCarBrush4D"] as ImageBrush;
                case "Premium SUV_SUV":
                    return Application.Current.Resources["PremiumSUVBrush"] as ImageBrush;
                case "Premium Pickup Regular Cab_PICKUPREGULARCAB":
                    return Application.Current.Resources["PremiumPickupBrush"] as ImageBrush;
                case "Premium Pickup Extended Cab_PICKUPEXTENDEDCAB":
                    return Application.Current.Resources["PremiumPickupExtendedBrush"] as ImageBrush;
                case "Midsize SUV_SUV":
                    return Application.Current.Resources["MidsizeSUVBrush"] as ImageBrush;
                case "Standard SUV_SUV":
                    return Application.Current.Resources["StandardSUVBrush"] as ImageBrush;
                case "Fullsize SUV_SUV":
                    return Application.Current.Resources["FullsizeSUVBrush"] as ImageBrush;
                case "Luxury SUV_SUV":
                    return Application.Current.Resources["LuxurySUVBrush"] as ImageBrush;
                case "Special SUV_SUV":
                    return Application.Current.Resources["SpecialSUVBrush"] as ImageBrush;
                case "Compact SUV_SUV":
                    return Application.Current.Resources["CompactSUVBrush"] as ImageBrush;
                case "Standard Pickup Regular Cab_PICKUPREGULARCAB":
                    return Application.Current.Resources["StandardPickupBrush"] as ImageBrush;
                case "Midsize Pickup Regular Cab_PICKUPREGULARCAB":
                    return Application.Current.Resources["MidsizePickupBrush"] as ImageBrush;
                case "Standard SportsCar_SPORTSCAR":
                    return Application.Current.Resources["StandardSportsCarBrush"] as ImageBrush;
                case "Special SportsCar_SPORTSCAR":
                    return Application.Current.Resources["SpecialSportsCarBrush"] as ImageBrush;
                case "Standard Convertible_CONVERTIBLE":
                    return Application.Current.Resources["StandardConvertibleBrush"] as ImageBrush;
                case "Special Convertible_CONVERTIBLE":
                    return Application.Current.Resources["SpecialConvertibleBrush"] as ImageBrush;
                case "Mini Convertible_CONVERTIBLE":
                    return Application.Current.Resources["MiniConvertibleBrush"] as ImageBrush;
                case "Compact Convertible_CONVERTIBLE":
                    return Application.Current.Resources["CompactConvertibleBrush"] as ImageBrush;
                case "Fullsize Convertible_CONVERTIBLE":
                    return Application.Current.Resources["FullsizeConvertibleBrush"] as ImageBrush;
                case "Midsize Convertible_CONVERTIBLE":
                    return Application.Current.Resources["MidsizeConvertibleBrush"] as ImageBrush;
                case "Premium Convertible_CONVERTIBLE":
                    return Application.Current.Resources["PremiumConvertibleBrush"] as ImageBrush;
                case "Luxury Convertible_CONVERTIBLE":
                    return Application.Current.Resources["LuxuryConvertibleBrush"] as ImageBrush;
                case "Fullsize Recreational Vehicle_RECREATIONALVEHICLE":
                    return Application.Current.Resources["FullsizeRecreationalVehicleBrush"] as ImageBrush;
                case "Fullsize Pickup Regular Cab_PICKUPREGULARCAB":
                    return Application.Current.Resources["FullsizePickupRegularBrush"] as ImageBrush;
                case "Premium Wagon_WAGON":
                    return Application.Current.Resources["PremiumWagonBrush"] as ImageBrush;


                default:
                    return Application.Current.Resources["StandardCarBrush"] as ImageBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
