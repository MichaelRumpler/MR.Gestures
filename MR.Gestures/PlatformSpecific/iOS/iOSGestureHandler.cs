#define LOGINSTANCES

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using UIKit;

using MR.Gestures.iOS.EventArgs;


namespace MR.Gestures.iOS
{
	public class iOSGestureHandler : IDownUpListener, IMultiTouchListener, IDisposable
	{
#if LOGINSTANCES
		private static int Instances;
#endif

		#region Static Methods

		private static Dictionary<IGestureAwareControl, iOSGestureHandler> allGestureHandlers = new Dictionary<IGestureAwareControl, iOSGestureHandler>();

        public static void AddInstance(IGestureAwareControl element, UIView view)
        {
			if (allGestureHandlers.TryGetValue(element, out var concreteHandler))
			{
				concreteHandler.RemoveGestureRecognizers();
			}
			else
			{
				concreteHandler = new iOSGestureHandler(element);
				allGestureHandlers[element] = concreteHandler;
			}

            concreteHandler.AddGestureRecognizers(view);
        }

		public static void RemoveInstance(IGestureAwareControl element)
		{
			if (element != null && allGestureHandlers.Remove(element, out var concreteHandler))
			{
                if (element is ListView listView)
                {
                    foreach (var child in listView.CellsToDispose.OfType<IGestureAwareControl>().ToArray())
                        RemoveInstance(child);
                }

                if (element is TableView tableView)
                {
                    foreach (var child in tableView.Root.SelectMany(r => r).OfType<IGestureAwareControl>().ToArray())
                        RemoveInstance(child);
                }

				concreteHandler.Dispose();
            }
		}

        public static void OnElementPropertyChanged(IGestureAwareControl element, UIView view)
		{
			if (element != null)
			{
                if (!allGestureHandlers.TryGetValue(element, out var handler))
                {
                    handler = new iOSGestureHandler(element);
                    allGestureHandlers[element] = handler;
                }

				handler.RemoveGestureRecognizers();
				handler.AddGestureRecognizers(view);
			}
		}

		static bool? s_isiOS13_4OrNewer;
		public static bool IsiOS13_4OrNewer
		{
			get
			{
				if (!s_isiOS13_4OrNewer.HasValue)
					s_isiOS13_4OrNewer = UIDevice.CurrentDevice.CheckSystemVersion(13, 4);
				return s_isiOS13_4OrNewer.Value;
			}
		}

		#endregion

		#region Private fields

		private IGestureAwareControl element;
		private UIView view;
		private IGestureListener listener;
		
		private UIGestureRecognizer[] gestureRecognizers;
		private UIHoverGestureRecognizer hoverGr;

		private bool isVisible = true;

		#endregion Private fields

		#region Constructor, Add-/RemoveGestureRecognizers

		private iOSGestureHandler(IGestureAwareControl element, IGestureListener listener = null)
		{
			this.element = element;
			this.listener = listener ?? new GestureThrottler(new GestureFilter(element, element.GestureHandler));

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
                cell.PropertyChanged += Cell_PropertyChanged;

                if (cell.Parent is ListView parent)
                    parent.CellsToDispose.Add(cell);
            }

#if LOGINSTANCES
            Instances++;
            LogInstances("Constructor");
#endif
        }

		private void RemoveGestureRecognizers()
		{
			if (gestureRecognizers != null)
			{
				foreach (var gr in gestureRecognizers)
					gr.View.RemoveGestureRecognizer(gr);
				gestureRecognizers = null;
			}
		}

		private void AddGestureRecognizers(UIView view)
		{
			this.view = view;
			this.gestureRecognizers = CreateGestureRecognizers();

			foreach (var gr in gestureRecognizers)
			{
				if (IsiOS13_4OrNewer)
					gr.ShouldReceiveEvent = (gr, ev) => true;
				view.AddGestureRecognizer(gr);
			}
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

		#endregion Constructor, Add-/RemoveGestureRecognizers

		#region CreateGestureRecognizers

		private UIGestureRecognizer[] CreateGestureRecognizers()
		{
			// https://developer.apple.com/library/content/documentation/EventHandling/Conceptual/EventHandlingiPhoneOS/GestureRecognizer_basics/GestureRecognizer_basics.html

			var rc = new List<UIGestureRecognizer>();

			if (element.GestureHandler.HandlesDown || element.GestureHandler.HandlesUp
				|| element.GestureHandler.HandlesPanning || element.GestureHandler.HandlesPanned)		// first panning needs lastDownArgs
			{
				//if (view is UIControl ctrl)
				//{
				//	// see https://github.com/xamarin/xamarin-macios/issues/4598
					
				//	//ctrl.AddTarget(this, new ObjCRuntime.Selector("slider:event:"), UIControlEvent.ValueChanged);		// "this" needs to be a NSObject

				//	ctrl.AddTarget(OnTouchDown, UIControlEvent.TouchDown);
				//	ctrl.AddTarget(OnTouchUpInside, UIControlEvent.TouchUpInside);
				//	ctrl.AddTarget(OnTouchUpOutside, UIControlEvent.TouchUpOutside);
				//}

				// TODO: else

				var downUpGr = new DownUpGestureRecognizer(this)
				{
					ShouldRecognizeSimultaneously = (thisGr, otherGr) => true,
					CancelsTouchesInView = false
				};
				//downUpGr.ShouldBegin = (otherGr) => false;			?? does not seem to have any effect

				rc.Add(downUpGr);
			}

			UILongPressGestureRecognizer longPressGr = null;
			if (element.GestureHandler.HandlesLongPressing || element.GestureHandler.HandlesLongPressed)
			{
				longPressGr = new UILongPressGestureRecognizer(OnLongPressed)
				{
					ShouldRecognizeSimultaneously = (thisGr, otherGr) => !(otherGr is UITapGestureRecognizer),
					CancelsTouchesInView = false
				};
				/*
				 * can be configured with:
				 * allowableMovement
				 * minimumPressDuration
				 * numberOfTapsRequired
				 * numberOfTouchesRequired
				 */

				rc.Add(longPressGr);
			}

			if (element.GestureHandler.HandlesTapping || element.GestureHandler.HandlesTapped || element.GestureHandler.HandlesDoubleTapped)
			{
				var tapGr = new UITapGestureRecognizer(OnTapped)
				{
					ShouldRecognizeSimultaneously = (thisGr, otherGr) => false,
					CancelsTouchesInView = false
				};
				/*
				 * can be configured with:
				 * numberOfTapsRequired
				 * numberOfTouchesRequired
				 */

				if (longPressGr != null)
					tapGr.RequireGestureRecognizerToFail(longPressGr);

				rc.Add(tapGr);
			}

			if (element.GestureHandler.HandlesPanning || element.GestureHandler.HandlesPanned || element.GestureHandler.HandlesSwiped
				|| element.GestureHandler.HandlesLongPressing || element.GestureHandler.HandlesLongPressed)		// a pan cancels LongPress
			{
				var panGr = new UIPanGestureRecognizer(OnPanned)
				{
					ShouldRecognizeSimultaneously = (thisGr, otherGr) => !(otherGr is UITapGestureRecognizer),
					CancelsTouchesInView = false
				};
				/*
				 * can be configured with:
				 * maximumNumberOfTouches
				 * minimumNumberOfTouches - default is 1, but it is still sometimes raised with 0 touches
				 */

				rc.Add(panGr);
			}

			if (element.GestureHandler.HandlesPinching || element.GestureHandler.HandlesPinched
				|| element.GestureHandler.HandlesRotating || element.GestureHandler.HandlesRotated)
			{
				var multiGr = new MultiTouchGestureRecognizer(this)
				{
					ShouldRecognizeSimultaneously = (thisGr, otherGr) => !(otherGr is UITapGestureRecognizer),
					CancelsTouchesInView = false
				};

				rc.Add(multiGr);
			}

			if (element.GestureHandler.HandlesMouseEntered || element.GestureHandler.HandlesMouseMoved || element.GestureHandler.HandlesMouseExited)
			{
				if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0) && UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
				{
					hoverGr = new UIHoverGestureRecognizer(OnHover)
					{
						ShouldRecognizeSimultaneously = (thisGr, otherGr) => !(otherGr is UITapGestureRecognizer),
						CancelsTouchesInView = false
					};

					rc.Add(hoverGr);
				}
				else
					Debug.WriteLine("MouseEntered, MouseMoved and MouseExited require an iPad with at least iOS 13.0!");
			}

			if(element.GestureHandler.HandlesScrollWheelChanged)
            {
				if (UIDevice.CurrentDevice.CheckSystemVersion(13, 4) && UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
				{
					var panGr = new UIPanGestureRecognizer(OnScrollWheelChanged)
					{
						ShouldRecognizeSimultaneously = (thisGr, otherGr) => !(otherGr is UITapGestureRecognizer),
						CancelsTouchesInView = false,
						AllowedScrollTypesMask = UIScrollTypeMask.All,
					};

					rc.Add(panGr);
				}
				else
					Debug.WriteLine("ScrollWheelChanged requires an iPad with at least iOS 13.4!");
			}
			return rc.ToArray();
		}

        #endregion

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
				if (gestureRecognizers != null)
				{
					foreach (var gr in gestureRecognizers)
						gr.View.RemoveGestureRecognizer(gr);
					gestureRecognizers = null;
				}

				if (element is VisualElement visElem)
				{
					visElem.HandlerChanging -= Element_HandlerChanging;
				}

				if (element is Cell cell)
                {
                    cell.Appearing -= Cell_Appearing;
                    cell.Disappearing -= Cell_Disappearing;
                    cell.PropertyChanged -= Cell_PropertyChanged;
                }

#if LOGINSTANCES
                Instances--;
				LogInstances("Dispose");
#endif
			}
		}

        #endregion

        #region Cell event handlers

        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (GestureHandler.AllProperties.Contains(e.PropertyName))
            {
                RemoveGestureRecognizers();
                AddGestureRecognizers(view);
            }
        }

        private void Cell_Appearing(object sender, System.EventArgs e)
        {
            isVisible = true;
        }
        private void Cell_Disappearing(object sender, System.EventArgs e)
        {
            isVisible = false;
        }

        #endregion Cell event handlers

        #region Down & Up

        DownUpEventArgs lastDownArgs;

		public void OnDown(UIGestureRecognizer gr, UITouch[] touchesBegan)
		{
			if (!isVisible) return;                         // Views are reused for Cells, but this Cell is currently not visible

			lastDownArgs = new iOSDownUpEventArgs(gr, touchesBegan);

			if (element.GestureHandler.HandlesDown)
			{
				listener.OnDown(lastDownArgs);
				gr.CancelsTouchesInView = lastDownArgs.Handled;
			}
		}

		public void OnUp(UIGestureRecognizer gr, UITouch[] touchesEnded)
		{
			if (!isVisible) return;							// Views are reused for Cells, but this Cell is currently not visible

			if (element.GestureHandler.HandlesUp)
			{
				var args = new iOSDownUpEventArgs(gr, touchesEnded);
				listener.OnUp(args);
				gr.CancelsTouchesInView = args.Handled;
			}
		}

		#endregion
		#region Tap

		int numberOfTaps = 0;
		CancellationTokenSource cancelTappedRaiser;

		private void OnTapped(UITapGestureRecognizer gr)
		{
			if (!isVisible) return;												// Views are reused for Cells, but this Cell is currently not visible

			if (cancelTappedRaiser != null)
			{
				try
				{
					cancelTappedRaiser.Cancel();
					//Log("onTapped: cancelled task");
				}
				catch { }
			}

			numberOfTaps++;
			var args = new iOSTapEventArgs(gr, numberOfTaps);
			//Log("onTapped: raising Tapping with " + args.NumberOfTaps + " taps");

			if (element.GestureHandler.HandlesTapping)
			{
				listener.OnTapping(args);
				gr.CancelsTouchesInView = args.Handled;
			}

			cancelTappedRaiser = new CancellationTokenSource();
			Task.Run(async () => {
				//Log("onTapped: task started, waiting...");
				await Task.Delay(MR.Gestures.Settings.MsUntilTapped, cancelTappedRaiser.Token).ConfigureAwait(false);
				cancelTappedRaiser = null;		// I cannot be cancelled anymore
				numberOfTaps = 0;
				//Log("onTapped: task waited, raising *Tapped with " + args.NumberOfTaps + " taps");
				if (args.NumberOfTaps == 1 && element.GestureHandler.HandlesTapped)
					view.InvokeOnMainThread(() => {
						listener.OnTapped(args);
					});
				else if (args.NumberOfTaps == 2 && element.GestureHandler.HandlesDoubleTapped)
					view.InvokeOnMainThread(() => {
						listener.OnDoubleTapped(args);
					});

			}, cancelTappedRaiser.Token);
		}

		#endregion
		#region LongPress

		bool longPressing = false;
		private Stopwatch startPressing;

		private void OnLongPressed(UILongPressGestureRecognizer gr)
		{
			//Log("onLongPressed, state=" + gr.State);

			if (!isVisible) return;                         // Views are reused for Cells, but this Cell is currently not visible

			if (lastPanArgs != null)							// can probably also be configured when creating the gr
				gr.State = UIGestureRecognizerState.Failed;     // if a pan has been started, then the UILongPressGestureRecognizer failed

			if (gr.State == UIGestureRecognizerState.Ended || gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed)
			{
				EndLongPressed(gr);
			}
			else
			{
				BeginLongPressing(gr);
			}
		}

		private void BeginLongPressing(UIGestureRecognizer gr)
		{
			if (!longPressing)
			{
				longPressing = true;
				if (startPressing == null)
					startPressing = new Stopwatch();
				else
					startPressing.Reset();
				startPressing.Start();

				if (element.GestureHandler.HandlesLongPressing)
				{
					var args = new iOSLongPressEventArgs(gr);
					listener.OnLongPressing(args);
					gr.CancelsTouchesInView = args.Handled;
				}
			}
		}

		private void EndLongPressed(UIGestureRecognizer gr)
		{
			if (longPressing)
			{
				longPressing = false;
				startPressing.Stop();
				if (element.GestureHandler.HandlesLongPressed)
				{
					var args = new iOSLongPressEventArgs(gr, startPressing.ElapsedMilliseconds);
					listener.OnLongPressed(args);
					gr.CancelsTouchesInView = args.Handled;
				}
			}
		}

		#endregion
		#region Pan & Swipe

		private PanEventArgs lastPanArgs;

		private void OnPanned(UIPanGestureRecognizer gr)
		{
			if (!isVisible) return;                         // Views are reused for Cells, but this Cell is currently not visible

			var args = lastPanArgs != null ? new iOSPanEventArgs(gr, lastPanArgs) : new iOSPanEventArgs(gr, lastDownArgs);

			if (args.Touches.Length < 1)
				return;										// MinimumNumberOfTouches == 1, but this is still sometimes called without touches

			if (gr.State == UIGestureRecognizerState.Ended || gr.State == UIGestureRecognizerState.Cancelled || gr.State == UIGestureRecognizerState.Failed)
			{
				if(lastPanArgs == null) return;				// this gesture was too short to be raised - the finger probably only bounced

				lastPanArgs = null;

				// panned or swiped?

				if(element.GestureHandler.HandlesSwiped)
				{	// check if it was fast enough for a swipe
					var v = args.Velocity;

					var swipedX = Math.Abs(v.X) > Settings.SwipeVelocityThreshold.X;
					var swipedY = Math.Abs(v.Y) > Settings.SwipeVelocityThreshold.Y;
					if (swipedX || swipedY)
					{
						var direction = Direction.NotClear;
						if (!swipedY)
							direction = v.X > 0 ? Direction.Right : Direction.Left;
						else if (!swipedX)
							direction = v.Y > 0 ? Direction.Down : Direction.Up;

						SwipeEventArgs swipeArgs = new iOSSwipeEventArgs(gr, direction, args);
						listener.OnSwiped(swipeArgs);
						gr.CancelsTouchesInView = swipeArgs.Handled;
						return;
					}
				}

				if (element.GestureHandler.HandlesPanning || element.GestureHandler.HandlesPanned)
				{
					listener.OnPanned(args);
					gr.CancelsTouchesInView = args.Handled;
				}
			}
			else
			{
				if (!args.Equals(lastPanArgs) && element.GestureHandler.HandlesPanning)
				{
					listener.OnPanning(args);
					gr.CancelsTouchesInView = args.Handled;
				}
				lastPanArgs = args;
			}
		}

        #endregion
        #region Pinch & Rotate

        private PinchEventArgs previousPinchArgs;
		private RotateEventArgs previousRotateArgs;

		public void OnMultiTouchMoving(UIGestureRecognizer gr)
		{
			if (!isVisible) return;                         // Views are reused for Cells, but this Cell is currently not visible

			if (gr.NumberOfTouches < 1)
				return;                                     // MinimumNumberOfTouches == 1, but this is still sometimes called without touches

			var pinchArgs = new iOSPinchEventArgs(gr, previousPinchArgs);
			var rotateArgs = new iOSRotateEventArgs(gr, previousRotateArgs);

			if (element.GestureHandler.HandlesPinching)
				listener.OnPinching(pinchArgs);
			if (element.GestureHandler.HandlesRotating)
				listener.OnRotating(rotateArgs);
			gr.CancelsTouchesInView = (element.GestureHandler.HandlesPinching && pinchArgs.Handled) || (element.GestureHandler.HandlesRotating && rotateArgs.Handled);

			previousPinchArgs = pinchArgs;
			previousRotateArgs = rotateArgs;
		}

		public void OnMultiTouchEnded(UIGestureRecognizer gr)
		{
			if (!isVisible) return;                         // Views are reused for Cells, but this Cell is currently not visible

			if (gr.NumberOfTouches < 1)
				return;                                     // MinimumNumberOfTouches == 1, but this is still sometimes called without touches

			if (previousPinchArgs == null && previousRotateArgs == null) return;        // gesture has already been finished

			var pinchArgs = new iOSPinchEventArgs(gr, previousPinchArgs);
			var rotateArgs = new iOSRotateEventArgs(gr, previousRotateArgs);

			if (element.GestureHandler.HandlesPinching || element.GestureHandler.HandlesPinched)
				listener.OnPinched(pinchArgs);
			if (element.GestureHandler.HandlesRotating || element.GestureHandler.HandlesRotated)
				listener.OnRotated(rotateArgs);
			gr.CancelsTouchesInView = (element.GestureHandler.HandlesPinched && pinchArgs.Handled) || (element.GestureHandler.HandlesRotated && rotateArgs.Handled);

			previousPinchArgs = null;
			previousRotateArgs = null;
		}

		#endregion

		#region Mouse*

		private void OnHover()
		{
			if (!isVisible) return;                         // Views are reused for Cells, but this Cell is currently not visible

			switch (hoverGr?.State)
			{
				case UIGestureRecognizerState.Began:
					OnMouseEntered(hoverGr);
					break;
				case UIGestureRecognizerState.Changed:
					OnMouseMoved(hoverGr);
					break;
				case UIGestureRecognizerState.Ended:
				case UIGestureRecognizerState.Cancelled:
					OnMouseExited(hoverGr);
					break;
				default:
					break;
			}
		}

		private iOSMouseEventArgs previousMouseArgs = null;
		
		private void OnMouseEntered(UIHoverGestureRecognizer gr)
        {
			var mouseArgs = new iOSMouseEventArgs(gr, null);

			if (element.GestureHandler.HandlesMouseEntered)
				listener.OnMouseEntered(mouseArgs);
			//gr.CancelsTouchesInView = element.GestureHandler.HandlesMouseEntered && mouseArgs.Handled;

			previousMouseArgs = mouseArgs;
		}

		private void OnMouseMoved(UIHoverGestureRecognizer gr)
		{
			var mouseArgs = new iOSMouseEventArgs(gr, previousMouseArgs);

			if (element.GestureHandler.HandlesMouseMoved)
				listener.OnMouseMoved(mouseArgs);
			//gr.CancelsTouchesInView = element.GestureHandler.HandlesMouseEntered && mouseArgs.Handled;

			previousMouseArgs = mouseArgs;
		}

		private void OnMouseExited(UIHoverGestureRecognizer gr)
		{
			var mouseArgs = new iOSMouseEventArgs(gr, previousMouseArgs);

			if (element.GestureHandler.HandlesMouseExited)
				listener.OnMouseExited(mouseArgs);
			//gr.CancelsTouchesInView = element.GestureHandler.HandlesMouseEntered && mouseArgs.Handled;

			previousMouseArgs = null;
		}

		private void OnScrollWheelChanged(UIPanGestureRecognizer gr)
		{
			if (!isVisible) return;                         // Views are reused for Cells, but this Cell is currently not visible

			if (!element.GestureHandler.HandlesScrollWheelChanged
				|| gr.NumberOfTouches > 0
				|| (gr.State != UIGestureRecognizerState.Began && gr.State != UIGestureRecognizerState.Changed))
				return;

			var args = new iOSScrollWheelEventArgs(gr);
			listener.OnScrollWheelChanged(args);
		}

		#endregion

		#region Logging

#if LOGINSTANCES
		private void LogInstances(string method)
		{
			Log(String.Format("iOSGestureHandler.{0}, Element is a {1}, Total instances: {2}", method, element.GetType().Name, Instances));
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
