namespace MR.Gestures
{
    /// <summary>
    /// The arguments for the <code>MouseEntered</code>, <code>MouseMoved</code> and <code>MouseExited</code> events.
    /// </summary>
    public class MouseEventArgs : BaseGestureEventArgs
	{
		/// <summary>
		/// The distance in X/Y that the mouse was moved since the last mouse event was raised.
		/// </summary>
		public virtual Point DeltaDistance { get; protected set; }
		/// <summary>
		/// The distance in X/Y that the mouse was moved since the mouse gesture began.
		/// </summary>
		public virtual Point TotalDistance { get; protected set; }
		/// <summary>
		/// The velocity of the mouse in X/Y.
		/// </summary>
		public virtual Point Velocity { get; protected set; }


		protected void CalculateDistances(BaseGestureEventArgs previous)
		{
			MouseEventArgs previousPan = previous as MouseEventArgs;

			if (previous == null)
			{
				DeltaDistance = Point.Zero;
				TotalDistance = Point.Zero;
			}
			else if (Touches.Length != previous.Touches.Length)
			{
				DeltaDistance = Point.Zero;
				TotalDistance = previousPan?.TotalDistance ?? Point.Zero;
			}
			else
			{
				DeltaDistance = Center.Subtract(previous.Center);
				TotalDistance = previousPan?.TotalDistance.Add(DeltaDistance) ?? DeltaDistance;
			}
		}

		internal MouseEventArgs Diff(MouseEventArgs lastArgs)
		{
			var newArgs = new MouseEventArgs()
			{
				Cancelled = Cancelled,
				Handled = Handled,
				ViewPosition = ViewPosition,
				Touches = Touches,
				Sources = Sources,
				Sender = Sender,

				Velocity = Velocity,
				TotalDistance = TotalDistance,
				DeltaDistance = TotalDistance.Subtract(lastArgs.TotalDistance),
			};
			return newArgs;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			var other = obj as MouseEventArgs;
			if (other == null)
				return false;
			return Equals(other);
		}

		public bool Equals(MouseEventArgs other)
		{
			if (other == null)
				return false;

			if (!DeltaDistance.Equals(other.DeltaDistance))
				return false;

			if (!TotalDistance.Equals(other.TotalDistance))
				return false;

			if (!Velocity.Equals(other.Velocity))
				return false;

			return base.Equals(other);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ DeltaDistance.GetHashCode() ^ TotalDistance.GetHashCode() ^ Velocity.GetHashCode();
		}
	}
}
