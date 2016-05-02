using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.Utilities;
using Expedia.Client.ViewModels;
using Expedia.Entities.Suggestions;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class SearchCarsView : Page
    {
        public SearchCarsView()
        {
            this.InitializeComponent();
            this.DataContext = ExpediaKernel.Instance().Get<ISearchCarsViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PhoneUIHelper.Instance().ShowBurgerMenu();
            var context = DataContext as SearchCarsViewModel;
            context.SetExistingTripSearchDetails();
        }

        private void AutoSuggestBox_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var context = DataContext as SearchCarsViewModel;
            context.SetSearchSuggestion(args.SelectedItem as SuggestionResult);
        }

        private void AutoSuggestBox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var context = DataContext as SearchCarsViewModel;
            context.SuggestionTextChanged(sender.Text);
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as AutoSuggestBox;
            box.IsSuggestionListOpen = true;
        }

        private void Map_OnLoading(FrameworkElement sender, object args)
        {
            var context = DataContext as SearchCarsViewModel;
            var map = sender as MapControl;
            context.MapControl = map;
            if (context.MapCenter != null)
            {
                var geoPoint = new BasicGeoposition
                {
                    Latitude = context.MapCenter.Coordinate.Point.Position.Latitude,
                    Longitude = context.MapCenter.Coordinate.Point.Position.Longitude
                };
                map.Center = new Geopoint(geoPoint);
            }
            else
            {
                if (!context.IsGPSEnabled)
                {
                    var geoPoint = new BasicGeoposition
                    {
                        Latitude = double.Parse(context.SelectedSearchSuggestion.Coordinates.Latitude),
                        Longitude = double.Parse(context.SelectedSearchSuggestion.Coordinates.Longitude)
                    };

                    map.Center = new Geopoint(geoPoint);
                }
                else
                {
                    map.Center = new Geopoint(new BasicGeoposition());
                }
            }
        }

        //CalendarDatePicker is super broken so all of this nasty code is necessary until msoft fixes it.
        private void CalendarDatePicker_OnStartDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var context = DataContext as SearchCarsViewModel;
            if (sender.Date != null)
            {
                context.StartDate = sender.Date.Value.Date;

                if (context.StartDate > context.EndDate)
                {
                    context.EndDate = context.StartDate.AddDays(1);
                    EndDatePicker.Date = context.EndDate;
                }
            }
        }

        private void CalendarDatePicker_OnEndDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var context = DataContext as SearchCarsViewModel;
            if (sender.Date != null)
                context.EndDate = sender.Date.Value.Date;
        }

        private void StartDateLoaded(object sender, RoutedEventArgs e)
        {
            var calendar = sender as CalendarDatePicker;
            var context = DataContext as SearchCarsViewModel;
            calendar.Date = DateTime.Now;
            calendar.MinDate = context.MinimumDate;
            calendar.MaxDate = context.MaximumDate;
        }

        private void EndDateLoaded(object sender, RoutedEventArgs e)
        {
            var calendar = sender as CalendarDatePicker;
            var context = DataContext as SearchCarsViewModel;
            calendar.Date = DateTime.Now.AddDays(1);
            calendar.MinDate = context.MinimumDate;
            calendar.MaxDate = context.MaximumDate;
        }
    }
}
