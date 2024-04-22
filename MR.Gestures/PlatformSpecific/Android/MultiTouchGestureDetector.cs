using Android.Content;
using Android.Views;


namespace MR.Gestures.Android
{
	internal class MultiTouchGestureDetector
	{
		protected readonly Context Context;
		protected readonly IMultiTouchGestureListener Listener;

		protected readonly int touchSlopSquare;

		private bool gestureInProgress = false;
		public bool IsInProgress
		{
			get { return gestureInProgress; }
		}

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

		internal MultiTouchGestureDetector(Context context, IMultiTouchGestureListener listener)
		{
			Context = context;
			Listener = listener;
			touchSlopSquare = ViewConfiguration.Get(Context).ScaledTouchSlop;
			touchSlopSquare = touchSlopSquare * touchSlopSquare;
		}

		public bool OnTouchEvent(MotionEvent e)
		{
			var handled = false;

			if (e.PointerCount >= 2)
			{
				switch (e.ActionMasked)
				{
					case MotionEventActions.PointerDown:
						// we have the second finger on the screen. now start listening for the movements.
						//Console.WriteLine("MultiTouchGestureDetector: " + e.Action);
						if(e.PointerCount == 2)
							Start = e;
						break;

					case MotionEventActions.Move:
						//Console.WriteLine("MultiTouchGestureDetector: " + e.Action);
						if (Start == null)		// Pointer2Down did not come
							Start = e;			// so this is the first motion event
						if (!gestureInProgress)
						{
							// check if any finger moved far enough to start gesture
							for (int i = 0; i < e.PointerCount; i++)
							{
								int li = Start.FindPointerIndex(e.GetPointerId(i));
								if (li >= 0)
								{
									var diffX = Start.GetX(li) - e.GetX(i);
									var diffY = Start.GetY(li) - e.GetY(i);

									if (diffX * diffX + diffY * diffY > touchSlopSquare)
									{
										gestureInProgress = true;
									}
								}
							}
						}

						if (gestureInProgress)
						{
							handled = Listener.OnMoving(e);
						}
						break;

					case MotionEventActions.PointerUp:
					case MotionEventActions.Cancel:
						handled = EndGesture(e);
						break;
				}
			}
			else
				handled = EndGesture(e);

			return handled;
		}

		private bool EndGesture(MotionEvent e)
		{
			bool handled = false;

			if (gestureInProgress)
			{
				//Console.WriteLine("MultiTouchGestureDetector.EndGesture: " + e.Action + ", PointerCount=" + e.PointerCount);
				handled = Listener.OnMoved(e);
			}

			Start = null;
			gestureInProgress = false;
			return handled;
		}
	}
}
