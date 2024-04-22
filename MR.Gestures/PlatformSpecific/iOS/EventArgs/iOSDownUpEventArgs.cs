using UIKit;

namespace MR.Gestures.iOS.EventArgs
{
	public class iOSDownUpEventArgs : DownUpEventArgs
	{
		public iOSDownUpEventArgs(UIGestureRecognizer gr, UITouch[] triggeringTouches)
		{
			Cancelled = gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed;

			ViewPosition = iOSEventArgsHelper.GetViewPosition(gr.View);

			Touches = iOSEventArgsHelper.GetTouches(gr);
			Sources = iOSEventArgsHelper.GetSources(gr);

			TriggeringTouches = GetTriggeringTouches(gr, triggeringTouches);
		}

		private int[] GetTriggeringTouches(UIGestureRecognizer gr, UITouch[] triggeringTouches)
		{
			var triggeringTouchPoints = triggeringTouches.Select(t =>
			{
				var pos = t.LocationInView(gr.View);
				return new Point(pos.X, pos.Y);
			}).ToArray();

			var indexes = new List<int>();
			for (int i = 0; i < Touches.Length; i++)
			{
				var t = Touches[i];
				if (triggeringTouchPoints.Any(p => (Math.Abs(p.X - t.X) < 0.1) && (Math.Abs(p.Y - t.Y) < 0.1)))
					indexes.Add(i);
			}

			return indexes.ToArray();
		}
	}
}
