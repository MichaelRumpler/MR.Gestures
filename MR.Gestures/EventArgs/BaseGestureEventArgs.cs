namespace MR.Gestures
{
    /// <summary>
    /// The base class for all gesture event args.
    /// </summary>
    public class BaseGestureEventArgs : EventArgs
	{
		/// <summary>
		/// This flag could be set to false to run other eventhandlers of the same or surrounding elements.
		/// It is not used on every platform! Default value is <value>true</value>.
		/// </summary>
		public bool Handled { get; set; } = true;

		/// <summary>
		/// The element which raised this event.
		/// </summary>
		public virtual IGestureAwareControl Sender { get; set; }

		/// <summary>
		/// Android and iOS sometimes cancel a touch gesture. In this case we raise a *ed event with Cancelled set to <value>true</value>.
		/// </summary>
		public virtual bool Cancelled { get; protected set; }

		/// <summary>
		/// The position of the <see cref="Sender"/> on the screen.
		/// </summary>
		public virtual Rect ViewPosition { get; protected set; }

		/// <summary>
		/// Returns the position of the fingers on the screen.
		/// </summary>
		public virtual Point[] Touches { get; protected set; }

		/// <summary>
		/// The number of fingers on the screen.
		/// </summary>
		public virtual int NumberOfTouches => Touches?.Length ?? 0;

		/// <summary>
		/// The sources of the coordinates in <see cref="Touches"/>. The same index in Touches and Sources refer to the same Point.
		/// </summary>
		public TouchSource[] Sources { get; protected set; }

		private Point center;
		/// <summary>
		/// The center of the fingers on the screen.
		/// </summary>
		public virtual Point Center
		{
			get
			{
				if (center.IsEmpty)
				{
					center = Touches.Center();
				}
				return center;
			}
			protected set { center = value; }
		}

		public override bool Equals(object obj) => (obj is BaseGestureEventArgs other) ? Equals(other) : false;

		public bool Equals(BaseGestureEventArgs other)
		{
			if (other == null)
				return false;

			if (Touches == null && other.Touches == null)
				return true;

			if (Touches.Length != other.Touches.Length)
				return false;

			for (int i = 0; i < Touches.Length; i++)
				if (!Touches[i].Equals(other.Touches[i]))
					return false;

			return true;
		}

		public override int GetHashCode() => Touches.GetHashCode();
	}
}
