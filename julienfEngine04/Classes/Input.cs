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

        [DllImport("User32", SetLastError = true)] static extern short GetAsyncKeyState(Keyboard Key);

        #endregion


        #region ---ATRIBUTES

        static List<Keyboard> _keysDown = new List<Keyboard>();
        static List<Keyboard> _keysUp = new List<Keyboard>();

        #endregion


        #region ---METHODS

        public static bool GetKey(Keyboard key)
        {
            return GetAsyncKeyState(key) != 0 ? true : false;
        }

        public static bool GetKeyDown(Keyboard key)
        {
            if (GetAsyncKeyState(key) != 0)
            {
                if (_keysDown.Contains(key) == false)
                {
                    _keysDown.Add(key);
                    return true;
                }
            }
            else if (_keysDown.Contains(key) == true) _keysDown.Remove(key);
            
            return false;
        }

        public static bool GetKeyUp(Keyboard key)
        {
            if (GetAsyncKeyState(key) == 0)
            {
                if (_keysUp.Contains(key))
                {
                    _keysUp.Remove(key);
                    return true;
                }
            }
            else if (_keysUp.Contains(key) == false) _keysUp.Add(key);

            return false;
        }

        #endregion
    }
}
