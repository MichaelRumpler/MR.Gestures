using Android.Views;

namespace MR.Gestures.Android.EventArgs
{
	public class AndroidDownUpEventArgs : DownUpEventArgs
	{
		public AndroidDownUpEventArgs(MotionEvent current, global::Android.Views.View view)
		{
			Cancelled = current.Action == MotionEventActions.Cancel;

			ViewPosition = AndroidEventArgsHelper.GetViewPosition(view);

			Touches = AndroidEventArgsHelper.GetTouches(current);
			Sources = AndroidEventArgsHelper.GetSources(current);

			TriggeringTouches = new[] { current.ActionIndex };
		}
	}
}