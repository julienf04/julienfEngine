using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Buffers;
using System.IO;

namespace julienfEngine1
{

    static class julienfEngine //This class manages console and all relationed with how to behaves the engine
    {
        #region ---ATRIBUTES;

        private static int _screenX; //width screen of the game
        private static int _screenY; //height screen of the game

        private static Camera mainCamera = new Camera(0, 0);

        //private static bool _isFirstScreenBuffer = true; //True if the first screen to draw gameObjects, and false if the second screen to draw gameObjects

        private static int _currentScreenBufferID = 1;


        #endregion

        #region ---METHODS;

        public static void Initialize() //Initialize the engine
        {
            _screenX = Console.LargestWindowWidth;
            _screenY = Console.LargestWindowHeight + 3;

            Console.SetBufferSize(_screenX, _screenY);

            DllImporter.RemoveBorders();

            DllImporter.MaximizeWindow();

            //DllController.ConfineCursor(true);

            DllImporter.CreateScreenBuffer();

            _currentScreenBufferID = DllImporter.P_NumberOfScreenBuffers;
        }

        public static void DrawConsole(GameObject gameObject)
        {
            for (int i = 0, y = gameObject.P_posY; y < (gameObject.P_posY + gameObject.P_figure.P_Figure.Length); y++, i++)
            {
                if (y >= 0 && y <= _screenY)
                {
                    if (gameObject.P_posX + gameObject.P_figure.P_Figure[i].Length <= _screenX && gameObject.P_posX >= 0)
                    {
                        DllImporter.WriteConsole(_currentScreenBufferID, gameObject.P_figure.P_Figure[i], new DllImporter.COORD((short)gameObject.P_posX, (short)y));
                        //Console.SetCursorPosition(gameObject.P_posX, y);

                        //Console.Write(gameObject.P_figure.P_Figure[i]);
                    }
                    else if (gameObject.P_posX < _screenX && gameObject.P_posX + gameObject.P_figure.P_Figure[i].Length >= _screenX)
                    {
                        DllImporter.WriteConsole(_currentScreenBufferID, gameObject.P_figure.P_Figure[i].Substring(0, _screenX - gameObject.P_posX), new DllImporter.COORD((short)gameObject.P_posX, (short)y));
                        //Console.SetCursorPosition(gameObject.P_posX, y);

                        //Console.Write(gameObject.P_figure.P_Figure[i].Substring(0, _screenX - gameObject.P_posX));
                    }
                    else if (gameObject.P_posX < 0 && gameObject.P_posX + gameObject.P_figure.P_Figure[i].Length > 0)
                    {
                        DllImporter.WriteConsole(_currentScreenBufferID, gameObject.P_figure.P_Figure[i].Substring(-gameObject.P_posX, gameObject.P_figure.P_Figure[i].Length + gameObject.P_posX), new DllImporter.COORD(0, (short)y));
                        //Console.SetCursorPosition(0, y);

                        //Console.Write(gameObject.P_figure.P_Figure[i].Substring(-gameObject.P_posX, gameObject.P_figure.P_Figure[i].Length + gameObject.P_posX));
                    }
                }
            }

            //Console.Clear();
            ChangeScreenBuffer();
        }

        private static void ChangeScreenBuffer()
        {
            DllImporter.SetScreenBuffer(_currentScreenBufferID);

            _currentScreenBufferID = _currentScreenBufferID % DllImporter.P_NumberOfScreenBuffers;
            _currentScreenBufferID++;

            DllImporter.ClearConsole(_currentScreenBufferID);
        }

        public static void ResetValuesUpdate()
        {

        }

        #endregion

        #region ---PROPIERTIES;

        public static int P_ScreenX
        {
            get
            {
                return _screenX;
            }
        }

        public static int P_ScreenY
        {
            get
            {
                return _screenY;
            }
        }

        #endregion
    }
}
