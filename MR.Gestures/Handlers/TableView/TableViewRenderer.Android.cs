using System.ComponentModel;

using Android.Content;
using Android.Views;

using Microsoft.Maui.Controls.Platform;

using MR.Gestures.Android;

namespace MR.Gestures.Handlers
{
    public partial class TableViewRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.TableViewRenderer
    {
        public TableViewRenderer(Context context) : base(context)
        {
        }

        protected override global::Android.Widget.ListView CreateNativeControl()
            => new GesturesTableViewAndroidView(Context);

        protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Maui.Controls.TableView> e)
        {
            base.OnElementChanged(e);

            ((GesturesTableViewAndroidView)Control).Element = (IGestureAwareControl)e.NewElement;
            AndroidGestureHandler.OnElementChanged(null, (IGestureAwareControl)Element, Control);
        }

        class GesturesTableViewAndroidView : global::Android.Widget.ListView
        {
            public GesturesTableViewAndroidView(Context context) : base(context) { }

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
