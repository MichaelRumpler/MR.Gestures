using Android.Content;
using Android.Views;

using MR.Gestures.Android;

namespace MR.Gestures.Handlers
{
    public partial class ActivityIndicatorHandler : Microsoft.Maui.Handlers.ActivityIndicatorHandler
    {
        protected override global::Android.Widget.ProgressBar CreatePlatformView()
            => new GesturesActivityIndicatorAndroidView(Context);

        protected override void ConnectHandler(global::Android.Widget.ProgressBar platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesActivityIndicatorAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesActivityIndicatorAndroidView : global::Android.Widget.ProgressBar
        {
            public GesturesActivityIndicatorAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class BorderHandler : Microsoft.Maui.Handlers.BorderHandler
    {
        protected override global::Microsoft.Maui.Platform.ContentViewGroup CreatePlatformView()
            => new GesturesBorderAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.ContentViewGroup platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesBorderAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesBorderAndroidView : global::Microsoft.Maui.Platform.ContentViewGroup
        {
            public GesturesBorderAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
    {
        protected override global::Google.Android.Material.Button.MaterialButton CreatePlatformView()
            => new GesturesButtonAndroidView(Context);

        protected override void ConnectHandler(global::Google.Android.Material.Button.MaterialButton platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesButtonAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesButtonAndroidView : global::Google.Android.Material.Button.MaterialButton
        {
            public GesturesButtonAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class CheckBoxHandler : Microsoft.Maui.Handlers.CheckBoxHandler
    {
        protected override global::AndroidX.AppCompat.Widget.AppCompatCheckBox CreatePlatformView()
            => new GesturesCheckBoxAndroidView(Context);

        protected override void ConnectHandler(global::AndroidX.AppCompat.Widget.AppCompatCheckBox platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesCheckBoxAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesCheckBoxAndroidView : global::AndroidX.AppCompat.Widget.AppCompatCheckBox
        {
            public GesturesCheckBoxAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class ContentViewHandler : Microsoft.Maui.Handlers.ContentViewHandler
    {
        protected override global::Microsoft.Maui.Platform.ContentViewGroup CreatePlatformView()
            => new GesturesContentViewAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.ContentViewGroup platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesContentViewAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesContentViewAndroidView : global::Microsoft.Maui.Platform.ContentViewGroup
        {
            public GesturesContentViewAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class DatePickerHandler : Microsoft.Maui.Handlers.DatePickerHandler
    {
        protected override global::Microsoft.Maui.Platform.MauiDatePicker CreatePlatformView()
            => new GesturesDatePickerAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.MauiDatePicker platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesDatePickerAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesDatePickerAndroidView : global::Microsoft.Maui.Platform.MauiDatePicker
        {
            public GesturesDatePickerAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class EditorHandler : Microsoft.Maui.Handlers.EditorHandler
    {
        protected override global::AndroidX.AppCompat.Widget.AppCompatEditText CreatePlatformView()
            => new GesturesEditorAndroidView(Context);

        protected override void ConnectHandler(global::AndroidX.AppCompat.Widget.AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesEditorAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesEditorAndroidView : global::AndroidX.AppCompat.Widget.AppCompatEditText
        {
            public GesturesEditorAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class EntryHandler : Microsoft.Maui.Handlers.EntryHandler
    {
        protected override global::AndroidX.AppCompat.Widget.AppCompatEditText CreatePlatformView()
            => new GesturesEntryAndroidView(Context);

        protected override void ConnectHandler(global::AndroidX.AppCompat.Widget.AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesEntryAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesEntryAndroidView : global::AndroidX.AppCompat.Widget.AppCompatEditText
        {
            public GesturesEntryAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class GraphicsViewHandler : Microsoft.Maui.Handlers.GraphicsViewHandler
    {
        protected override global::Microsoft.Maui.Platform.PlatformTouchGraphicsView CreatePlatformView()
            => new GesturesGraphicsViewAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.PlatformTouchGraphicsView platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesGraphicsViewAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesGraphicsViewAndroidView : global::Microsoft.Maui.Platform.PlatformTouchGraphicsView
        {
            public GesturesGraphicsViewAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class ImageHandler : Microsoft.Maui.Handlers.ImageHandler
    {
        protected override global::Android.Widget.ImageView CreatePlatformView()
            => new GesturesImageAndroidView(Context);

        protected override void ConnectHandler(global::Android.Widget.ImageView platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesImageAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesImageAndroidView : global::Android.Widget.ImageView
        {
            public GesturesImageAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class ImageButtonHandler : Microsoft.Maui.Handlers.ImageButtonHandler
    {
        protected override global::Google.Android.Material.ImageView.ShapeableImageView CreatePlatformView()
            => new GesturesImageButtonAndroidView(Context);

        protected override void ConnectHandler(global::Google.Android.Material.ImageView.ShapeableImageView platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesImageButtonAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesImageButtonAndroidView : global::Google.Android.Material.ImageView.ShapeableImageView
        {
            public GesturesImageButtonAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class IndicatorViewHandler : Microsoft.Maui.Handlers.IndicatorViewHandler
    {
        protected override global::Microsoft.Maui.Platform.MauiPageControl CreatePlatformView()
            => new GesturesIndicatorViewAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.MauiPageControl platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesIndicatorViewAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesIndicatorViewAndroidView : global::Microsoft.Maui.Platform.MauiPageControl
        {
            public GesturesIndicatorViewAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class LabelHandler : Microsoft.Maui.Handlers.LabelHandler
    {
        protected override global::AndroidX.AppCompat.Widget.AppCompatTextView CreatePlatformView()
            => new GesturesLabelAndroidView(Context);

        protected override void ConnectHandler(global::AndroidX.AppCompat.Widget.AppCompatTextView platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesLabelAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesLabelAndroidView : global::AndroidX.AppCompat.Widget.AppCompatTextView
        {
            public GesturesLabelAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class LayoutHandler : Microsoft.Maui.Handlers.LayoutHandler
    {
        protected override global::Microsoft.Maui.Platform.LayoutViewGroup CreatePlatformView()
            => new GesturesLayoutAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.LayoutViewGroup platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesLayoutAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesLayoutAndroidView : global::Microsoft.Maui.Platform.LayoutViewGroup
        {
            public GesturesLayoutAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class PageHandler : Microsoft.Maui.Handlers.PageHandler
    {
        protected override global::Microsoft.Maui.Platform.ContentViewGroup CreatePlatformView()
            => new GesturesPageAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.ContentViewGroup platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesPageAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesPageAndroidView : global::Microsoft.Maui.Platform.ContentViewGroup
        {
            public GesturesPageAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class PickerHandler : Microsoft.Maui.Handlers.PickerHandler
    {
        protected override global::Microsoft.Maui.Platform.MauiPicker CreatePlatformView()
            => new GesturesPickerAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.MauiPicker platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesPickerAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesPickerAndroidView : global::Microsoft.Maui.Platform.MauiPicker
        {
            public GesturesPickerAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class ProgressBarHandler : Microsoft.Maui.Handlers.ProgressBarHandler
    {
        protected override global::Android.Widget.ProgressBar CreatePlatformView()
            => new GesturesProgressBarAndroidView(Context);

        protected override void ConnectHandler(global::Android.Widget.ProgressBar platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesProgressBarAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesProgressBarAndroidView : global::Android.Widget.ProgressBar
        {
            public GesturesProgressBarAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class ScrollViewHandler : Microsoft.Maui.Handlers.ScrollViewHandler
    {
        protected override global::Microsoft.Maui.Platform.MauiScrollView CreatePlatformView()
            => new GesturesScrollViewAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.MauiScrollView platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesScrollViewAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesScrollViewAndroidView : global::Microsoft.Maui.Platform.MauiScrollView
        {
            public GesturesScrollViewAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class SearchBarHandler : Microsoft.Maui.Handlers.SearchBarHandler
    {
        protected override global::AndroidX.AppCompat.Widget.SearchView CreatePlatformView()
            => new GesturesSearchBarAndroidView(Context);

        protected override void ConnectHandler(global::AndroidX.AppCompat.Widget.SearchView platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesSearchBarAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesSearchBarAndroidView : global::AndroidX.AppCompat.Widget.SearchView
        {
            public GesturesSearchBarAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class ShapeViewHandler : Microsoft.Maui.Handlers.ShapeViewHandler
    {
        protected override global::Microsoft.Maui.Platform.MauiShapeView CreatePlatformView()
            => new GesturesShapeViewAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.MauiShapeView platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesShapeViewAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesShapeViewAndroidView : global::Microsoft.Maui.Platform.MauiShapeView
        {
            public GesturesShapeViewAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class SliderHandler : Microsoft.Maui.Handlers.SliderHandler
    {
        protected override global::Android.Widget.SeekBar CreatePlatformView()
            => new GesturesSliderAndroidView(Context);

        protected override void ConnectHandler(global::Android.Widget.SeekBar platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesSliderAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesSliderAndroidView : global::Android.Widget.SeekBar
        {
            public GesturesSliderAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class StepperHandler : Microsoft.Maui.Handlers.StepperHandler
    {
        protected override global::Microsoft.Maui.Platform.MauiStepper CreatePlatformView()
            => new GesturesStepperAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.MauiStepper platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesStepperAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesStepperAndroidView : global::Microsoft.Maui.Platform.MauiStepper
        {
            public GesturesStepperAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class SwitchHandler : Microsoft.Maui.Handlers.SwitchHandler
    {
        protected override global::AndroidX.AppCompat.Widget.SwitchCompat CreatePlatformView()
            => new GesturesSwitchAndroidView(Context);

        protected override void ConnectHandler(global::AndroidX.AppCompat.Widget.SwitchCompat platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesSwitchAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesSwitchAndroidView : global::AndroidX.AppCompat.Widget.SwitchCompat
        {
            public GesturesSwitchAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class TimePickerHandler : Microsoft.Maui.Handlers.TimePickerHandler
    {
        protected override global::Microsoft.Maui.Platform.MauiTimePicker CreatePlatformView()
            => new GesturesTimePickerAndroidView(Context);

        protected override void ConnectHandler(global::Microsoft.Maui.Platform.MauiTimePicker platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesTimePickerAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesTimePickerAndroidView : global::Microsoft.Maui.Platform.MauiTimePicker
        {
            public GesturesTimePickerAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

    public partial class WebViewHandler : Microsoft.Maui.Handlers.WebViewHandler
    {
        protected override global::Android.Webkit.WebView CreatePlatformView()
            => new GesturesWebViewAndroidView(Context);

        protected override void ConnectHandler(global::Android.Webkit.WebView platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesWebViewAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)VirtualView, platformView);
        }

        class GesturesWebViewAndroidView : global::Android.Webkit.WebView
        {
            public GesturesWebViewAndroidView(Context context) : base(context) { }

            public IGestureAwareControl Element;

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchTouchEvent(e);
            }

            public override bool DispatchGenericMotionEvent(MotionEvent e)
            {
                AndroidGestureHandler.HandleMotionEvent(Element, this, e);
                return base.DispatchGenericMotionEvent(e);
            }
        }
    }

}
