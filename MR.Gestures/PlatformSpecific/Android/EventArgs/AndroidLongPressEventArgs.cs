using Android.Views;

namespace MR.Gestures.Android.EventArgs
{
	public class AndroidLongPressEventArgs : LongPressEventArgs
	{
		public AndroidLongPressEventArgs(MotionEvent end, global::Android.Views.View view, bool cancel = false)
		{
			Cancelled = cancel || end.Action == MotionEventActions.Cancel;

			ViewPosition = AndroidEventArgsHelper.GetViewPosition(view);

			Touches = AndroidEventArgsHelper.GetTouches(end);
			Sources = AndroidEventArgsHelper.GetSources(end);

			Duration = end.EventTime - end.DownTime;
		}

        public AndroidLongPressEventArgs(IEnumerable<Point> touches, MotionEvent start, long duration, global::Android.Views.View view)
        {
            Cancelled = false;

            ViewPosition = AndroidEventArgsHelper.GetViewPosition(view);

            Touches = AndroidEventArgsHelper.GetTouches(touches);
			Sources = AndroidEventArgsHelper.GetSources(touches, start);

			Duration = duration;
        }
    }
}