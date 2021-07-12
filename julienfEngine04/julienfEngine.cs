﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Buffers;
using System.IO;

namespace julienfEngine1
{

    class julienfEngine : DllImporter //This class manages console and all relationed with how to behaves the engine
    {
        #region ---ATRIBUTES;

        private static int _screenX; //width screen of the game
        private static int _screenY; //height screen of the game

        private static Camera _mainCamera = new Camera(0, 0); //This Camera is the main camera, the camera displayed 

        private static int _currentScreenBufferID = 1; //This variable is a variable that contains the number of the current screen buffer that should be displayed 


        #endregion

        #region ---ENUMS

        public enum ForegroundColors
        {
            Black = 0,
            DarkGray = FOREGROUND_INTENSITY,
            DarkRed = FOREGROUND_RED,
            DarkGreen = FOREGROUND_GREEN,
            DarkBlue = FOREGROUND_BLUE,
            DarkYellow = FOREGROUND_RED | FOREGROUND_GREEN,
            DarkViolet = FOREGROUND_RED | FOREGROUND_BLUE,
            DarkLightblue = FOREGROUND_GREEN | FOREGROUND_BLUE,
            Gray = FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE,
            Red = FOREGROUND_RED | FOREGROUND_INTENSITY,
            Green = FOREGROUND_GREEN | FOREGROUND_INTENSITY,
            Blue = FOREGROUND_BLUE | FOREGROUND_INTENSITY,
            Yellow = FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_INTENSITY,
            Violet = FOREGROUND_RED | FOREGROUND_BLUE | FOREGROUND_INTENSITY,
            Lightblue = FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_INTENSITY,
            White = FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_INTENSITY
        }

        public enum BackgroundColors
        {
            Black = 0,
            DarkGray = BACKGROUND_INTENSITY,
            DarkRed = BACKGROUND_RED,
            DarkGreen = BACKGROUND_GREEN,
            DarkBlue = BACKGROUND_BLUE,
            DarkYellow = BACKGROUND_RED | BACKGROUND_GREEN,
            DarkPink = BACKGROUND_RED | BACKGROUND_BLUE,
            DarkLightblue = BACKGROUND_GREEN | BACKGROUND_BLUE,
            Gray = BACKGROUND_RED | BACKGROUND_GREEN | BACKGROUND_BLUE,
            Red = BACKGROUND_RED | BACKGROUND_INTENSITY,
            Green = BACKGROUND_GREEN | BACKGROUND_INTENSITY,
            Blue = BACKGROUND_BLUE | BACKGROUND_INTENSITY,
            Yellow = BACKGROUND_RED | BACKGROUND_GREEN | BACKGROUND_INTENSITY,
            Pink = BACKGROUND_RED | BACKGROUND_BLUE | BACKGROUND_INTENSITY,
            Lightblue = BACKGROUND_GREEN | BACKGROUND_BLUE | BACKGROUND_INTENSITY,
            White = BACKGROUND_RED | BACKGROUND_GREEN | BACKGROUND_BLUE | BACKGROUND_INTENSITY
        }

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

            Timer.StartTime();
            Timer.StartDeltaTime();
        }

        public static void DrawConsole(GameObject gameObject)
        {
            if (gameObject.P_Visible)
            {
                int y = gameObject.P_IsUI ? gameObject.P_PosY : gameObject.P_PosY - _mainCamera.P_PosY;
                int xStart = gameObject.P_IsUI ? gameObject.P_PosX : gameObject.P_PosX - _mainCamera.P_PosX;
                for (int i = 0; y < (y - i + gameObject.P_GameObjectFigure.P_Figure.Length); y++, i++)
                {
                    if (y >= 0 && y <= _screenY)
                    {
                        int xEnd = xStart + gameObject.P_GameObjectFigure.P_Figure[i].Length;

                        if (xStart >= 0 && xEnd <= _screenX)
                        {
                            DllImporter.WriteConsole(_currentScreenBufferID, gameObject.P_GameObjectFigure.P_Figure[i], new DllImporter.COORD((short)(xStart), (short)y), gameObject.P_ForegroundColor, gameObject.P_BackgroundColor);
                        }
                        else if (xStart < _screenX && xEnd >= _screenX)
                        {
                            DllImporter.WriteConsole(_currentScreenBufferID, gameObject.P_GameObjectFigure.P_Figure[i].Substring(0, _screenX - xStart), new DllImporter.COORD((short)(xStart), (short)y), gameObject.P_ForegroundColor, gameObject.P_BackgroundColor);
                        }
                        else if (xStart < 0 && xEnd > 0)
                        {
                            DllImporter.WriteConsole(_currentScreenBufferID, gameObject.P_GameObjectFigure.P_Figure[i].Substring(-xStart, xEnd), new DllImporter.COORD((short)(0), (short)y), gameObject.P_ForegroundColor, gameObject.P_BackgroundColor);
                        }
                    }
                }
            }
        }

        private static void ChangeScreenBuffer() //This method changes the current screen buffer to the next screen buffer
        {
            DllImporter.SetScreenBuffer(_currentScreenBufferID);

            _currentScreenBufferID = _currentScreenBufferID % DllImporter.P_NumberOfScreenBuffers;
            _currentScreenBufferID++;

            DllImporter.ClearConsole(_currentScreenBufferID);
        }

        public static void ResetValuesUpdate() //this method resets all values of the game so that it works correctly
        {
            ChangeScreenBuffer();

            Timer.ResetDeltaTime();
            Timer.StartDeltaTime();
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

        public static Camera P_MainCamera
        {
            get
            {
                return _mainCamera;
            }

            set
            {
                _mainCamera = value;
            }
        }

        #endregion
    }
}
