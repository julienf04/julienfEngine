using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    static class Screen
    {
        private static int _width; //width screen of the game
        private static int _height; //height screen of the game

        public static int P_Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                Console.BufferWidth = _width;
            }
        }

        public static int P_Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                Console.BufferHeight = _height;
            }
        }
    }
}
