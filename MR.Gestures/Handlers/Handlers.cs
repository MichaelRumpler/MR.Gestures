namespace MR.Gestures.Handlers
{
	public partial class ActivityIndicatorHandler : Microsoft.Maui.Handlers.ActivityIndicatorHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.ActivityIndicatorHandler.Mapper);

		public ActivityIndicatorHandler() : base(Mapper) { }

		public ActivityIndicatorHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class BorderHandler : Microsoft.Maui.Handlers.BorderHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.BorderHandler.Mapper);

		public BorderHandler() : base(Mapper) { }

		public BorderHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.ButtonHandler.Mapper);

		public ButtonHandler() : base(Mapper) { }

		public ButtonHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class CheckBoxHandler : Microsoft.Maui.Handlers.CheckBoxHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.CheckBoxHandler.Mapper);

		public CheckBoxHandler() : base(Mapper) { }

		public CheckBoxHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class ContentViewHandler : Microsoft.Maui.Handlers.ContentViewHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.ContentViewHandler.Mapper);

		public ContentViewHandler() : base(Mapper) { }

		public ContentViewHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class DatePickerHandler : Microsoft.Maui.Handlers.DatePickerHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.DatePickerHandler.Mapper);

		public DatePickerHandler() : base(Mapper) { }

		public DatePickerHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class EditorHandler : Microsoft.Maui.Handlers.EditorHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.EditorHandler.Mapper);

		public EditorHandler() : base(Mapper) { }

		public EditorHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class EntryHandler : Microsoft.Maui.Handlers.EntryHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.EntryHandler.Mapper);

		public EntryHandler() : base(Mapper) { }

		public EntryHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class GraphicsViewHandler : Microsoft.Maui.Handlers.GraphicsViewHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.GraphicsViewHandler.Mapper);

		public GraphicsViewHandler() : base(Mapper) { }

		public GraphicsViewHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class ImageHandler : Microsoft.Maui.Handlers.ImageHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.ImageHandler.Mapper);

		public ImageHandler() : base(Mapper) { }

		public ImageHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class ImageButtonHandler : Microsoft.Maui.Handlers.ImageButtonHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.ImageButtonHandler.Mapper);

		public ImageButtonHandler() : base(Mapper) { }

		public ImageButtonHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class IndicatorViewHandler : Microsoft.Maui.Handlers.IndicatorViewHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.IndicatorViewHandler.Mapper);

		public IndicatorViewHandler() : base(Mapper) { }

		public IndicatorViewHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class LabelHandler : Microsoft.Maui.Handlers.LabelHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.LabelHandler.Mapper);

		public LabelHandler() : base(Mapper) { }

		public LabelHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class LayoutHandler : Microsoft.Maui.Handlers.LayoutHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.LayoutHandler.Mapper);

		public LayoutHandler() : base(Mapper) { }

		public LayoutHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class PageHandler : Microsoft.Maui.Handlers.PageHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.PageHandler.Mapper);

		public PageHandler() : base(Mapper) { }

		public PageHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class PickerHandler : Microsoft.Maui.Handlers.PickerHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.PickerHandler.Mapper);

		public PickerHandler() : base(Mapper) { }

		public PickerHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class ProgressBarHandler : Microsoft.Maui.Handlers.ProgressBarHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.ProgressBarHandler.Mapper);

		public ProgressBarHandler() : base(Mapper) { }

		public ProgressBarHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class RadioButtonHandler : Microsoft.Maui.Handlers.RadioButtonHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.RadioButtonHandler.Mapper);

		public RadioButtonHandler() : base(Mapper) { }

		public RadioButtonHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class ScrollViewHandler : Microsoft.Maui.Handlers.ScrollViewHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.ScrollViewHandler.Mapper);

		public ScrollViewHandler() : base(Mapper) { }

		public ScrollViewHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class SearchBarHandler : Microsoft.Maui.Handlers.SearchBarHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.SearchBarHandler.Mapper);

		public SearchBarHandler() : base(Mapper) { }

		public SearchBarHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class ShapeViewHandler : Microsoft.Maui.Handlers.ShapeViewHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.ShapeViewHandler.Mapper);

		public ShapeViewHandler() : base(Mapper) { }

		public ShapeViewHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class SliderHandler : Microsoft.Maui.Handlers.SliderHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.SliderHandler.Mapper);

		public SliderHandler() : base(Mapper) { }

		public SliderHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class StepperHandler : Microsoft.Maui.Handlers.StepperHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.StepperHandler.Mapper);

		public StepperHandler() : base(Mapper) { }

		public StepperHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class SwitchHandler : Microsoft.Maui.Handlers.SwitchHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.SwitchHandler.Mapper);

		public SwitchHandler() : base(Mapper) { }

		public SwitchHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class TimePickerHandler : Microsoft.Maui.Handlers.TimePickerHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.TimePickerHandler.Mapper);

		public TimePickerHandler() : base(Mapper) { }

		public TimePickerHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

	public partial class WebViewHandler : Microsoft.Maui.Handlers.WebViewHandler
	{
		public static new PropertyMapper<IGestureAwareControl, IElementHandler> Mapper
			= new(HandlerHelper.GesturesMapper, Microsoft.Maui.Handlers.WebViewHandler.Mapper);

		public WebViewHandler() : base(Mapper) { }

		public WebViewHandler(IPropertyMapper mapper = null) : base(mapper ?? Mapper) { }
	}

}

namespace MR.Gestures
{
    public static partial class MauiAppBuilderExtensions
	{
		private static void ConfigureGestureHandlers(IMauiHandlersCollection handlers)
		{
            handlers.AddHandler<MR.Gestures.ActivityIndicator, MR.Gestures.Handlers.ActivityIndicatorHandler>();
            handlers.AddHandler<MR.Gestures.Border, MR.Gestures.Handlers.BorderHandler>();
            handlers.AddHandler<MR.Gestures.Button, MR.Gestures.Handlers.ButtonHandler>();
            handlers.AddHandler<MR.Gestures.CheckBox, MR.Gestures.Handlers.CheckBoxHandler>();
            handlers.AddHandler<MR.Gestures.ContentView, MR.Gestures.Handlers.ContentViewHandler>();
            handlers.AddHandler<MR.Gestures.DatePicker, MR.Gestures.Handlers.DatePickerHandler>();
            handlers.AddHandler<MR.Gestures.Editor, MR.Gestures.Handlers.EditorHandler>();
            handlers.AddHandler<MR.Gestures.Entry, MR.Gestures.Handlers.EntryHandler>();
            handlers.AddHandler<MR.Gestures.GraphicsView, MR.Gestures.Handlers.GraphicsViewHandler>();
            handlers.AddHandler<MR.Gestures.Image, MR.Gestures.Handlers.ImageHandler>();
            handlers.AddHandler<MR.Gestures.ImageButton, MR.Gestures.Handlers.ImageButtonHandler>();
            handlers.AddHandler<MR.Gestures.IndicatorView, MR.Gestures.Handlers.IndicatorViewHandler>();
            handlers.AddHandler<MR.Gestures.Label, MR.Gestures.Handlers.LabelHandler>();
            handlers.AddHandler<MR.Gestures.AbsoluteLayout, MR.Gestures.Handlers.LayoutHandler>();
            handlers.AddHandler<MR.Gestures.FlexLayout, MR.Gestures.Handlers.LayoutHandler>();
            handlers.AddHandler<MR.Gestures.Grid, MR.Gestures.Handlers.LayoutHandler>();
            handlers.AddHandler<MR.Gestures.HorizontalStackLayout, MR.Gestures.Handlers.LayoutHandler>();
            handlers.AddHandler<MR.Gestures.StackLayout, MR.Gestures.Handlers.LayoutHandler>();
            handlers.AddHandler<MR.Gestures.VerticalStackLayout, MR.Gestures.Handlers.LayoutHandler>();
            handlers.AddHandler<MR.Gestures.ContentPage, MR.Gestures.Handlers.PageHandler>();
            handlers.AddHandler<MR.Gestures.Picker, MR.Gestures.Handlers.PickerHandler>();
            handlers.AddHandler<MR.Gestures.ProgressBar, MR.Gestures.Handlers.ProgressBarHandler>();
            handlers.AddHandler<MR.Gestures.RadioButton, MR.Gestures.Handlers.RadioButtonHandler>();
            handlers.AddHandler<MR.Gestures.ScrollView, MR.Gestures.Handlers.ScrollViewHandler>();
            handlers.AddHandler<MR.Gestures.SearchBar, MR.Gestures.Handlers.SearchBarHandler>();
            handlers.AddHandler<MR.Gestures.BoxView, MR.Gestures.Handlers.ShapeViewHandler>();
            handlers.AddHandler<MR.Gestures.Slider, MR.Gestures.Handlers.SliderHandler>();
            handlers.AddHandler<MR.Gestures.Stepper, MR.Gestures.Handlers.StepperHandler>();
            handlers.AddHandler<MR.Gestures.Switch, MR.Gestures.Handlers.SwitchHandler>();
            handlers.AddHandler<MR.Gestures.TimePicker, MR.Gestures.Handlers.TimePickerHandler>();
            handlers.AddHandler<MR.Gestures.WebView, MR.Gestures.Handlers.WebViewHandler>();
		}
	}
}
