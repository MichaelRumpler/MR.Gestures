namespace MR.Gestures
{
	/// <summary>
	/// The arguments for the <code>Rotating</code> and <code>Rotated</code> events.
	/// </summary>
	public class RotateEventArgs : BaseGestureEventArgs
	{
		/// <summary>
		/// The angle a line from the first to the second finger currently has on the screen.
		/// </summary>
		public virtual double Angle { get; protected set; }
		/// <summary>
		/// The angle the fingers were rotated on the screen compared to the last time this event was raised.
		/// </summary>
		public virtual double DeltaAngle { get; protected set; }
		/// <summary>
		/// The angle the fingers were rotated on the screen compared to the start of the gesture.
		/// </summary>
		public virtual double TotalAngle { get; protected set; }

		protected void CalculateAngles(RotateEventArgs previous)
		{
			if (Touches.Length > 1)
				Angle = GetAngle();
			else if (previous != null)
				Angle = previous.Angle;
			else
				Angle = 0;

			if(previous == null)
			{
				DeltaAngle = 0;
				TotalAngle = 0;
			}
			else if (Touches.Length != previous.Touches.Length)
			{
				DeltaAngle = 0;		// if a finger has been removed, it could be finger 1 or 2 in which case I cannot calculate anything => set delta to 0
				TotalAngle = previous.TotalAngle;
			}
			else
			{
				DeltaAngle = Angle - previous.Angle;
				TotalAngle = previous.TotalAngle + DeltaAngle;
			}
		}

		private double GetAngle()
		{
			var deltaX = Touches[1].X - Touches[0].X;
			var deltaY = Touches[1].Y - Touches[0].Y;

			double radians = Math.Atan2(deltaY, deltaX);
			double degrees = radians * 180 / Math.PI;
			return degrees;
		}

		internal RotateEventArgs Diff(RotateEventArgs lastArgs)
		{
			var newArgs = new RotateEventArgs()
			{
				Cancelled = Cancelled,
				Handled = Handled,
				ViewPosition = ViewPosition,
				Touches = Touches,
				Sources = Sources,
				Sender = Sender,

				Angle = Angle,
				TotalAngle = TotalAngle,
				DeltaAngle = TotalAngle - lastArgs.TotalAngle,
			};
			return newArgs;
		}
	}
}
