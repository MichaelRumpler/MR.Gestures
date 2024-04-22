namespace MR.Gestures.Android
{
	public static class Settings
	{
		/// <summary>
		/// The velocity of a lifted finger relative to the ScaledMaximumFlingVelocity must be at least this value to count as Swipe.
		/// The default value is 0.1. Set it to a higher value if you want the user to move faster.
		/// </summary>
		/// <remarks>The value must be between 0.0 and 1.0.</remarks>
		public static float SwipeVelocityThreshold { get; set; } = 0.1F;
	}
}
