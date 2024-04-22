using System.Runtime.Versioning;

using UIKit;

using MR.Gestures.iOS;

namespace MR.Gestures.Handlers
{
    public partial class EntryCellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.EntryCellRenderer
    {
		[UnsupportedOSPlatform("ios14.0")]
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var tableViewCell = base.GetCell(item, reusableCell, tv);
            iOSGestureHandler.AddInstance((IGestureAwareControl)item, tableViewCell);
            return tableViewCell;
        }
    }
}
