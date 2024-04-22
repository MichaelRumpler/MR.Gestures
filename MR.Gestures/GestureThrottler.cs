namespace MR.Gestures;

/// <summary>
/// Only raises the events, if the finger(s) were moved more than Settings.MinimumDeltaDistance/MinimumDeltaAngle.
/// </summary>
public class GestureThrottler : IGestureListener
{
	#region Constructor

	IGestureListener listener;

	public GestureThrottler(IGestureListener nextListener)
	{
		listener = nextListener;
	}

	#endregion

	#region Events which just pass through

	public bool OnDown(DownUpEventArgs args) => listener.OnDown(args);
	public bool OnUp(DownUpEventArgs args) => listener.OnUp(args);
	public bool OnTapping(TapEventArgs args) => listener.OnTapping(args);
	public bool OnTapped(TapEventArgs args) => listener.OnTapped(args);
	public bool OnDoubleTapped(TapEventArgs args) => listener.OnDoubleTapped(args);
	public bool OnLongPressing(LongPressEventArgs args) => listener.OnLongPressing(args);
	public bool OnLongPressed(LongPressEventArgs args) => listener.OnLongPressed(args);

	public bool OnSwiped(SwipeEventArgs args)
	{
		lastPanArgs = null;
		return listener.OnSwiped(args);
	}

	public bool OnMouseEntered(MouseEventArgs args)
	{
		lastMouseArgs = args;
		return listener.OnMouseEntered(args);
	}

	public bool OnScrollWheelChanged(ScrollWheelEventArgs args) => listener.OnScrollWheelChanged(args);

	#endregion

	#region Throttled events

	PanEventArgs lastPanArgs = null;

	public bool OnPanning(PanEventArgs args)
	{
		var handled = false;

		if (lastPanArgs != null)
		{
			var newArgs = args.Diff(lastPanArgs);
			if (Math.Abs(newArgs.DeltaDistance.X) > Settings.MinimumDeltaDistance || Math.Abs(newArgs.DeltaDistance.Y) > Settings.MinimumDeltaDistance)
				args = newArgs;
			else
				args = null;
		}

		if (args != null)
		{
			handled = listener.OnPanning(args);
			lastPanArgs = args;
		}

		return handled;
	}

	public bool OnPanned(PanEventArgs args)
	{
		if (lastPanArgs != null)
		{
			args = args.Diff(lastPanArgs);
			lastPanArgs = null;
		}

		return listener.OnPanned(args);
	}

	PinchEventArgs lastPinchArgs = null;

	public bool OnPinching(PinchEventArgs args)
	{
		var handled = false;

		if (lastPinchArgs != null)
		{
			var newArgs = args.Diff(lastPinchArgs);
			if (Math.Abs(newArgs.DeltaScale - 1) > Settings.MinimumDeltaScale)
				args = newArgs;
			else
				args = null;
		}

		if (args != null)
		{
			handled = listener.OnPinching(args);
			lastPinchArgs = args;
		}

		return handled;
	}

	public bool OnPinched(PinchEventArgs args)
	{
		if (lastPinchArgs != null)
		{
			args = args.Diff(lastPinchArgs);
			lastPinchArgs = null;
		}

		return listener.OnPinched(args);
	}

	RotateEventArgs lastRotateArgs;

	public bool OnRotating(RotateEventArgs args)
	{
		var handled = false;

		if (lastRotateArgs != null)
		{
			var newArgs = args.Diff(lastRotateArgs);
			if (Math.Abs(newArgs.DeltaAngle) > Settings.MinimumDeltaAngle)
				args = newArgs;
			else
				args = null;
		}

		if (args != null)
		{
			handled = listener.OnRotating(args);
			lastRotateArgs = args;
		}

		return handled;
	}

	public bool OnRotated(RotateEventArgs args)
	{
		if (lastRotateArgs != null)
		{
			args = args.Diff(lastRotateArgs);
			lastRotateArgs = null;
		}

		return listener.OnRotated(args);
	}

	MouseEventArgs lastMouseArgs = null;

	public bool OnMouseMoved(MouseEventArgs args)
	{
		var handled = false;

		if (lastMouseArgs != null)
		{
			var newArgs = args.Diff(lastMouseArgs);
			if (Math.Abs(newArgs.DeltaDistance.X) > Settings.MinimumMouseDeltaDistance || Math.Abs(newArgs.DeltaDistance.Y) > Settings.MinimumMouseDeltaDistance)
				args = newArgs;
			else
				args = null;
		}

		if (args != null)
		{
			handled = listener.OnMouseMoved(args);
			lastMouseArgs = args;
		}

		return handled;
	}

	public bool OnMouseExited(MouseEventArgs args)
	{
		if (lastMouseArgs != null)
		{
			args = args.Diff(lastMouseArgs);
			lastMouseArgs = null;
		}

		return listener.OnMouseExited(args);
	}

	#endregion
}
