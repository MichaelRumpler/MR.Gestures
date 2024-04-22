//#define LOGINSTANCES

using System;
using System.Linq;

using Foundation;
using UIKit;

namespace MR.Gestures.iOS
{
	public class MultiTouchGestureRecognizer : UIGestureRecognizer
	{
		private readonly WeakReference<IMultiTouchListener> Listener;
		private bool isMultiTouchGesture = false;

		public MultiTouchGestureRecognizer(IMultiTouchListener listener) : base()
		{
			Listener = new WeakReference<IMultiTouchListener>(listener);
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);

			if (NumberOfTouches < 2)
				return;

			isMultiTouchGesture = true;

			if (Listener.TryGetTarget(out IMultiTouchListener listener))
			{
				listener.OnMultiTouchMoving(this);
			}
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);

			var touchesLeft = NumberOfTouches - touches.OfType<UITouch>().Count(t => t.Phase == UITouchPhase.Ended);
			//Log($"TouchesEnded: isMultiTouchGesture={isMultiTouchGesture}, NumberOfTouches ={NumberOfTouches}, touchesLeft={touchesLeft}");
			if (!isMultiTouchGesture || touchesLeft >= 2)
				return;

			if (Listener.TryGetTarget(out IMultiTouchListener listener))
			{
				listener.OnMultiTouchEnded(this);
			}

			isMultiTouchGesture = false;
		}

		public override void TouchesCancelled(NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
			// they do that on http://developer.xamarin.com/guides/cross-platform/application_fundamentals/touch/part_2_ios_touch_walkthrough/
			base.State = UIGestureRecognizerState.Failed;
		}

		#region Logging

#if LOGINSTANCES
		static StringBuilder logSb = new StringBuilder();
		private static void Log(string s)
		{
			logSb.AppendLine(s);
		}

		static MultiTouchGestureRecognizer()
		{
			Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(2), () =>
			{
				if (logSb != null && logSb.Length > 0)
				{
					Debug.WriteLine(logSb);
					logSb.Clear();
				}
				return true;
			});
		}
#endif

		#endregion
	}
}