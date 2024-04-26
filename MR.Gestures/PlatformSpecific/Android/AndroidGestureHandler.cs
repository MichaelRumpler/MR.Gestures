#define LOGINSTANCES

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

using Android.Views;
using AView = Android.Views.View;


namespace MR.Gestures.Android
{
	public class AndroidGestureHandler
	{
#if LOGINSTANCES
		private static int Instances;
#endif

		#region Static Methods

		private static Dictionary<IGestureAwareControl, AndroidGestureHandler> allGestureHandlers = new Dictionary<IGestureAwareControl, AndroidGestureHandler>();

        /// <summary>
        /// Adds the AndroidGestureHandler for a Element and View. Must be called from the Cell renderers GetCellCore method.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="view"></param>
        public static AndroidGestureHandler AddInstance(IGestureAwareControl element, AView view)
		{
            if (allGestureHandlers.Remove(element, out var oldHandler))
			{
				if(oldHandler.view == view)
				{
#if LOGINSTANCES
                    Log($"AddInstance, but element {ElementLog(element)} already exists with the same view. Leaving old instance!");
#endif
                    allGestureHandlers[element] = oldHandler;
					return oldHandler;
                }

#if LOGINSTANCES
                Log($"AddInstance, but element {ElementLog(element)} already exists with a different view. Disposing old handler and replacing it with a new instance.");
#endif
                oldHandler.Dispose();
			}

			var newHandler = new AndroidGestureHandler(element, view);
			allGestureHandlers[element] = newHandler;

			return newHandler;
        }

        /// <summary>
        /// Removes the gesture recognizers for the given IGestureAwareControl. Called from a Views Dispose method.
        /// </summary>
        /// <param name="element">The MR.Gestures element.</param>
        public static void RemoveInstance(IGestureAwareControl element)
		{
            if (element != null && allGestureHandlers.Remove(element, out var concreteHandler))
			{
#if LOGINSTANCES
				Log($"Removed Instance {element.GetType().Name}, BindingContext={((Element)element).BindingContext} from allGestureHandlers");
#endif

                concreteHandler.Dispose();
			}
        }

        /// <summary>
        /// Called from a View renderers OnElementPropertyChanged method. (not used on Android currently, but maybe in the future)
        /// </summary>
        /// <param name="element"></param>
        /// <param name="view"></param>
        public static void OnElementPropertyChanged(IGestureAwareControl element, AView view)
		{
			// not used on Android, currently all GestureListeners are always called

			//if (element != null)
			//{
			//	var handler = GetInstance(element);
			//	handler.removeGestureRecognizers();
			//	handler.addGestureRecognizers(renderer);
			//}
		}

		/// <summary>
		/// Is called from the renderer's DispatchTouchEvent and DispatchGenericMotionEvent method.
		/// This enables more gestures than just handling the Touch and GenericMotion events from here.
		/// </summary>
		/// <param name="element"></param>
		/// <param name="view"></param>
		/// <param name="e"></param>
		/// <returns></returns>
		public static bool HandleMotionEvent(IGestureAwareControl element, AView view, MotionEvent e)
		{
			if (!allGestureHandlers.TryGetValue(element, out var concreteHandler))
				concreteHandler = AddInstance(element, view);

			return concreteHandler.HandleMotionEvent(e);
		}

		#endregion

		private IGestureAwareControl element;
		private AView view;
		private bool isVisible = true;
		private MultiPage<Page> multiPage;

        private SimpleGestureListener simpleListener;
		private GestureDetector simpleGestureDetector;
		private TapGestureListener tapGestureListener;
		private TapGestureDetector tapGestureDetector;
		private MultiTouchGestureDetector multiTouchGestureDetector;
		private MouseGestureListener mouseGestureListener;
		private MouseGestureDetector mouseGestureDetector;

		internal AndroidGestureHandler(IGestureAwareControl element, AView view, IGestureListener listener = null)
		{
			this.element = element;
			this.view = view;
			listener = listener ?? new GestureThrottler(new GestureFilter(element, element.GestureHandler));

			simpleListener = new SimpleGestureListener(element, view, listener);
			simpleGestureDetector = new GestureDetector(view.Context, simpleListener);
			simpleGestureDetector.IsLongpressEnabled = false;		// will be handled by tapGestureDetector

			tapGestureListener = new TapGestureListener(element, view, listener);
			tapGestureDetector = new TapGestureDetector(view.Context, tapGestureListener);

			multiTouchGestureDetector = new MultiTouchGestureDetector(view.Context, simpleListener);

			mouseGestureListener = new MouseGestureListener(element, view, listener);
			mouseGestureDetector = new MouseGestureDetector(mouseGestureListener);

			if (element is not DatePicker && element is not Editor && element is not ListView && element is not Picker && element is not ScrollView && element is not Slider)
			{
				// In the elements listed above attaching the Touch event handler breaks the functionality of the element - even if the handler doesn't do anything.
				// But other elements (ContentPage, AbsoluteLayout, ...) stop working without those Touch event handlers even though the underlying Android view overrides OnTouchEvent and DispatchTouchEvent
				view.Touch += HandleTouch;
				view.GenericMotion += HandleGenericMotion;
			}

			if (element is VisualElement visElem)
            {
                // All but the Cells. Those have to be unloaded with the ListView/TableView.
                //visElem.Unloaded += Element_Unloaded;
				visElem.HandlerChanging += Element_HandlerChanging;
			}

			if (element is Cell cell)
            {
				cell.Appearing += Cell_Appearing;
				cell.Disappearing += Cell_Disappearing;

                if (cell.Parent is ListView list)
                    list.CellsToDispose.Add(cell);
            }

            multiPage = element.FindParent<MultiPage<Page>>();
			if (multiPage != null)
			{
				multiPage.CurrentPageChanged += MultiPage_CurrentPageChanged;
				//multiPage.Unloaded += MultiPage_Unloaded;
				multiPage.HandlerChanging += MultiPage_HandlerChanging;
			}

#if LOGINSTANCES
            Instances++;
			LogInstances("Constructor");
#endif
        }

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
                if (element is ListView listView)
                {
                    var children = listView.CellsToDispose.OfType<IGestureAwareControl>().ToArray();
#if LOGINSTANCES
                    Log($"element is a ListView with {children.Length} children");
#endif
                    foreach (var child in children)
                        AndroidGestureHandler.RemoveInstance(child);
                }

                if (element is TableView tableView)
                {
                    var children = tableView.Root.SelectMany(r => r).OfType<IGestureAwareControl>().ToArray();
#if LOGINSTANCES
                    Log($"element is a TableView with {children.Length} cells");
#endif
                    foreach (var child in children)
                        AndroidGestureHandler.RemoveInstance(child);
                }

                if (view != null)
				{
					view.Touch -= HandleTouch;
					view.GenericMotion -= HandleGenericMotion;
					view = null;
				}

				if (simpleGestureDetector != null)
				{
					simpleGestureDetector.Dispose();
					simpleGestureDetector = null;
					simpleListener.Dispose();
					simpleListener = null;
				}
				tapGestureDetector = null;
				tapGestureListener = null;
				multiTouchGestureDetector = null;
				mouseGestureListener = null;

				if (element is VisualElement visElem)
				{
					//visElem.Unloaded -= Element_Unloaded;
					visElem.HandlerChanging -= Element_HandlerChanging;
				}

				if (element is Cell cell)
                {
                    cell.Appearing -= Cell_Appearing;
                    cell.Disappearing -= Cell_Disappearing;

                    if (cell.Parent is ListView parentElement)
                        parentElement.CellsToDispose.Remove(cell);
}
                if (multiPage != null)
                {
                    multiPage.CurrentPageChanged -= MultiPage_CurrentPageChanged;
                    //multiPage.Unloaded -= MultiPage_Unloaded;
					multiPage.HandlerChanging -= MultiPage_HandlerChanging;
					if (element is VisualElement visElem2)
                        visElem2.Loaded -= MultiPage_Element_Loaded;
                    multiPage = null;
                }

#if LOGINSTANCES
                Instances--;
				LogInstances("Dispose");
#endif
			}
		}

        #endregion

        #region Other event handlers

//        private void Element_Unloaded(object sender, System.EventArgs e)
//        {
//            // A TabbedPage (=MultiPage) raises Unloaded when the current page changes.
//            // This must not remove the instance.
//            // Remove it only if no MultiPage is involved.

//#if LOGINSTANCES
//            Log($"Element_Unloaded {ElementLog(element)}");
//#endif
//            if (multiPage != null)
//				isVisible = false;
//			else
//	            RemoveInstance(element);
//        }

		private void Element_HandlerChanging(object sender, HandlerChangingEventArgs e)
		{
#if LOGINSTANCES
			var nh = e.NewHandler is null ? "null" : "set";
			var oh = e.OldHandler is null ? "null" : "set";
            Log($"Element_HandlerChanging {ElementLog(element)}, NewHandler is {nh}, OldHandler is {oh}");
#endif

			if (e.NewHandler is null)
				RemoveInstance(element);
		}

		private void Cell_Appearing(object sender, System.EventArgs e)
		{
			isVisible = true;
		}

		private void Cell_Disappearing(object sender, System.EventArgs e)
		{
			isVisible = false;
		}

        void MultiPage_CurrentPageChanged(object sender, System.EventArgs e)
        {
            isVisible = element.HasParent(multiPage.CurrentPage);
#if LOGINSTANCES
            Log($"MultiPage_CurrentPageChanged: isVisible set to {isVisible}");
#endif
        }

        private void MultiPage_Element_Loaded(object sender, System.EventArgs e)
        {
            isVisible = true;
#if LOGINSTANCES
            Log($"MultiPage_Element_Loaded: isVisible set to {isVisible}");
#endif
        }

//        private void MultiPage_Unloaded(object sender, System.EventArgs e)
//        {
//#if LOGINSTANCES
//            Log($"MultiPage_Unloaded");
//#endif
//            RemoveInstance(element);
//        }

		private void MultiPage_HandlerChanging(object sender, HandlerChangingEventArgs e)
		{
#if LOGINSTANCES
			var nh = e.NewHandler is null ? "null" : e.NewHandler.GetType().FullName;
			var oh = e.OldHandler is null ? "null" : e.OldHandler.GetType().FullName;
			Log($"MultiPage_HandlerChanging {ElementLog(element)}, NewHandler is {nh}, OldHandler is {oh}");
#endif

			if (e.NewHandler is null)
				RemoveInstance(element);
		}

        #endregion

        #region Touch handling

        void HandleTouch(object sender, AView.TouchEventArgs e)
		{
			HandleMotionEvent(e.Event);					// don't assign to e.Handled or there will be no more events until next DOWN action
		}

		private void HandleGenericMotion(object sender, AView.GenericMotionEventArgs e)
		{
			HandleMotionEvent(e.Event);
		}

		private bool HandleMotionEvent(MotionEvent e)
		{
			//PrintMotionEvent("HandleMotionEvent", e);

			var skip = !isVisible || MatchesLastMotionEvent(e);
            //Console.WriteLine($"MotionEvent.EventTime={e.EventTime}, Action={e.Action}, Source={e.Source}, ActionButton={e.ActionButton}, ButtonState={e.ButtonState} ... " + (skip ? "skipping" : "processing"));
            if (skip) return false;


			bool handled = false;

			if (mouseGestureDetector != null)
				handled |= mouseGestureDetector.OnGenericMotionEvent(e);
			if (simpleGestureDetector != null)
				handled |= simpleGestureDetector.OnTouchEvent(e);
			if (tapGestureDetector != null)
				handled |= tapGestureDetector.OnTouchEvent(e);
			if (e.PointerCount > 1)
			{
				if (multiTouchGestureDetector != null)
					handled |= multiTouchGestureDetector.OnTouchEvent(e);
			}

			// ButtonRelease is followed by Up, but Up does not have button info, so end the gestures on ButtonRelease already
			if (e.Action == MotionEventActions.Up || e.Action == MotionEventActions.ButtonRelease || e.Action == MotionEventActions.Cancel)
			{
				if (simpleListener != null)
					simpleListener.EndGestures(e);
			}

			return handled;
		}

		MotionEventActions lastAction = MotionEventActions.Up;
		long lastEventTime = 0;
		int lastPointerCount = 0;

		private bool MatchesLastMotionEvent(MotionEvent e)
		{
			if (e.Action == lastAction &&
				e.EventTime == lastEventTime &&
				e.PointerCount == lastPointerCount)
			{
				return true;
			}

			lastAction = e.Action;
			lastEventTime = e.EventTime;
			lastPointerCount = e.PointerCount;

			return false;
		}

		#endregion

		#region Logging

#if LOGINSTANCES
		internal void PrintMotionEvent(string title, MotionEvent e)		// for debugging
		{
			var sb = new StringBuilder(title);
			
			sb.AppendFormat(": Action: {0} on {1}", e.Action, element.GetType().Name);
			for (int i = 0; i < e.PointerCount; i++)
			{
				sb.AppendFormat(", Pointer {0}: {1}/{2}", i, e.GetX(i), e.GetY(i));
			}

			Debug.WriteLine(sb.ToString());
		}

		public static void PrintViewTree(global::Android.Views.View view, StringBuilder sb = null, int spaces = 0)
		{
			sb ??= new StringBuilder();
			var s = new string(' ', spaces);

			sb.AppendFormat("{0}{1}\n", s, view?.GetType().Name ?? "null");

			if (view is ViewGroup group)
			{
				for (int i = 0; i < group.ChildCount; i++)
				{
					var subView = group.GetChildAt(i);
					PrintViewTree(subView, sb, spaces + 2);
				}
			}

			if (spaces == 0)
				Debug.WriteLine($"Visible Views:\n{sb}\n");
		}

		private void LogInstances(string method)
		{
			Log($"AndroidGestureHandler.{method}, Element is a {ElementLog(element)} Total instances: {Instances}");
		}

		private static string ElementLog(IGestureAwareControl element)
		{
			if (element == null)
				return "null";

			var bindingContext = ((BindableObject)element).BindingContext ?? "null";
			return $"{element.GetType().Name}({element.GetHashCode()}), BindingContext={bindingContext}";
		}

        private static void Log(string s)
		{
			var threadType = System.Threading.Thread.CurrentThread.IsBackground ? "BG" : "UI";
			s = $"{DateTime.Now:HH:mm:ss.fff}: [T:{threadType}]: {s}";

			Debug.Write(s);
		}

#endif

		#endregion
	}
}