using System.Collections.Concurrent;

namespace MR.Gestures
{
    internal static class TaskHelpers
    {
        /*
         * The first version throws many TaskCanceledException and runs many Tasks.
         */

        //private static Dictionary<(object, object), CancellationTokenSource> cancelTokens = new();

        ///// <summary>
        ///// Executes <paramref name="action"/> <paramref name="waittime"/> after this method was last called with these arguments.
        ///// </summary>
        ///// <param name="action"></param>
        ///// <param name="waittime"></param>
        //public static void Debounce<T1, T2>(T1 arg1, T2 arg2, Action<T1, T2> action, TimeSpan waittime)
        //{
        //    var key = ((object)arg1, (object)arg2);
            
        //    if (cancelTokens.TryGetValue(key, out var token))
        //        token.Cancel();

        //    token = new CancellationTokenSource();
        //    cancelTokens[key] = token;

        //    Task.Run(async () =>
        //    {
        //        await Task.Delay(waittime, token.Token).ConfigureAwait(false);

        //        if (cancelTokens.Remove(key))
        //        {
        //            Device.BeginInvokeOnMainThread(() => action.Invoke(arg1, arg2));
        //        }

        //    }, token.Token);
        //}


        private static ConcurrentDictionary<(object, object), Timer> dict = new();

		/// <summary>
		/// Executes <paramref name="action"/> <paramref name="dueTime"/> after this method was last called with these arguments.
		/// </summary>
		/// <param name="arg1">First argument to <paramref name="action"/></param>
		/// <param name="arg2">Second argument to <paramref name="action"/></param>
		/// <param name="action">The action which should be called with <paramref name="arg1"/> and <paramref name="arg2"/>.</param>
		/// <param name="dueTime">Time to wait until the <paramref name="action"/> is called.</param>
		public static void Debounce<T1, T2>(
            T1 arg1, T2 arg2,
            Action<T1, T2> action,
            TimeSpan dueTime)
        {
            //Log($"Enter Debounce for arg1=({arg1.GetType().Name}){arg1.GetHashCode()}, arg2=({arg2.GetType().Name}){arg2.GetHashCode()}");
            var key = (arg1, arg2);

            dict.AddOrUpdate(key,
                key =>
                    new Timer(
                        k =>
                        {
                            var key = ((object, object))k;
                            var arg1 = key.Item1;
                            var arg2 = key.Item2;
                            //Log($"Executing Debounce for arg1=({arg1.GetType().Name}){arg1.GetHashCode()}, arg2=({arg2.GetType().Name}){arg2.GetHashCode()}");
                            dict.TryRemove(key, out var _);
                            BeginInvokeOnMainThread(() => { action((T1)arg1, (T2)arg2); });
                        },
                        key, dueTime, Timeout.InfiniteTimeSpan),
                (k, oldTimer) =>
                {
                    oldTimer.Change(dueTime, Timeout.InfiniteTimeSpan);
                    return oldTimer;
                });
        }

#if ANDROID
        static volatile global::Android.OS.Handler handler;
#endif

        public static void BeginInvokeOnMainThread(Action action)
        {
			// from https://github.com/dotnet/maui/tree/main/src/Essentials/src/MainThread

#if WINDOWS

			var dispatcher = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread() ?? WindowStateManager.Default.GetActiveWindow()?.DispatcherQueue;

			if (dispatcher == null)
				throw new InvalidOperationException("Unable to find main thread.");

			if (!dispatcher.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () => action()))
				throw new InvalidOperationException("Unable to queue on the main thread.");

#elif ANDROID

			if (handler?.Looper != global::Android.OS.Looper.MainLooper)
				handler = new global::Android.OS.Handler(global::Android.OS.Looper.MainLooper);

			handler.Post(action);

#elif IOS || MACCATALYST

            Foundation.NSRunLoop.Main.BeginInvokeOnMainThread(action.Invoke);

#else

#pragma warning disable CS0612 // Device is obsolete - what else should I use?
#pragma warning disable CS0618 // BeginInvokeOnMainThread is obsolete - I don't have a BindingObject here
			Device.BeginInvokeOnMainThread(action.Invoke);
#pragma warning restore CS0618 // BeginInvokeOnMainThread is obsolete
#pragma warning restore CS0612 // Device is obsolete

#endif
		}

		private static void Log(string msg)
        {
#if DEBUG
            var threadType = System.Threading.Thread.CurrentThread.IsBackground ? "BG" : "UI";
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: [T:{threadType}: {msg}");
#endif
        }
    }
}
