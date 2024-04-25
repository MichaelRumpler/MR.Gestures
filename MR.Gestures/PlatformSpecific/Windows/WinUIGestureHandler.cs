#define LOGINSTANCES

using System.Diagnostics;
using System.Text;

using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

using MR.Gestures.WinUI.EventArgs;

namespace MR.Gestures.WinUI
{
	public class WinUIGestureHandler : IDisposable
	{
#if LOGINSTANCES
		private static int Instances;
#endif

		#region Static Methods

		private static Dictionary<IGestureAwareControl, WinUIGestureHandler> allGestureHandlers = new Dictionary<IGestureAwareControl, WinUIGestureHandler>();

		public static void AddInstance(IGestureAwareControl element, FrameworkElement view)
		{
			RemoveInstance(element);

			var newHandler = new WinUIGestureHandler(element, view);
			allGestureHandlers[element] = newHandler;

			if (element is Cell cell)
			{
				if (cell.Parent is ListView parentElement)
					parentElement.CellsToDispose.Add(cell);
			}

			if (element is ListView || element is TableView)
				newHandler.cellContainerManager = new CellContainerManager(element, (Microsoft.UI.Xaml.Controls.ListView)view);
		}

		public static void RemoveInstance(IGestureAwareControl element)
		{
			if (element != null && allGestureHandlers.Remove(element, out var concreteHandler))
			{
				concreteHandler.Dispose();

				if (element is Cell cell)
				{
					if (cell.Parent is ListView parentElement)
						parentElement.CellsToDispose.Remove(cell);
				}
			}
		}

		public static void OnElementPropertyChanged(IGestureAwareControl element, FrameworkElement view)
		{
			if (element != null)
				AddInstance(element, view);
		}

		#endregion

		#region Fields

		private IGestureAwareControl element;
		FrameworkElement view;
		IGestureListener listener;
        
		Microsoft.UI.Input.GestureRecognizer gestureRecognizer;
		FrameworkElement root;

		Page parentPage = null;
		MultiPage<Page> multiPage = null;

		private bool isVisible = true;
		private CellContainerManager cellContainerManager;

		PointerEventHandler _pointerPressedHandler;
		PointerEventHandler _pointerMovedHandler;
		PointerEventHandler _pointerReleasedHandler;
		PointerEventHandler _pointerExitedHandler;
		PointerEventHandler _pointerWheelChangedHandler;

		#endregion

		#region Properties

		bool IsVisible
		{
			get
			{
				if (!isVisible)
					return false;

				for (DependencyObject v = view; v != null; v = VisualTreeHelper.GetParent(v))
				{
					if (v is UIElement u && u.Visibility != Microsoft.UI.Xaml.Visibility.Visible)
						return false;
				}

				return true;
			}
		}
		#endregion Properties

		#region Constructor

		private WinUIGestureHandler(IGestureAwareControl element, FrameworkElement view, IGestureListener listener = null)
		{
#if LOGINSTANCES
            Log($"Creating new WinUIGestureHandler for element {element.GetType().Name} and view {view.GetType().Name}.");
#endif
            this.element = element;
			this.view = view;
			this.listener = listener ?? new GestureThrottler(new GestureFilter(element, element.GestureHandler));

			// explains why ScrollView consumes gestures: http://blogs.msdn.com/b/wsdevsol/archive/2013/02/16/where-did-all-my-gestures-go.aspx
			// my stackoverflow article: http://stackoverflow.com/questions/31407832/touch-input-and-direct-manipulation


			// add event handlers

			//view.Loaded += (s, e) => Log(view.GetType().FullName + " view.Loaded");
			//view.Unloaded += (s, e) => Log(view.GetType().FullName + " view.Unloaded");

			#region handle elements visibility

			var cell = element as Cell;
			if (cell != null)
			{
				cell.Appearing += Appearing;
				cell.Disappearing += Disappearing;
			}
			else
			{
				parentPage = element.FindParent<Page>();
				if (parentPage != null)
				{
					parentPage.Appearing += Appearing;
					parentPage.Disappearing += Disappearing;
				}
				else
				{
#if LOGINSTANCES
					Log($"MR.Gestures Warning: The {element.GetType().Name} has no parent Page. Gesture events will not be reliable. The element hierarchy is {((Element)element).TreeHierarchy()}.");
#endif
				}
			}
			multiPage = element.FindParent<MultiPage<Page>>();
			if (multiPage != null)
				multiPage.CurrentPageChanged += MultiPage_CurrentPageChanged;

			#endregion
			#region initialize gestureRecognizer

			gestureRecognizer = new Microsoft.UI.Input.GestureRecognizer();
			var mode = GestureSettings.None;

			if (element.GestureHandler.HandlesTapping || element.GestureHandler.HandlesTapped || element.GestureHandler.HandlesDoubleTapped)
			{
				gestureRecognizer.Tapped += OnTap;
				//gestureRecognizer.DoubleTapped += onDoubleTap;

				mode |= GestureSettings.Tap;
			}

			if (element.GestureHandler.HandlesLongPressing || element.GestureHandler.HandlesLongPressed)
			{
				gestureRecognizer.Holding += OnHold;
				mode |= GestureSettings.Hold | GestureSettings.HoldWithMouse;
			}

			if (element.GestureHandler.HandlesPanning || element.GestureHandler.HandlesPanned
				|| element.GestureHandler.HandlesSwiped
				|| element.GestureHandler.HandlesPinching || element.GestureHandler.HandlesPinched
				|| element.GestureHandler.HandlesRotating || element.GestureHandler.HandlesRotated)
			{
				if (element.GestureHandler.HandlesPanning || element.GestureHandler.HandlesPanned || element.GestureHandler.HandlesSwiped)
					mode |= GestureSettings.ManipulationTranslateX | GestureSettings.ManipulationTranslateY;
				if (element.GestureHandler.HandlesPinching || element.GestureHandler.HandlesPinched)
					mode |= GestureSettings.ManipulationScale;
				if (element.GestureHandler.HandlesRotating || element.GestureHandler.HandlesRotated)
					mode |= GestureSettings.ManipulationRotate;

				//view.ManipulationStarted += onManipulationStarted;
				gestureRecognizer.ManipulationUpdated += OnManipulationDelta;
				gestureRecognizer.ManipulationCompleted += OnManipulationCompleted;
			}

			gestureRecognizer.GestureSettings = mode;

            #endregion
			#region initialize pointer events

			// the view needs to be added to the layout to find the root and listen to its pointer events
			if (view.IsLoaded)
				View_Loaded(this, null);
			else
	            view.Loaded += View_Loaded;

            #endregion

			// may use view.Unloaded instead
			if(element is VisualElement visElem)
            {
				// All but the Cells. Those have to be unloaded with the ListView/TableView.
				visElem.HandlerChanging += Element_HandlerChanging;
			}

#if LOGINSTANCES
			Instances++;
			LogInstances(".ctor");
#endif
		}

        #endregion

        #region Event Handlers

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
			//Log($"View_Loaded {view}, adding pointer handlers");

			if (view is null)		// initialization hang and view has already been disposed
				return;

            view.Loaded -= View_Loaded;

			_pointerPressedHandler = new PointerEventHandler(Window_PointerPressed);
			_pointerMovedHandler = new PointerEventHandler(Window_PointerMoved);
			_pointerReleasedHandler = new PointerEventHandler(Window_PointerReleased);
			_pointerExitedHandler = new PointerEventHandler(Window_PointerExited);
			_pointerWheelChangedHandler = new PointerEventHandler(Window_PointerWheelChanged);
			
			root = view.GetRoot();
            root.AddHandler(UIElement.PointerPressedEvent, _pointerPressedHandler, true);
			root.AddHandler(UIElement.PointerEnteredEvent, _pointerMovedHandler, true);
            root.AddHandler(UIElement.PointerMovedEvent, _pointerMovedHandler, true);
            root.AddHandler(UIElement.PointerReleasedEvent, _pointerReleasedHandler, true);
            root.AddHandler(UIElement.PointerCaptureLostEvent, _pointerExitedHandler, true);
            root.AddHandler(UIElement.PointerCanceledEvent, _pointerExitedHandler, true);
            root.AddHandler(UIElement.PointerExitedEvent, _pointerExitedHandler, true);
			root.AddHandler(UIElement.PointerWheelChangedEvent, _pointerWheelChangedHandler, true);
        }

		private void Element_HandlerChanging(object sender, HandlerChangingEventArgs e)
		{
#if LOGINSTANCES
			var nh = e.NewHandler is null ? "null" : "set";
			var oh = e.OldHandler is null ? "null" : "set";
			Log($"Element_HandlerChanging NewHandler is {nh}, OldHandler is {oh}");
#endif

			if (e.NewHandler is null)
				RemoveInstance(element);
		}

		#region Appearing/Disappearing

		private void Appearing(object sender, System.EventArgs e)
		{
			isVisible = true;
		}
		private void Disappearing(object sender, System.EventArgs e)
		{
			isVisible = false;
		}

		void MultiPage_CurrentPageChanged(object sender, System.EventArgs e)
		{
			isVisible = element.HasParent(multiPage.CurrentPage);
			//Log("MultiPage_CurrentPageChanged: isVisible set to {0}", isVisible);
		}

		#endregion

		#region Window Pointer events + Down/Up

		PointerDeviceType currentInputDeviceType;
		List<PointerPoint> currentPointers = new List<PointerPoint>();		// the pointers currently over the view
		PointerPoint currentPoint;											// current mouse pointer
		bool isHover = false;                                               // is the mouse pointer over the view?
		ulong pointerExitedTimestamp = 0;									// timestamp when pointer exited the page

        private void Window_PointerPressed(object sender, PointerRoutedEventArgs ev)
		{
			//LogPointerEvent("Window_PointerPressed");

            if (!IsVisible)                                                                 // ignore elements which are currently not visible
				return;

			currentPoint = ev.GetCurrentPoint(null);

			if (currentPointers.Count == 0)             //first pointer down
				currentInputDeviceType = currentPoint.PointerDeviceType;
			else if (currentInputDeviceType != currentPoint.PointerDeviceType)        // ignore pointers from other devices
				return;

			if (!IsOverView(currentPoint.Position))
				return;

			currentPointers.Add(currentPoint);

			if (element.GestureHandler.HandlesDown)
				listener.OnDown(new WinUIDownUpEventArgs(currentPointers, currentPointers.Count - 1, view));

			try
			{
				gestureRecognizer.ProcessDownEvent(currentPoint);
			}
			catch { }
		}

        private void Window_PointerMoved(object sender, PointerRoutedEventArgs ev)
		{
			//LogPointerEvent("Window_PointerMoved");

			if (!IsVisible)                                                                 // ignore elements which are currently not visible
                return;

            currentPoint = ev.GetCurrentPoint(null);

			if (currentPoint.Timestamp == pointerExitedTimestamp)		// if this Moved event was raised after an Exited event but at the same time,
				return;													// then ignore it


            var newIsHover = IsOverView(currentPoint.Position);
            if (newIsHover != isHover)
            {
				if (ev.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
				{
					if (newIsHover)
						OnMouseEntered();
					else
                        OnMouseExited();
                }

                isHover = newIsHover;
            }

            if (isHover && ev.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
                OnMouseMoved();

            var i = IndexOfPointer(currentPoint);
            if (i > -1)
            {
                currentPointers[i] = currentPoint;

                try
                {
                    gestureRecognizer.ProcessMoveEvents(ev.GetIntermediatePoints(null));
                }
                catch { }
            }
        }

        private void Window_PointerReleased(object sender, PointerRoutedEventArgs ev)
		{
			//LogPointerEvent("Window_PointerReleased");

			currentPoint = ev.GetCurrentPoint(null);

            //LogPointerEvent($"OnPointerReleased {currentPoint.Timestamp}");
			if (OnUp(currentPoint, false))
			{
				try
				{
					gestureRecognizer.ProcessUpEvent(currentPoint);
				}
				catch { }
			}

            //LogPointerEvent("OnPointerReleased finished");
			
        }

        private void Window_PointerExited(object sender, PointerRoutedEventArgs ev)
        {
			//LogPointerEvent("Window_PointerExited");

			currentPoint = ev.GetCurrentPoint(null);
			pointerExitedTimestamp = currentPoint.Timestamp;

            if (isHover)
            {
                if (ev.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
                    OnMouseExited();

                isHover = false;
            }

            if (OnUp(currentPoint, true))
            {
                try
                {
                    gestureRecognizer.CompleteGesture();
                }
                catch { }
            }
        }

        private void Window_PointerWheelChanged(object sender, PointerRoutedEventArgs ev)
        {
            currentPoint = ev.GetCurrentPoint(null);

            if (isHover && ev.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
                OnScrollWheelChanged();
        }

        private bool OnUp(PointerPoint pointer, bool cancel)
		{
            //Log("OnUp: pointerId: " + pointer.PointerId);

            if (!IsVisible)                                                                 // ignore elements which are currently not visible
				return false;

			var i = IndexOfPointer(pointer);
			if (i > -1)
			{
				if (element.GestureHandler.HandlesUp)
					listener.OnUp(new WinUIDownUpEventArgs(currentPointers, i, view, cancel));

				currentPointers.RemoveAt(i);

				return true;
			}

            //Log("OnUp: could not find pointer with id " + pointer.PointerId);
            return false;
		}

        private MouseEventArgs lastMouseArgs;
        ulong prevMouseTimestamp;

        private bool OnMouseEntered()
        {
            bool handled = false;

            var mouseArgs = new WinUIMouseEventArgs(view, currentPoint, null, currentPoint.Timestamp);

            if (element.GestureHandler.HandlesMouseEntered)
                handled = listener.OnMouseEntered(mouseArgs);

            lastMouseArgs = mouseArgs;
            prevMouseTimestamp = currentPoint.Timestamp;

            return handled;
        }

        private bool OnMouseMoved()
        {
            bool handled = false;

            var mouseArgs = new WinUIMouseEventArgs(view, currentPoint, lastMouseArgs, prevMouseTimestamp);

            if (element.GestureHandler.HandlesMouseMoved)
                handled = listener.OnMouseMoved(mouseArgs);

            lastMouseArgs = mouseArgs;
            prevMouseTimestamp = currentPoint.Timestamp;

            return handled;
        }

        private bool OnMouseExited()
        {
            bool handled = false;

            var mouseArgs = new WinUIMouseEventArgs(view, currentPoint, lastMouseArgs, prevMouseTimestamp);

            if (element.GestureHandler.HandlesMouseExited)
                handled = listener.OnMouseExited(mouseArgs);

            lastMouseArgs = null;
            prevMouseTimestamp = 0;

            return handled;
        }

        private bool OnScrollWheelChanged()
        {
            bool handled = false;

            var mouseArgs = new WinUIScrollWheelEventArgs(view, currentPoint);

            if (element.GestureHandler.HandlesScrollWheelChanged)
                handled = listener.OnScrollWheelChanged(mouseArgs);

            return handled;
        }

        private bool IsOverView(Windows.Foundation.Point point)
        {
            UIElement[] elements = null;
            try
            {
                //elements = VisualTreeHelper.FindElementsInHostCoordinates(point, view, true)?.ToArray();
                //LogFindElementsInHostCoordinatesCalls("point, view, true", elements);
                //elements = VisualTreeHelper.FindElementsInHostCoordinates(point, view, false)?.ToArray();
                //LogFindElementsInHostCoordinatesCalls("point, view, false", elements);
                //elements = VisualTreeHelper.FindElementsInHostCoordinates(point, null, true)?.ToArray();          // null funktioniert in WinUI nicht mehr. throws
                //LogFindElementsInHostCoordinatesCalls("point, null, true", elements);
                //elements = VisualTreeHelper.FindElementsInHostCoordinates(point, null, false)?.ToArray();
                //LogFindElementsInHostCoordinatesCalls("point, null, false", elements);

                elements = VisualTreeHelper.FindElementsInHostCoordinates(point, view, true)?.ToArray();
                //elements = VisualTreeHelper.FindElementsInHostCoordinates(point, null, true)
                //	.Where(e => !(e is Windows.UI.Xaml.Shapes.Rectangle))		// have to ignore TextBlock too
                //	.ToArray();
            }
            catch
            {
                //Log($"IsOverView: an exception occured in VisualTreeHelper.FindElementsInHostCoordinates: {ex}");
            }

            return elements != null && elements.Length > 0;
        }

        private int IndexOfPointer(PointerPoint pointer)
		{
			for (int i = 0; i < currentPointers.Count; i++)
			{
				if (currentPointers[i].PointerId == pointer.PointerId)
					return i;
			}

			return -1;
		}

		#endregion

		#region Tapping, Tapped, DoubleTapped

		int numberOfTaps = 0;
		CancellationTokenSource cancelTappedRaiser;

		private void OnTap(Microsoft.UI.Input.GestureRecognizer sender, Microsoft.UI.Input.TappedEventArgs args)
		{
			//Log($"OnTap: Position: {args.Position}, PointerDeviceType: {args.PointerDeviceType}, TapCount: {args.TapCount}");

			if (cancelTappedRaiser != null)
			{
				try
				{
					cancelTappedRaiser.Cancel();
				}
				catch { }
			}

			numberOfTaps++;
			var tapArgs = new WinUITapEventArgs(currentPoint, view, numberOfTaps);

			if (element.GestureHandler.HandlesTapping)
				listener.OnTapping(tapArgs);

			cancelTappedRaiser = new CancellationTokenSource();
			Task.Run(async () =>
			{
				await Task.Delay(MR.Gestures.Settings.MsUntilTapped, cancelTappedRaiser.Token).ConfigureAwait(false);
				cancelTappedRaiser = null;      // I cannot be cancelled anymore

				numberOfTaps = 0;
				if (tapArgs.NumberOfTaps == 1 && element.GestureHandler.HandlesTapped)
					view.DispatcherQueue?.TryEnqueue(() => listener.OnTapped(tapArgs));
				else if (tapArgs.NumberOfTaps == 2 && element.GestureHandler.HandlesDoubleTapped)
					view.DispatcherQueue?.TryEnqueue(() => listener.OnDoubleTapped(tapArgs));

			}, cancelTappedRaiser.Token);
		}

		#endregion

		#region LongPressing

		private LongPressEventArgs lastPressing = null;
		private Stopwatch startPressing;

		private void OnHold(Microsoft.UI.Input.GestureRecognizer sender, HoldingEventArgs args)
		{
			//Log("onHold: HoldingState=" + e.HoldingState.ToString() + ", longPressing=" + longPressing);

			bool handled = false;

			switch (args.HoldingState)
			{
				case HoldingState.Started:
					handled = OnHoldStarted();
					break;

				case HoldingState.Completed:
				case HoldingState.Canceled:
					handled = OnHoldCompleted(args.HoldingState == HoldingState.Canceled);
					break;
			}
		}

		private bool OnHoldStarted()
		{
			bool handled = false;

			if (startPressing == null)
				startPressing = new Stopwatch();
			else
				startPressing.Reset();
			startPressing.Start();

			lastPressing = new WinUILongPressEventArgs(currentPointers, view);

			if (element.GestureHandler.HandlesLongPressing)
				handled = listener.OnLongPressing(lastPressing);

			return handled;
		}

		private bool OnHoldCompleted(bool canceled)
		{
			bool handled = false;

			startPressing?.Stop();

			if (lastPressing != null)
			{
				if (element.GestureHandler.HandlesLongPressed)
					handled = listener.OnLongPressed(new WinUILongPressEventArgs(currentPointers, view, startPressing.ElapsedMilliseconds, canceled, lastPressing));

				lastPressing = null;
			}

			return handled;
		}

		#endregion

		#region Manipulation

		private PanEventArgs lastPanArgs;
		private PinchEventArgs lastPinchArgs;
		private RotateEventArgs lastRotateArgs;

		private void OnManipulationDelta(Microsoft.UI.Input.GestureRecognizer sender, ManipulationUpdatedEventArgs args)
		{
			OnManipulationDelta(args.Delta.Translation, args.Cumulative.Translation, args.Velocities.Linear);
		}

		private bool OnManipulationDelta(Windows.Foundation.Point deltaDistance, Windows.Foundation.Point totalDistance, Windows.Foundation.Point velocity)
		{
			//Log("onManipulationDelta: currentPointers.Count=" + currentPointers.Count);

			bool handled = false;

			if (currentPointers.Count > 0)
			{
				//	Panning

				var panArgs = new WinUIPanEventArgs(view, currentPointers, deltaDistance, totalDistance, velocity);
				if (element.GestureHandler.HandlesPanning)
					handled = listener.OnPanning(panArgs);
				lastPanArgs = panArgs;

				if (currentPointers.Count > 1)
				{
					//	Pinching

					var pinchArgs = new WinUIPinchEventArgs(currentPointers, view, lastPinchArgs);
					if (element.GestureHandler.HandlesPinching)
						handled |= listener.OnPinching(pinchArgs);
					lastPinchArgs = pinchArgs;

					//	Rotating

					var rotateArgs = new WinUIRotateEventArgs(currentPointers, view, lastRotateArgs);
					if (element.GestureHandler.HandlesRotating)
						handled |= listener.OnRotating(rotateArgs);
					lastRotateArgs = rotateArgs;
				}
				else
					handled |= EndTwoFingerGestures();
			}

			return handled;
		}

		private void OnManipulationCompleted(Microsoft.UI.Input.GestureRecognizer sender, ManipulationCompletedEventArgs args)
		{
			OnManipulationCompleted(args.Cumulative.Translation, args.Velocities.Linear);
		}

		private bool OnManipulationCompleted(Windows.Foundation.Point totalDistance, Windows.Foundation.Point velocity)
		{
			//Log("onManipulationCompleted: currentPointers.Count=" + currentPointers.Count);

			bool handled = false;

			//	Swiped / Panned

			var swipedX = Math.Abs(velocity.X) > Settings.SwipeVelocityThreshold;
			var swipedY = Math.Abs(velocity.Y) > Settings.SwipeVelocityThreshold;
			if ((swipedX || swipedY) && element.GestureHandler.HandlesSwiped)
			{
				var direction = Direction.NotClear;
				if (!swipedY)
					direction = velocity.X > 0 ? Direction.Right : Direction.Left;
				else if (!swipedX)
					direction = velocity.Y > 0 ? Direction.Down : Direction.Up;

				handled |= listener.OnSwiped(new WinUISwipeEventArgs(velocity, currentPointers, view, direction, lastPanArgs));
			}
			else if (element.GestureHandler.HandlesPanning || element.GestureHandler.HandlesPanned)
			{
				PanEventArgs panArgs = null;
				if (currentPointers.Count > 0)
					panArgs = new WinUIPanEventArgs(view, currentPointers, new Windows.Foundation.Point(0, 0), totalDistance, velocity);
				else
					panArgs = lastPanArgs;

				if (panArgs != null)
					handled |= listener.OnPanned(panArgs);
			}
			lastPanArgs = null;

			handled |= EndTwoFingerGestures();

			// Sometimes PointerReleased is not raised. So I remove the pointers here too as a precaution.
			while (currentPointers.Count > 0)
				OnUp(currentPointers[0], false);

			return handled;
		}

		private bool EndTwoFingerGestures()
		{
			bool handled = false;

			//	Pinched
			if (lastPinchArgs != null && (element.GestureHandler.HandlesPinching || element.GestureHandler.HandlesPinched))
				handled = listener.OnPinched(new WinUIPinchEventArgs(currentPointers, view, lastPinchArgs));
			lastPinchArgs = null;

			//	Rotated
			if (lastRotateArgs != null && (element.GestureHandler.HandlesRotating || element.GestureHandler.HandlesRotated))
				handled |= listener.OnRotated(new WinUIRotateEventArgs(currentPointers, view, lastRotateArgs));
			lastRotateArgs = null;

			return handled;
		}

		#endregion

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
                // remove event handlers

                if (element is Cell cell)
                {
                    cell.Appearing -= Appearing;
                    cell.Disappearing -= Disappearing;
                }
                if (parentPage != null)
				{
					parentPage.Appearing -= Appearing;
					parentPage.Disappearing -= Disappearing;
					parentPage = null;
				}
				if (multiPage != null)
				{
					multiPage.CurrentPageChanged -= MultiPage_CurrentPageChanged;
					multiPage = null;
				}

				if (root != null)
				{
					root.RemoveHandler(UIElement.PointerPressedEvent, _pointerPressedHandler);
					root.RemoveHandler(UIElement.PointerEnteredEvent, _pointerMovedHandler);
					root.RemoveHandler(UIElement.PointerMovedEvent, _pointerMovedHandler);
					root.RemoveHandler(UIElement.PointerReleasedEvent, _pointerReleasedHandler);
					root.RemoveHandler(UIElement.PointerCaptureLostEvent, _pointerExitedHandler);
					root.RemoveHandler(UIElement.PointerCanceledEvent, _pointerExitedHandler);
					root.RemoveHandler(UIElement.PointerExitedEvent, _pointerExitedHandler);
					root.RemoveHandler(UIElement.PointerWheelChangedEvent, _pointerWheelChangedHandler);
                
					root = null;
					_pointerPressedHandler = null;
					_pointerMovedHandler = null;
					_pointerReleasedHandler = null;
					_pointerExitedHandler = null;
					_pointerWheelChangedHandler = null;
				}

				if (gestureRecognizer != null)
				{
					gestureRecognizer.Tapped -= OnTap;

					gestureRecognizer.Holding -= OnHold;

					gestureRecognizer.ManipulationUpdated -= OnManipulationDelta;
					gestureRecognizer.ManipulationCompleted -= OnManipulationCompleted;

					gestureRecognizer = null;
				}

				if (cellContainerManager != null)
				{
					cellContainerManager.Dispose();
					cellContainerManager = null;
				}

				if (element is VisualElement visElem)
					visElem.HandlerChanging -= Element_HandlerChanging;

#if LOGINSTANCES
				Instances--;
				LogInstances("Dispose");
#endif

				element = null;
				view = null;
				listener = null;
			}
		}

        #endregion

        #region Logging

#if LOGINSTANCES
        private void LogInstances(string method)
		{
			Log(String.Format("WinUIGestureHandler.{0}, Element is a {1}, Total instances: {2}", method, element.GetType().Name, Instances));
		}

		private void LogFindElementsInHostCoordinatesCalls(string parameters, UIElement[] elements)
		{
			var sb = new StringBuilder($"FindElementsInHostCoordinates({parameters}):");
			if (elements == null)
				sb.Append(" returned null");
			else if (elements.Length == 0)
				sb.Append(" returned no elements");
			else
			{
				int i = 0;
				foreach (var el in elements)
					sb.Append($"\n  [{i++}]: {el.GetType().FullName}");
			}

			Log(sb.ToString());
		}

		int moved = 0;
		private void LogPointerEvent(string method)
		{
            if (method.EndsWith("PointerMoved"))
            {
                moved++;
                return;
            }

            if (moved > 0)
            {
                Log(string.Format("OnPointerMoved {0} times", moved));
                moved = 0;
            }
            Log(method);
		}

		private static void Log(string s)
		{
            var threadType = System.Threading.Thread.CurrentThread.IsBackground ? "BG" : "UI";
            s = $"{DateTime.Now:HH:mm:ss.fff}: [T:{threadType}]: {s}";

			Debug.WriteLine(s);
		}
#endif

		#endregion
	}
}
