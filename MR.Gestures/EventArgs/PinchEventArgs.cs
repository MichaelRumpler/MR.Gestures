namespace MR.Gestures
{
	/// <summary>
	/// The arguments for the <code>Pinching</code> and <code>Pinched</code> events.
	/// </summary>
	public class PinchEventArgs : BaseGestureEventArgs
	{
		/// <summary>
		/// The distance between the first two fingers.
		/// </summary>
		public virtual double Distance { get; protected set; }
		/// <summary>
		/// The relative distance between the two fingers on the screen compared to the last time this event was raised.
		/// </summary>
		public virtual double DeltaScale { get; protected set; }
		/// <summary>
		/// The relative distance between the two fingers on the screen compared to when the gesture started.
		/// </summary>
		public virtual double TotalScale { get; protected set; }

		/// <summary>
		/// The X distance between the first two fingers.
		/// </summary>
		public virtual double DistanceX { get; protected set; }
		/// <summary>
		/// The relative X distance between the two fingers on the screen compared to the last time this event was raised.
		/// </summary>
		public virtual double DeltaScaleX { get; protected set; }
		/// <summary>
		/// The relative X distance between the two fingers on the screen compared to when the gesture started.
		/// </summary>
		public virtual double TotalScaleX { get; protected set; }

		/// <summary>
		/// The Y distance between the first two fingers.
		/// </summary>
		public virtual double DistanceY { get; protected set; }
		/// <summary>
		/// The relative Y distance between the two fingers on the screen compared to the last time this event was raised.
		/// </summary>
		public virtual double DeltaScaleY { get; protected set; }
		/// <summary>
		/// The relative Y distance between the two fingers on the screen compared to when the gesture started.
		/// </summary>
		public virtual double TotalScaleY { get; protected set; }

		protected void CalculateScales(PinchEventArgs previous)
		{
			// calculate Distance
			if (Touches.Length > 1)
			{
				DistanceX = Touches[1].X - Touches[0].X;
				DistanceY = Touches[1].Y - Touches[0].Y;
				Distance = Math.Sqrt(DistanceX * DistanceX + DistanceY * DistanceY);
			}
			else if (previous != null)
			{
				Distance = previous.Distance;
				DistanceX = previous.DistanceX;
				DistanceY = previous.DistanceY;
			}
			else
			{
				Distance = DistanceX = DistanceY = 0.0;
			}

			// calculate Scale
			if (previous == null)
			{
				DeltaScale = DeltaScaleX = DeltaScaleY = 1.0;
				TotalScale = TotalScaleX = TotalScaleY = 1.0;
			}
			else if (Touches.Length != previous.Touches.Length)
			{
				// if a finger has been removed, it could be finger 1 or 2 in which case I cannot calculate anything => set delta to 1
				DeltaScale = DeltaScaleX = DeltaScaleY = 1.0;
				TotalScale = previous.TotalScale;
				TotalScaleX = previous.TotalScaleX;
				TotalScaleY = previous.TotalScaleY;
			}
			else
			{
				CalculateScale(Distance, previous.Distance, previous.TotalScale, out var delta, out var total);
				DeltaScale = delta; TotalScale = total;
				CalculateScale(DistanceX, previous.DistanceX, previous.TotalScaleX, out delta, out total);
				DeltaScaleX = delta; TotalScaleX = total;
				CalculateScale(DistanceY, previous.DistanceY, previous.TotalScaleY, out delta, out total);
				DeltaScaleY = delta; TotalScaleY = total;
			}
		}

		private void CalculateScale(double distance, double previousDistance, double previousTotalScale, out double delta, out double total)
		{
			if (distance < 0) distance = -distance;
			if (previousDistance < 0) previousDistance = -previousDistance;

			delta = distance < Settings.MinimumDistanceForScale || previousDistance < Settings.MinimumDistanceForScale
				? 1.0
				: distance / previousDistance;

			total = previousTotalScale * delta;
		}

		internal PinchEventArgs Diff(PinchEventArgs lastArgs)
		{
			var newArgs = new PinchEventArgs()
			{
				Cancelled = Cancelled,
				Handled = Handled,
				ViewPosition = ViewPosition,
				Touches = Touches,
				Sources = Sources,
				Sender = Sender,

				Distance = Distance,
				DistanceX = DistanceX,
				DistanceY = DistanceY,
				TotalScale = TotalScale,
				TotalScaleX = TotalScaleX,
				TotalScaleY = TotalScaleY,
				DeltaScale = TotalScale / lastArgs.TotalScale,
				DeltaScaleX = TotalScaleX / lastArgs.TotalScaleX,
				DeltaScaleY = TotalScaleY / lastArgs.TotalScaleY,
			};
			return newArgs;
		}
	}
}
