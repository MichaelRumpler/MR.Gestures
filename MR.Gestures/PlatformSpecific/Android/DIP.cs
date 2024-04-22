namespace MR.Gestures.Android
{
	/// <summary>
	/// Calculator to help calculate Device Independant Pixels to real pixels.
	/// </summary>
	public static class DIP
	{
		// DIPs are not used in Xamarin Forms 1.2.
		// See https://forums.xamarin.com/discussion/28131/absolutelayout-layoutbounds-units#latest

		public static readonly float Density = global::Android.App.Application.Context.Resources.DisplayMetrics.Density;

		public static Point ToPoint(double dipX, double dipY)
		{
			return new Point(dipX / Density, dipY / Density);
			//return new Point(dipX * 160.0 / Xdpi, dipY * 160.0 / Ydpi);
		}

		public static Rect ToRect(double dipX, double dipY, double dipWidth, double dipHeight)
		{
			return new Rect(dipX / Density, dipY / Density, dipWidth / Density, dipHeight / Density);
			//return new Rect(dipX * 160.0 / Xdpi, dipY * 160.0 / Ydpi, dipWidth * 160.0 / Xdpi, dipHeight * 160.0 / Ydpi);
		}
	}
}
