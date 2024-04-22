namespace MR.Gestures
{
    /// <summary>
    /// The source of some coordinates in <see cref="BaseGestureEventArgs.Touches"/>.
    /// </summary>
    [Flags]
	public enum TouchSource
    {
		Unknown = 0,
		Touchscreen = 1,
		LeftMouseButton = 2,
		RightMouseButton = 4,
		MiddleMouseButton = 8,
		Touchpad = 16,
		XButton1 = 32,
		XButton2 = 64,
		Pen = 128,
		MousePointer = 256,
    }
}
