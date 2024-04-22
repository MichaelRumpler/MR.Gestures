using Android.Views;

namespace MR.Gestures.Android
{
	internal class MouseGestureDetector
	{
		protected readonly MouseGestureListener Listener;
		private long lastUpTime = 0;

		internal MouseGestureDetector(MouseGestureListener listener)
		{
			Listener = listener;
		}

		public bool OnGenericMotionEvent(MotionEvent e)
		{
			var handled = false;

			switch (e.ActionMasked)
			{
				case MotionEventActions.Down:               // Down				comes for the first contact on the touchscreen/mouse
					if (e.Source != InputSourceType.Mouse)  // with the mouse, Down will be followed by ButtonPress and handled there
						handled = Listener.OnDown(e);
					break;

				case MotionEventActions.PointerDown:        // PointerDown		comes for every following touch on a multi touch screen
#pragma warning disable CA1416 // Validate platform compatibility - this is only supported from Android 23 but the library is available on Andoir 21.
				case MotionEventActions.ButtonPress:        // ButtonPress		is sent on a mouse button, but after Down
#pragma warning restore CA1416 // Validate platform compatibility
					handled = Listener.OnDown(e);
					break;

				case MotionEventActions.Up:					// Up				is sent last when the last contact is raised from the touchscreen
				case MotionEventActions.PointerUp:          // PointerUp		comes for every 2+ finger on the multi touch screen
#pragma warning disable CA1416 // Validate platform compatibility - this is only supported from Android 23 but the library is available on Andoir 21.
				case MotionEventActions.ButtonRelease:      // ButtonRelease	is sent when a mousebutton is released - before Up and here ActionButton should be set, so I need to raise Up from here
#pragma warning restore CA1416 // Validate platform compatibility
				case MotionEventActions.Cancel:             // Cancel			cancels any gestures
					if (e.EventTime != lastUpTime)
					{
						handled = Listener.OnUp(e);
						lastUpTime = e.EventTime;
					}
					break;

				case MotionEventActions.HoverEnter:
					handled = Listener.OnMouseEntered(e);
					break;

				case MotionEventActions.HoverMove:
					handled = Listener.OnMouseMoved(e);
					break;

				case MotionEventActions.HoverExit:
					handled = Listener.OnMouseExited(e);
					break;

				case MotionEventActions.Scroll:
					handled = Listener.OnScrollWheelChanged(e);
					break;
			}

			return handled;
		}
	}
}
