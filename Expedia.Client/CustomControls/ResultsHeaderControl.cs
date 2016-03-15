using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Expedia.Client.CustomControls
{
    public partial class ResultsHeaderControl: Control
    {
		public ResultsHeaderControl()
		{
			this.DefaultStyleKey = typeof(ResultsHeaderControl);
		}

		public string ResultType
		{
			get { return (string)GetValue(ResultTypeProperty); }
			set { SetValue(ResultTypeProperty, value); }
		}
		public static readonly DependencyProperty ResultTypeProperty =
			DependencyProperty.Register("ResultType", typeof(string), typeof(ResultsHeaderControl), new PropertyMetadata(null));

		public string City
		{
			get { return (string)GetValue(CityProperty); }
			set { SetValue(CityProperty, value); }
		}
		public static readonly DependencyProperty CityProperty =
			DependencyProperty.Register("City", typeof(string), typeof(ResultsHeaderControl), new PropertyMetadata(null));

		public string DepartureDate
		{
			get { return (string)GetValue(DepartureDateProperty); }
			set { SetValue(DepartureDateProperty, value); }
		}
		public static readonly DependencyProperty DepartureDateProperty =
			DependencyProperty.Register("DepartureDate", typeof(string), typeof(ResultsHeaderControl), new PropertyMetadata(null));

		public string ArrivalDate
		{
			get { return (string)GetValue(ArrivalDateProperty); }
			set { SetValue(ArrivalDateProperty, value); }
		}
		public static readonly DependencyProperty ArrivalDateProperty =
			DependencyProperty.Register("ArrivalDate", typeof(string), typeof(ResultsHeaderControl), new PropertyMetadata(null));

		public int NumberOfPerson
		{
			get { return (int)GetValue(NumberOfPersonProperty); }
			set { SetValue(NumberOfPersonProperty, value); }
		}
		public static readonly DependencyProperty NumberOfPersonProperty =
			DependencyProperty.Register("NumberOfPerson", typeof(int), typeof(ResultsHeaderControl), new PropertyMetadata(null));

		public Visibility GuestVisibility
		{
			get { return (Visibility)GetValue(GuestVisibilityProperty); }
			set { SetValue(GuestVisibilityProperty, value); }
		}

		// Using a DependencyProperty as the backing store for GuestVisibility.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty GuestVisibilityProperty =
			DependencyProperty.Register("GuestVisibility", typeof(Visibility), typeof(ResultsHeaderControl), new PropertyMetadata(Visibility.Collapsed));

		public Visibility TravelerVisibility
		{
			get { return (Visibility)GetValue(TravelerVisibilityProperty); }
			set { SetValue(TravelerVisibilityProperty, value); }
		}

		// Using a DependencyProperty as the backing store for TravelerVisibility.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TravelerVisibilityProperty =
			DependencyProperty.Register("TravelerVisibility", typeof(Visibility), typeof(ResultsHeaderControl), new PropertyMetadata(Visibility.Collapsed));

        public Visibility PersonVisibility
        {
            get { return (Visibility)GetValue(PersonVisibilityProperty); }
            set { SetValue(PersonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty PersonVisibilityProperty = 
            DependencyProperty.Register("PersonVisibility", typeof (Visibility), typeof (ResultsHeaderControl), new PropertyMetadata(Visibility.Visible));
    }
}
