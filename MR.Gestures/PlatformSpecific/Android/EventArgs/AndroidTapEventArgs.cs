using Android.Views;

namespace MR.Gestures.Android.EventArgs
{
	public class AndroidTapEventArgs : TapEventArgs
	{
		public AndroidTapEventArgs(MotionEvent tap, global::Android.Views.View view, int numberOfTaps)
		{
			Cancelled = tap.Action == MotionEventActions.Cancel;

			ViewPosition = AndroidEventArgsHelper.GetViewPosition(view);

			Touches = AndroidEventArgsHelper.GetTouches(tap);
			Sources = AndroidEventArgsHelper.GetSources(tap);

			NumberOfTaps = numberOfTaps;
		}
	}
}