using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public class iOSPinchEventArgs : PinchEventArgs
	{
		public iOSPinchEventArgs(UIGestureRecognizer gr, PinchEventArgs previous)
		{
			/*
			 * iOS: https://developer.apple.com/library/IOS/documentation/UIKit/Reference/UIPinchGestureRecognizer_Class/index.html#//apple_ref/occ/instp/UIPinchGestureRecognizer/scale
			 * 
			 * double scale
			 * double velocity
			 */

			// interesting data:
			//System.Drawing.PointF centralLocation = gr.LocationInView(view);
			//System.Drawing.PointF finger0Location = gr.LocationOfTouch(0, view);
			//int gr.NumberOfTouches;
			//float gr.Scale
			//UIGestureRecognizerState gr.State;
			//float gr.Velocity
			//UIView gr.View;

			Cancelled = gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed;

			ViewPosition = iOSEventArgsHelper.GetViewPosition(gr.View);

			Touches = iOSEventArgsHelper.GetTouches(gr, 2, previous);
			Sources = iOSEventArgsHelper.GetSources(gr, 2, previous);

			CalculateScales(previous);
		}
	}
}