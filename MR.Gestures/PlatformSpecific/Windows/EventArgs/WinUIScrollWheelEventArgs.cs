using Microsoft.UI.Xaml;
using Microsoft.UI.Input;

namespace MR.Gestures.WinUI.EventArgs
{
	public class WinUIScrollWheelEventArgs : ScrollWheelEventArgs
	{
		public WinUIScrollWheelEventArgs(FrameworkElement view, PointerPoint currentPointer)
		{
			ViewPosition = WinUIEventArgsHelper.GetViewPosition(view);
			Touches = WinUIEventArgsHelper.GetTouches(currentPointer, view);
			Sources = WinUIEventArgsHelper.GetSources(currentPointer);

			ScrollDelta = currentPointer.Properties.IsHorizontalMouseWheel
				? new Point(currentPointer.Properties.MouseWheelDelta, 0)
				: new Point(0, currentPointer.Properties.MouseWheelDelta);
		}
	}
}
