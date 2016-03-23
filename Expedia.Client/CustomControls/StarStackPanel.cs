using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Expedia.Client.CustomControls
{
	public class StarStackPanel : Panel
	{
		private static void AddToTotalSize(Orientation orientation, Size desiredSize, ref Size totalSize)
		{
			if (orientation == Orientation.Vertical)
			{
				totalSize.Height += desiredSize.Height;
				totalSize.Width = Math.Max(desiredSize.Width, totalSize.Width);
			}
			else
			{
				totalSize.Height = Math.Max(desiredSize.Height, totalSize.Height);
				totalSize.Width += desiredSize.Width;
			}
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			var orientation = Orientation;
			var hvsize = 0d;
			var starTotal = 0d;
			var arrangeRecords = ComputeKnownSizeForArrange(orientation, ref finalSize, ref hvsize, ref starTotal);

			if (orientation == Orientation.Vertical)
				hvsize = finalSize.Height - hvsize;
			else
				hvsize = finalSize.Width - hvsize;

			if (hvsize <= 0)
			{
				starTotal = 0;
			}
			else if (starTotal != 0)
			{
				starTotal = hvsize/starTotal;
			}

			return ArrangeChildren(arrangeRecords, orientation, finalSize, starTotal);
		}

		private static Size ArrangeChildren(IEnumerable<Record> arrangeRecords, Orientation orientation, Size finalSize, double starTotal)
		{
			var hvsize = 0d;

			foreach (var record in arrangeRecords)
			{
				var size = record.Size;

				if (record.SizeHint.IsStar)
				{
					var portion = record.SizeHint.Value * starTotal;
					if (orientation == Orientation.Vertical)
					{
						size.Width = finalSize.Width;
						size.Height = portion;
					}
					else
					{
						size.Width = portion;
						size.Height = finalSize.Height;
					}
				}

				var rect = new Rect(new Point(), size);
				if (orientation == Orientation.Vertical)
				{
					rect.Y = hvsize;
					hvsize += size.Height;
				}
				else
				{
					rect.X = hvsize;
					hvsize += size.Width;
				}

				record.Child.Arrange(rect);
			}

			if (orientation == Orientation.Vertical)
				finalSize.Height = hvsize;
			else
				finalSize.Width = hvsize;

			return finalSize;
		}

		private IEnumerable<Record> ComputeKnownSizeForArrange(Orientation orientation, ref Size finalSize, ref double hvsize, ref double starTotal)
		{
			var children = Children;
			var array = new Record[children.Count];

			for (var i = 0; i < array.Length; i++)
			{
				var child = children[i];
				var sizeHint = GetSizeInternal(child);
				var size = new Size();

				if (sizeHint.IsStar)
				{
					starTotal += sizeHint.Value;
				}
				else if (sizeHint.IsAuto)
				{
					if (orientation == Orientation.Vertical)
					{
						size.Width = finalSize.Width;
						hvsize += size.Height = Math.Min(child.DesiredSize.Height, Math.Max(finalSize.Height - hvsize, 0));
					}
					else
					{
						hvsize += size.Width = Math.Min(child.DesiredSize.Width, Math.Max(finalSize.Width - hvsize, 0));
						size.Height = finalSize.Height;
					}
				}
				else if (sizeHint.IsAbsolute)
				{
					if (orientation == Orientation.Vertical)
					{
						size.Width = finalSize.Width;
						hvsize += size.Height = sizeHint.Value;
					}
					else
					{
						hvsize += size.Width = sizeHint.Value;
						size.Height = finalSize.Height;
					}
				}

				array[i] = new Record
				{
					Child = child,
					Size = size,
					SizeHint = sizeHint,
				};
			}
			return array;
		}

		private static void InvalidateLayoutOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var panel = d as StarStackPanel;
			if (panel == null)
				return;

			panel.InvalidateMeasure();
			panel.InvalidateArrange();
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			var orientation = Orientation;
			var totalSize = new Size();
			var starTotal = 0d;

			var children = Children
				.Cast<UIElement>()
				.ToArray()
			;
			
			Array.Sort(children, (x, y) => GetPriority(x) - GetPriority(y));

			foreach(var child in children)
			{
				var sizeHint = GetSizeInternal(child);

				if (sizeHint.IsStar)
				{
					starTotal += sizeHint.Value;
				}
				else if (sizeHint.IsAuto)
				{
					MesureChildAuto(child, orientation, availableSize, ref totalSize);
				}
				else if (sizeHint.IsAbsolute)
				{
					MesureChildAbsolute(child, orientation, availableSize, sizeHint, ref totalSize);
				}
			}

			if (orientation == Orientation.Vertical)
			{
				availableSize.Height = Math.Max(availableSize.Height - totalSize.Height, 0);
			}
			else
			{
				availableSize.Width = Math.Max(availableSize.Width - totalSize.Width, 0);
			}

			if (starTotal != 0)
			{
				foreach (UIElement child in children)
				{
					var sizeHint = GetSizeInternal(child);
					if (sizeHint.IsStar)
					{
						MesureChildStar(child, orientation, availableSize, sizeHint, starTotal, ref totalSize);
					}
				}
			}

			return totalSize;
		}

		private static void MesureChildAbsolute(UIElement child, Orientation orientation, Size availableSize, GridLength sizeHint, ref Size totalSize)
		{
			if (orientation == Orientation.Vertical)
				availableSize.Height = Math.Max(sizeHint.Value, 0);
			else
				availableSize.Width = Math.Max(sizeHint.Value, 0);

			child.Measure(availableSize);

			var desiredSize = child.DesiredSize;

			if (orientation == Orientation.Vertical)
				desiredSize.Height = availableSize.Height;
			else
				desiredSize.Width = availableSize.Width;

			AddToTotalSize(orientation, desiredSize, ref totalSize);
		}

		private static void MesureChildAuto(UIElement child, Orientation orientation, Size availableSize, ref Size totalSize)
		{
			if (orientation == Orientation.Vertical)
				availableSize.Height = Math.Max(availableSize.Height - totalSize.Height, 0);
			else
				availableSize.Width = Math.Max(availableSize.Width - totalSize.Width, 0);

			child.Measure(availableSize);
			AddToTotalSize(orientation, child.DesiredSize, ref totalSize);
		}

		private static void MesureChildStar(UIElement child, Orientation orientation, Size availableSize, GridLength sizeHint, double starTotal, ref Size totalSize)
		{
			var portion = sizeHint.Value / starTotal;

			if (orientation == Orientation.Vertical)
				availableSize.Height *= portion;
			else
				availableSize.Width *= portion;

			child.Measure(availableSize);
			AddToTotalSize(orientation, child.DesiredSize, ref totalSize);
		}

		private GridLength GetSizeInternal(UIElement child)
		{
			if (child == null)
			{
				return default(GridLength);
			}
			var sizes = _sizes;

			if (sizes != null && sizes.Length > 0)
			{
				var index = Children.IndexOf(child);
				if (index >= 0 && sizes.Length > index)
				{
					return sizes[index];
				}
			}

			return GetSize(child);
		}

		#region Orientation DependencyProperty
		public Orientation Orientation
		{
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register
		(
			"Orientation",
			typeof(Orientation),
			typeof(StarStackPanel),
			new PropertyMetadata(Orientation.Vertical, InvalidateLayoutOnChanged)
		);
		#endregion

		#region Priority AttachedProperty
		public static int GetPriority(DependencyObject obj)
		{
			return (int)obj.GetValue(PriorityProperty);
		}

		public static void SetPriority(DependencyObject obj, int value)
		{
			obj.SetValue(PriorityProperty, value);
		}

		public static readonly DependencyProperty PriorityProperty = DependencyProperty.RegisterAttached
		(
			"Priority",
			typeof(int),
			typeof(StarStackPanel),
			new PropertyMetadata(0)
		);
		#endregion

		#region Sizes DependencyProperty
		public static readonly DependencyProperty SizesProperty = DependencyProperty.Register(
			"Sizes", typeof (string), typeof (StarStackPanel), new PropertyMetadata(null, HandleSizesChanged));

		public string Sizes
		{
			get { return (string)GetValue(SizesProperty); }
			set { SetValue(SizesProperty, value); }
		}

		private static void HandleSizesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var pnl = d as StarStackPanel;
			if (pnl == null)
			{
				return;
			}

			InvalidateLayoutOnChanged(d, e);

			var sizes = e.NewValue as string;
			if (string.IsNullOrWhiteSpace(sizes))
			{
				pnl._sizes = null;
			}
			else
			{
				pnl._sizes = ParseGridLength(sizes);
			}
		}

		private static readonly Regex GridLengthParsingRegex =
			new Regex(
				@"^(?:(?<stars>\d*(?:.\d*))\*)|(?<abs>\d+(?:.\d*))|(?<auto>Auto)|(?<star>\*)$",
#if NETFX_CORE || (SILVERLIGHT && !WINDOWS_PHONE)
				RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);
#else
				RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.Compiled);
#endif

		private static GridLength[] ParseGridLength(string s)
		{
			var parts = s.Split(',');

			var result = new List<GridLength>(parts.Length);

			foreach (var part in parts)
			{
				if (string.IsNullOrEmpty(part))
				{
					result.Add(new GridLength(0, GridUnitType.Auto));
					continue;
				}

				var match = GridLengthParsingRegex.Match(part);
				if (!match.Success)
				{
					throw new InvalidOperationException("Invalid value '" + part + "', unable to parse.");
				}

				var autoGroup = match.Groups["auto"];
				if (autoGroup.Success)
				{
					result.Add(new GridLength(0, GridUnitType.Auto));
					continue;
				}

				var starsGroup = match.Groups["stars"];
				if (starsGroup.Success)
				{
					var value =
						!string.IsNullOrWhiteSpace(starsGroup.Value)
							? double.Parse(starsGroup.Value, CultureInfo.InvariantCulture)
							: 1;
					result.Add(new GridLength(value, GridUnitType.Star));
					continue;
				}

				var starGroup = match.Groups["star"];
				if (starGroup.Success)
				{
					result.Add(new GridLength(1, GridUnitType.Star));
					continue;
				}

				var absGroup = match.Groups["abs"];
				if (absGroup.Success)
				{
					var value = double.Parse(absGroup.Value, CultureInfo.InvariantCulture);
					result.Add(new GridLength(value, GridUnitType.Pixel));
					continue;
				}

				throw new Exception("Unknown parsing error");
			}

			return result.ToArray();
		}

		private GridLength[] _sizes;

		#endregion

		#region Size AttachedProperty
		public static GridLength GetSize(DependencyObject obj)
		{
			return (GridLength)obj.GetValue(SizeProperty);
		}

		public static void SetSize(DependencyObject obj, GridLength value)
		{
			obj.SetValue(SizeProperty, value);
		}

		public static readonly DependencyProperty SizeProperty = DependencyProperty.RegisterAttached
		(
			"Size",
			typeof(GridLength),
			typeof(StarStackPanel),
			new PropertyMetadata(GridLength.Auto, HandleSizePropertyChanged)
		);

		private static void HandleSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var element = d as FrameworkElement;
			if (element != null)
				InvalidateLayoutOnChanged(element.Parent, default(DependencyPropertyChangedEventArgs));
		}
		#endregion

		#region struct Record
		private struct Record
		{
			public UIElement Child;
			public Size Size;
			public GridLength SizeHint;
		}
		#endregion
	}
}
