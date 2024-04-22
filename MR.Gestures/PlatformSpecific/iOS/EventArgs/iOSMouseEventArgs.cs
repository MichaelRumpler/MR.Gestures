using System.Diagnostics;

using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public class iOSMouseEventArgs : MouseEventArgs
	{
		private readonly long Timestamp;

		public iOSMouseEventArgs(UIHoverGestureRecognizer gr, iOSMouseEventArgs previous)
		{
			Cancelled = gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed;
			ViewPosition = iOSEventArgsHelper.GetViewPosition(gr.View);
			Touches = iOSEventArgsHelper.GetTouches(gr);
			Sources = iOSEventArgsHelper.GetSources(gr);

			CalculateDistances(previous);

			Timestamp = Stopwatch.GetTimestamp();
            Velocity = GetVelocity(gr, previous);
		}

		private Point GetVelocity(UIHoverGestureRecognizer gr, iOSMouseEventArgs previous)
		{
			//var v = gr.VelocityInView(gr.View);		only in UIPanGestureRecognizer
			//return new Point(v.X, v.Y);

			if(previous == null)
				return new Point(0, 0);

			var delta = (double)(Timestamp - previous.Timestamp) * 1000 / Stopwatch.Frequency;
			return new Point(DeltaDistance.X / delta, DeltaDistance.Y / delta);
		}
	}
}