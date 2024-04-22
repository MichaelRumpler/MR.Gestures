using Android.Views;

namespace MR.Gestures.Android
{
	interface IMultiTouchGestureListener
	{
		bool OnMoved(MotionEvent current);
		bool OnMoving(MotionEvent current);
	}
}
