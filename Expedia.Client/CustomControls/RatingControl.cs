using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Expedia.Client.CustomControls
{
    [TemplatePart(Name = StackPanelPartName, Type = typeof(StackPanel))]
    public class RatingControl : ContentControl
    {
        private const string StackPanelPartName = "PART_StackPanel";
        private StackPanel _stackPanel;

        public static readonly DependencyProperty MaxRatingValueProperty = DependencyProperty.Register("MaxRatingValue", typeof(int), typeof(RatingControl), new PropertyMetadata(0));

        public static readonly DependencyProperty RatingValueProperty = DependencyProperty.Register("RatingValue", typeof(double), typeof(RatingControl), new PropertyMetadata(0d, new PropertyChangedCallback(OnRatingValueChanged)));

        private static void OnRatingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var ratingCtrl = sender as RatingControl;

            if (ratingCtrl == null)
            {
                return;
            }

            ratingCtrl.UpdateControls();
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(RatingControl), new PropertyMetadata(Orientation.Horizontal));

        public static readonly DependencyProperty FullIconUriProperty = DependencyProperty.Register("FullIconUri", typeof(Uri), typeof(RatingControl), new PropertyMetadata(null));

        public static readonly DependencyProperty HalfIconUriProperty = DependencyProperty.Register("HalfIconUri", typeof(Uri), typeof(RatingControl), new PropertyMetadata(null));

        public static readonly DependencyProperty EmptyIconUriProperty = DependencyProperty.Register("EmptyIconUri", typeof(Uri), typeof(RatingControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ElementWidthProperty = DependencyProperty.Register("ElementWidth", typeof(double), typeof(RatingControl), new PropertyMetadata(0d));
        public static readonly DependencyProperty ElementHeightProperty = DependencyProperty.Register("ElementHeight", typeof(double), typeof(RatingControl), new PropertyMetadata(0d));

        public static readonly DependencyProperty InterElementMarginProperty = DependencyProperty.Register("InterElementMargin", typeof(double), typeof(RatingControl), new PropertyMetadata(0d));

        public RatingControl()
        {
            this.DefaultStyleKey = typeof(RatingControl);
        }

        private void UpdateControls()
        {
            if (_stackPanel == null)
            {
                return;
            }

            if (RatingValue.Equals(0))
            {
                return;
            }

            if (!(MaxRatingValue > 0))
            {
                throw new ArgumentException("MaxRatingValue needs a value to be able to build the rating control", "MaxRatingValue");
            }

            for (double i = 1; i <= MaxRatingValue; i++)
            {
                Uri content;

                var margin = new Thickness(0);
                var topBottomMargin = Orientation == Orientation.Vertical ? InterElementMargin : 0;
                var leftRightMargin = Orientation == Orientation.Horizontal ? InterElementMargin : 0;

                if (i < MaxRatingValue)
                {
                    margin = new Thickness(0, 0, leftRightMargin, topBottomMargin);
                }
                else
                {
                    margin = new Thickness(0);
                }


                if (RatingValue >= i)
                {
                    content = FullIconUri;
                }
                else
                {
                    var rest = i - RatingValue;
                    content = rest < 1 ? HalfIconUri : EmptyIconUri;
                }

                var image = this.GetImageControl(((int)i - 1), margin);

                image.Source = new BitmapImage(content);

            }
        }

        private Image GetImageControl(int index, Thickness margin)
        {
            if (_stackPanel.Children.Count >= index + 1)
            {
                return _stackPanel.Children[index] as Image;
            }

            var buildContentPresenter = new Image()
            {
                Stretch = Stretch.UniformToFill,
                Width = ElementWidth,
                Height = ElementHeight,
                Margin = margin
            };
            _stackPanel.Children.Add(buildContentPresenter);

            return buildContentPresenter;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _stackPanel = GetTemplateChild(StackPanelPartName) as StackPanel;
            Contract.Assert(_stackPanel != null, StackPanelPartName + " must be defined in the template");

            UpdateControls();
        }



        public int MaxRatingValue
        {
            get { return (int)GetValue(MaxRatingValueProperty); }
            set { SetValue(MaxRatingValueProperty, value); }
        }
        public double RatingValue
        {
            get { return (double)GetValue(RatingValueProperty); }
            set { SetValue(RatingValueProperty, value); }
        }
        public Orientation Orientation
        {
            get
            {
                if (OrientationProperty == null)
                {
                    return Orientation.Horizontal;
                }
                return (Orientation)GetValue(OrientationProperty);
            }
            set { SetValue(OrientationProperty, value); }
        }
        public Uri FullIconUri
        {
            get { return (Uri)GetValue(FullIconUriProperty); }
            set { SetValue(FullIconUriProperty, value); }
        }
        public Uri HalfIconUri
        {
            get { return (Uri)GetValue(HalfIconUriProperty); }
            set { SetValue(HalfIconUriProperty, value); }
        }
        public Uri EmptyIconUri
        {
            get { return (Uri)GetValue(EmptyIconUriProperty); }
            set { SetValue(EmptyIconUriProperty, value); }
        }

        public double ElementWidth
        {
            get { return (double)GetValue(ElementWidthProperty); }
            set { SetValue(ElementWidthProperty, value); }
        }

        public double ElementHeight
        {
            get { return (double)GetValue(ElementHeightProperty); }
            set { SetValue(ElementHeightProperty, value); }
        }

        public double InterElementMargin
        {
            get { return (double)GetValue(InterElementMarginProperty); }
            set { SetValue(InterElementMarginProperty, value); }
        }
    }
}
