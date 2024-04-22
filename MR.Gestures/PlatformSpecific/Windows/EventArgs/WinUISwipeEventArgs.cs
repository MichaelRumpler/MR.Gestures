using Microsoft.UI.Xaml;
using Microsoft.UI.Input;

namespace MR.Gestures.WinUI.EventArgs
{
	public class WinUISwipeEventArgs : SwipeEventArgs
	{
		public WinUISwipeEventArgs(Windows.Foundation.Point velocity, List<PointerPoint> currentPointers, FrameworkElement view, Direction direction, PanEventArgs previous)
		{
			ViewPosition = WinUIEventArgsHelper.GetViewPosition(view);
			Touches = WinUIEventArgsHelper.GetTouches(currentPointers, view, previous);
			Sources = WinUIEventArgsHelper.GetSources(currentPointers, previous);

			DeltaDistance = previous.DeltaDistance;
			TotalDistance = previous.TotalDistance;
			Velocity = previous.Velocity;

			Direction = direction;
		}
	}
}
