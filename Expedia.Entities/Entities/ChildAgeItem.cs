using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Expedia.Entities.Entities
{
    public class ChildAgeItem
    {
        public int Rank { get; set; }
        public int Age { get; set; }
        public ObservableCollection<string> AgeRange { get; set; }

        public ChildAgeItem(int rank)
        {
            Rank = rank;
            Age = 0;
            AgeRange = new ObservableCollection<string> {"<1", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };
        }
    }
}
