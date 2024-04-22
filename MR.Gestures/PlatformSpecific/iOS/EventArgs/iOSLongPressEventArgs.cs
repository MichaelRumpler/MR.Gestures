using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public class iOSLongPressEventArgs : LongPressEventArgs
	{
		public iOSLongPressEventArgs(UIGestureRecognizer gr, long duration = 0)
		{
			// interesting data:
			//float gr.AllowableMovement		init
			//System.Drawing.PointF centralLocation = gr.LocationInView(view);
			//System.Drawing.PointF finger0Location = gr.LocationOfTouch(0, view);
			//double gr.MinimumPressDuration
			//int gr.NumberOfTapsRequired		init
			//int gr.NumberOfTouches
			//uint gr.NumberOfTouchesRequired	init
			//UIGestureRecognizerState gr.State;
			//UIView gr.View;

			Cancelled = gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed;

			ViewPosition = iOSEventArgsHelper.GetViewPosition(gr.View);

			Touches = iOSEventArgsHelper.GetTouches(gr);
			Sources = iOSEventArgsHelper.GetSources(gr);

			Duration = duration;
		}
	}
}