# DotScreen
**DotScreen** is library to get information about system screens in applications without dependency on WinForms or WPF.

> Primary console/library projects will benefit using this library as they dont want direct dependency on Winforms nor WPF

### Install
Install nuget package - [ScaleHQ.DotScreen](https://www.nuget.org/packages/ScaleHQ.DotScreen/)

or via *dotnet CLI*:
```bash
dotnet add package ScaleHQ.DotScreen
```

### API
Class `ScaleHQ.DotScreen.Screen.Screen` has almost all properties matching `System.Windows.Forms.Screen`.

There is only difference in namespace, instead of `System.Windows.Forms` there is `ScaleHQ.DotScreen.Screen`.

```c#
foreach (var screen in ScaleHQ.DotScreen.Screen.AllScreens)
{
    string deviceName = screen.DeviceName;
    Rectangle bounds = screen.Bounds;
    Rectangle workingArea = screen.WorkingArea;
    // rest...
}

```

In **DotScreen** there are some additions to `Screen` class:
- ScaleFactor (double)
- WorkingAreaScaled (RectangleD)
- BoundsScaled (RectangleD)
- SystemVirtualScreen (Rectangle)
- SystemVirtualScreenScaled (RectangleD)
- MultiMonitorSupport (bool)
- IsProcessDPIAware (bool)
- ...

> There is not need to add \<UseWPF> or \<UseWindowsForms> into project (csproj) file when using DotScreen

### Remark
This library is port of [WpfScreenHelper](https://www.nuget.org/packages/WpfScreenHelper). 
It avoids dependencies on Windows Forms and WPF libraries.


