using Microsoft.UI.Xaml;
using Microsoft.UI.Input;

namespace MR.Gestures.WinUI.EventArgs
{
    public class WinUIDownUpEventArgs : DownUpEventArgs
	{
		public WinUIDownUpEventArgs(List<PointerPoint> currentPointers, int triggeringTouch, FrameworkElement view, bool cancel = false)
		{
			Cancelled = cancel;
			ViewPosition = WinUIEventArgsHelper.GetViewPosition(view);
			Touches = WinUIEventArgsHelper.GetTouches(currentPointers, view);
			Sources = WinUIEventArgsHelper.GetSources(currentPointers);

			TriggeringTouches = new[] { triggeringTouch };
		}
	}
}
