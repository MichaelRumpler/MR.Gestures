﻿using UIKit;

using MR.Gestures.iOS;

namespace MR.Gestures.Handlers
{
    public partial class SwitchCellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.SwitchCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var tableViewCell = base.GetCell(item, reusableCell, tv);
            iOSGestureHandler.AddInstance((IGestureAwareControl)item, tableViewCell);
            return tableViewCell;
        }
    }
}
