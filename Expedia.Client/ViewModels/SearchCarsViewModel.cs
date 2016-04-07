using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Client.Interfaces;
using Expedia.Entities.Suggestions;

namespace Expedia.Client.ViewModels
{
    public class SearchCarsViewModel : BaseSearchViewModel, ISearchCarsViewModel
    {
        public SearchCarsViewModel() : base(SuggestionLob.CARS)
        {
        }
    }
}
