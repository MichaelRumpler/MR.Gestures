namespace MR.Gestures
{
	/// <summary>
	/// The arguments for the <code>LongPressing</code> and <code>LongPressed</code> events.
	/// </summary>
	public class LongPressEventArgs : TapEventArgs
	{
		/// <summary>
		/// Duration of long press in milliseconds.
		/// </summary>
		public virtual long Duration { get; protected set; }
	}
}
