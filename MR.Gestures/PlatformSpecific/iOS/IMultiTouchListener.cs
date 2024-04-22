using System;
using UIKit;

namespace MR.Gestures.iOS
{
	public interface IMultiTouchListener
	{
		void OnMultiTouchMoving(UIGestureRecognizer gr);

		void OnMultiTouchEnded(UIGestureRecognizer gr);
	}
}
