namespace MR.Gestures;

public static class LoadedHelper
{
	public static void Element_Loaded(object sender, EventArgs e)
	{
		var element = (VisualElement)sender;
		if (element.Handler is Microsoft.Maui.Handlers.ViewHandler viewHandler)
			element.Handler.UpdateValue(nameof(IViewHandler.ContainerView));
		else
			viewHandler = null;

#if IOS || MACCATALYST
		iOS.iOSGestureHandler.AddInstance((IGestureAwareControl)element, viewHandler?.ContainerView ?? (UIKit.UIView)element.Handler.PlatformView);
#elif ANDROID
		Android.AndroidGestureHandler.AddInstance((IGestureAwareControl)element, viewHandler?.ContainerView ?? (global::Android.Views.View)element.Handler.PlatformView);
#elif WINDOWS
		WinUI.WinUIGestureHandler.AddInstance((IGestureAwareControl)element, viewHandler?.ContainerView ?? (Microsoft.UI.Xaml.FrameworkElement)element.Handler.PlatformView);
#endif
	}

	public static void Element_Unloaded(object sender, EventArgs e)
	{
#if IOS || MACCATALYST
		iOS.iOSGestureHandler.RemoveInstance((IGestureAwareControl)sender);
#elif ANDROID
		Android.AndroidGestureHandler.RemoveInstance((IGestureAwareControl)sender);
#elif WINDOWS
		WinUI.WinUIGestureHandler.RemoveInstance((IGestureAwareControl)sender);
#endif
	}
}
