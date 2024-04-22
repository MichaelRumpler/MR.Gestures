using System.Windows.Input;

namespace MR.Gestures;

public interface IGestureAwareControl : IElement
{
	/// <summary>
	/// The object which handles all the gestures.
	/// </summary>
	GestureHandler GestureHandler { get; }

	#region Down

	/// <summary>
	/// The event which is raised when a finger comes down. The finger(s) is/are still on the touch screen.
	/// </summary>
	event EventHandler<DownUpEventArgs> Down;

	/// <summary>
	/// Gets or sets the command which is executed when a finger comes down. This is a bindable property.
	/// </summary>
	ICommand DownCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the DownCommand. This is a bindable property.
	/// </summary>
	object DownCommandParameter { get; set; }

	#endregion

	#region Up

	/// <summary>
	/// The event which is raised when a finger is lifted from the touch screen. There may be other fingers still on it.
	/// </summary>
	event EventHandler<DownUpEventArgs> Up;

	/// <summary>
	/// Gets or sets the command which is executed when a finger is liftet from the touch screen. This is a bindable property.
	/// </summary>
	ICommand UpCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the UpCommand. This is a bindable property.
	/// </summary>
	object UpCommandParameter { get; set; }

	#endregion

	#region Tapping

	/// <summary>
	/// The event which is raised when a finger comes down and up again.
	/// </summary>
	event EventHandler<TapEventArgs> Tapping;

	/// <summary>
	/// Gets or sets the command which is executed when the element is tapped and another tap may follow. This is a bindable property.
	/// </summary>
	ICommand TappingCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the TappingCommand. This is a bindable property.
	/// </summary>
	object TappingCommandParameter { get; set; }

	#endregion

	#region Tapped

	/// <summary>
	/// The event which is raised when no other touch gesture followed Tapping within 250ms.
	/// </summary>
	event EventHandler<TapEventArgs> Tapped;

	/// <summary>
	/// Gets or sets the command which is executed when the element is tapped. This is a bindable property.
	/// </summary>
	ICommand TappedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the TappedCommand. This is a bindable property.
	/// </summary>
	object TappedCommandParameter { get; set; }

	#endregion

	#region DoubleTapped

	/// <summary>
	/// The event which is raised when two Tapping events came within ~250ms.
	/// </summary>
	event EventHandler<TapEventArgs> DoubleTapped;

	/// <summary>
	/// Gets or sets the command which is executed when the element is tapped twice. This is a bindable property.
	/// </summary>
	ICommand DoubleTappedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the DoubleTappedCommand. This is a bindable property.
	/// </summary>
	object DoubleTappedCommandParameter { get; set; }

	#endregion

	#region LongPressing

	/// <summary>
	/// The event which is raised when a finger comes down and stays there.
	/// </summary>
	event EventHandler<LongPressEventArgs> LongPressing;

	/// <summary>
	/// Gets or sets the command which is executed when the element is pressed long. This is a bindable property.
	/// </summary>
	ICommand LongPressingCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the LongPressingCommand. This is a bindable property.
	/// </summary>
	object LongPressingCommandParameter { get; set; }

	#endregion

	#region LongPressed

	/// <summary>
	/// The event which is raised when a finger finally comes up after a LongPressing event.
	/// </summary>
	event EventHandler<LongPressEventArgs> LongPressed;

	/// <summary>
	/// Gets or sets the command which is executed when the LongPressed event is raised. This is a bindable property.
	/// </summary>
	ICommand LongPressedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the LongPressedCommand. This is a bindable property.
	/// </summary>
	object LongPressedCommandParameter { get; set; }

	#endregion

	#region Pinching

	/// <summary>
	/// The event which is raised when two fingers are moved together or away from each other.
	/// </summary>
	event EventHandler<PinchEventArgs> Pinching;

	/// <summary>
	/// Gets or sets the command which is executed when the element is pinched. This is a bindable property.
	/// </summary>
	ICommand PinchingCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the PinchingCommand. This is a bindable property.
	/// </summary>
	object PinchingCommandParameter { get; set; }

	#endregion

	#region Pinched

	/// <summary>
	/// The event which is raised when at least one finger is released after a Pinching event.
	/// </summary>
	event EventHandler<PinchEventArgs> Pinched;

	/// <summary>
	/// Gets or sets the command which is executed when the Pinched event is raised. This is a bindable property.
	/// </summary>
	ICommand PinchedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the PinchedCommand. This is a bindable property.
	/// </summary>
	object PinchedCommandParameter { get; set; }

	#endregion

	#region Panning

	/// <summary>
	/// The event which is raised when a finger comes down and then moves in any direction.
	/// </summary>
	event EventHandler<PanEventArgs> Panning;

	/// <summary>
	/// Gets or sets the command which is executed when the element is panned. This is a bindable property.
	/// </summary>
	ICommand PanningCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the PanningCommand. This is a bindable property.
	/// </summary>
	object PanningCommandParameter { get; set; }

	#endregion

	#region Panned

	/// <summary>
	/// The event which is raised when a finger comes up after a Panning event.
	/// </summary>
	event EventHandler<PanEventArgs> Panned;

	/// <summary>
	/// Gets or sets the command which is executed when the element was panned. This is a bindable property.
	/// </summary>
	ICommand PannedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the PannedCommand. This is a bindable property.
	/// </summary>
	object PannedCommandParameter { get; set; }

	#endregion

	#region Swiped

	/// <summary>
	/// The event which is raised when a finger comes down, is moved and still moves when it is raised again.
	/// </summary>
	event EventHandler<SwipeEventArgs> Swiped;

	/// <summary>
	/// Gets or sets the command which is executed when the element is swiped. This is a bindable property.
	/// </summary>
	ICommand SwipedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the SwipedCommand. This is a bindable property.
	/// </summary>
	object SwipedCommandParameter { get; set; }

	#endregion

	#region Rotating

	/// <summary>
	/// The event which is raised when two fingers come down and their angle is changed.
	/// </summary>
	event EventHandler<RotateEventArgs> Rotating;

	/// <summary>
	/// Gets or sets the command which is executed when the element is rotated. This is a bindable property.
	/// </summary>
	ICommand RotatingCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the RotatingCommand. This is a bindable property.
	/// </summary>
	object RotatingCommandParameter { get; set; }

	#endregion

	#region Rotated

	/// <summary>
	/// The event which is raised when at least one finger is released after a Rotating event.
	/// </summary>
	event EventHandler<RotateEventArgs> Rotated;

	/// <summary>
	/// Gets or sets the command which is executed when the element was rotated. This is a bindable property.
	/// </summary>
	ICommand RotatedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the RotatedCommand. This is a bindable property.
	/// </summary>
	object RotatedCommandParameter { get; set; }

	#endregion

	#region MouseEntered

	/// <summary>
	/// The event which is raised when the mouse pointer enters the area of an element.
	/// </summary>
	event EventHandler<MouseEventArgs> MouseEntered;

	/// <summary>
	/// Gets or sets the command which is executed when the mouse entered the elements area. This is a bindable property.
	/// </summary>
	ICommand MouseEnteredCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the MouseEnteredCommand. This is a bindable property.
	/// </summary>
	object MouseEnteredCommandParameter { get; set; }

	#endregion

	#region MouseMoved

	/// <summary>
	/// The event which is raised when the mouse is moved around over an element.
	/// </summary>
	event EventHandler<MouseEventArgs> MouseMoved;

	/// <summary>
	/// Gets or sets the command which is executed when the mouse moved over the elements area. This is a bindable property.
	/// </summary>
	ICommand MouseMovedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the MouseMovedCommand. This is a bindable property.
	/// </summary>
	object MouseMovedCommandParameter { get; set; }

	#endregion

	#region MouseExited

	/// <summary>
	/// The event which is raised when the mouse is moved away from an element.
	/// </summary>
	event EventHandler<MouseEventArgs> MouseExited;

	/// <summary>
	/// Gets or sets the command which is executed when the mouse moved off the elements area. This is a bindable property.
	/// </summary>
	ICommand MouseExitedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the MouseExitedCommand. This is a bindable property.
	/// </summary>
	object MouseExitedCommandParameter { get; set; }

	#endregion

	#region ScrollWheelChanged

	/// <summary>
	/// The event which is raised when the mouse wheel changed.
	/// </summary>
	event EventHandler<ScrollWheelEventArgs> ScrollWheelChanged;

	/// <summary>
	/// Gets or sets the command which is executed when the mouse wheel changed. This is a bindable property.
	/// </summary>
	ICommand ScrollWheelChangedCommand { get; set; }

	/// <summary>
	/// Gets or sets the parameter to pass to the ScrollWheelChangedCommand. This is a bindable property.
	/// </summary>
	object ScrollWheelChangedCommandParameter { get; set; }

	#endregion
}
