using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using PlatformView = Android.Views.View;

using MR.Gestures.Android;

namespace MR.Gestures.Handlers
{
    public partial class RadioButtonHandler : Microsoft.Maui.Handlers.RadioButtonHandler
    {
        protected override AndroidX.AppCompat.Widget.AppCompatRadioButton CreatePlatformView()
            => new GesturesRadioButtonAndroidView(Context);

        protected override void ConnectHandler(PlatformView platformView)
        {
            base.ConnectHandler(platformView);
            ((GesturesRadioButtonAndroidView)platformView).Element = (IGestureAwareControl)VirtualView;
        }

        class GesturesRadioButtonAndroidView : AndroidX.AppCompat.Widget.AppCompatRadioButton
        {
            public GesturesRadioButtonAndroidView(Context context) : base(context) { }
            public GesturesRadioButtonAndroidView(Context context, IAttributeSet attrs) : base(context, attrs) { }
            public GesturesRadioButtonAndroidView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }
            protected GesturesRadioButtonAndroidView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

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
