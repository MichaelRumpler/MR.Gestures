using Android.Runtime;
using Android.Views;

using MR.Gestures.Android.EventArgs;

namespace MR.Gestures.Android
{
	internal class SimpleGestureListener : GestureDetector.SimpleOnGestureListener, IMultiTouchGestureListener
	{
		#region Private members and properties.

		private IGestureAwareControl element;
		private global::Android.Views.View view;
		private IGestureListener listener;

		private MotionEvent start;
		private MotionEvent Start
		{
			get { return start; }
			set
			{
				if (start != null) start.Recycle();
				start = value != null ? MotionEvent.Obtain(value) : null;
			}
		}

		private MotionEvent lastPan;
		private MotionEvent LastPan
		{
			get { return lastPan; }
			set
			{
				if (lastPan != null) lastPan.Recycle();
				lastPan = value != null ? MotionEvent.Obtain(value) : null;
			}
		}

		private bool scrolling = false;

		#endregion

		internal SimpleGestureListener(IGestureAwareControl element, global::Android.Views.View view, IGestureListener listener)
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
        internal SimpleGestureListener(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
		{
        }
		#endregion internal java constructor

		public override bool OnDown(MotionEvent e)
		{
			//Console.WriteLine($"SimpleGestureListener.OnDown {e.Action}");

			Start = e;
			scrolling = false;
			return false;
		}

		public void EndGestures(MotionEvent e)
		{
			if (Start != null)
			{
				OnPanned(e);
			}

			OnPinched(e);
			OnRotated(e);

			//Console.WriteLine($"SimpleGestureListener.EndGestures, resetting Start {e.Action}");
			Start = null;
			lastPanArgs = null;
			LastPan = null;
		}

		#region Panning/Panned/Swiped

		private PanEventArgs lastPanArgs;

		public override bool OnScroll (MotionEvent down, MotionEvent move, float distanceX, float distanceY)
		{
			//var eq = Start == null ? "==" : "!=";
			//var lpeq = LastPan == null ? "==" : "!=";
			//Console.WriteLine($"OnScroll, Start {eq} null, LastPan {lpeq} null, distanceX={distanceX}, distanceY={distanceY}");       // distances are from bottom right to top left !!!

			scrolling = true;

			if (LastPan != null || Start != null)
			{
				var args = new AndroidPanEventArgs(LastPan ?? Start, move, lastPanArgs, view);

				if (element.GestureHandler.HandlesPanning || element.GestureHandler.HandlesPanned)
					listener.OnPanning(args);

				lastPanArgs = args;
				LastPan = move;
			}

			//return args.Handled;
			return false;
		}

		private void OnPanned(MotionEvent e)
		{
			if (scrolling)
			{
				if (element.GestureHandler.HandlesPanning || element.GestureHandler.HandlesPanned)
				{
					//var eq = Start == null ? "==" : "!=";
					//var lpeq = LastPan == null ? "==" : "!=";
					//Console.WriteLine($"OnPanned, Start {eq} null, LastPan {lpeq} null");

					if (LastPan != null || Start != null)
					{
						var args = new AndroidPanEventArgs(LastPan ?? Start, e, lastPanArgs, view);
						listener.OnPanned(args);
					}
				}
				scrolling = false;
			}
		}

		public override bool OnFling(MotionEvent down, MotionEvent up, float velocityX, float velocityY)
		{
			var handled = false;

			if (scrolling)		// only if we had a one finger scroll gesture before
			{
				var maxFling = ViewConfiguration.Get(view.Context).ScaledMaximumFlingVelocity;
				var relativeVelocityX = velocityX / maxFling;
				var relativeVelocityY = velocityY / maxFling;

				var swipedX = Math.Abs(relativeVelocityX) > Settings.SwipeVelocityThreshold;
				var swipedY = Math.Abs(relativeVelocityY) > Settings.SwipeVelocityThreshold;
				if ((swipedX || swipedY) && element.GestureHandler.HandlesSwiped)
				{
					var direction = Direction.NotClear;
					if (!swipedY)
						direction = relativeVelocityX > 0 ? Direction.Right : Direction.Left;
					else if (!swipedX)
						direction = relativeVelocityY > 0 ? Direction.Down : Direction.Up;

					//var eq = Start == null ? "==" : "!=";
					//var lpeq = LastPan == null ? "==" : "!=";
					//Console.WriteLine($"OnFling, Start {eq} null, LastPan {lpeq} null");

					if (LastPan != null || Start != null)
					{
						SwipeEventArgs args = new AndroidSwipeEventArgs(LastPan ?? Start, up, lastPanArgs, view, direction);
						listener.OnSwiped(args);
						//handled = args.Handled;
					}
				}
				else
				{
					OnPanned(up);
				}
			}

			scrolling = false;
			lastPanArgs = null;
			LastPan = null;
			Start = null;

			return handled;
		}

		#endregion

		#region IMultiTouchGestureListener Members

		private PinchEventArgs previousPinchArgs;
		private RotateEventArgs previousRotateArgs;

		public bool OnMoving(MotionEvent current)
		{
			var handled = OnScroll(Start, current, 0, 0);

			OnPinching(current);
			OnRotating(current);

			return handled;
		}

		private void OnPinching(MotionEvent current)
		{
			var pinchArgs = new AndroidPinchEventArgs(current, previousPinchArgs, view);
			if (element.GestureHandler.HandlesPinching)
			{
				listener.OnPinching(pinchArgs);
				//handled |= pinchArgs.Handled;
			}
			previousPinchArgs = pinchArgs;
		}

		private void OnRotating(MotionEvent current)
		{
			var rotateArgs = new AndroidRotateEventArgs(current, previousRotateArgs, view);
			if (element.GestureHandler.HandlesRotating)
			{
				listener.OnRotating(rotateArgs);
				//handled |= rotateArgs.Handled;
			}
			previousRotateArgs = rotateArgs;
		}

		public bool OnMoved(MotionEvent current)
		{
			bool handled = false;

			OnPinched(current);
			OnRotated(current);

			return handled;
		}

		private void OnPinched(MotionEvent current)
		{
			if (previousPinchArgs != null)
			{
				if (element.GestureHandler.HandlesPinching || element.GestureHandler.HandlesPinched)
				{
					var pinchArgs = new AndroidPinchEventArgs(current, previousPinchArgs, view);
					listener.OnPinched(pinchArgs);
					//handled |= pinchArgs.Handled;
				}
				previousPinchArgs = null;
			}
		}

		private void OnRotated(MotionEvent current)
		{
			//System.Diagnostics.Debug.WriteLine("onRotated");
			if (previousRotateArgs != null)
			{
				if (element.GestureHandler.HandlesRotating || element.GestureHandler.HandlesRotated)
				{
					var rotateArgs = new AndroidRotateEventArgs(current, previousRotateArgs, view);
					listener.OnRotated(rotateArgs);
					//handled |= rotateArgs.Handled;
				}
				previousRotateArgs = null;
			}
		}

		#endregion
	}
}
