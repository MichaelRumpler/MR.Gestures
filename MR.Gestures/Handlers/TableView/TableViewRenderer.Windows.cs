using System.ComponentModel;

using MR.Gestures.WinUI;

namespace MR.Gestures.Handlers
{
    public partial class TableViewRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.TableViewRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (GestureHandler.AllProperties.Contains(e.PropertyName))
                WinUIGestureHandler.OnElementPropertyChanged((IGestureAwareControl)Element, Control);
        }

        protected override void Dispose(bool disposing)
        {
            WinUIGestureHandler.RemoveInstance((IGestureAwareControl)Element);
            base.Dispose(disposing);
        }
    }
}
