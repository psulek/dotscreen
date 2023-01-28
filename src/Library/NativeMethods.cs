using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace ScaleHQ.DotScreen
{
    internal static class NativeMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

        internal enum PROCESS_DPI_AWARENESS {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }

        internal enum DpiType
        {
            EFFECTIVE = 0,
            ANGULAR = 1,
            RAW = 2
        }

        internal enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
            SM_XVIRTUALSCREEN = 76,
            SM_YVIRTUALSCREEN = 77,
            SM_CXVIRTUALSCREEN = 78,
            SM_CYVIRTUALSCREEN = 79,
            SM_CMONITORS = 80
        }

        internal enum SPI : uint
        {
            /// <summary>
            /// Retrieves the size of the work area on the primary display monitor. The work area is the portion of the screen not obscured
            /// by the system taskbar or by application desktop toolbars. The pvParam parameter must point to a RECT structure that receives
            /// the coordinates of the work area, expressed in virtual screen coordinates.
            /// To get the work area of a monitor other than the primary display monitor, call the GetMonitorInfo function.
            /// </summary>
            SPI_GETWORKAREA = 0x0030
        }

        [Flags]
        internal enum SPIF
        {
            None = 0x00,

            /// <summary>Writes the new system-wide parameter setting to the user profile.</summary>
            SPIF_UPDATEINIFILE = 0x01,

            /// <summary>Broadcasts the WM_SETTINGCHANGE message after updating the user profile.</summary>
            SPIF_SENDCHANGE = 0x02,

            /// <summary>Same as SPIF_SENDCHANGE.</summary>
            SPIF_SENDWININICHANGE = 0x02
        }

        internal enum MonitorDefault
        {
            /// <summary>If the point is not contained within any display monitor, return a handle to the display monitor that is nearest to the point.</summary>
            MONITOR_DEFAULTTONEAREST = 0x00000002,

            /// <summary>If the point is not contained within any display monitor, return NULL.</summary>
            MONITOR_DEFAULTTONULL = 0x00000000,

            /// <summary>If the point is not contained within any display monitor, return a handle to the primary display monitor.</summary>
            MONITOR_DEFAULTTOPRIMARY = 0x00000001
        }

        internal enum D2D1_FACTORY_TYPE
        {
            D2D1_FACTORY_TYPE_SINGLE_THREADED = 0,
            D2D1_FACTORY_TYPE_MULTI_THREADED = 1,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            internal int left;
            internal int top;
            internal int right;
            internal int bottom;

            internal RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            internal RECT(Rectangle r)
            {
                left = r.Left;
                top = r.Top;
                right = r.Right;
                bottom = r.Bottom;
            }

            internal static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            internal Size Size => new Size(right - left, bottom - top);
        }

        // use this in cases where the Native API takes a POINT not a POINT*
        // classes marshal by ref.
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINTSTRUCT
        {
            internal int x;
            internal int y;

            internal POINTSTRUCT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class POINT
        {
            internal int x;
            internal int y;

            internal POINT()
            {
            }

            internal POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

#if DEBUG

            public override string ToString()
            {
                return "{x=" + x + ", y=" + y + "}";
            }
#endif
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        internal class MONITORINFOEX
        {
            internal int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));

            internal RECT rcMonitor = new RECT();
            internal RECT rcWork = new RECT();
            internal int dwFlags = 0;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            internal char[] szDevice = new char[32];
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class COMRECT
        {
            internal int bottom;
            internal int left;
            internal int right;
            internal int top;

            internal COMRECT()
            {
            }

            internal COMRECT(Rectangle r)
            {
                left = r.X;
                top = r.Y;
                right = r.Right;
                bottom = r.Bottom;
            }

            internal COMRECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            internal static COMRECT FromXYWH(int x, int y, int width, int height)
            {
                return new COMRECT(x, y, x + width, y + height);
            }

            public override string ToString()
            {
                return "Left = " + left + " Top " + top + " Right = " + right + " Bottom = " + bottom;
            }
        }

        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("06152247-6f50-465a-9245-118bfd3b6007")]
        internal interface ID2D1Factory
        {
            int ReloadSystemMetrics();

            [PreserveSig]
            void GetDesktopDpi(out float dpiX, out float dpiY);

            // the rest is not implemented as we don't need it
        }

        internal enum DeviceCap
		{
			/// <summary>Device driver version</summary>
			DRIVERVERSION = 0,

			/// <summary>Device classification</summary>
			TECHNOLOGY = 2,

			/// <summary>Horizontal size in millimeters</summary>
			HORZSIZE = 4,

			/// <summary>Vertical size in millimeters</summary>
			VERTSIZE = 6,

			/// <summary>Horizontal width in pixels</summary>
			HORZRES = 8,

			/// <summary>Vertical height in pixels</summary>
			VERTRES = 10,

			/// <summary>Number of bits per pixel</summary>
			BITSPIXEL = 12,

			/// <summary>Number of planes</summary>
			PLANES = 14,

			/// <summary>Number of brushes the device has</summary>
			NUMBRUSHES = 16,

			/// <summary>Number of pens the device has</summary>
			NUMPENS = 18,

			/// <summary>Number of markers the device has</summary>
			NUMMARKERS = 20,

			/// <summary>Number of fonts the device has</summary>
			NUMFONTS = 22,

			/// <summary>Number of colors the device supports</summary>
			NUMCOLORS = 24,

			/// <summary>Size required for device descriptor</summary>
			PDEVICESIZE = 26,

			/// <summary>Curve capabilities</summary>
			CURVECAPS = 28,

			/// <summary>Line capabilities</summary>
			LINECAPS = 30,

			/// <summary>Polygonal capabilities</summary>
			POLYGONALCAPS = 32,

			/// <summary>Text capabilities</summary>
			TEXTCAPS = 34,

			/// <summary>Clipping capabilities</summary>
			CLIPCAPS = 36,

			/// <summary>Bitblt capabilities</summary>
			RASTERCAPS = 38,

			/// <summary>Length of the X leg</summary>
			ASPECTX = 40,

			/// <summary>Length of the Y leg</summary>
			ASPECTY = 42,

			/// <summary>Length of the hypotenuse</summary>
			ASPECTXY = 44,

			/// <summary>Shading and Blending caps</summary>
			SHADEBLENDCAPS = 45,

			/// <summary>Logical pixels inch in X</summary>
			LOGPIXELSX = 88,

			/// <summary>Logical pixels inch in Y</summary>
			LOGPIXELSY = 90,

			/// <summary>Number of entries in physical palette</summary>
			SIZEPALETTE = 104,

			/// <summary>Number of reserved entries in palette</summary>
			NUMRESERVED = 106,

			/// <summary>Actual color resolution</summary>
			COLORRES = 108,

			// Printing related DeviceCaps. These replace the appropriate Escapes
			/// <summary>Physical Width in device units</summary>
			PHYSICALWIDTH = 110,

			/// <summary>Physical Height in device units</summary>
			PHYSICALHEIGHT = 111,

			/// <summary>Physical Printable Area x margin</summary>
			PHYSICALOFFSETX = 112,

			/// <summary>Physical Printable Area y margin</summary>
			PHYSICALOFFSETY = 113,

			/// <summary>Scaling factor x</summary>
			SCALINGFACTORX = 114,

			/// <summary>Scaling factor y</summary>
			SCALINGFACTORY = 115,

			/// <summary>Current vertical refresh rate of the display device (for displays only) in Hz</summary>
			VREFRESH = 116,

			/// <summary>Vertical height of entire desktop in pixels</summary>
			DESKTOPVERTRES = 117,

			/// <summary>Horizontal width of entire desktop in pixels</summary>
			DESKTOPHORZRES = 118,

			/// <summary>Preferred blt alignment</summary>
			BLTALIGNMENT = 119
		}


        internal static readonly HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

        [DllImport(ExternDll.Shcore, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern IntPtr GetDpiForMonitor([In] IntPtr hmonitor, [In] DpiType dpiType, [Out] out uint dpiX, [Out] out uint dpiY);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern bool GetMonitorInfo(HandleRef hmonitor, [In][Out] MONITORINFOEX info);

        [DllImport(ExternDll.User32, ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern bool EnumDisplayMonitors(HandleRef hdc, COMRECT rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

        [DllImport(ExternDll.User32, ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern int GetSystemMetrics(SystemMetric nIndex);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern bool SystemParametersInfo(SPI nAction, int nParam, ref RECT rc, SPIF nUpdate);

        [DllImport(ExternDll.User32, ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern IntPtr MonitorFromPoint(POINTSTRUCT pt, MonitorDefault flags);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern bool GetCursorPos([In][Out] POINT pt);

        [DllImport(ExternDll.User32, SetLastError = true)]
        internal static extern bool IsProcessDPIAware();

        [DllImport(ExternDll.Gdi32, SetLastError=true, EntryPoint="CreateDC", CharSet=CharSet.Auto)]
        [ResourceExposure(ResourceScope.Process)]
        internal static extern IntPtr CreateDC(string lpszDriver, string lpszDeviceName, string lpszOutput, HandleRef devMode);

        [DllImport(ExternDll.Gdi32, SetLastError=true, ExactSpelling=true, EntryPoint="DeleteDC", CharSet=CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        internal static extern bool DeleteDC(HandleRef hDC);

        [DllImport(ExternDll.Gdi32, SetLastError=true, ExactSpelling=true, CharSet=CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetDeviceCaps(HandleRef hDC, DeviceCap nIndex);

        [DllImport(ExternDll.D2D1)]
        internal static extern int D2D1CreateFactory(D2D1_FACTORY_TYPE factoryType, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, 
            IntPtr pFactoryOptions, out ID2D1Factory ppIFactory);
    }
}