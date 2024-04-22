rmdir /S/Q		D:\MR.Gestures\MR.Gestures\bin			D:\MR.Gestures\MR.Gestures\obj

dotnet build D:\MR.Gestures\MR.Gestures.sln -c Release
dotnet pack D:\MR.Gestures\MR.Gestures.sln -c Release

pause
