namespace MR.Gestures
{
    public partial class ListView : Microsoft.Maui.Controls.ListView, IGestureAwareControl
    {
        public ListView() : base()
        {
        }

        public ListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
        }

        public HashSet<Cell> CellsToDispose { get; } = new HashSet<Cell>();
    }
}
