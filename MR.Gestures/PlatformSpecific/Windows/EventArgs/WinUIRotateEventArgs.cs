using Microsoft.UI.Xaml;
using Microsoft.UI.Input;

namespace MR.Gestures.WinUI.EventArgs
{
	public class WinUIRotateEventArgs : RotateEventArgs
	{
		public WinUIRotateEventArgs(List<PointerPoint> currentPointers, FrameworkElement view, RotateEventArgs previous)
		{
			ViewPosition = WinUIEventArgsHelper.GetViewPosition(view);

			Touches = WinUIEventArgsHelper.GetTouches(currentPointers, view, previous, 2);
			Sources = WinUIEventArgsHelper.GetSources(currentPointers, previous, 2);

			CalculateAngles(previous);
		}
	}
}
