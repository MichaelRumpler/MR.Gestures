using Microsoft.UI.Xaml;

namespace MR.Gestures.WinUI
{
	public class CellContainerManager : IDisposable
	{
        readonly IGestureAwareControl element;
		Microsoft.UI.Xaml.Controls.ListView listView;
		
		HashSet<IGestureAwareControl> cells;		// the cells which have my functionality added

		public CellContainerManager(IGestureAwareControl element, Microsoft.UI.Xaml.Controls.ListView view)
		{
			this.element = element;
			listView = view;

			listView.LayoutUpdated += ThrottleUpdateCells;

			ThrottleUpdateCells(null, null);
		}

        #region method to throttle the LayoutUpdated event

        CancellationTokenSource cancelLayoutUpdatedHandler;

		void ThrottleUpdateCells(object sender, object e)
		{
			// LayoutUpdated is raised quite often
			// specifically it is raised BEFORE all items have been added to the layout
			// so I wait a bit when it is raised and only work if no more LayoutUpdated events are raised within 200ms

			if (cancelLayoutUpdatedHandler != null)
			{
				try
				{
					cancelLayoutUpdatedHandler.Cancel();
				}
				catch { }
			}

			try
			{
				cancelLayoutUpdatedHandler = new CancellationTokenSource();
				Task.Run(async () =>
				{
					await Task.Delay(200, cancelLayoutUpdatedHandler.Token);
					cancelLayoutUpdatedHandler = null;      // I cannot be cancelled anymore

					listView?.DispatcherQueue?.TryEnqueue(UpdateCells);

				}, cancelLayoutUpdatedHandler.Token);
			}
			catch (TaskCanceledException) { }
		}

		#endregion

		private void UpdateCells()
		{
			if (listView == null)
				return;

			var allCells = listView.FindAllChildren<Microsoft.Maui.Controls.Platform.Compatibility.CellControl>().ToArray();

			if (cells == null) cells = new HashSet<IGestureAwareControl>();
			var unusedCells = new HashSet<IGestureAwareControl>(cells);

			foreach (var cellTemplate in allCells)
			{
                if (cellTemplate.Cell is IGestureAwareControl cell && cell.Parent is not null)
				{
					if (!cells.Contains(cell))
					{
						Add((Cell)cell, cellTemplate);
						cells.Add(cell);
					}

					unusedCells.Remove(cell);
				}
			}
			foreach (var cell in unusedCells)
			{
				Remove(cell);
				if (cells != null)
					cells.Remove(cell);
			}
		}

		void Add(Cell cell, FrameworkElement view) => WinUIGestureHandler.AddInstance((IGestureAwareControl)cell, view);
		void Remove(IGestureAwareControl cell) => WinUIGestureHandler.RemoveInstance(cell);

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.listView != null)
				{
                    this.listView.LayoutUpdated -= ThrottleUpdateCells;
					this.listView = null;
				}

				if (cells != null)
				{
					foreach (var cell in cells)
						Remove(cell);
					cells = null;
				}

                if (element is ListView listView)
                {
                    foreach (var child in listView.CellsToDispose.OfType<IGestureAwareControl>().ToArray())
                        WinUIGestureHandler.RemoveInstance(child);
                }

                if (element is TableView tableView)
                {
                    foreach (var child in tableView.Root.SelectMany(r => r).OfType<IGestureAwareControl>().ToArray())
                        WinUIGestureHandler.RemoveInstance(child);
                }
            }
		}

		#endregion
	}
}
