using Microsoft.Maui.Controls.Compatibility.Hosting;

using MR.Gestures.Handlers;

namespace MR.Gestures
{
    public static partial class MauiAppBuilderExtensions
    {
        /// <summary>
        /// Initialize MR.Gestures.
        /// Without licenseKey the EventArgs in all MR.Gestures events will not be set!
        /// </summary>
        /// <param name="builder">The <see cref="MauiAppBuilder"/>.</param>
        /// <returns>The <see cref="MauiAppBuilder"/> (for builder pattern).</returns>
		public static MauiAppBuilder ConfigureMRGestures(this MauiAppBuilder builder)
        {
            // Only in Microsoft.Maui.Controls.Handlers.Compatibility.*Renderer:
            // - Frame
            // - ListView
            // - TableView
            // - EntryCell
            // - ImageCell
            // - SwitchCell
            // - TextCell
            // - ViewCell

            // Missing in MAUI:
            // - RelativeLayout

            builder.ConfigureMauiHandlers(handlers =>
            {
                ConfigureGesturesHandlers(handlers);

#if WINDOWS || ANDROID || IOS || MACCATALYST
                handlers.AddHandler<MR.Gestures.Frame, MR.Gestures.Handlers.FrameRenderer>();
                handlers.AddHandler<MR.Gestures.ListView, MR.Gestures.Handlers.ListViewRenderer>();
                handlers.AddHandler<MR.Gestures.TableView, MR.Gestures.Handlers.TableViewRenderer>();
#endif

#if ANDROID || IOS || MACCATALYST
                handlers.AddHandler<MR.Gestures.TextCell, MR.Gestures.Handlers.TextCellRenderer>();
                handlers.AddHandler<MR.Gestures.ImageCell, MR.Gestures.Handlers.ImageCellRenderer>();
                handlers.AddHandler<MR.Gestures.EntryCell, MR.Gestures.Handlers.EntryCellRenderer>();
                handlers.AddHandler<MR.Gestures.SwitchCell, MR.Gestures.Handlers.SwitchCellRenderer>();
                handlers.AddHandler<MR.Gestures.ViewCell, MR.Gestures.Handlers.ViewCellRenderer>();
#endif
            });

            return builder;
        }

        /// <summary>
        /// Initialize MR.Gestures.
        /// </summary>
        /// <param name="builder">The <see cref="MauiAppBuilder"/>.</param>
        /// <param name="licenseKey">The license key for MR.Gestures.</param>
        /// <returns>The <see cref="MauiAppBuilder"/> (for builder pattern).</returns>
        [Obsolete("MR.Gestures is now open source and free of charge. Please use ConfigureMRGestures() instead.")]
        public static MauiAppBuilder ConfigureMRGestures(this MauiAppBuilder builder, string licenseKey)
		{
			return builder.ConfigureMRGestures();
		}
	}
}
