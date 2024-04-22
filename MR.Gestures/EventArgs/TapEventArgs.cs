namespace MR.Gestures
{
	/// <summary>
	/// The arguments for the <code>Tapping</code>, <code>Tapped</code> and <code>DoubleTapped</code> events.
	/// </summary>
	public class TapEventArgs : BaseGestureEventArgs
	{
		/// <summary>
		/// The number of taps in a short period of time (~250ms).
		/// </summary>
		public virtual int NumberOfTaps { get; protected set; }
	}
}
