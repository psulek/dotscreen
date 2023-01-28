using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestAppWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            foreach (Screen screen in Screen.AllScreens)
            {
                var screenBitsPerPixel = screen.BitsPerPixel;
                string deviceName = screen.DeviceName;
                Rectangle bounds = screen.Bounds;
                Rectangle workingArea = screen.WorkingArea;
            }

            RefreshData();
        }

        private void RefreshData()
        {
            var screens = ScaleHQ.DotScreen.Screen.AllScreens.ToArray();
            StringBuilder sb = new StringBuilder($"This system has {screens.Length} screen(s).\n");

            sb.AppendLine($"MultiMonitorSupport: {ScaleHQ.DotScreen.Screen.MultiMonitorSupport}");
            sb.AppendLine($"IsProcessDPIAware: {ScaleHQ.DotScreen.Screen.IsProcessDPIAware}");
            sb.AppendLine($"SystemVirtualScreen: {ScaleHQ.DotScreen.Screen.SystemVirtualScreen}");
            sb.AppendLine($"SystemVirtualScreenScaled: {ScaleHQ.DotScreen.Screen.SystemVirtualScreenScaled}");
            sb.AppendLine();

            foreach (var screen in screens)
            {
                sb.AppendLine($"{screen.DeviceName}");
                sb.AppendLine($"\tPrimary: {screen.Primary}");
                sb.AppendLine($"\tBounds: {screen.Bounds}");
                sb.AppendLine($"\tWorkingArea: {screen.WorkingArea}");
                sb.AppendLine($"\tScaleFactor: {screen.ScaleFactor}");
                sb.AppendLine($"\tBoundsScaled: {screen.BoundsScaled}");
                sb.AppendLine($"\tWorkingAreaScaled: {screen.WorkingAreaScaled}");
            }

            richTextBox1.Text = sb.ToString();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            RefreshData();
        }
    }
}