using Microsoft.UI.Xaml;
using Microsoft.UI.Input;

namespace MR.Gestures.WinUI.EventArgs
{
	public class WinUIMouseEventArgs : MouseEventArgs
	{
		public WinUIMouseEventArgs(FrameworkElement view, PointerPoint currentPointer, MouseEventArgs previous, ulong prevTimestamp)
		{
			ViewPosition = WinUIEventArgsHelper.GetViewPosition(view);
			Touches = WinUIEventArgsHelper.GetTouches(currentPointer, view);
			Sources = WinUIEventArgsHelper.GetSources(currentPointer);

			CalculateDistances(previous);

			Velocity = GetVelocity(currentPointer, previous, prevTimestamp);
		}

		private Point GetVelocity(PointerPoint currentPointer, BaseGestureEventArgs prevArgs, ulong prevTimestamp)
		{
			if (prevArgs == null)
				return Point.Zero;
			else
			{
				var thisTimestamp = currentPointer.Timestamp;
				var ms = thisTimestamp > prevTimestamp ? (thisTimestamp - prevTimestamp) : prevTimestamp - thisTimestamp;
				if (ms > 0)
					return new Point(DeltaDistance.X * 1000 / ms, DeltaDistance.Y * 1000 / ms);
				else
					return (prevArgs as PanEventArgs)?.Velocity ?? Point.Zero;
			}
		}
	}
}
