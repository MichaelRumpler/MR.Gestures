namespace MR.Gestures.Handlers
{
    internal static class HandlerHelper
    {
		public static IPropertyMapper<IGestureAwareControl, IElementHandler> GesturesMapper = new PropertyMapper<IGestureAwareControl, IElementHandler>()
		{
			[nameof(IGestureAwareControl.Down)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.DownCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Up)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.UpCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Tapping)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.TappingCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Tapped)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.TappedCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.DoubleTapped)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.DoubleTappedCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.LongPressing)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.LongPressingCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.LongPressed)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.LongPressedCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Pinching)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.PinchingCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Pinched)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.PinchedCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Panning)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.PanningCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Panned)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.PannedCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Swiped)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.SwipedCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Rotating)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.RotatingCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.Rotated)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.RotatedCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.MouseEntered)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.MouseEnteredCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.MouseMoved)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.MouseMovedCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.MouseExited)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.MouseExitedCommand)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.ScrollWheelChanged)] = MapPropertyChanged,
			[nameof(IGestureAwareControl.ScrollWheelChangedCommand)] = MapPropertyChanged,
		};

        private static void MapPropertyChanged(IElementHandler handler, IGestureAwareControl element)
        {
            TaskHelpers.Debounce(handler, element, (handler, element) =>
            {
                if (handler is Microsoft.Maui.Handlers.ViewHandler viewHandler)
					handler.UpdateValue(nameof(IViewHandler.ContainerView));
				else
					viewHandler = null;

#if IOS || MACCATALYST
			    iOS.iOSGestureHandler.OnElementPropertyChanged(element, viewHandler?.ContainerView ?? (UIKit.UIView)handler.PlatformView);
#elif ANDROID
				Android.AndroidGestureHandler.OnElementPropertyChanged(element, viewHandler?.ContainerView ?? (global::Android.Views.View)handler.PlatformView);
#elif WINDOWS
                WinUI.WinUIGestureHandler.OnElementPropertyChanged(element, viewHandler?.ContainerView ?? (Microsoft.UI.Xaml.FrameworkElement)handler.PlatformView);
#endif
            }, TimeSpan.FromMilliseconds(100));
        }
	}
}
