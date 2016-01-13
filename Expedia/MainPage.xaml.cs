using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Contracts;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Hotels;
using Expedia.Entities.Suggestions;
using Expedia.Injection;
using Expedia.Services;
using Expedia.Services.Interfaces;

namespace Expedia
{
    public sealed partial class MainPage : Page
    {
        public CancellationTokenSource CT { get; set; }
        public IHotelService HotelService { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            CT = new CancellationTokenSource();
            HotelService = ExpediaKernel.Instance().Get<IHotelService>();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           //testing hotels

            var results = await HotelService.GetHotels(new HotelSearchQueryParameters
            { CheckInDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(1), City = "SFO", Room = new[] { 1 } }, CT.Token);

            var hotelCheck = results;

            //suggestion testing
            var suggestion = ExpediaKernel.Instance().Get<ISuggestionService>();
            var results2 = await suggestion.Suggest(new CancellationToken(false), "Detroit", SuggestionLob.HOTELS);

            var suggestCheck = results2;

            //nearby testing
            var results3 = await suggestion.Suggest(new CancellationToken(false), 40.440625, -79.995886, SuggestionLob.HOTELS);
            var nearbyCheck = results3;

        }

        private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            CT.Cancel();
        }
    }
}
