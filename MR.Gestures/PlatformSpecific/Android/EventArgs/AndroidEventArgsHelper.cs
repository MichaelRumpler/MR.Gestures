using Android.Views;

namespace MR.Gestures.Android.EventArgs
{
	public static class AndroidEventArgsHelper
	{
		public static Rect GetViewPosition(global::Android.Views.View view)
		{
			var location = new int[2];
			view.GetLocationInWindow(location);

			int x = location[0];
			int y = location[1];
			int w = view.Width;
			int h = view.Height;
			return DIP.ToRect(x, y, w, h);
		}

		public static Point[] GetTouches(MotionEvent current, int requiredTouches = 1, BaseGestureEventArgs previous = null)
		{
			var l = current?.PointerCount ?? 0;

			if (l < requiredTouches && previous != null)
				return previous.Touches;

			var coords = new MotionEvent.PointerCoords();
			var rc = new Point[l];

			for (int i = 0; i < l; i++)
			{
				current.GetPointerCoords(i, coords);
				rc[i] = DIP.ToPoint(coords.X, coords.Y);
			}

			return rc;
		}

		private static readonly TouchSource[] MouseSource = new[] { TouchSource.MousePointer };

		public static TouchSource[] GetSources(MotionEvent current, int requiredTouches = 1, BaseGestureEventArgs previous = null)
        {
			var l = current?.PointerCount ?? 0;

			if (l < requiredTouches && previous != null)
				return previous.Sources;

			if (l > 0)
			{
				var source = ToTouchSource(current);    // only one source for all points
				return Enumerable.Repeat(source, l).ToArray();
			}
			else
				return MouseSource;
		}

		public static Point[] GetTouches(IEnumerable<Point> touches)
        {
            return touches.Select(p => DIP.ToPoint(p.X, p.Y)).ToArray();
        }

		public static TouchSource[] GetSources(IEnumerable<Point> touches, MotionEvent start)
        {
			var source = ToTouchSource(start);    // only one source for all points
			return Enumerable.Repeat(source, touches.Count()).ToArray();
		}

        private static TouchSource ToTouchSource(MotionEvent current)
        {
			switch(current?.Source)
            {
				case InputSourceType.Touchscreen:
					return TouchSource.Touchscreen;
				case InputSourceType.Mouse:
#pragma warning disable CA1416 // Validate platform compatibility
                    var actBtn = global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.M ? current.ActionButton : 0;
#pragma warning restore CA1416 // Validate platform compatibility
                    var src =
						(((actBtn & MotionEventButtonState.Primary) > 0) ? TouchSource.LeftMouseButton : TouchSource.Unknown) |
						(((actBtn & MotionEventButtonState.Tertiary) > 0) ? TouchSource.MiddleMouseButton : TouchSource.Unknown) |
						(((actBtn & MotionEventButtonState.Secondary) > 0) ? TouchSource.RightMouseButton : TouchSource.Unknown) |
						(((actBtn & MotionEventButtonState.Back) > 0) ? TouchSource.XButton1 : TouchSource.Unknown) |
						(((actBtn & MotionEventButtonState.Forward) > 0) ? TouchSource.XButton2 : TouchSource.Unknown) |
						(((current.ButtonState & MotionEventButtonState.Primary) > 0) ? TouchSource.LeftMouseButton : TouchSource.Unknown) |
						(((current.ButtonState & MotionEventButtonState.Tertiary) > 0) ? TouchSource.MiddleMouseButton : TouchSource.Unknown) |
						(((current.ButtonState & MotionEventButtonState.Secondary) > 0) ? TouchSource.RightMouseButton : TouchSource.Unknown) |
						(((current.ButtonState & MotionEventButtonState.Back) > 0) ? TouchSource.XButton1 : TouchSource.Unknown) |
						(((current.ButtonState & MotionEventButtonState.Forward) > 0) ? TouchSource.XButton2 : TouchSource.Unknown);
					 return src != TouchSource.Unknown ? src : TouchSource.MousePointer;
                case InputSourceType.Stylus:
					return TouchSource.Pen;
                case InputSourceType.Touchpad:
					return TouchSource.Touchpad;
				default:
					return TouchSource.Unknown;
            };
        }
    }
}