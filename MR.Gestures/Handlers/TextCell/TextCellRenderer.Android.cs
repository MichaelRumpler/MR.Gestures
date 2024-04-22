using Android.Content;
using Android.Views;

using MR.Gestures.Android;

using System.Diagnostics;

namespace MR.Gestures.Handlers
{
    public partial class TextCellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.TextCellRenderer
    {
        protected override global::Android.Views.View GetCellCore(Cell item, global::Android.Views.View convertView, ViewGroup parent, Context context)
        {
            var view = base.GetCellCore(item, convertView, parent, context);

            //Debug.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: TextCellRenderer.GetCellCore for cell {item.BindingContext}");

            AndroidGestureHandler.AddInstance((IGestureAwareControl)item, view);
            return view;
        }
    }
}
