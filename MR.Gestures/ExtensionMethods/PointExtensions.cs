namespace MR.Gestures
{
	public static class PointExtensions
	{
		/// <summary>
		/// Adds the coordinates of one Point to another.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static Point Add(this Point first, Point second)
		{
			return new Point(first.X + second.X, first.Y + second.Y);
		}

		/// <summary>
		/// Subtracts the coordinates of one Point from another.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static Point Subtract(this Point first, Point second)
		{
			return new Point(first.X - second.X, first.Y - second.Y);
		}

		/// <summary>
		/// Gets the center of some touch points.
		/// </summary>
		/// <param name="touches"></param>
		/// <returns></returns>
		public static Point Center(this Point[] touches)
		{
			var l = touches?.Length ?? 0;
			switch(l)
            {
				case 0: return Point.Zero;
				case 1: return touches[0];
				default:
					double x = 0, y = 0;
					for (int i = 0; i < l; i++)
					{
						x += touches[i].X;
						y += touches[i].Y;
					}
					return new Point(x / l, y / l);
			}
		}
	}
}
