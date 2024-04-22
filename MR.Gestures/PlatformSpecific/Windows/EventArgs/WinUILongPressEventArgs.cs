using Microsoft.UI.Xaml;
using Microsoft.UI.Input;

namespace MR.Gestures.WinUI.EventArgs
{
	public class WinUILongPressEventArgs : LongPressEventArgs
	{
		public WinUILongPressEventArgs(List<PointerPoint> currentPointers, FrameworkElement view, long duration = 0, bool cancelled = false, BaseGestureEventArgs previous = null)
		{
			Cancelled = cancelled;
			ViewPosition = WinUIEventArgsHelper.GetViewPosition(view);
			Touches = WinUIEventArgsHelper.GetTouches(currentPointers, view, previous);
			Sources = WinUIEventArgsHelper.GetSources(currentPointers, previous);

			Duration = duration;
		}
	}
}
