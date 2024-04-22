using Android.Views;

namespace MR.Gestures.Android.EventArgs
{
	public class AndroidScrollWheelEventArgs : ScrollWheelEventArgs
	{
		public AndroidScrollWheelEventArgs(MotionEvent current, global::Android.Views.View view)
		{
			Cancelled = current.Action == MotionEventActions.Cancel;

			ViewPosition = AndroidEventArgsHelper.GetViewPosition(view);
			Touches = AndroidEventArgsHelper.GetTouches(current);
			Sources = AndroidEventArgsHelper.GetSources(current);

			ScrollDelta = new Point(current.GetAxisValue(Axis.Hscroll), current.GetAxisValue(Axis.Vscroll));
		}
	}
}