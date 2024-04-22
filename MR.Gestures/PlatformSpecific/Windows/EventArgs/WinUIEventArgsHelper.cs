using Microsoft.UI.Xaml;

using Microsoft.UI.Input;

using Window = Microsoft.UI.Xaml.Window;

namespace MR.Gestures.WinUI.EventArgs
{
	public static class WinUIEventArgsHelper
	{
		public static Rect GetViewPosition(FrameworkElement view)
		{
			//var transform = view.TransformToVisual(Window.Current.Content);
			var transform = view.TransformToVisual((UIElement)view.GetRoot());
			var pos = transform.TransformPoint(new Windows.Foundation.Point(0, 0));

			return new Rect(pos.X, pos.Y, view.ActualWidth, view.ActualHeight);
		}

		public static Point[] GetTouches(List<PointerPoint> currentPointers, FrameworkElement view, BaseGestureEventArgs previous = null, int minimumTouches = 1)
		{
			if (currentPointers.Count < minimumTouches && previous != null)
				return previous.Touches;

            // absolute coordinates:
            //return currentPointers
            //    .Select(p => ToXFPoint(p.Position))
            //    .ToArray();

			// relative coordinates:
            var transform = view.GetRoot().TransformToVisual(view);
            return currentPointers
                .Select(p => ToXFPoint(transform.TransformPoint(p.Position)))
                .ToArray();
        }

        public static TouchSource[] GetSources(List<PointerPoint> currentPointers, BaseGestureEventArgs previous = null, int minimumTouches = 1)
		{
			if (currentPointers.Count < minimumTouches && previous != null)
				return previous.Sources;

			return currentPointers
				.Select(p => ToTouchSource(p))
				.ToArray();
		}

		public static Point[] GetTouches(PointerPoint point, FrameworkElement view)
		{
            // absolute coordinates:
            //return new[] { ToXFPoint(point.Position) };

            // relative coordinates:
            var transform = view.GetRoot().TransformToVisual(view);
            return new[] { ToXFPoint(transform.TransformPoint(point.Position)) };
        }

		public static TouchSource[] GetSources(PointerPoint point)
		{
			return new[] { ToTouchSource(point) };
		}

		public static Point ToXFPoint(Windows.Foundation.Point winPoint)
		{
			return new Point(winPoint.X, winPoint.Y);
		}

		private static TouchSource ToTouchSource(PointerPoint point)
		{
			switch (point.PointerDeviceType)
			{
				case PointerDeviceType.Touch:
					return TouchSource.Touchscreen;
				case PointerDeviceType.Mouse:
					var src =
						  (point.Properties.IsLeftButtonPressed   || point.Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonReleased ? TouchSource.LeftMouseButton : 0)
						| (point.Properties.IsRightButtonPressed  || point.Properties.PointerUpdateKind == PointerUpdateKind.RightButtonReleased ? TouchSource.RightMouseButton : 0)
						| (point.Properties.IsMiddleButtonPressed || point.Properties.PointerUpdateKind == PointerUpdateKind.MiddleButtonReleased ? TouchSource.MiddleMouseButton : 0)
						| (point.Properties.IsXButton1Pressed     || point.Properties.PointerUpdateKind == PointerUpdateKind.XButton1Released ? TouchSource.XButton1 : 0)
						| (point.Properties.IsXButton2Pressed     || point.Properties.PointerUpdateKind == PointerUpdateKind.XButton2Released ? TouchSource.XButton2 : 0);
					return src != 0 ? src : TouchSource.MousePointer;
				case PointerDeviceType.Pen:
					return TouchSource.Pen;
				default:
					return TouchSource.Unknown;
			}
		}
	}
}
