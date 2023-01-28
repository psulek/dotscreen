using ScaleHQ.DotScreen;

var scaleFactor = ScreenInformation.PrimaryScreen.ScaleFactor;
var dpiX_2 = scaleFactor * 96.0;
Console.WriteLine($"(Lib) dpiX: {dpiX_2}, ScaleF: {scaleFactor}");

var screens = ScreenInformation.AllScreens.ToArray();
Console.WriteLine($"This system has {screens.Length} screen(s).\n");

foreach (var screen in screens)
{
    Console.WriteLine($"{screen.DeviceName}\n\tbounds: {screen.Bounds}\n\tworking area: {screen.WorkingArea}\n\tprimary: {screen.Primary}");
}

Console.WriteLine("Program ends.");