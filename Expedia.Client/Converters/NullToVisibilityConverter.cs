﻿using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
