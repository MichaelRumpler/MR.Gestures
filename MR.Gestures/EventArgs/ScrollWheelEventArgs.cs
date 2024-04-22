namespace MR.Gestures
{
    /// <summary>
    /// The arguments for the <code>ScrollWheelChanged</code> event.
    /// </summary>
    public class ScrollWheelEventArgs : BaseGestureEventArgs
	{
        /// <summary>
        /// Determines the direction in which the mouse wheel has been moved.
        /// </summary>
        public Point ScrollDelta { get; protected set; }
    }
}
