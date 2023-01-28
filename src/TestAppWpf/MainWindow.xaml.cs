using System.Linq;
using System.Text;
using System.Windows;
using ScaleHQ.DotScreen;
// ReSharper disable UnusedMember.Global

namespace TestAppWpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var screens = ScreenInformation.AllScreens.ToArray();
            StringBuilder sb = new($"This system has {screens.Length} screen(s).\n");

            foreach (var screen in screens)
            {
                sb.Append($"{screen.DeviceName}\n\tbounds: {screen.Bounds}\n\tworking area: {screen.WorkingArea}\n\tprimary: {screen.Primary}");
            }

            Label.Content = sb.ToString();
        }
    }
}
