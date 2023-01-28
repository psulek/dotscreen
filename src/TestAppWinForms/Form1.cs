using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ScaleHQ.DotScreen.ScreenInformation;

namespace TestAppWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            RefreshData();
        }

        private void RefreshData()
        {
            var screens = AllScreens.ToArray();
            StringBuilder sb = new StringBuilder($"This system has {screens.Length} screen(s).\n");

            sb.AppendLine($"MultiMonitorSupport: {MultiMonitorSupport}");
            sb.AppendLine($"IsProcessDPIAware: {IsProcessDPIAware}");
            sb.AppendLine($"SystemVirtualScreen: {SystemVirtualScreen}");
            sb.AppendLine($"SystemVirtualScreenScaled: {SystemVirtualScreenScaled}");
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