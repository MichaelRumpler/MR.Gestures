namespace MR.Gestures
{
    public partial class ListView : Microsoft.Maui.Controls.ListView, IGestureAwareControl
    {
        public ListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
			Loaded += LoadedHelper.Element_Loaded;
			Unloaded += LoadedHelper.Element_Unloaded;
        }

        public HashSet<Cell> CellsToDispose { get; } = new HashSet<Cell>();
    }
}
