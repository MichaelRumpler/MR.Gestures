using Android.Runtime;
using Android.Views;
using AView = Android.Views.View;

using MR.Gestures.Android.EventArgs;

namespace MR.Gestures.Android
{
	internal class MouseGestureListener : Java.Lang.Object
    {
		private IGestureAwareControl element;
		private AView view;
		private IGestureListener listener;

		private MotionEvent lastMouse;
		private MotionEvent LastMouse
		{
			get { return lastMouse; }
			set
			{
				if (lastMouse != null) lastMouse.Recycle();
				lastMouse = value != null ? MotionEvent.Obtain(value) : null;
			}
		}

		private MouseEventArgs lastMouseArgs;


		internal MouseGestureListener(IGestureAwareControl element, AView view, IGestureListener listener)
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
        internal MouseGestureListener(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
		{
        }
        #endregion internal java constructor


        public bool OnDown(MotionEvent e)
		{
			var handled = false;

			if (element.GestureHandler.HandlesDown)
			{
				var args = new AndroidDownUpEventArgs(e, view);

				listener.OnDown(args);
				//handled = args.Handled;
			}

			return handled;
		}

		public bool OnUp(MotionEvent e)
		{
			var handled = false;

			if (element.GestureHandler.HandlesUp)
			{
				var args = new AndroidDownUpEventArgs(e, view);

				listener.OnUp(args);
				//handled = args.Handled;
			}

			return handled;
		}

		public bool OnMouseEntered(MotionEvent e)
		{
			var handled = false;

			var mouseArgs = new AndroidMouseEventArgs(null, e, null, view);

			if (element.GestureHandler.HandlesMouseEntered)
			{

				listener.OnMouseEntered(mouseArgs);
				//handled = args.Handled;
			}

			LastMouse = e;
			lastMouseArgs = mouseArgs;

			return handled;
		}

		public bool OnMouseMoved(MotionEvent e)
		{
			var handled = false;

			var mouseArgs = new AndroidMouseEventArgs(LastMouse, e, lastMouseArgs, view);

			if (element.GestureHandler.HandlesMouseMoved)
			{

				listener.OnMouseMoved(mouseArgs);
				//handled = args.Handled;
			}

			LastMouse = e;
			lastMouseArgs = mouseArgs;

			return handled;
		}

		public bool OnMouseExited(MotionEvent e)
		{
			var handled = false;

			var mouseArgs = new AndroidMouseEventArgs(LastMouse, e, lastMouseArgs, view);

			if (element.GestureHandler.HandlesMouseExited)
			{

				listener.OnMouseExited(mouseArgs);
				//handled = args.Handled;
			}

			LastMouse = null;
			lastMouseArgs = null;

			return handled;
		}

		public bool OnScrollWheelChanged(MotionEvent e)
        {
			var handled = false;

			var mouseArgs = new AndroidScrollWheelEventArgs(e, view);

			if (element.GestureHandler.HandlesScrollWheelChanged)
			{

				listener.OnScrollWheelChanged(mouseArgs);
				//handled = args.Handled;
			}

			return handled;
		}
	}
}
