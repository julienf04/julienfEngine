using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace julienfEngine1
{
    static class DllImporter
    {
        #region ---DLL TO GET THE WINDOW AND MAXIMIZE IT;

        [DllImport("kernel32.dll", ExactSpelling = true)] private static extern IntPtr GetConsoleWindow();

        private static IntPtr _myConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")] private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

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

        #region ---DLL TO MANAGE SCREEN BUFFERS

        [DllImport("Kernel32.dll")] static extern IntPtr CreateConsoleScreenBuffer(long dwDesiredAccess, long dwShareMode, IntPtr secutiryAttributes, uint flags, IntPtr screenBufferData);

        [DllImport("Kernel32.dll")] static extern bool SetConsoleActiveScreenBuffer(IntPtr hConsoleOutput);

        [DllImport("kernel32.dll", SetLastError = true)] static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, string lpCharacter, int nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

        [DllImport("kernel32.dll", SetLastError = true)] static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)] static extern bool FillConsoleOutputCharacter(IntPtr hConsoleOutput, char cCharacter, int nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);


        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;

            public COORD(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };


        const long GENERIC_READ = 0x80000000L;
        const long GENERIC_WRITE = 0x40000000L;

        const long FILE_SHARED_READ = 0x00000001;
        const long FILE_SHARED_WRITE = 0x00000002;

        const int CONSOLE_TEXTMODE_BUFFER = 1;

        const int STD_INPUT_HANDLE = -10;
        const int STD_OUTPUT_HANDLE = -11;
        const int STD_ERROR_HANDLE = -12;


        private static List<IntPtr> _screenBuffers = new List<IntPtr>() { GetStdHandle((STD_OUTPUT_HANDLE)) };
        private static int _numberOfScreenBuffers = 1;

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

        public static int CreateScreenBuffer()
        {
            IntPtr screenBufferHanlde = CreateConsoleScreenBuffer(GENERIC_WRITE, FILE_SHARED_WRITE, IntPtr.Zero, CONSOLE_TEXTMODE_BUFFER, IntPtr.Zero);
            _screenBuffers.Add(screenBufferHanlde);
            _numberOfScreenBuffers++;
            return _screenBuffers.Count;
        }

        public static void SetScreenBuffer(int screenBufferID)
        {
            SetConsoleActiveScreenBuffer(_screenBuffers[--screenBufferID]);
        }

        public static void WriteConsole(int screenBufferID, string message, COORD coords)
        {
            uint ignore = 0;
            WriteConsoleOutputCharacter(_screenBuffers[--screenBufferID], message, message.Length, coords, out ignore);
        }

        public static void WriteConsole(int screenBufferID, string message, int x, int y)
        {
            uint ignore = 0;
            WriteConsoleOutputCharacter(_screenBuffers[--screenBufferID], message, message.Length, new COORD((short)x, (short)y), out ignore);
        }

        public static void ClearConsole(int screenBufferID)
        {
            uint ignore = 0;
            FillConsoleOutputCharacter(_screenBuffers[--screenBufferID], ' ', julienfEngine.P_ScreenX * julienfEngine.P_ScreenY, new COORD(0, 0), out ignore);
        }

        #endregion

        #region ---Properties

        public static int P_NumberOfScreenBuffers
        {
            get
            {
                return _numberOfScreenBuffers;
            }
        }

        #endregion
    }
}