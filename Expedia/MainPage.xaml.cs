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
using Expedia.Entities.User;
using Expedia.Injection;
using Expedia.Services;
using Expedia.Services.Interfaces;

namespace Expedia
{
    public sealed partial class MainPage : Page
    {
        public CancellationTokenSource CT { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            CT = new CancellationTokenSource();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //pos testing
            var posService = ExpediaKernel.Instance().Get<IPointOfSaleService>();
            var posResults = posService.GetPointsOfSale();
            var posResults2 = posService.SetCurrentPointOfSale(posResults.Result.First().CountryId); //PoS MUST be set before the other services can be called - have to get domain name for URI

            //testing hotels
            var hotelService = ExpediaKernel.Instance().Get<IHotelService>();
            var results = await hotelService.GetHotels(new HotelSearchQueryParameters
            { CheckInDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(1), City = "SFO", Room = new[] { 1 } }, CT.Token);

            var hotelCheck = results;

            //suggestion testing
            var suggestion = ExpediaKernel.Instance().Get<ISuggestionService>();
            var results2 = await suggestion.Suggest(new CancellationToken(false), "Detroit", SuggestionLob.HOTELS);

            var suggestCheck = results2;

            //nearby testing
            var results3 = await suggestion.Suggest(new CancellationToken(false), 40.440625, -79.995886, SuggestionLob.HOTELS);
            var nearbyCheck = results3;

            //auth testing
            var authservice = ExpediaKernel.Instance().Get<IAuthenticationService>();
            var authResult = await authservice.SignIn(new CancellationToken(false), "baclifton@gmail.com", "soapdish"); //kill this

           //account creation
            var createResult = await authservice.CreateAccount(new CancellationToken(false), new AccountCreationParams("baclifton@gmail.com","apassword","Test","Test",false));
            var createResult2 = createResult;

        }

        private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            CT.Cancel();
        }
    }
}
