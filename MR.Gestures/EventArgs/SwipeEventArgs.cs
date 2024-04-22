namespace MR.Gestures
{
	/// <summary>
	/// The direction or <value>NotClear</value> if the direction is not without any doubt.
	/// </summary>
	public enum Direction
	{
		/// <summary>
		/// The swipe gesture was not in a clear direction.
		/// </summary>
		NotClear,
		Left,
		Right,
		Up,
		Down
	}

	/// <summary>
	/// The argument for the <code>Swiped</code> event.
	/// </summary>
	public class SwipeEventArgs : PanEventArgs
	{
		/// <summary>
		/// The direction in which the finger moved when it was lifted from the screen.
		/// </summary>
		public Direction Direction { get; protected set; }

		public SwipeEventArgs()
		{
		}

		public SwipeEventArgs(PanEventArgs panArgs, Direction direction)
		{
			Cancelled = panArgs.Cancelled;
			ViewPosition = panArgs.ViewPosition;
			Touches = panArgs.Touches;
			Sources = panArgs.Sources;

			DeltaDistance = panArgs.DeltaDistance;
			TotalDistance = panArgs.TotalDistance;
			Velocity = panArgs.Velocity;

			Direction = direction;
		}
	}
}
