namespace MR.Gestures
{
	/// <summary>
	/// The arguments for the <code>Down</code> and <code>Up</code> events.
	/// </summary>
	public class DownUpEventArgs : BaseGestureEventArgs
	{
		/// <summary>
		/// The touches which triggered the Down or Up event.
		/// </summary>
		public virtual int[] TriggeringTouches { get; protected set; }
	}
}
