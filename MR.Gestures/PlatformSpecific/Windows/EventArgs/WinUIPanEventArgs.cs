using Microsoft.UI.Xaml;
using Microsoft.UI.Input;

namespace MR.Gestures.WinUI.EventArgs
{
	public class WinUIPanEventArgs : PanEventArgs
	{
		public WinUIPanEventArgs(FrameworkElement view, List<PointerPoint> currentPointers,
			Windows.Foundation.Point deltaDistance, Windows.Foundation.Point totalDistance, Windows.Foundation.Point velocity)
		{
			ViewPosition = WinUIEventArgsHelper.GetViewPosition(view);
			Touches = WinUIEventArgsHelper.GetTouches(currentPointers, view);
			Sources = WinUIEventArgsHelper.GetSources(currentPointers);

			DeltaDistance = WinUIEventArgsHelper.ToXFPoint(deltaDistance);
			TotalDistance = WinUIEventArgsHelper.ToXFPoint(totalDistance);
			Velocity = WinUIEventArgsHelper.ToXFPoint(velocity);
		}
	}
}
