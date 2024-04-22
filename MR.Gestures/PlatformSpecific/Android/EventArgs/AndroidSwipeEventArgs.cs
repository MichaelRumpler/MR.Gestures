using Android.Views;

namespace MR.Gestures.Android.EventArgs
{
	public class AndroidSwipeEventArgs : SwipeEventArgs
	{
		public AndroidSwipeEventArgs(MotionEvent previous, MotionEvent current, PanEventArgs prevArgs, global::Android.Views.View view, Direction direction)
		{
			Cancelled = current.Action == MotionEventActions.Cancel;
			ViewPosition = AndroidEventArgsHelper.GetViewPosition(view);
			Touches = AndroidEventArgsHelper.GetTouches(current);
			Sources = AndroidEventArgsHelper.GetSources(current);

			Direction = direction;

			CalculateDistances(prevArgs);
			if (DeltaDistance.IsEmpty && prevArgs != null)
				DeltaDistance = prevArgs.DeltaDistance;

			Velocity = GetVelocity(previous, current);

		}

		private Point GetVelocity(MotionEvent previous, MotionEvent current)
		{
			if (previous == null)
				return new Point(0, 0);

			var d = DeltaDistance;
			double ms = current.EventTime - previous.EventTime;

			return new Point(d.X * 1000 / ms, d.Y * 1000 / ms);
		}
	}
}