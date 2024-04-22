using Android.OS;
using Android.Runtime;
using Android.Views;
using AView = Android.Views.View;
using MR.Gestures.Android.EventArgs;

namespace MR.Gestures.Android
{
	internal class TapGestureListener : Java.Lang.Object, ITapGestureListener
	{
		static volatile Handler handler;

		private IGestureAwareControl element;
		private AView view;
		private IGestureListener listener;

		internal TapGestureListener(IGestureAwareControl element, AView view, IGestureListener listener)
		{
			this.element = element;
			this.view = view;
			this.listener = listener;
		}

        #region internal java constructor
        // according to https://github.com/xamarin/Xamarin.Forms/blob/master/Xamarin.Forms.Platform.Android/InnerGestureListener.cs#L40 :
        // This is needed because GestureRecognizer callbacks can be delayed several hundred milliseconds
        // which can result in the need to resurect this object if it has already been disposed. We dispose
        // eagerly to allow easier garbage collection of the renderer
        internal TapGestureListener(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
		{
        }
        #endregion internal java constructor

        #region ITapGestureListener Members

        int numberOfTaps = 0;
		CancellationTokenSource cancelTappedRaiser;

		public bool OnTapping(MotionEvent e)
		{
			if (cancelTappedRaiser != null)																		//	and no other event between DOWN and UP
			{
				try
				{
					cancelTappedRaiser.Cancel();
				}
				catch { }
			}

			numberOfTaps++;
			var args = new AndroidTapEventArgs(e, view, numberOfTaps);

			bool handled = false;
			if (element.GestureHandler.HandlesTapping)
			{
				listener.OnTapping(args);
				//handled = args.Handled;
			}

			cancelTappedRaiser = new CancellationTokenSource();
			Task.Run(async () =>
			{
				await Task.Delay(MR.Gestures.Settings.MsUntilTapped, cancelTappedRaiser.Token).ConfigureAwait(false);
				cancelTappedRaiser = null;		// I cannot be cancelled anymore
				numberOfTaps = 0;
				if (args.NumberOfTaps == 1 && element.GestureHandler.HandlesTapped)
					BeginInvokeOnMainThread(() => listener.OnTapped(args));
				else if (args.NumberOfTaps == 2 && element.GestureHandler.HandlesDoubleTapped)
					BeginInvokeOnMainThread(() => listener.OnDoubleTapped(args));

			}, cancelTappedRaiser.Token);

			return handled;
		}

		public bool OnLongPressing(MotionEvent e)
		{
            //Console.WriteLine("TapGestureListener.onLongPressing due to move event");
            bool handled = false;

			if (element.GestureHandler.HandlesLongPressing)
			{
				var args = new AndroidLongPressEventArgs(e, view);
				BeginInvokeOnMainThread(() => listener.OnLongPressing(args));
				//handled = args.Handled;
			}

			return handled;
		}

        public bool OnLongPressing(IEnumerable<Point> touches, MotionEvent start, long duration)
        {
            //Console.WriteLine("TapGestureListener.onLongPressing due to timeout");
            bool handled = false;

            if (element.GestureHandler.HandlesLongPressing)
            {
                var args = new AndroidLongPressEventArgs(touches, start, duration, view);
				BeginInvokeOnMainThread(() => listener.OnLongPressing(args));
                //handled = args.Handled;
            }

            return handled;
        }

        public bool OnLongPressed(MotionEvent e, bool cancel)
		{
			bool handled = false;

			if (element.GestureHandler.HandlesLongPressed)
			{
				var args = new AndroidLongPressEventArgs(e, view, cancel);
				BeginInvokeOnMainThread(() => listener.OnLongPressed(args));
				//handled = args.Handled;
			}

			return handled;
		}

		#endregion

		#region BeginInvokeOnMainThread

		private static void BeginInvokeOnMainThread(Action action)
		{
			if (handler?.Looper != Looper.MainLooper)
				handler = new Handler(Looper.MainLooper);

			handler.Post(action);
		}

		#endregion BeginInvokeOnMainThread
	}
}
