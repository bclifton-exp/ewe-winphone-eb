using System;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Suggestions;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class SearchFlightsView : Page
    {
        public SearchFlightsView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<ISearchFlightsViewModel>();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var context = DataContext as SearchFlightsViewModel;
            context.SetExistingTripSearchDetails();
        }

        private void DepartureSuggestBox_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var context = DataContext as SearchFlightsViewModel;
            context.SetSearchSuggestion(args.SelectedItem as SuggestionResult);
            context.BuildDeparturePushPin(args.SelectedItem as SuggestionResult);
        }

        private void ArrivalSuggestBox_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var context = DataContext as SearchFlightsViewModel;
            context.SetSearchSuggestion2(args.SelectedItem as SuggestionResult);
            context.BuildArrivalPushPin(args.SelectedItem as SuggestionResult);
        }

        private void DepartureSuggestBox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var context = DataContext as SearchFlightsViewModel;
            context.SuggestionTextChanged(sender.Text);
        }

        private void ArrivalSuggestBox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var context = DataContext as SearchFlightsViewModel;
            context.SuggestionTextChanged2(sender.Text);
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as AutoSuggestBox;
            box.IsSuggestionListOpen = true;
        }

        private void Map_OnLoading(FrameworkElement sender, object args)
        {
            var context = DataContext as SearchFlightsViewModel;
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
                    if (context.SelectedSearchSuggestion != null)
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
                        var geoPoint = new BasicGeoposition
                        {
                            Latitude = double.Parse(context.SelectedSearchSuggestion2.Coordinates.Latitude),
                            Longitude = double.Parse(context.SelectedSearchSuggestion2.Coordinates.Longitude)
                        };

                        map.Center = new Geopoint(geoPoint);
                    }
                }
                else
                {
                    map.Center = new Geopoint(new BasicGeoposition());
                }
            }

            if (context.IsGPSEnabled == false)
            {
                if (context.SelectedSearchSuggestion != null)
                {
                    context.BuildDeparturePushPin(context.SelectedSearchSuggestion);
                }
                if (context.SelectedSearchSuggestion2 != null)
                {
                    context.BuildArrivalPushPin(context.SelectedSearchSuggestion2);
                }
            }
        }

        //CalendarDatePicker is super broken so all of this nasty code is necessary until msoft fixes it.
        private void CalendarDatePicker_OnStartDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            var context = DataContext as SearchFlightsViewModel;
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
            var context = DataContext as SearchFlightsViewModel;
            if (sender.Date != null)
                context.EndDate = sender.Date.Value.Date;
        }

        private void StartDateLoaded(object sender, RoutedEventArgs e)
        {
            var calendar = sender as CalendarDatePicker;
            var context = DataContext as SearchFlightsViewModel;
            calendar.Date = DateTime.Now;
            calendar.MinDate = context.MinimumDate;
            calendar.MaxDate = context.MaximumDate;
        }

        private void EndDateLoaded(object sender, RoutedEventArgs e)
        {
            var calendar = sender as CalendarDatePicker;
            var context = DataContext as SearchFlightsViewModel;
            calendar.Date = DateTime.Now.AddDays(1);
            calendar.MinDate = context.MinimumDate;
            calendar.MaxDate = context.MaximumDate;
        }
    }
}
