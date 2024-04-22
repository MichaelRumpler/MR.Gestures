using Android.Views;

namespace MR.Gestures.Android.EventArgs
{
	public class AndroidRotateEventArgs : RotateEventArgs
	{
		public AndroidRotateEventArgs(MotionEvent current, RotateEventArgs previous, global::Android.Views.View view)
		{
			Cancelled = current.Action == MotionEventActions.Cancel;

			ViewPosition = AndroidEventArgsHelper.GetViewPosition(view);

			Touches = AndroidEventArgsHelper.GetTouches(current, 2, previous);
			Sources = AndroidEventArgsHelper.GetSources(current, 2, previous);

			CalculateAngles(previous);
		}
	}
}