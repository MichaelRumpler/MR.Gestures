using Microsoft.UI.Xaml;
using Microsoft.UI.Input;

namespace MR.Gestures.WinUI.EventArgs
{
	public class WinUITapEventArgs : TapEventArgs
	{
		public WinUITapEventArgs(PointerPoint point, FrameworkElement view, int numberOfTaps)
		{
			ViewPosition = WinUIEventArgsHelper.GetViewPosition(view);
			Touches = WinUIEventArgsHelper.GetTouches(point, view);
			Sources = WinUIEventArgsHelper.GetSources(point);

			NumberOfTaps = numberOfTaps;
		}
	}
}
