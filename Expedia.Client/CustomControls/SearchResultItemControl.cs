using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Expedia.Client.CustomControls
{
    public class SearchResultItemControl : Control
    {
        public SearchResultItemControl()
        {
            this.DefaultStyleKey = typeof(SearchResultItemControl);
        }

        public string AirlineName
        {
            get { return (string)GetValue(AirlineNameProperty); }
            set { SetValue(AirlineNameProperty, value); }
        }

        public static readonly DependencyProperty AirlineNameProperty =
            DependencyProperty.Register("AirlineName", typeof(string), typeof(SearchResultItemControl), new PropertyMetadata(null));

        public string TicketPrice
        {
            get { return (string)GetValue(TicketPriceProperty); }
            set { SetValue(TicketPriceProperty, value); }
        }

        public static readonly DependencyProperty TicketPriceProperty =
            DependencyProperty.Register("TicketPrice", typeof(string), typeof(SearchResultItemControl), new PropertyMetadata(null));

        public string DepartTime
        {
            get { return (string)GetValue(DepartTimeProperty); }
            set { SetValue(DepartTimeProperty, value); }
        }

        public static readonly DependencyProperty DepartTimeProperty =
            DependencyProperty.Register("DepartTime", typeof(string), typeof(SearchResultItemControl), new PropertyMetadata(null));

        public List<string> ListOfSegments
        {
            get { return (List<string>)GetValue(ListOfSegmentsProperty); }
            set { SetValue(ListOfSegmentsProperty, value); }
        }

        public static readonly DependencyProperty ListOfSegmentsProperty =
            DependencyProperty.Register("ListOfSegments", typeof(List<string>), typeof(SearchResultItemControl), new PropertyMetadata(null));

        public string ArrivalTime
        {
            get { return (string)GetValue(ArrivalTimeProperty); }
            set { SetValue(ArrivalTimeProperty, value); }
        }

        public static readonly DependencyProperty ArrivalTimeProperty =
            DependencyProperty.Register("ArrivalTime", typeof(string), typeof(SearchResultItemControl), new PropertyMetadata(null));

        public int NumberOfDays
        {
            get { return (int)GetValue(NumberOfDaysProperty); }
            set { SetValue(NumberOfDaysProperty, value); }
        }

        public static readonly DependencyProperty NumberOfDaysProperty =
            DependencyProperty.Register("NumberOfDays", typeof(int), typeof(SearchResultItemControl), new PropertyMetadata(null));

        public string FlightDuration
        {
            get { return (string)GetValue(FlightDurationProperty); }
            set { SetValue(FlightDurationProperty, value); }
        }

        public static readonly DependencyProperty FlightDurationProperty =
            DependencyProperty.Register("FlightDuration", typeof(string), typeof(SearchResultItemControl), new PropertyMetadata(null));

        public bool IsDayVisible
        {
            get { return (bool)GetValue(IsDayVisibleProperty); }
            set { SetValue(IsDayVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsDayVisibleProperty =
            DependencyProperty.Register("IsDayVisible", typeof(bool), typeof(SearchResultItemControl), new PropertyMetadata(false));

    }
}
