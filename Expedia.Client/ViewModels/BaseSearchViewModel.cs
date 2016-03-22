using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
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
    public abstract class BaseSearchViewModel : INotifyPropertyChanged
    {
        #region Properties

        public readonly ISuggestionService SuggestionService;
        public readonly ISettingsService SettingsService;
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
                UpdateMapVisibility(value);
                OnPropertyChanged("SelectedSearchSuggestion");
            }
        }

        private SuggestionResult _selectedSearchSuggestion2;
        public SuggestionResult SelectedSearchSuggestion2
        {
            get { return _selectedSearchSuggestion2; }
            set
            {
                _selectedSearchSuggestion2 = value;
                UpdateMapVisibility(value);
                OnPropertyChanged("SelectedSearchSuggestion2");
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

        private ObservableCollection<SuggestionResult> _searchSuggestions2;
        public ObservableCollection<SuggestionResult> SearchSuggestions2
        {
            get { return _searchSuggestions2; }
            set
            {
                _searchSuggestions2 = value;
                OnPropertyChanged("SearchSuggestions2");
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

        private bool _isSuggestionListOpen2;
        public bool IsSuggestionListOpen2
        {
            get { return _isSuggestionListOpen2; }
            set
            {
                _isSuggestionListOpen2 = value;
                OnPropertyChanged("IsSuggestionListOpen2");
            }
        }

        private bool _isGPSEnabled;
        public bool IsGPSEnabled
        {
            get { return _isGPSEnabled; }
            set
            {
                _isGPSEnabled = value;
                OnPropertyChanged("IsGPSEnabled");
            }
        }

        private bool _isMapVisible;
        public bool IsMapVisible
        {
            get { return _isMapVisible; }
            set
            {
                _isMapVisible = value;
                OnPropertyChanged("IsMapVisible");
            }
        }

        private Geoposition _mapCenter;
        public Geoposition MapCenter
        {
            get { return _mapCenter; }
            set
            {
                _mapCenter = value;
                OnPropertyChanged("MapCenter");
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

        protected BaseSearchViewModel(SuggestionLob lob)
        {
            SuggestionService = ExpediaKernel.Instance().Get<ISuggestionService>();
            SettingsService = ExpediaKernel.Instance().Get<ISettingsService>();
            Lob = lob;
            MapCenter = GeoLocationMemory.Instance().GetCurrentGeoposition();
            AdultCountSource = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6 };
            ChildCountSource = new ObservableCollection<int> { 0, 1, 2, 3, 4, 5 };
            AdultCount = 1;
            ChildCount = 0;
            ChildAges = new ObservableCollection<ChildAgeItem>();
            SetGPS();
        }

        private void SetGPS()
        {
            var status = SettingsService.GetUseLocationService();
            IsGPSEnabled = status == GeolocationAccessStatus.Allowed;
            IsMapVisible = IsGPSEnabled;
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

                SelectedSearchSuggestion = null;
            }
        }

        public async void GetNearbySuggestions2()
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

                SearchSuggestions2 = orderedSuggestions;

                SelectedSearchSuggestion2 = null;
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

            SelectedSearchSuggestion = null;
        }

        public async void GetTypeaheadSuggestions2(string inputQuery)
        {
            var results = await SuggestionService.Suggest(new CancellationToken(false), inputQuery, Lob);

            if (results == null)
                return;

            var orderedSuggestions = new ObservableCollection<SuggestionResult>();
            foreach (var suggestion in results.SortedSuggestionsList.SelectMany(suggestionList => suggestionList))
            {
                orderedSuggestions.Add(suggestion);
            }

            SearchSuggestions2 = orderedSuggestions;

            SelectedSearchSuggestion2 = null;
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

        public void SetSearchSuggestion2(SuggestionResult suggestionResult)
        {
            SelectedSearchSuggestion2 = suggestionResult;
            //var geoPoint = new BasicGeoposition { Latitude = double.Parse(suggestionResult.Coordinates.Latitude), Longitude = double.Parse(suggestionResult.Coordinates.Longitude) };
            //if (MapControl != null)
            //{
            //    MapControl.Center = new Geopoint(geoPoint);
            //}
            IsSuggestionListOpen2 = false;
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

        public void SuggestionTextChanged2(string input)
        {
            if (input.Length > 2)
            {
                if (SelectedSearchSuggestion2 == null || input != SelectedSearchSuggestion2.Display)
                    GetTypeaheadSuggestions2(input);
            }

            if (string.IsNullOrEmpty(input))
            {
                GetNearbySuggestions2();
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

        private void UpdateMapVisibility(SuggestionResult suggestionValue)
        {
            if (!IsGPSEnabled)
            {
                IsMapVisible = suggestionValue != null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
