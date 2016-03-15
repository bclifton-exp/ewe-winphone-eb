using System;
using System.Collections;
using System.Globalization;
using System.Linq;

#if NETFX_CORE
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace Expedia.Client.CustomControls
{
	[TemplatePart(Name = TextBlockPartName, Type = typeof(TextBlock))]
	public class PluralityChooser : Control
	{
		private const string TextBlockPartName = "PART_TextBlock";

		private static readonly Thickness DefaultThickness = new Thickness(double.MinValue + 1);
		private const double DefaultFontSize = -1;
		private static readonly FontWeight DefaultFontWeight = FontWeights.Normal;
		private static readonly Brush DefaultBrush = new SolidColorBrush(Color.FromArgb(1, 1, 1, 1));

		private TextBlock _textBlock;

		#region Dependency Properties

		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(object), typeof(PluralityChooser), new PropertyMetadata(null, Refresh));

		public static readonly DependencyProperty SingularValueProperty =
			DependencyProperty.Register("SingularValue", typeof(string), typeof(PluralityChooser), new PropertyMetadata(string.Empty, Refresh));

		public static readonly DependencyProperty PluralValueProperty =
			DependencyProperty.Register("PluralValue", typeof(string), typeof(PluralityChooser), new PropertyMetadata(string.Empty, Refresh));

		public static readonly DependencyProperty ZeroValueProperty =
			DependencyProperty.Register("ZeroValue", typeof(string), typeof(PluralityChooser), new PropertyMetadata(string.Empty, Refresh));

		public static readonly DependencyProperty TextBlockStyleProperty =
			DependencyProperty.Register("TextBlockStyle", typeof(Style), typeof(PluralityChooser), new PropertyMetadata(null, Refresh));

		public static readonly DependencyProperty TextWrappingProperty =
			DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(PluralityChooser), new PropertyMetadata(TextWrapping.NoWrap, Refresh));

		public static readonly DependencyProperty TextBlockMarginProperty =
			DependencyProperty.Register("TextBlockMargin", typeof(Thickness), typeof(PluralityChooser), new PropertyMetadata(DefaultThickness, Refresh));

		public static readonly DependencyProperty TextBlockFontSizeProperty =
			DependencyProperty.Register("TextBlockFontSize", typeof(double), typeof(PluralityChooser), new PropertyMetadata(DefaultFontSize, Refresh));

		public static readonly DependencyProperty TextBlockFontWeightProperty =
			DependencyProperty.Register("TextBlockFontWeight", typeof(FontWeight), typeof(PluralityChooser), new PropertyMetadata(DefaultFontWeight, Refresh));

		public static readonly DependencyProperty TextBlockForegroundProperty =
			DependencyProperty.Register("TextBlockForeground", typeof(Brush), typeof(PluralityChooser), new PropertyMetadata(DefaultBrush, Refresh));

		public static readonly DependencyProperty TextBlockPaddingProperty =
			DependencyProperty.Register("TextBlockPadding", typeof(Thickness), typeof(PluralityChooser), new PropertyMetadata(DefaultThickness, Refresh));

		public static readonly DependencyProperty TextTrimmingProperty =
			DependencyProperty.Register("TextTrimming", typeof(TextTrimming), typeof(PluralityChooser), new PropertyMetadata(TextTrimming.None));

		public static void Refresh(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PluralityChooser)d).Refresh();
		}

		public object Source
		{
			get { return GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		public string SingularValue
		{
			get { return GetValue(SingularValueProperty) as string; }
			set { SetValue(SingularValueProperty, value); }
		}

		/// <summary>
		/// You can put a {0} as a placeholder for the count
		/// </summary>
		public string PluralValue
		{
			get { return GetValue(PluralValueProperty) as string; }
			set { SetValue(PluralValueProperty, value); }
		}

		public string ZeroValue
		{
			get { return GetValue(ZeroValueProperty) as string; }
			set { SetValue(ZeroValueProperty, value); }
		}

		public Style TextBlockStyle
		{
			get { return GetValue(TextBlockStyleProperty) as Style; }
			set { SetValue(TextBlockStyleProperty, value); }
		}

		public TextWrapping TextWrapping
		{
			get { return (TextWrapping)GetValue(TextWrappingProperty); }
			set { SetValue(TextWrappingProperty, value); }
		}

		public double TextBlockFontSize
		{
			get { return (double)GetValue(TextBlockFontSizeProperty); }
			set { SetValue(TextBlockFontSizeProperty, value); }
		}

		public FontWeight TextBlockFontWeight
		{
			get { return (FontWeight)GetValue(TextBlockFontWeightProperty); }
			set { SetValue(TextBlockFontWeightProperty, value); }
		}

		public Brush TextBlockForeground
		{
			get { return (Brush)GetValue(TextBlockForegroundProperty); }
			set { SetValue(TextBlockForegroundProperty, value); }
		}

		public Thickness TextBlockMargin
		{
			get { return (Thickness)GetValue(TextBlockMarginProperty); }
			set { SetValue(TextBlockMarginProperty, value); }
		}

		public Thickness TextBlockPadding
		{
			get { return (Thickness)GetValue(TextBlockPaddingProperty); }
			set { SetValue(TextBlockPaddingProperty, value); }
		}

		public TextTrimming TextTrimming
		{
			get { return (TextTrimming)GetValue(TextTrimmingProperty); }
			set { SetValue(TextTrimmingProperty, value); }
		}

		#endregion

		public PluralityChooser()
		{
			DefaultStyleKey = typeof(PluralityChooser);
		}

#if NETFX_CORE
		protected override void OnApplyTemplate()
#else
		public override void OnApplyTemplate()
#endif
		{
			base.OnApplyTemplate();

			_textBlock = GetTemplateChild(TextBlockPartName) as TextBlock ?? new TextBlock();

			Refresh();
		}

		private void Refresh()
		{
			if (_textBlock == null)
			{
				return; // template not initialized
			}

			var count = GetCount();

			if (count == 0)
			{
				_textBlock.Text = ZeroValue;
			}
			else if (count == 1)
			{
				_textBlock.Text = SingularValue;
			}
			else
			{
				_textBlock.Text = string.Format(CultureInfo.CurrentCulture, PluralValue, count);
			}

			if (TextBlockMargin != DefaultThickness)
				_textBlock.Margin = TextBlockMargin;

			if (TextBlockFontSize != DefaultFontSize)
				_textBlock.FontSize = TextBlockFontSize;

#if NETFX_CORE
			if (TextBlockFontWeight.Weight != DefaultFontWeight.Weight)
				_textBlock.FontWeight = TextBlockFontWeight;
#else
			if (TextBlockFontWeight != DefaultFontWeight)
				_textBlock.FontWeight = TextBlockFontWeight;
#endif

			if (TextBlockForeground != DefaultBrush)
				_textBlock.Foreground = TextBlockForeground;

			if (TextBlockPadding != DefaultThickness)
				_textBlock.Padding = TextBlockPadding;
		}

		private long GetCount()
		{
			var source = Source;
			if (source == null)
			{
				return 0;
			}

#if NETFX_CORE
			try
			{
				return Convert.ToInt64(source);
			}
			catch
			{
				// Could not convert, ignore it
			}
#else
			var convertible = source as IConvertible;
			if (convertible != null)
			{
				return convertible.ToInt64(CultureInfo.InvariantCulture);
			}
#endif
			var enumerable = source as IEnumerable;
			return enumerable != null ? enumerable.Cast<object>().Count() : 0;
		}
	}
}