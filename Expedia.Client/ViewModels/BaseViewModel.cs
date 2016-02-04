using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Bing.Maps;
using Expedia.Client.Utilities;
using Expedia.Entities.Extensions;
using Expedia.Entities.Suggestions;
using Expedia.Injection;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public readonly ISuggestionService SuggestionService;
        private SuggestionLob Lob { get; set; }

        private SuggestionResult _selectedSearchSuggestion;
        public SuggestionResult SelectedSearchSuggestion
        {
            get { return _selectedSearchSuggestion; }
            set
            {
                _selectedSearchSuggestion = value;
                OnPropertyChanged("SelectedSearchSuggestion");
            }
        }

        private ObservableCollection<SuggestionResult> _searchSuggestions;
        public ObservableCollection<SuggestionResult> SearchSuggestions
        {
            get { return _searchSuggestions; }
            set
            {
                _searchSuggestions = value;
                OnPropertyChanged("SearchSuggestions");
            }
        }

        private bool _isSuggestionListOpen;
        public bool IsSuggestionListOpen
        {
            get { return _isSuggestionListOpen; }
            set
            {
                _isSuggestionListOpen = value;
                OnPropertyChanged("IsSuggestionListOpen");
            }
        }

        private Map _mapControl;
        public Map MapControl
        {
            get { return _mapControl; }
            set
            {
                _mapControl = value;
                OnPropertyChanged("MapControl");
            }
        }


        public BaseViewModel(SuggestionLob lob)
        {
            SuggestionService = ExpediaKernel.Instance().Get<ISuggestionService>();
            Lob = lob;
        }

        public async void GetNearbySuggestions()
        {
            var location = GeoLocationMemory.Instance().GetCurrentGeoposition();
            if (location != null)
            {
                var results = await SuggestionService.Suggest(new CancellationToken(false), location.Coordinate.Point.Position.Latitude, location.Coordinate.Point.Position.Longitude, Lob);

                var orderedSuggestions = new ObservableCollection<SuggestionResult>();
                foreach (var suggestion in results.SortedSuggestionsList.SelectMany(suggestionList => suggestionList))
                {
                    orderedSuggestions.Add(suggestion);
                }

                SearchSuggestions = orderedSuggestions;
            }
        }

        public async void GetTypeaheadSuggestions(string inputQuery)
        {
            var results = await SuggestionService.Suggest(new CancellationToken(false), inputQuery, Lob);

            var orderedSuggestions = new ObservableCollection<SuggestionResult>();
            foreach (var suggestion in results.SortedSuggestionsList.SelectMany(suggestionList => suggestionList))
            {
                orderedSuggestions.Add(suggestion);
            }

            SearchSuggestions = orderedSuggestions;
        }

        public void SetSearchSuggestion(SuggestionResult suggestionResult)
        {
            SelectedSearchSuggestion = suggestionResult;
            MapControl.Center = new Location(double.Parse(suggestionResult.Coordinates.Latitude),double.Parse(suggestionResult.Coordinates.Longitude));
            IsSuggestionListOpen = false;
        }

        public void SuggestionTextChanged(string input)
        {
            if (input.Length > 2)
            {
                if(SelectedSearchSuggestion == null || input != SelectedSearchSuggestion.Display)
                    GetTypeaheadSuggestions(input);
            }

            if (string.IsNullOrEmpty(input))
            {
                GetNearbySuggestions();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
