using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Expedia.Client.CustomControls
{
    public class ArrivalPointControl : Control
    {
        public ArrivalPointControl()
        {
            this.DefaultStyleKey = typeof(ArrivalPointControl);
        }

        public string ArrivalAirport
        {
            get { return (string)GetValue(ArrivalAirportProperty); }
            set { SetValue(ArrivalAirportProperty, value); }
        }

        public static readonly DependencyProperty ArrivalAirportProperty =
            DependencyProperty.Register("ArrivalAirport", typeof(string), typeof(ArrivalPointControl), new PropertyMetadata(null));

    }
}
