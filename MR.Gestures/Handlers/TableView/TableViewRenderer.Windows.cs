using System.ComponentModel;

using Microsoft.Maui.Controls.Platform;

using MR.Gestures.WinUI;

namespace MR.Gestures.Handlers
{
    public partial class TableViewRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.TableViewRenderer
    {
        //public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
        //    = new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.ImageHandler.Mapper);

        //public TableViewRenderer() : base(Mapper) { }

        //public TableViewRenderer(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Maui.Controls.TableView> e)
        {
            base.OnElementChanged(e);

            WinUIGestureHandler.OnElementChanged((IGestureAwareControl)e.OldElement, (IGestureAwareControl)e.NewElement, Control);
        }

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
