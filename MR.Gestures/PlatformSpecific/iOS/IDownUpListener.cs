using System;
using UIKit;

namespace MR.Gestures.iOS
{
	public interface IDownUpListener
	{
		void OnDown(UIGestureRecognizer gr, UITouch[] touchesBegan);

		void OnUp(UIGestureRecognizer gr, UITouch[] touchesEnded);
	}
}
