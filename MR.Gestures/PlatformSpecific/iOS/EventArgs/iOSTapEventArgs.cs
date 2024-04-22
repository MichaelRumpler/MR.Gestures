using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public class iOSTapEventArgs : TapEventArgs
	{
		public iOSTapEventArgs(UITapGestureRecognizer gr, int numberOfTaps)
		{
			// interesting data:
			//System.Drawing.PointF centralLocation = gr.LocationInView(view);
			//System.Drawing.PointF finger0Location = gr.LocationOfTouch(0, view);
			//int gr.NumberOfTapsRequired		init
			//int gr.NumberOfTouches
			//uint gr.NumberOfTouchesRequired	init
			//UIGestureRecognizerState gr.State;
			//UIView gr.View;

			Cancelled = gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed;

			ViewPosition = iOSEventArgsHelper.GetViewPosition(gr.View);

			Touches = iOSEventArgsHelper.GetTouches(gr);
			Sources = iOSEventArgsHelper.GetSources(gr);

			NumberOfTaps = numberOfTaps;
		}
	}
}