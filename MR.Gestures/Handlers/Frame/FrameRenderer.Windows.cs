using System.ComponentModel;

using MR.Gestures.WinUI;

namespace MR.Gestures.Handlers
{
    public partial class FrameRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.FrameRenderer   // is a Microsoft.UI.Xaml.Controls.Border
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (GestureHandler.AllProperties.Contains(e.PropertyName))
                WinUIGestureHandler.OnElementPropertyChanged((IGestureAwareControl)Element, this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                WinUIGestureHandler.RemoveInstance((IGestureAwareControl)Element);

            base.Dispose(disposing);
        }
    }
}
