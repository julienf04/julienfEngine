using System;
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

        private static GameObject[] _currentGameObjectsToDraw;

        private static GameObject[] _currentGameObjectCollisions;

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
            {
                if (!_currentScene.P_GameObjectsToDraw.Contains(gameObject))
                {
                    if (_currentScene.P_GameObjectsToDraw.Max(gameObjectOfList => gameObjectOfList.P_Layer) > gameObject.P_Layer) _currentScene.P_GameObjectsToDraw.Insert(_currentScene.P_GameObjectsToDraw.FindIndex(gameObjectOfList => gameObjectOfList.P_Layer > gameObject.P_Layer), gameObject);
                    else _currentScene.P_GameObjectsToDraw.Add(gameObject);

                    _currentGameObjectsToDraw = _currentScene.P_GameObjectsToDraw.ToArray();
                }
            }
            else
            {
                _currentScene.P_GameObjectsToDraw.Add(gameObject);
                _currentGameObjectsToDraw = _currentScene.P_GameObjectsToDraw.ToArray();
            }
        }

        private static void DrawAllGameObjects()
        {
            for (int iGameObject = 0; iGameObject < _currentGameObjectsToDraw.Length; iGameObject++)
            {
                GameObject currentGameObjectToDraw = _currentGameObjectsToDraw[iGameObject];
                if (currentGameObjectToDraw.P_Visible)
                {
                    int figureIndex = currentGameObjectToDraw.P_Animation.P_IsRunning ? currentGameObjectToDraw.P_Animation.P_CurrentFigure : currentGameObjectToDraw.P_BaseFigure;

                    int y = currentGameObjectToDraw.P_IsUI ? (int)currentGameObjectToDraw.P_PosY : (int)(currentGameObjectToDraw.P_PosY - _currentScene.P_MainCamera.P_PosY);
                    int xStart = currentGameObjectToDraw.P_IsUI ? (int)currentGameObjectToDraw.P_PosX : (int)(currentGameObjectToDraw.P_PosX - _currentScene.P_MainCamera.P_PosX);
                    for (int iFigure = 0; y < (y - iFigure + currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure.Length); y++, iFigure++)
                    {
                        if (y >= 0 && y <= _screenY)
                        {
                            int xEnd = xStart + currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Length;

                            if (xStart >= 0 && xEnd <= _screenX)
                            {
                                DllImporter.WriteConsole(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure], new DllImporter.COORD((short)(xStart), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor);
                            }
                            else if (xStart < _screenX && xEnd >= _screenX)
                            {
                                DllImporter.WriteConsole(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Substring(0, _screenX - xStart), new DllImporter.COORD((short)(xStart), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor);
                            }
                            else if (xStart < 0 && xEnd > 0)
                            {
                                DllImporter.WriteConsole(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Substring(-xStart, xEnd), new DllImporter.COORD((short)(0), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor);
                            }
                        }
                    }

                    if (currentGameObjectToDraw.P_Animation.P_IsRunning) currentGameObjectToDraw.P_Animation.NextFigure();
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
            DetectAllCollisions();

            DrawAllGameObjects();
            ChangeScreenBuffer();

            Timer.ResetDeltaTime();
            Timer.StartDeltaTime();
        }

        public static void AllowCollisions(GameObject gameObject)
        {
            if (!_currentScene.P_GameObjectsToDetectCollisions.Contains(gameObject))
            {
                _currentScene.P_GameObjectsToDetectCollisions.Add(gameObject);
                _currentGameObjectCollisions = _currentScene.P_GameObjectsToDetectCollisions.ToArray();
            }
        }

        public static void NotAllowCollisions(GameObject gameObject)
        {
            if (_currentScene.P_GameObjectsToDetectCollisions.Remove(gameObject)) _currentGameObjectCollisions = _currentScene.P_GameObjectsToDetectCollisions.ToArray();
        }

        private static void DetectAllCollisions()
        {
            for (int i = 0; i < _currentGameObjectCollisions.Length; i++)
            {
                GameObject currentGameObjectCollision1 = _currentGameObjectCollisions[i];
                for (int i2 = i + 1; i2 < _currentGameObjectCollisions.Length; i2++)
                {
                    GameObject currentGameObjectCollision2 = _currentGameObjectCollisions[i2];
                    if (currentGameObjectCollision1.CollisionWith(currentGameObjectCollision2))
                    {
                        if (!currentGameObjectCollision1.P_CurrentOnCollisionStayGameObjects.Contains(currentGameObjectCollision2))
                        {
                            currentGameObjectCollision1.P_CurrentOnCollisionEnterGameObjects.Add(currentGameObjectCollision2);
                            currentGameObjectCollision2.P_CurrentOnCollisionEnterGameObjects.Add(currentGameObjectCollision1);
                            currentGameObjectCollision1.P_CurrentOnCollisionStayGameObjects.Add(currentGameObjectCollision2);
                            currentGameObjectCollision2.P_CurrentOnCollisionStayGameObjects.Add(currentGameObjectCollision1);
                        }
                    }
                    else if (currentGameObjectCollision1.P_CurrentOnCollisionStayGameObjects.Remove(currentGameObjectCollision2) && currentGameObjectCollision2.P_CurrentOnCollisionStayGameObjects.Remove(currentGameObjectCollision1))
                    {
                        currentGameObjectCollision1.P_CurrentOnCollisionExitGameObjects.Add(currentGameObjectCollision2);
                        currentGameObjectCollision2.P_CurrentOnCollisionExitGameObjects.Add(currentGameObjectCollision1);
                    }
                }

                if (currentGameObjectCollision1.P_CurrentOnCollisionEnterGameObjects.Count != 0)
                {
                    currentGameObjectCollision1.OnCollisionEnter(currentGameObjectCollision1.P_CurrentOnCollisionEnterGameObjects.ToArray());
                    currentGameObjectCollision1.OnCollisionStay(currentGameObjectCollision1.P_CurrentOnCollisionStayGameObjects.ToArray());
                }
                else if (currentGameObjectCollision1.P_CurrentOnCollisionStayGameObjects.Count != 0) currentGameObjectCollision1.OnCollisionStay(currentGameObjectCollision1.P_CurrentOnCollisionStayGameObjects.ToArray());

                if (currentGameObjectCollision1.P_CurrentOnCollisionExitGameObjects.Count != 0) currentGameObjectCollision1.OnCollisionExit(currentGameObjectCollision1.P_CurrentOnCollisionExitGameObjects.ToArray());

                currentGameObjectCollision1.P_CurrentOnCollisionEnterGameObjects.Clear();
                currentGameObjectCollision1.P_CurrentOnCollisionExitGameObjects.Clear();
            }
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

            while (true)
            {
                Game.Update();

                julienfEngine.ResetValuesUpdate();
            }
        }

        #endregion
    }
}
