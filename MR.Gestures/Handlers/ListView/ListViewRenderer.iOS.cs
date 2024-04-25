using System.ComponentModel;

using MR.Gestures.iOS;

namespace MR.Gestures.Handlers
{
    public partial class ListViewRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ListViewRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (GestureHandler.AllProperties.Contains(e.PropertyName))
                iOSGestureHandler.OnElementPropertyChanged((IGestureAwareControl)Element, Control);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                iOSGestureHandler.RemoveInstance((IGestureAwareControl)Element);

            base.Dispose(disposing);
        }
    }
}
