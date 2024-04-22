using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public class iOSRotateEventArgs : RotateEventArgs
	{
		public iOSRotateEventArgs(UIGestureRecognizer gr, RotateEventArgs previous)
		{
			// interesting data:
			//System.Drawing.PointF centralLocation = gr.LocationInView(view);
			//System.Drawing.PointF finger0Location = gr.LocationOfTouch(0, view);
			//int gr.NumberOfTouches;
			//float gr.Rotation;
			//UIGestureRecognizerState gr.State;
			//float gr.Velocity;
			//UIView gr.View;

			Cancelled = gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed;

			ViewPosition = iOSEventArgsHelper.GetViewPosition(gr.View);

			Touches = iOSEventArgsHelper.GetTouches(gr, 2, previous);
			Sources = iOSEventArgsHelper.GetSources(gr, 2, previous);

			CalculateAngles(previous);
		}
	}
}