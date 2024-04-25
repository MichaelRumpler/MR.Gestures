using System.ComponentModel;

using MR.Gestures.iOS;

namespace MR.Gestures.Handlers
{
    public partial class FrameRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.FrameRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (GestureHandler.AllProperties.Contains(e.PropertyName))
                iOSGestureHandler.OnElementPropertyChanged((IGestureAwareControl)Element, this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                iOSGestureHandler.RemoveInstance((IGestureAwareControl)Element);

            base.Dispose(disposing);
        }
    }
}
