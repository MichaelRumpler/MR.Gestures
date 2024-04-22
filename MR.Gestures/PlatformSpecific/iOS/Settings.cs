namespace MR.Gestures.iOS
{
	public static class Settings
	{
		/// <summary>
		/// The velocity (UIPanGestureRecognizer.VelocityInView) of a lifted finger in X or Y direction must be at least this value to count as Swipe.
		/// The default value is 800/800. Set it to a higher value if you want the user to move faster.
		/// </summary>
		public static Point SwipeVelocityThreshold { get; set; } = new Point(800, 800);
	}
}