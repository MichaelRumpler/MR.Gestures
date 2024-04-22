using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public class iOSScrollWheelEventArgs : ScrollWheelEventArgs
	{
		public iOSScrollWheelEventArgs(UIPanGestureRecognizer gr)
		{
			Cancelled = gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed;
			ViewPosition = iOSEventArgsHelper.GetViewPosition(gr.View);
			Touches = iOSEventArgsHelper.GetTouches(gr);
			Sources = iOSEventArgsHelper.GetSources(gr);

			var t = gr.TranslationInView(gr.View);
            ScrollDelta = new Point(t.X, t.Y);			// Apple scrolls in the opposite direction than the rest of the world. Can be configured with Settings / General / Trackpad & Mouse / Natural Scrolling.
		}
	}
}