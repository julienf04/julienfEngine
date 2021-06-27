using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace julienfEngine1
{
    class DllController
    {
        #region ---DLL TO GET THE WINDOW AND MAXIMIZE IT;

        [DllImport("kernel32.dll", ExactSpelling = true)] private static extern IntPtr GetConsoleWindow();

        private static IntPtr _myConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")] public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        private const int _SW_RRHIDE = 0;
        private const int _SW_MAXIMIZE = 3;
        private const int _SW_MINIMIZE = 6;
        private const int _SW_RESTORE = 9;
        private const int _SWP_NOSIZE = 0x0001;

        #endregion

        #region ---DLL TO REMOVE THE TITLE BAR

        const int WS_BORDER = 8388608;
        const int WS_DLGFRAME = 4194304;
        const int WS_CAPTION = WS_BORDER | WS_DLGFRAME;
        const long GWL_STYLE = -16L;
        const int SWP_FRAMECHANGED = 0x20;
        const uint MF_REMOVE = 0x1000;

        [DllImport("user32.dll")] static extern int GetWindowLong(IntPtr hWnd, long nIndex);
        [DllImport("user32.dll")] static extern int SetWindowLong(IntPtr hWnd, long nIndex, long dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)] static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        #endregion

        #region ---DLL TO DISABLE THE CURSOR

        [DllImport("user32.dll")] static extern bool ClipCursor(ref RECT lpRect);

        [DllImport("user32.dll")] private static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        [DllImport("user32.dll")] private static extern bool GetMonitorInfo(IntPtr hMonitor, NativeMonitorInfo lpmi);

        [DllImport("user32.dll")] private static extern IntPtr GetDesktopWindow();

        private static IntPtr myDesktopWindow = GetDesktopWindow();

        [DllImport("user32.dll")] private static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        private const int _MONITOR_DEFAULTTOPRIMERTY = 0x00000001;
        private const int _MONITOR_DEFAULTTONEAREST = 0x00000002;


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)] private sealed class NativeMonitorInfo
        {
            public int size = Marshal.SizeOf(typeof(NativeMonitorInfo));
            public _NativeRectangle monitor;
            public _NativeRectangle work;
            public int flags;

            public _NativeRectangle Monitor
            {
                get
                {
                    return monitor;
                }
            }
        }


        [Serializable, StructLayout(LayoutKind.Sequential)] private struct _NativeRectangle
        {
            private _NativeRectangle(int left, int top, int right, int bottom)
            {
                this._left = left;
                this._top = top;
                this._right = right;
                this._bottom = bottom;
            }

            private int _left;
            private int _top;
            private int _right;
            private int _bottom;

            public int Left
            {
                get
                {
                    return _left;
                }
            }

            public int Right
            {
                get
                {
                    return _right;
                }
            }

            public int Top
            {
                get
                {
                    return _top;
                }
            }

            public int Bottom
            {
                get
                {
                    return _bottom;
                }
            }
        }


        [StructLayout(LayoutKind.Sequential)] struct RECT
        {
            public RECT(int Top, int Bottom, int Left, int Right)
            {
                this.Top = Top;
                this.Bottom = Bottom;
                this.Left = Left;
                this.Right = Right;
            }

            private int Top;
            private int Bottom;
            private int Left;
            private int Right;
        }



        #endregion


        #region ---METHODS

        public static void MaximizeWindow()
        {
            ShowWindow(_myConsole, _SW_MAXIMIZE);
        }

        public static void RemoveBorders()
        {
            long Style = 0;
            Style = GetWindowLong(_myConsole, GWL_STYLE);

            SetWindowLong(_myConsole, GWL_STYLE, Style & ~WS_CAPTION & ~MF_REMOVE);

            SetWindowPos(_myConsole, new IntPtr(0), 0, 0, 0, 0, SWP_FRAMECHANGED);
        }

        public static void ConfineCursor(bool enabled)
        {
            IntPtr monitor = MonitorFromWindow(myDesktopWindow, _MONITOR_DEFAULTTONEAREST);

            NativeMonitorInfo monitorInfo = new NativeMonitorInfo();

            GetMonitorInfo(monitor, monitorInfo);

            int width = monitorInfo.Monitor.Right - monitorInfo.monitor.Left;
            int height = monitorInfo.Monitor.Bottom - monitorInfo.monitor.Top;

            RECT rectCursorConfined = new RECT(width, height, width, height);

            ClipCursor(ref rectCursorConfined);

            EnableWindow(_myConsole, false);
        }

        #endregion
    }
}