namespace MR.Gestures.WinUI
{
	public static class Settings
	{
		/// <summary>
		/// The velocity of a lifted finger must be at least this value to count as Swipe.
		/// The velocity comes from ManipulationCompletedRoutedEventArgs.Velocities.Linear and is in device-independent pixel per millisecond.
		/// The default value is 1. Set it to a higher value if you want the user to move faster.
		/// </summary>
		public static double SwipeVelocityThreshold { get; set; } = 1;
	}
}
