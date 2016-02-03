using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Client.Interfaces;
using Expedia.Entities.Suggestions;

namespace Expedia.Client.ViewModels
{
    public class SearchFlightsViewModel : BaseViewModel, ISearchFlightsViewModel
    {
        public SearchFlightsViewModel() : base(SuggestionLob.FLIGHTS)
        {

        }
    }
}
