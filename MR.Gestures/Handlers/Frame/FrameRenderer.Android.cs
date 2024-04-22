using System.ComponentModel;

using Android.Content;
using Android.Views;

using Microsoft.Maui.Controls.Platform;

using MR.Gestures.Android;

namespace MR.Gestures.Handlers
{
    public partial class FrameRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.FrameRenderer   // inherits from CardView
    {
        public FrameRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Maui.Controls.Frame> e)
        {
            base.OnElementChanged(e);

            AndroidGestureHandler.OnElementChanged((IGestureAwareControl)e.OldElement, (IGestureAwareControl)e.NewElement, Control);
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            AndroidGestureHandler.HandleMotionEvent((IGestureAwareControl)Element, this, e);
            return base.DispatchTouchEvent(e);
        }

        public override bool DispatchGenericMotionEvent(MotionEvent e)
        {
            AndroidGestureHandler.HandleMotionEvent((IGestureAwareControl)Element, this, e);
            return base.DispatchGenericMotionEvent(e);
        }
    }
}
