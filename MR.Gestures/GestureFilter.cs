namespace MR.Gestures;

/// <summary>
/// A simple filter so that events are only raised if Element.InputTransparent == false.
/// </summary>
internal class GestureFilter : IGestureListener
{
	#region Constructor

	VisualElement element;
	IGestureListener listener;

	bool RaiseEvent => element == null || !element.InputTransparent;

	public GestureFilter(IGestureAwareControl element, IGestureListener nextListener)
	{
		this.element = element as VisualElement;
		listener = nextListener;
	}

	#endregion

	public bool OnDown(DownUpEventArgs args) => RaiseEvent ? listener.OnDown(args) : false;
	public bool OnUp(DownUpEventArgs args) => RaiseEvent ? listener.OnUp(args) : false;
	public bool OnTapping(TapEventArgs args) => RaiseEvent ? listener.OnTapping(args) : false;
	public bool OnTapped(TapEventArgs args) => RaiseEvent ? listener.OnTapped(args) : false;
	public bool OnDoubleTapped(TapEventArgs args) => RaiseEvent ? listener.OnDoubleTapped(args) : false;
	public bool OnLongPressing(LongPressEventArgs args) => RaiseEvent ? listener.OnLongPressing(args) : false;
	public bool OnLongPressed(LongPressEventArgs args) => RaiseEvent ? listener.OnLongPressed(args) : false;
	public bool OnPanning(PanEventArgs args) => RaiseEvent ? listener.OnPanning(args) : false;
	public bool OnPanned(PanEventArgs args) => RaiseEvent ? listener.OnPanned(args) : false;
	public bool OnSwiped(SwipeEventArgs args) => RaiseEvent ? listener.OnSwiped(args) : false;
	public bool OnPinching(PinchEventArgs args) => RaiseEvent ? listener.OnPinching(args) : false;
	public bool OnPinched(PinchEventArgs args) => RaiseEvent ? listener.OnPinched(args) : false;
	public bool OnRotating(RotateEventArgs args) => RaiseEvent ? listener.OnRotating(args) : false;
	public bool OnRotated(RotateEventArgs args) => RaiseEvent ? listener.OnRotated(args) : false;
	public bool OnMouseEntered(MouseEventArgs args) => RaiseEvent ? listener.OnMouseEntered(args) : false;
	public bool OnMouseMoved(MouseEventArgs args) => RaiseEvent ? listener.OnMouseMoved(args) : false;
	public bool OnMouseExited(MouseEventArgs args) => RaiseEvent ? listener.OnMouseExited(args) : false;
	public bool OnScrollWheelChanged(ScrollWheelEventArgs args) => RaiseEvent ? listener.OnScrollWheelChanged(args) : false;
}
