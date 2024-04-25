﻿using Android.Content;
using Android.Views;

using MR.Gestures.Android;

namespace MR.Gestures.Handlers
{
    public partial class SwitchCellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.SwitchCellRenderer
    {
        protected override global::Android.Views.View GetCellCore(Cell item, global::Android.Views.View convertView, ViewGroup parent, Context context)
        {
            var view = base.GetCellCore(item, convertView, parent, context);
			//System.Diagnostics.Debug.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: SwitchCellRenderer.GetCellCore for cell {item.BindingContext}");
			AndroidGestureHandler.AddInstance((IGestureAwareControl)item, view);
            return view;
        }
    }
}
