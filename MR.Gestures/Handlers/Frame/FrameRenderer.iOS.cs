using Microsoft.Maui.Controls.Platform;

using MR.Gestures.iOS;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.Gestures.Handlers
{
    public partial class FrameRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Maui.Controls.Frame> e)
        {
            base.OnElementChanged(e);
            iOSGestureHandler.OnElementChanged((IGestureAwareControl)e.OldElement, (IGestureAwareControl)e.NewElement, this);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (GestureHandler.AllProperties.Contains(e.PropertyName))
                iOSGestureHandler.OnElementPropertyChanged((IGestureAwareControl)Element, this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                iOSGestureHandler.RemoveInstance((IGestureAwareControl)Element);
            }
            base.Dispose(disposing);
        }
    }
}
