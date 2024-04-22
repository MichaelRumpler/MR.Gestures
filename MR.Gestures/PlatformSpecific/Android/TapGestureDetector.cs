using Android.Content;
using Android.Views;

namespace MR.Gestures.Android
{
	internal class TapGestureDetector
	{
		protected readonly ITapGestureListener Listener;

		protected readonly int touchSlopSquare;             // Distance in pixels a touch can wander before we think the user is scrolling ^2
        protected readonly int longPressTimeout;            // the duration in milliseconds before a press turns into a long press

        private Dictionary<int, Point> touches = new Dictionary<int, Point>();
		private long lastdown = 0;
		private bool ignoreThisGesture = false;
		private bool isLongPressing = false;
        CancellationTokenSource cancelLongPress;


        internal TapGestureDetector(Context context, ITapGestureListener listener)
		{
			Listener = listener;
			var touchSlop = ViewConfiguration.Get(context).ScaledTouchSlop;
			touchSlopSquare = touchSlop * touchSlop;
			longPressTimeout = ViewConfiguration.LongPressTimeout;
		}

		public bool OnTouchEvent(MotionEvent e)
		{
			var handled = false;

			switch (e.ActionMasked)
			{
				case MotionEventActions.Down:
                    //Console.WriteLine("LongPressGestureDetector: " + e.Action);

                    if (cancelLongPress != null)
                    {
                        try { cancelLongPress.Cancel(); }
                        catch { }
                    }

                    if (e.PointerCount == 1)
					{
						// first finger down -> start gesture
						ignoreThisGesture = false;
						isLongPressing = false;
						touches.Clear();
					}

					for (int i = 0; i < e.PointerCount; i++)
					{
						var id = e.GetPointerId(i);
						if (!touches.ContainsKey(id))
						{
							lastdown = e.EventTime;
							touches.Add(id, new Point(e.GetX(i), e.GetY(i)));
						}
					}

                    cancelLongPress = new CancellationTokenSource();
                    Task.Run(async () =>
                    {
                        await Task.Delay(longPressTimeout, cancelLongPress.Token).ConfigureAwait(false);
                        cancelLongPress = null;         // I cannot be cancelled anymore

                        if (lastdown > 0)
                        {
                            isLongPressing = true;
                            lastdown = 0;
							Listener.OnLongPressing(touches.Values.ToArray(), e, longPressTimeout);     // raise LongPressing
                        }

                    }, cancelLongPress.Token);

                    break;

				case MotionEventActions.Move:
                    //Console.WriteLine("LongPressGestureDetector: " + e.Action);
                    //Console.WriteLine($"Moving: ignore = {ignoreThisGesture}, e.EventTime = {e.EventTime}, lastdown = {lastdown}, delta = {e.EventTime - lastdown}");

                    if (!ignoreThisGesture)
					{
						if (AnyPointerMoved(e))
						{
							handled = EndGesture(e, true);        // if isLongPressing -> cancel longpressed, tapping&tapped
						}
						else
						{
							if (lastdown > 0 && (e.EventTime - lastdown > longPressTimeout))
							{
                                if (cancelLongPress != null)
                                {
                                    try { cancelLongPress.Cancel(); }
                                    catch { }
                                }

                                isLongPressing = true;
								lastdown = 0;
								Listener.OnLongPressing(e);
							}
						}
					}

					break;

				case MotionEventActions.Up:
				case MotionEventActions.PointerUp:
#pragma warning disable CA1416 // Validate platform compatibility - this is only supported from Android 23 but the library is available on Android 21.
				case MotionEventActions.ButtonRelease:
#pragma warning restore CA1416 // Validate platform compatibility
				case MotionEventActions.Cancel:
					handled = EndGesture(e, e.ActionMasked == MotionEventActions.Cancel);
					break;
			}

			return handled;
		}

		private bool EndGesture(MotionEvent e, bool cancel = false)
		{
            if (cancelLongPress != null)
            {
                try { cancelLongPress.Cancel(); }
                catch { }
            }

            bool handled = false;

			if (isLongPressing)
			{
				//Console.WriteLine("LongPressGestureDetector.EndGesture: " + e.Action + ", PointerCount=" + e.PointerCount);
				Listener.OnLongPressed(e, cancel);
			}
			else if (lastdown > 0 && !cancel)
			{
				handled = Listener.OnTapping(e);
			}

			ignoreThisGesture = true;
			isLongPressing = false;
			touches.Clear();
			lastdown = 0;

			return handled;
		}

		private bool AnyPointerMoved(MotionEvent e)
		{
			for (int i = 0; i < e.PointerCount; i++)
			{
				var id = e.GetPointerId(i);
				if (!touches.ContainsKey(id))
				{
					lastdown = e.EventTime;
					touches.Add(id, new Point(e.GetX(i), e.GetY(i)));
				}
				else
				{
					var pos = touches[id];
					var diffX = pos.X - e.GetX(i);
					var diffY = pos.Y - e.GetY(i);

					if (diffX * diffX + diffY * diffY > touchSlopSquare)
						return true;
				}
			}

			return false;
		}
	}
}
