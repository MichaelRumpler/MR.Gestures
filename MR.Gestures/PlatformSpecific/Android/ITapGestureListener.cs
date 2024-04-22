using Android.Views;

namespace MR.Gestures.Android
{
	interface ITapGestureListener
	{
		bool OnTapping(MotionEvent current);
		bool OnLongPressing(MotionEvent current);
        bool OnLongPressing(IEnumerable<Point> touches, MotionEvent start, long duration);

        bool OnLongPressed(MotionEvent current, bool cancel);
	}
}
