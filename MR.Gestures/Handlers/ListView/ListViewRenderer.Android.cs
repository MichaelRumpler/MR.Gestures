using Android.Content;
using Android.Views;

using Microsoft.Maui.Controls.Platform;

using MR.Gestures.Android;

namespace MR.Gestures.Handlers
{
    public partial class ListViewRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ListViewRenderer
    {
        public ListViewRenderer(Context context) : base(context)
        {
        }

        protected override global::Android.Widget.ListView CreateNativeControl()
            => new GesturesListViewAndroidView(Context);

        protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Maui.Controls.ListView> e)
        {
            base.OnElementChanged(e);

            ((GesturesListViewAndroidView)Control).Element = (IGestureAwareControl)e.NewElement;
        }

        protected override void Dispose(bool disposing)
        {
            //System.Diagnostics.Debug.WriteLine("ListView Disposed");
            AndroidGestureHandler.RemoveInstance((IGestureAwareControl)Element);
            base.Dispose(disposing);
        }

        class GesturesListViewAndroidView : global::Android.Widget.ListView
        {
            public GesturesListViewAndroidView(Context context) : base(context) { }

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
