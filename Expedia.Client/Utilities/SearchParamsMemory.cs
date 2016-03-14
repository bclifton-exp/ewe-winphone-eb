using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Expedia.Entities.Hotels;
using Expedia.Entities.Suggestions;

namespace Expedia.Client.Utilities
{
    public class SearchParamsMemory
    {
        private static SearchParamsMemory _instance;
        public SearchHotelsLocalParameters HotelParams;

        public static SearchParamsMemory Instance()
        {
            return _instance ?? (_instance = new SearchParamsMemory());
        }
    }
}
