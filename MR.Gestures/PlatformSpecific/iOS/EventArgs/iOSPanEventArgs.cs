using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public class iOSPanEventArgs : PanEventArgs
	{
		public iOSPanEventArgs(UIPanGestureRecognizer gr, BaseGestureEventArgs previous)
		{
			/*
			 * iOS: https://developer.apple.com/library/IOs/documentation/UIKit/Reference/UIPanGestureRecognizer_Class/index.html#//apple_ref/occ/instm/UIPanGestureRecognizer/translationInView:
			 * 
			 * CGPoint translationInView	(contains x, y)
			 * CGPoint velocityInView		(contains x, y per second)
			 */
			// interesting data:
			//System.Drawing.PointF centralLocation = gr.LocationInView(view);
			//System.Drawing.PointF finger0Location = gr.LocationOfTouch(0, view);
			//uint gr.MaximumNumberOfTouches;
			//uint gr.MinimumNumberOfTouches;
			//int gr.NumberOfTouches;
			//UIGestureRecognizerState gr.State;
			//PointF gr.TranslationInView(view);
			//PointF gr.VelocityInView(view);
			//UIView gr.View;

			Cancelled = gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed;
			ViewPosition = iOSEventArgsHelper.GetViewPosition(gr.View);
			Touches = iOSEventArgsHelper.GetTouches(gr, 1, previous);
			Sources = iOSEventArgsHelper.GetSources(gr, 1, previous);

			CalculateDistances(previous);

			Velocity = GetVelocity(gr);
		}

		private Point GetVelocity(UIPanGestureRecognizer gr)
		{
			var v = gr.VelocityInView(gr.View);
			return new Point(v.X, v.Y);
		}
	}
}