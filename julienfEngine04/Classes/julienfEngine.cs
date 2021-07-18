﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Buffers;
using System.IO;
using System.Linq;

namespace julienfEngine1
{
    class julienfEngine : DllImporter //This class manages console and all relationed with how to behaves the engine
    {
        #region ---ATRIBUTES;

        private static int _screenX; //width screen of the game
        private static int _screenY; //height screen of the game

        private static int _currentScreenBufferID = 1; //This variable is a variable that contains the number of the current screen buffer that should be displayed 

        private static Scene _currentScene = new Scene();


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
            if (_currentScene.P_GameObjectsToDraw.Count != 0)
                if (_currentScene.P_GameObjectsToDraw.Max(gameObjectOfList => gameObjectOfList.P_Layer) > gameObject.P_Layer) _currentScene.P_GameObjectsToDraw.Insert(_currentScene.P_GameObjectsToDraw.FindIndex(gameObjectOfList => gameObjectOfList.P_Layer > gameObject.P_Layer), gameObject);
                else _currentScene.P_GameObjectsToDraw.Add(gameObject);
            else _currentScene.P_GameObjectsToDraw.Add(gameObject);
        }

        private static void DrawAllGameObjects()
        {
            for (int iGameObject = 0; iGameObject < _currentScene.P_GameObjectsToDraw.Count; iGameObject++)
            {
                if (_currentScene.P_GameObjectsToDraw[iGameObject].P_Visible)
                {
                    int figureIndex = _currentScene.P_GameObjectsToDraw[iGameObject].P_Animation.P_IsRunning ? _currentScene.P_GameObjectsToDraw[iGameObject].P_Animation.P_CurrentFigure : _currentScene.P_GameObjectsToDraw[iGameObject].P_BaseFigure;

                    int y = _currentScene.P_GameObjectsToDraw[iGameObject].P_IsUI ? _currentScene.P_GameObjectsToDraw[iGameObject].P_PosY : _currentScene.P_GameObjectsToDraw[iGameObject].P_PosY - _currentScene.P_MainCamera.P_PosY;
                    int xStart = _currentScene.P_GameObjectsToDraw[iGameObject].P_IsUI ? _currentScene.P_GameObjectsToDraw[iGameObject].P_PosX : _currentScene.P_GameObjectsToDraw[iGameObject].P_PosX - _currentScene.P_MainCamera.P_PosX;
                    for (int iFigure = 0; y < (y - iFigure + _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].P_Figure.Length); y++, iFigure++)
                    {
                        if (y >= 0 && y <= _screenY)
                        {
                            int xEnd = xStart + _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].P_Figure[iFigure].Length;

                            if (xStart >= 0 && xEnd <= _screenX)
                            {
                                DllImporter.WriteConsole(_currentScreenBufferID, _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].P_Figure[iFigure], new DllImporter.COORD((short)(xStart), (short)y), _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].ForegroundColor, _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].BackgroundColor);
                            }
                            else if (xStart < _screenX && xEnd >= _screenX)
                            {
                                DllImporter.WriteConsole(_currentScreenBufferID, _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].P_Figure[iFigure].Substring(0, _screenX - xStart), new DllImporter.COORD((short)(xStart), (short)y), _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].ForegroundColor, _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].BackgroundColor);
                            }
                            else if (xStart < 0 && xEnd > 0)
                            {
                                DllImporter.WriteConsole(_currentScreenBufferID, _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].P_Figure[iFigure].Substring(-xStart, xEnd), new DllImporter.COORD((short)(0), (short)y), _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].ForegroundColor, _currentScene.P_GameObjectsToDraw[iGameObject].P_GameObjectFigures[figureIndex].BackgroundColor);
                            }
                        }
                    }

                    if (_currentScene.P_GameObjectsToDraw[iGameObject].P_Animation.P_IsRunning) _currentScene.P_GameObjectsToDraw[iGameObject].P_Animation.NextFigure();
                }
            }
        }

        public static void SetScene(Scene scene, bool resetCurrentSceneAnimations)
        {
            if (resetCurrentSceneAnimations)
            {
                for (int i = 0; i < _currentScene.P_GameObjectsToDraw.Count; i++)
                {
                    _currentScene.P_GameObjectsToDraw[i].P_Animation.StopAnimation(true);
                }
            }

            _currentScene = scene;
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
            DrawAllGameObjects();

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

        public static Scene P_CurrentScene
        {
            get
            {
                return _currentScene;
            }
        }

        #endregion



        #region ---MAIN

        static void Main(string[] args)
        {
            julienfEngine.Initialize();

            Game.Start();

            Game.Update();
        }

        #endregion
    }
}
