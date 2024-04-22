using MR.Gestures.iOS;

#if IOS && !MACCATALYST
using PlatformViewDatePicker = Microsoft.Maui.Platform.MauiDatePicker;
#elif MACCATALYST
using PlatformViewDatePicker = UIKit.UIDatePicker;
#endif

#if IOS && !MACCATALYST
using PlatformViewTimePicker = Microsoft.Maui.Platform.MauiTimePicker;
#elif MACCATALYST
using PlatformViewTimePicker = UIKit.UIDatePicker;
#endif


namespace MR.Gestures.Handlers;

    public partial class ActivityIndicatorHandler : Microsoft.Maui.Handlers.ActivityIndicatorHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.MauiActivityIndicator platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class BorderHandler : Microsoft.Maui.Handlers.BorderHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.ContentView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
    {
        protected override void ConnectHandler(UIKit.UIButton platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class CheckBoxHandler : Microsoft.Maui.Handlers.CheckBoxHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.MauiCheckBox platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class ContentViewHandler : Microsoft.Maui.Handlers.ContentViewHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.ContentView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class DatePickerHandler : Microsoft.Maui.Handlers.DatePickerHandler
    {
        protected override void ConnectHandler(PlatformViewDatePicker platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class EditorHandler : Microsoft.Maui.Handlers.EditorHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.MauiTextView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class EntryHandler : Microsoft.Maui.Handlers.EntryHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.MauiTextField platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class GraphicsViewHandler : Microsoft.Maui.Handlers.GraphicsViewHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.PlatformTouchGraphicsView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class ImageHandler : Microsoft.Maui.Handlers.ImageHandler
    {
        protected override void ConnectHandler(UIKit.UIImageView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class ImageButtonHandler : Microsoft.Maui.Handlers.ImageButtonHandler
    {
        protected override void ConnectHandler(UIKit.UIButton platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class IndicatorViewHandler : Microsoft.Maui.Handlers.IndicatorViewHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.MauiPageControl platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class LabelHandler : Microsoft.Maui.Handlers.LabelHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.MauiLabel platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class LayoutHandler : Microsoft.Maui.Handlers.LayoutHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.LayoutView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class PageHandler : Microsoft.Maui.Handlers.PageHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.ContentView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class PickerHandler : Microsoft.Maui.Handlers.PickerHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.MauiPicker platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class ProgressBarHandler : Microsoft.Maui.Handlers.ProgressBarHandler
    {
        protected override void ConnectHandler(UIKit.UIProgressView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class RadioButtonHandler : Microsoft.Maui.Handlers.RadioButtonHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.ContentView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class ScrollViewHandler : Microsoft.Maui.Handlers.ScrollViewHandler
    {
        protected override void ConnectHandler(UIKit.UIScrollView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class SearchBarHandler : Microsoft.Maui.Handlers.SearchBarHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.MauiSearchBar platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class ShapeViewHandler : Microsoft.Maui.Handlers.ShapeViewHandler
    {
        protected override void ConnectHandler(Microsoft.Maui.Platform.MauiShapeView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class SliderHandler : Microsoft.Maui.Handlers.SliderHandler
    {
        protected override void ConnectHandler(UIKit.UISlider platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class StepperHandler : Microsoft.Maui.Handlers.StepperHandler
    {
        protected override void ConnectHandler(UIKit.UIStepper platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class SwitchHandler : Microsoft.Maui.Handlers.SwitchHandler
    {
        protected override void ConnectHandler(UIKit.UISwitch platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class TimePickerHandler : Microsoft.Maui.Handlers.TimePickerHandler
    {
        protected override void ConnectHandler(PlatformViewTimePicker platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

    public partial class WebViewHandler : Microsoft.Maui.Handlers.WebViewHandler
    {
        protected override void ConnectHandler(WebKit.WKWebView platformView)
        {
            base.ConnectHandler(platformView);
            iOSGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }
    }

