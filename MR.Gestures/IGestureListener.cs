namespace MR.Gestures;

public interface IGestureListener
{
	bool OnDown(DownUpEventArgs args);
	bool OnUp(DownUpEventArgs args);
	bool OnTapping(TapEventArgs args);
	bool OnTapped(TapEventArgs args);
	bool OnDoubleTapped(TapEventArgs args);
	bool OnLongPressing(LongPressEventArgs args);
	bool OnLongPressed(LongPressEventArgs args);
	bool OnPanning(PanEventArgs args);
	bool OnPanned(PanEventArgs args);
	bool OnSwiped(SwipeEventArgs args);
	bool OnPinching(PinchEventArgs args);
	bool OnPinched(PinchEventArgs args);
	bool OnRotating(RotateEventArgs args);
	bool OnRotated(RotateEventArgs args);
	bool OnMouseEntered(MouseEventArgs args);
	bool OnMouseMoved(MouseEventArgs args);
	bool OnMouseExited(MouseEventArgs args);
	bool OnScrollWheelChanged(ScrollWheelEventArgs args);
}
