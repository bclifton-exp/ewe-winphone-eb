using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Expedia.Client.CustomControls
{
	[TemplateVisualState(GroupName = ExpandingStatesName, Name = CollapsedVisualStateName)]
	[TemplateVisualState(GroupName = ExpandingStatesName, Name = ExpandedVisualStateName)]
	[TemplatePart(Name = MainPresenterPartName, Type = typeof(ContentPresenter))]
	[TemplatePart(Name = ExpandablePresenterPartName, Type = typeof(ContentPresenter))]
	[TemplatePart(Name = ExpandButtonPartName, Type = typeof(ToggleButton))]
	[TemplatePart(Name = OutsideAreaPartName, Type = typeof(UIElement))]
    public class ExpandableContentControl : ContentControl
	{
		private const string ExpandingStatesName = "ExpandingStates";
		private const string ExpandedVisualStateName = "Expanded";
		private const string CollapsedVisualStateName = "Collapsed";

		private const string MainPresenterPartName = "PART_MainPresenter";
		private const string ExpandablePresenterPartName = "PART_ExpandablePresenter";
		private const string ExpandButtonPartName = "PART_ExpandButton";
		private const string OutsideAreaPartName = "PART_OutsideArea";

		public ExpandableContentControl()
		{
			this.DefaultStyleKey = typeof(ExpandableContentControl);
		}

        public string DailyPrice
        {
            get { return (string)GetValue(DailyPriceProperty); }
            set { SetValue(DailyPriceProperty, value); }
        }

        public static readonly DependencyProperty DailyPriceProperty =
            DependencyProperty.Register("DailyPrice", typeof(string), typeof(ExpandableContentControl), new PropertyMetadata(null));

        public string DailyText
        {
            get { return (string)GetValue(DailyTextProperty); }
            set { SetValue(DailyTextProperty, value); }
        }

        public static readonly DependencyProperty DailyTextProperty =
            DependencyProperty.Register("DailyText", typeof(string), typeof(ExpandableContentControl), new PropertyMetadata(null));

	    public static readonly DependencyProperty VendorProperty = DependencyProperty.Register(
	        "Vendor", typeof (string), typeof (ExpandableContentControl), new PropertyMetadata(default(string)));

	    public string Vendor
	    {
	        get { return (string) GetValue(VendorProperty); }
	        set { SetValue(VendorProperty, value); }
	    }

	    public static readonly DependencyProperty MakeProperty = DependencyProperty.Register(
	        "Make", typeof (string), typeof (ExpandableContentControl), new PropertyMetadata(default(string)));

	    public string Make
	    {
	        get { return (string) GetValue(MakeProperty); }
	        set { SetValue(MakeProperty, value); }
	    }

	    public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
	        "Location", typeof (string), typeof (ExpandableContentControl), new PropertyMetadata(default(string)));

	    public string Location
	    {
	        get { return (string) GetValue(LocationProperty); }
	        set { SetValue(LocationProperty, value); }
	    }

	    public static readonly DependencyProperty OrSimilarTextProperty = DependencyProperty.Register(
	        "OrSimilarText", typeof (string), typeof (ExpandableContentControl), new PropertyMetadata(default(string)));

	    public string OrSimilarText
        {
	        get { return (string) GetValue(OrSimilarTextProperty); }
	        set { SetValue(OrSimilarTextProperty, value); }
	    }

        public bool IsExpanded
		{
			get { return (bool)GetValue(IsExpandedProperty); }
			set { SetValue(IsExpandedProperty, value); }
		}

		public static readonly DependencyProperty IsExpandedProperty =
			DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ExpandableContentControl), new PropertyMetadata(false, OnIsExpandedChanged));

		private static void OnIsExpandedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			VisualStateManager.GoToState(obj as Control, (bool)args.NewValue ? ExpandedVisualStateName : CollapsedVisualStateName, true);
		}


		#region ExpandableContent PROPERTY

		public object ExpandableContent
		{
			get { return (object)GetValue(ExpandableContentProperty); }
			set { SetValue(ExpandableContentProperty, value); }
		}

		public static readonly DependencyProperty ExpandableContentProperty =
			DependencyProperty.Register("ExpandableContent", typeof(object), typeof(ExpandableContentControl), new PropertyMetadata(null));

		#endregion

		#region ExpandableContentTemplate PROPERTY

		public DataTemplate ExpandableContentTemplate
		{
			get { return (DataTemplate)GetValue(ExpandableContentTemplateProperty); }
			set { SetValue(ExpandableContentTemplateProperty, value); }
		}

		public static readonly DependencyProperty ExpandableContentTemplateProperty =
			DependencyProperty.Register("ExpandableContentTemplate", typeof(DataTemplate), typeof(ExpandableContentControl), new PropertyMetadata(null));

		#endregion

		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			var outside = this.GetTemplateChild(OutsideAreaPartName) as UIElement;
			var expandButton = this.GetTemplateChild(ExpandButtonPartName) as ToggleButton;

			if (outside != null)
			{
				outside.Tapped += OnOutsideTapped;
			}
		}

		private void OnOutsideTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
		{
			e.Handled = true;
			this.IsExpanded = false;
		}
	}
}
