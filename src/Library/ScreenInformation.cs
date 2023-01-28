using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ScaleHQ.DotScreen
{
    /// <summary>
    /// Provides information about the current system environment.
    /// </summary>
    public static class ScreenInformation
    {
        /// <summary>
        /// Determine whenever system has running more than one visible monitor.
        /// </summary>
        public static bool MultiMonitorSupport => Screen.MultiMonitorSupport;

        /// <summary>
        /// Gets flag whenever current process is DPI aware or not.
        /// </summary>
        public static bool IsProcessDPIAware => Screen.IsProcessDPIAware;

        /// <summary>
        /// Gets an array of all displays on the system.
        /// </summary>
        /// <returns>An enumerable of type Screen, containing all displays on the system.</returns>
        public static IEnumerable<Screen> AllScreens => Screen.AllScreens;

        /// <summary>
        /// Gets primary screen.
        /// </summary>
        public static Screen PrimaryScreen => Screen.PrimaryScreen;

        /// <summary>
        /// Gets the bounds of the system virtual screen in pixels (without applying scale factor).
        /// </summary>
        /// <value>
        ///     A <see cref="T:System.Windows.RectangleD" /> that specifies the bounding rectangle of the entire virtual screen in pixels.
        /// </value>
        public static Rectangle SystemVirtualScreen
        {
            get
            {
                var size = new Size(
                    NativeMethods.GetSystemMetrics(NativeMethods.SystemMetric.SM_CXVIRTUALSCREEN),
                    NativeMethods.GetSystemMetrics(NativeMethods.SystemMetric.SM_CYVIRTUALSCREEN));
                var location = new Point(
                    NativeMethods.GetSystemMetrics(NativeMethods.SystemMetric.SM_XVIRTUALSCREEN),
                    NativeMethods.GetSystemMetrics(NativeMethods.SystemMetric.SM_YVIRTUALSCREEN));
                return new Rectangle(location.X, location.Y, size.Width, size.Height);
            }
        }

        /// <summary>
        /// Gets the bounds of the system virtual screen in pixels scaled by all screens <see cref="Screen.ScaleFactor"/>.
        /// </summary>
        /// <value>
        ///     A <see cref="T:System.Windows.RectangleD" /> that specifies the bounding rectangle of the entire virtual screen in pixels
        ///     scaled by all screens <see cref="Screen.ScaleFactor"/>.
        /// </value>
        /// <remarks>
        /// If <see cref="IsProcessDPIAware"/> is <c>false</c> or then result is same as <see cref="SystemVirtualScreen"/>.
        /// </remarks>
        public static RectangleD SystemVirtualScreenScaled
        {
            get
            {
                if (!IsProcessDPIAware)
                {
                    return new RectangleD(SystemVirtualScreen);
                }

                var values = Screen.AllScreens.Aggregate(
                    new
                    {
                        xMin = 0.0,
                        yMin = 0.0,
                        xMax = 0.0,
                        yMax = 0.0
                    },
                    (accumulator, s) => new
                    {
                        xMin = Math.Min(s.BoundsScaled.X, accumulator.xMin),
                        yMin = Math.Min(s.BoundsScaled.Y, accumulator.yMin),
                        xMax = Math.Max(s.BoundsScaled.Right, accumulator.xMax),
                        yMax = Math.Max(s.BoundsScaled.Bottom, accumulator.yMax)
                    });

                return new RectangleD(values.xMin, values.yMin, values.xMax - values.xMin, values.yMax - values.yMin);
            }
        }
    }
}