using System;
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

        [DllImport("user32.dll")] static extern bool SetKeyboardState(byte[] lpKeyState);

        //[DllImport("user32.dll")] static extern uint GetKeyboardLayoutList(int nBuff, [Out] IntPtr[] lpList);

        //[DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] static extern bool GetKeyboardState(byte[] lpKeyState);

        #endregion


        #region ---ATRIBUTES

        private static List<E_Keyboard> _keysPressedThisFrame = new List<E_Keyboard>();
        private static List<E_Keyboard> _keysDown = new List<E_Keyboard>();
        private static List<E_Keyboard> _keysUp = new List<E_Keyboard>();
        private static List<E_Keyboard> _keysDownToCheck = new List<E_Keyboard>();
        private static List<E_Keyboard> _keysUpToCheck = new List<E_Keyboard>();

        private static E_Keyboard _lastKeyPressed = 0;
        private static E_Keyboard _lastKeyUp = 0;

        private static bool _anyKeyPressed = false;

        private static Stack<E_Keyboard> _keysUsedForThisGame = new Stack<E_Keyboard>();

        #endregion


        #region ---METHODS

        private static bool InternalGetKey(E_Keyboard key)
        {
            if (_keysPressedThisFrame.Contains(key))
            {
                _lastKeyPressed = key;
                return true;
            }
            else if (GetAsyncKeyState(key) != 0)
            {
                _keysPressedThisFrame.Add(key);
                _lastKeyPressed = key;
                return true;
            }

            return false;
        }

        public static bool GetKey(E_Keyboard key)
        {
            if (!_keysUsedForThisGame.Contains(key))
            {
                _keysUsedForThisGame.Push(key);
                return GetAsyncKeyState(key) < 0;
            }

            return InternalGetKey(key);
        }

        public static bool GetKeyDown(E_Keyboard key)
        {
            if (GetKey(key))
            {
                if (!_keysDownToCheck.Contains(key)) _keysDownToCheck.Add(key);
                _lastKeyPressed = key;
                return !_keysDown.Contains(key);
            }

            return false;
        }

        public static bool GetKeyUp(E_Keyboard key)
        {
            if (!GetKey(key))
            {
                if (_keysUp.Contains(key))
                {
                    _lastKeyUp = key;
                    return true;
                }
                return false;
            }

            if (!_keysUpToCheck.Contains(key)) _keysUpToCheck.Add(key);
            _lastKeyPressed = key;
            return false;
        }

        internal static void ResetValues()
        {
            for (int i = 0; i < _keysDownToCheck.Count; i++)
            {
                E_Keyboard currentKeyToCheck = _keysDownToCheck[i];
                if (InternalGetKey(currentKeyToCheck))
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
                if (!InternalGetKey(currentKeyToCheck))
                {
                    if (!_keysUp.Contains(currentKeyToCheck)) _keysUp.Add(currentKeyToCheck);
                    else
                    {
                        _keysUp.Remove(currentKeyToCheck);
                        _keysUpToCheck.Remove(currentKeyToCheck);
                    }
                }
            }

            foreach (E_Keyboard key in _keysUsedForThisGame) GetAsyncKeyState(key);

            _keysPressedThisFrame.Clear();
        }



        public static bool AnyKeyPressed()
        {
            E_Keyboard[] allKeys = Enum.GetValues<E_Keyboard>();
            foreach (E_Keyboard key in allKeys)
            {
                if (GetKeyDown(key))
                {
                    _anyKeyPressed = true;
                    break;
                }
            }

            bool anyKeyPressedNow = _anyKeyPressed;
            _anyKeyPressed = false;
            return anyKeyPressedNow;
        }

        #endregion

        #region PROPERTIES

        public static E_Keyboard P_LastKeyPressed
        {
            get
            {
                return _lastKeyPressed;
            }
        }

        public static E_Keyboard P_LastKeyUp
        {
            get
            {
                return _lastKeyUp;
            }
        }

        #endregion
    }
}
