using Microsoft.UI.Xaml;
using Microsoft.UI.Input;

namespace MR.Gestures.WinUI.EventArgs
{
	public class WinUIPinchEventArgs : PinchEventArgs
	{
		public WinUIPinchEventArgs(List<PointerPoint> currentPointers, FrameworkElement view, PinchEventArgs previous)
		{
			ViewPosition = WinUIEventArgsHelper.GetViewPosition(view);

			Touches = WinUIEventArgsHelper.GetTouches(currentPointers, view, previous, 2);
			Sources = WinUIEventArgsHelper.GetSources(currentPointers, previous, 2);

			CalculateScales(previous);
		}
	}
}
