﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace julienfEngine1
{
    internal abstract class DllImporter
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

        private const int _WS_BORDER = 8388608;
        private const int _WS_DLGFRAME = 4194304;
        private const int _WS_CAPTION = _WS_BORDER | _WS_DLGFRAME;
        private const long _GWL_STYLE = -16L;
        private const int _SWP_FRAMECHANGED = 0x20;
        private const uint _MF_REMOVE = 0x1000;

        [DllImport("user32.dll")] private static extern int GetWindowLong(IntPtr hWnd, long nIndex);
        [DllImport("user32.dll")] private static extern int SetWindowLong(IntPtr hWnd, long nIndex, long dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)] private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        #endregion

        #region ---DLL TO DISABLE THE CURSOR

        [DllImport("user32.dll")] private static extern bool ClipCursor(ref RECT lpRect);

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


        [StructLayout(LayoutKind.Sequential)] private struct RECT
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

        [DllImport("Kernel32.dll")] private static extern IntPtr CreateConsoleScreenBuffer(long dwDesiredAccess, long dwShareMode, IntPtr secutiryAttributes, uint flags, IntPtr screenBufferData);

        [DllImport("Kernel32.dll")] private static extern bool SetConsoleActiveScreenBuffer(IntPtr hConsoleOutput);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, string lpCharacter, int nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool FillConsoleOutputCharacter(IntPtr hConsoleOutput, char cCharacter, int nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool FillConsoleOutputAttribute(IntPtr hConsoleOutput, int wAttribute, int nLength, COORD dwWriteCoord, out uint lpNumberOfAttrsWritten);


        [StructLayout(LayoutKind.Sequential)]
        internal protected struct COORD
        {
            public short X;
            public short Y;

            public COORD(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };


        private const long _GENERIC_READ = 0x80000000L;
        private const long _GENERIC_WRITE = 0x40000000L;

        private const long _FILE_SHARED_READ = 0x00000001;
        private const long _FILE_SHARED_WRITE = 0x00000002;

        private const int _CONSOLE_TEXTMODE_BUFFER = 1;

        private const int _STD_INPUT_HANDLE = -10;
        private const int _STD_OUTPUT_HANDLE = -11;
        private const int _STD_ERROR_HANDLE = -12;


        internal const ushort FOREGROUND_BLUE = 0x0001;
        internal const ushort FOREGROUND_GREEN = 0x0002;
        internal const ushort FOREGROUND_RED = 0x0004;
        internal const ushort FOREGROUND_INTENSITY = 0x0008;

        internal const ushort BACKGROUND_BLUE = 0x0010;
        internal const ushort BACKGROUND_GREEN = 0x0020;
        internal const ushort BACKGROUND_RED = 0x0040;
        internal const ushort BACKGROUND_INTENSITY = 0x0080;
        
        private const ushort COMMON_LVB_LEADING_BYTE = 0x0100;
        private const ushort COMMON_LVB_TRAILING_BYTE = 0x0200;
        private const ushort COMMON_LVB_GRID_HORIZONTAL = 0x0400;
        private const ushort COMMON_LVB_GRID_LVERTICAL = 0x0800;
        private const ushort COMMON_LVB_GRID_RVERTICAL = 0x1000;
        private const ushort COMMON_LVB_REVERSE_VIDEO = 0x4000;
        private const ushort COMMON_LVB_UNDERSCORE = 0x8000;


        private static List<IntPtr> _screenBuffers = new List<IntPtr>() { GetStdHandle((_STD_OUTPUT_HANDLE)) };
        private static int _numberOfScreenBuffers = 1;

        #endregion

        #region ---DLL TO HIDE CONSOLE CURSOR

        [DllImport("kernel32.dll", SetLastError = true)] static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput, [In] ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo);


        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_CURSOR_INFO
        {
            public uint Size;
            public bool Visible;
        }

        #endregion

        #region ---METHODS

        internal protected static void MaximizeWindow()
        {
            ShowWindow(_myConsole, _SW_MAXIMIZE);
        }

        internal protected static void RemoveBorders()
        {
            long Style = GetWindowLong(_myConsole, _GWL_STYLE);

            SetWindowLong(_myConsole, _GWL_STYLE, Style & ~_WS_CAPTION & ~_MF_REMOVE);

            SetWindowPos(_myConsole, new IntPtr(0), 0, 0, 0, 0, _SWP_FRAMECHANGED);
        }

        internal protected static void ConfineCursor(bool enabled)
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

        internal protected static int CreateScreenBuffer()
        {
            IntPtr screenBufferHanlde = CreateConsoleScreenBuffer(_GENERIC_WRITE, _FILE_SHARED_WRITE, IntPtr.Zero, _CONSOLE_TEXTMODE_BUFFER, IntPtr.Zero);
            _screenBuffers.Add(screenBufferHanlde);
            _numberOfScreenBuffers++;
            return _screenBuffers.Count;
        }

        internal protected static void SetScreenBuffer(int screenBufferID)
        {
            screenBufferID--;
            SetConsoleActiveScreenBuffer(_screenBuffers[screenBufferID]);

        }

        internal protected static void WriteConsole(int screenBufferID, string message, COORD coords, E_ForegroundColors foregroundColor, E_BackgroundColors backgroundColor)
        {
            uint ignore = 0;
            screenBufferID--;
            IntPtr screenBufferToWrite = _screenBuffers[screenBufferID];
            FillConsoleOutputAttribute(screenBufferToWrite, (int)foregroundColor | (int)backgroundColor, message.Length,coords, out ignore);
            WriteConsoleOutputCharacter(screenBufferToWrite, message, message.Length, coords, out ignore);
        }

        internal protected static void WriteConsole(int screenBufferID, string message, int x, int y, E_ForegroundColors foregroundColor, E_BackgroundColors backgroundColor)
        {
            uint ignore = 0;
            COORD coords = new COORD((short)x, (short)y);
            screenBufferID--;
            IntPtr screenBufferToWrite = _screenBuffers[screenBufferID];
            FillConsoleOutputAttribute(screenBufferToWrite, (int)foregroundColor | (int)backgroundColor, message.Length, coords, out ignore);
            WriteConsoleOutputCharacter(screenBufferToWrite, message, message.Length, coords, out ignore);
        }

        internal protected static void WriteConsoleWithSpecialCharacters(int screenBufferID, string message, COORD coords, E_ForegroundColors foregroundColor, E_BackgroundColors backgroundColor, short[] startIndexes, int[] lengthToPaint)
        {
            uint ignore = 0;
            screenBufferID--;
            IntPtr screenBufferToWrite = _screenBuffers[screenBufferID];

            COORD coordsForLoop = coords;
            for (int i = 0; i < startIndexes.Length; i++)
            {
                COORD coordsThisIteration = coords;
                coordsThisIteration.X += startIndexes[i];
                FillConsoleOutputAttribute(screenBufferToWrite, (int)foregroundColor | (int)backgroundColor, lengthToPaint[i], coordsThisIteration, out ignore);
            }
            WriteConsoleOutputCharacter(screenBufferToWrite, message, message.Length, coords, out ignore);
        }

        internal protected static void ClearConsole(int screenBufferID)
        {
            uint ignore = 0;
            screenBufferID--;
            FillConsoleOutputAttribute(_screenBuffers[screenBufferID], 0, Screen.P_Width * Screen.P_Height + Screen.P_Width, new COORD(0, 0), out ignore);
            //FillConsoleOutputCharacter(_screenBuffers[--screenBufferID], ' ', julienfEngine.P_ScreenX * julienfEngine.P_ScreenY + julienfEngine.P_ScreenX, new COORD(0, 0), out ignore);
        }

        internal protected static void HideConsoleCursor()
        {
            CONSOLE_CURSOR_INFO consoleCursorInfo = new CONSOLE_CURSOR_INFO();
            consoleCursorInfo.Size = 1;
            consoleCursorInfo.Visible = false;
            bool succesfull = SetConsoleCursorInfo(_screenBuffers[0], ref consoleCursorInfo);
            bool succesfull2 = SetConsoleCursorInfo(_screenBuffers[1], ref consoleCursorInfo);
            bool x = succesfull;
            bool y = succesfull;
        }

        #endregion

        #region ---Properties

        internal protected static int P_NumberOfScreenBuffers
        {
            get
            {
                return _numberOfScreenBuffers;
            }
        }

        #endregion
    }
}