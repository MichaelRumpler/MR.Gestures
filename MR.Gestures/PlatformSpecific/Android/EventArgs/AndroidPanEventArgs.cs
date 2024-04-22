using Android.Views;

namespace MR.Gestures.Android.EventArgs
{
	public class AndroidPanEventArgs : PanEventArgs
	{
		public AndroidPanEventArgs(MotionEvent previous, MotionEvent current, PanEventArgs prevArgs, global::Android.Views.View view)
		{
			Cancelled = current.Action == MotionEventActions.Cancel;
			ViewPosition = AndroidEventArgsHelper.GetViewPosition(view);
			Touches = AndroidEventArgsHelper.GetTouches(current);
			Sources = AndroidEventArgsHelper.GetSources(current);

			if (prevArgs != null)
				CalculateDistances(prevArgs);
			else
			{
				// set DeltaDistance and TotalDistance
				var previousTouches = AndroidEventArgsHelper.GetTouches(previous);
				if (previousTouches.Length == Touches.Length)
				{
					var previousCenter = previousTouches.Center();
					DeltaDistance = Center.Subtract(previousCenter);
				}
				else
				{
					DeltaDistance = Point.Zero;
				}
				TotalDistance = DeltaDistance;
			}

			Velocity = GetVelocity(previous, current, prevArgs);

		}

		private Point GetVelocity(MotionEvent previous, MotionEvent current, PanEventArgs prevArgs)
		{
			if (previous == null)
				return new Point(0, 0);

			if (DeltaDistance.X == 0 && DeltaDistance.Y == 0)
				return prevArgs != null ? prevArgs.Velocity : new Point(0, 0);

			var d = DeltaDistance;
			double ms = current.EventTime - previous.EventTime;

			return new Point(d.X * 1000 / ms, d.Y * 1000 / ms);
		}
	}
}