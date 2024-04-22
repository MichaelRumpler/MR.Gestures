using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public static class iOSEventArgsHelper
	{
		public static Rect GetViewPosition(UIView view)
		{
			// algorithm from https://stackoverflow.com/a/58031897/1722408
#pragma warning disable CA1416 // Validate platform compatibility
			UIWindow window = UIDevice.CurrentDevice.CheckSystemVersion(13, 0)
				? UIApplication.SharedApplication.ConnectedScenes.OfType<UIWindowScene>().SelectMany(s => s.Windows).FirstOrDefault(w => w.IsKeyWindow)
				: UIApplication.SharedApplication.KeyWindow;
#pragma warning restore CA1416 // Validate platform compatibility

            var frame = view.ConvertRectToView(view.Bounds, window);
			return new Rect(frame.X, frame.Y, frame.Width, frame.Height);
		}

		public static Point[] GetTouches(UIGestureRecognizer gestureRecognizer, int requiredTouches = 1, BaseGestureEventArgs previous = null)
		{
			var l = gestureRecognizer.NumberOfTouches;
			if (l < requiredTouches && previous != null)
				return previous.Touches;

			if (l > 0)
			{
				var rc = new Point[l];
				for (int i = 0; i < l; i++)
				{
					var pos = gestureRecognizer.LocationOfTouch(i, gestureRecognizer.View);
					rc[i] = new Point(pos.X, pos.Y);
				}
				return rc;
			}
			else
            {
				var p = gestureRecognizer.LocationInView(gestureRecognizer.View);
				return new[] { new Point(p.X, p.Y) };
			}
		}

		private static readonly TouchSource[] MouseSource = new[] { TouchSource.MousePointer };

		public static TouchSource[] GetSources(UIGestureRecognizer gestureRecognizer, int requiredTouches = 1, BaseGestureEventArgs previous = null)
		{
			var l = gestureRecognizer.NumberOfTouches;
			if (l < requiredTouches && previous != null)
				return previous.Sources;

			if (l > 0)
			{
				// there is UIEventButtonMask, but it only defines Primary (Left) and Secondary (Right), no middle or anything else
				ulong buttons = UIDevice.CurrentDevice.CheckSystemVersion(13, 4)
					? (ulong)gestureRecognizer.ButtonMask
					: 0;

				var source = buttons == 0 ? TouchSource.Touchscreen :
					(((buttons & 1) > 0) ? TouchSource.LeftMouseButton : TouchSource.Unknown) |
					(((buttons & 2) > 0) ? TouchSource.RightMouseButton : TouchSource.Unknown) |
					(((buttons & 4) > 0) ? TouchSource.MiddleMouseButton : TouchSource.Unknown);

				return Enumerable.Repeat(source, (int)l).ToArray();
			}
			else
				return MouseSource;
		}
	}
}