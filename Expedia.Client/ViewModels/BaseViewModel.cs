using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Utilities;
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


        public BaseViewModel(SuggestionLob lob)
        {
            SuggestionService = ExpediaKernel.Instance().Get<ISuggestionService>();
            Lob = lob;
        }

        public async void GetNearbySuggestions()
        {
            //after 3rd character, start polling the suggestion service + every new character change after that

            var location = GeoLocationMemory.Instance().GetCurrentGeoposition();
            var results = await SuggestionService.Suggest(new CancellationToken(false), location.Coordinate.Latitude, location.Coordinate.Longitude, Lob);

            var temp = new ObservableCollection<SuggestionResult>();
            foreach (var suggestionList in results.SortedSuggestionsList) //make the To observable collection extension
            {
                foreach (var suggestion in suggestionList)
                {
                    temp.Add(suggestion);
                }
            }

            SearchSuggestions = temp;
        }

        public async void GetTypeaheadSuggestions(string inputQuery)
        {
            var results = await SuggestionService.Suggest(new CancellationToken(false), inputQuery, Lob);

            var temp = new ObservableCollection<SuggestionResult>();
            foreach (var suggestionList in results.SortedSuggestionsList) //make the To observable collection extension
            {
                foreach (var suggestion in suggestionList)
                {
                    temp.Add(suggestion);
                }
            }

            SearchSuggestions = temp;
        }

        public void SetSearchSuggestion(SuggestionResult suggestionResult)
        {
            SelectedSearchSuggestion = suggestionResult;
            IsSuggestionListOpen = false;
        }

        public void SuggestionTextChanged(string input)
        {
            if (input.Length > 2)
            {
                if(SelectedSearchSuggestion == null || input != SelectedSearchSuggestion.Display)
                    GetTypeaheadSuggestions(input);
            }

            //if (string.IsNullOrEmpty(input))
            //{
            //    GetNearbySuggestions();
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
