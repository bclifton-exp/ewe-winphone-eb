using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Expedia.Client.Converters
{
    public class ImageToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var imageUri = value as Uri;
            return imageUri != null ? new ImageBrush {ImageSource = new BitmapImage(imageUri), Stretch = Stretch.UniformToFill} : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
