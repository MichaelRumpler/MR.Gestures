# MR.Gestures

MR.Gestures adds events and Commands for handling touch and mouse gestures to all the .NET MAUI elements.

With version 5 and the end of support for Xamarin.Forms I released it as open source under the MIT license.

To get started you

1) install the NuGet package to your MAUI project,
2) initialize it with a call to `ConfigureMRGestures()` in your `MauiProgram`
3) and then use the elements from `MR.Gestures` instead of `Microsoft.Maui.Controls`.

The official website with a more detailed description is at [www.mrgestures.com](https://www.mrgestures.com/).

The [GestureSample](https://github.com/MichaelRumpler/GestureSample) app demos how all the elements are used and what you get in the `EventArgs`.

If you want to enhance MR.Gestures or debug it, you should start by reading about its [Architecture](Architecture.md).
