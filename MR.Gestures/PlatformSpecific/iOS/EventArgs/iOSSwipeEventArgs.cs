using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public class iOSSwipeEventArgs : SwipeEventArgs
	{
		public iOSSwipeEventArgs(UIPanGestureRecognizer gr, Direction direction, PanEventArgs previous)
		{
			Cancelled = gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed;

			ViewPosition = iOSEventArgsHelper.GetViewPosition(gr.View);

			Touches = iOSEventArgsHelper.GetTouches(gr, 1, previous);
			Sources = iOSEventArgsHelper.GetSources(gr, 1, previous);

			DeltaDistance = previous.DeltaDistance;
			TotalDistance = previous.TotalDistance;
			Velocity = previous.Velocity;

			Direction = direction;
		}
	}
}