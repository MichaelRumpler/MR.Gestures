using Android.Views;

namespace MR.Gestures.Android.EventArgs
{
	public class AndroidPinchEventArgs : PinchEventArgs
	{
		public AndroidPinchEventArgs(MotionEvent current, PinchEventArgs previous, global::Android.Views.View view)
		{
			Cancelled = current.Action == MotionEventActions.Cancel;

			ViewPosition = AndroidEventArgsHelper.GetViewPosition(view);

			Touches = AndroidEventArgsHelper.GetTouches(current, 2, previous);
			Sources = AndroidEventArgsHelper.GetSources(current, 2, previous);

			CalculateScales(previous);
		}
	}
}