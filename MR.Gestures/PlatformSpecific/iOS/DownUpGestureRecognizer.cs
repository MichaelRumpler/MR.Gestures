using System;
using System.Linq;

using Foundation;
using UIKit;

namespace MR.Gestures.iOS
{
	public class DownUpGestureRecognizer : UIGestureRecognizer
	{
		private readonly WeakReference<IDownUpListener> Listener;

		public DownUpGestureRecognizer(IDownUpListener listener) : base()
		{
			Listener = new WeakReference<IDownUpListener>(listener);
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);

			if (Listener.TryGetTarget(out IDownUpListener listener))
			{
				var touchesBegan = touches.OfType<UITouch>().Where(t => t.Phase == UITouchPhase.Began).ToArray();
				//Console.WriteLine($"TouchesBegan: ButtonMask={evt.ButtonMask}, Touches: " + String.Join(", ", touchesBegan.Select(t => $"Type={t.Type}")));
				listener.OnDown(this, touchesBegan);
			}
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);

			if (Listener.TryGetTarget(out IDownUpListener listener))
			{
				var touchesEnded = touches.OfType<UITouch>().Where(t => t.Phase == UITouchPhase.Ended).ToArray();
				listener.OnUp(this, touchesEnded);
			}
		}

		public override void TouchesCancelled(NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
			// they do that on https://docs.microsoft.com/en-us/xamarin/ios/app-fundamentals/touch/ios-touch-walkthrough#custom-gesture-recognizer
			// we fail the recognizer so that there isn't unexpected behavior
			// if the application comes back into view
			base.State = UIGestureRecognizerState.Failed;
		}

		#region Logging

#if LOGINSTANCES
		static StringBuilder logSb = new StringBuilder();
		private static void Log(string s)
		{
			logSb.AppendLine(s);
		}

		static DownUpGestureRecognizer()
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