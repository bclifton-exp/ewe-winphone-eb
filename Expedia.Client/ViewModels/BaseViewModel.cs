using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Expedia.Client.Utilities;
using Expedia.Entities.Entities;
using Expedia.Entities.Extensions;
using Expedia.Entities.Suggestions;
using Expedia.Injection;
using Expedia.Services.Interfaces;

namespace Expedia.Client.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Properties

        public readonly ISuggestionService SuggestionService;
        private SuggestionLob Lob { get; set; }

        private ObservableCollection<int> _adultCountSource;
        public ObservableCollection<int> AdultCountSource
        {
            get { return _adultCountSource; }
            set
            {
                _adultCountSource = value;
                OnPropertyChanged("AdultCountSource");
            }
        }

        private ObservableCollection<int> _childCountSource;
        public ObservableCollection<int> ChildCountSource
        {
            get { return _childCountSource; }
            set
            {
                _childCountSource = value;
                OnPropertyChanged("ChildCountSource");
            }
        }

        private int _adultCount;
        public int AdultCount
        {
            get { return _adultCount; }
            set
            {
                _adultCount = value;
                OnPropertyChanged("AdultCount");
            }
        }

        private int _childCount;
        public int ChildCount
        {
            get { return _childCount; }
            set
            {
                _childCount = value;
                BuildChildAgeControls(value);
                OnPropertyChanged("ChildCount");
            }
        }

        private ObservableCollection<ChildAgeItem> _childAges;
        public ObservableCollection<ChildAgeItem> ChildAges
        {
            get { return _childAges; }
            set
            {
                _childAges = value;
                OnPropertyChanged("ChildAges");
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

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

        private MapControl _mapControl;
        public MapControl MapControl
        {
            get { return _mapControl; }
            set
            {
                _mapControl = value;
                OnPropertyChanged("MapControl");
            }
        }
        #endregion

        public BaseViewModel(SuggestionLob lob)
        {
            SuggestionService = ExpediaKernel.Instance().Get<ISuggestionService>();
            Lob = lob;
            AdultCountSource = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6 };
            ChildCountSource = new ObservableCollection<int> { 0, 1, 2, 3, 4, 5 };
            AdultCount = 1;
            ChildCount = 0;
            ChildAges = new ObservableCollection<ChildAgeItem>();
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

            if (results == null)
                return;

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
            var geoPoint = new BasicGeoposition {Latitude = double.Parse(suggestionResult.Coordinates.Latitude), Longitude = double.Parse(suggestionResult.Coordinates.Longitude)};
            if (MapControl != null)
            {
                MapControl.Center = new Geopoint(geoPoint);
            }
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

        private void BuildChildAgeControls(int childCount)
        {
            if (ChildAges != null)
            {
                if (ChildAges.Count > childCount)
                {
                    while (ChildAges.Count > childCount)
                    {
                        ChildAges.Remove(ChildAges.Last());
                    }
                }

                if (ChildAges.Count < childCount)
                {
                    while (ChildAges.Count < childCount)
                    {
                        ChildAges.Add(ChildAges.Count == 0
                            ? new ChildAgeItem(1)
                            : new ChildAgeItem(ChildAges.IndexOf(ChildAges.Last()) + 2));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
