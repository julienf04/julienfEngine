﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace julienfEngine1
{
    static class Input
    {
        #region ---DLL GetAsyncKeyState

        [DllImport("User32", SetLastError = true)] static extern short GetAsyncKeyState(E_Keyboard Key);

        //[DllImport("user32.dll")] static extern uint GetKeyboardLayoutList(int nBuff, [Out] IntPtr[] lpList);

        //[DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] static extern bool GetKeyboardState(byte[] lpKeyState);

        #endregion


        #region ---ATRIBUTES

        //private static byte[] _keyboardState = new byte[256];

        private static List<E_Keyboard> _keysPressedThisFrame = new List<E_Keyboard>();
        private static List<E_Keyboard> _keysDown = new List<E_Keyboard>();
        private static List<E_Keyboard> _keysUp = new List<E_Keyboard>();
        private static List<E_Keyboard> _keysDownToCheck = new List<E_Keyboard>();
        private static List<E_Keyboard> _keysUpToCheck = new List<E_Keyboard>();

        #endregion


        #region ---METHODS

        public static bool GetKey(E_Keyboard key)
        {
            if (_keysPressedThisFrame.Contains(key)) return true;
            else if (GetAsyncKeyState(key) != 0)
            {
                _keysPressedThisFrame.Add(key);
                return true;
            }

            return false;
        }

        public static bool GetKeyDown(E_Keyboard key)
        {
            if (GetKey(key))
            {
                if (!_keysDownToCheck.Contains(key)) _keysDownToCheck.Add(key);
                return !_keysDown.Contains(key);
            }

            return false;
        }

        public static bool GetKeyUp(E_Keyboard key)
        {
            if (!GetKey(key))
            {
                return _keysUp.Contains(key);
            }

            if (!_keysUpToCheck.Contains(key)) _keysUpToCheck.Add(key);

            return false;
        }

        internal static void ResetValues()
        {
            for (int i = 0; i < _keysDownToCheck.Count; i++)
            {
                E_Keyboard currentKeyToCheck = _keysDownToCheck[i];
                if (GetKey(currentKeyToCheck))
                {
                    if (!_keysDown.Contains(currentKeyToCheck)) _keysDown.Add(currentKeyToCheck);
                }
                else
                {
                    _keysDownToCheck.Remove(currentKeyToCheck);
                    _keysDown.Remove(currentKeyToCheck);
                }
            }


            for (int i = 0; i < _keysUpToCheck.Count; i++)
            {
                E_Keyboard currentKeyToCheck = _keysUpToCheck[i];
                if (!GetKey(currentKeyToCheck))
                {
                    if (!_keysUp.Contains(currentKeyToCheck)) _keysUp.Add(currentKeyToCheck);
                    else
                    {
                        _keysUp.Remove(currentKeyToCheck);
                        _keysUpToCheck.Remove(currentKeyToCheck);
                    }
                }
            }

            _keysPressedThisFrame.Clear();
        }

        #endregion
    }
}
