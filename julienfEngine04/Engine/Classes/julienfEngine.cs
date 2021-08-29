using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Buffers;
using System.IO;
using System.Linq;

namespace julienfEngine1
{
    internal class julienfEngine : DllImporter //This class manages console and all relationed with how to behaves the engine
    {
        #region ---ATRIBUTES;

        private static int _currentScreenBufferID = 1; //This variable is a variable that contains the number of the current screen buffer that should be displayed 

        #endregion

        #region ---METHODS;

        private static void Initialize() //Initialize the engine
        {
            Screen.P_Width = Console.LargestWindowWidth;
            Screen.P_Height = Console.LargestWindowHeight + 3;

            //Console.SetBufferSize(_screenX, _screenY);

            DllImporter.RemoveBorders();

            DllImporter.MaximizeWindow();

            //DllController.ConfineCursor(true);

            DllImporter.CreateScreenBuffer();

            _currentScreenBufferID = DllImporter.P_NumberOfScreenBuffers;

            Timer.StartTime();
            Timer.StartDeltaTime();
        }

        private static void DrawAllGameObjects()
        {
            for (int iGameObject = 0; iGameObject < Scene.P_CurrentScene.P_GameObjectsToDrawArray.Length; iGameObject++)
            {
                GameObject currentGameObjectToDraw = Scene.P_CurrentScene.P_GameObjectsToDrawArray[iGameObject];
                if (currentGameObjectToDraw.P_Visible)
                {
                    int figureIndex = currentGameObjectToDraw.P_Animation.P_IsRunning ? currentGameObjectToDraw.P_Animation.P_CurrentFigure : currentGameObjectToDraw.P_BaseFigure;

                    int y = currentGameObjectToDraw.P_IsUI ? (int)currentGameObjectToDraw.P_PosY : (int)(currentGameObjectToDraw.P_PosY - Scene.P_CurrentScene.P_MainCamera.P_PosY);
                    int xStart = currentGameObjectToDraw.P_IsUI ? (int)currentGameObjectToDraw.P_PosX : (int)(currentGameObjectToDraw.P_PosX - Scene.P_CurrentScene.P_MainCamera.P_PosX);
                    for (int iFigure = 0; y < (y - iFigure + currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure.Length); y++, iFigure++)
                    {
                        if (y >= 0 && y <= Screen.P_Height)
                        {
                            int xEnd = xStart + currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Length;

                            if (xStart >= 0 && xEnd <= Screen.P_Width)
                            {
                                DllImporter.WriteConsole(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure], new DllImporter.COORD((short)(xStart), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor);
                            }
                            else if (xStart < Screen.P_Width && xEnd >= Screen.P_Width)
                            {
                                DllImporter.WriteConsole(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Substring(0, Screen.P_Width - xStart), new DllImporter.COORD((short)(xStart), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor);
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

        //public static void InitializeScene(Type sceneType)
        //{
        //    if (Scene.IsScene(sceneType)) Scene.Initialize(sceneType);
        //    else throw new Exception("The type is not a scene");
        //}

        //public static void SetLoadedScene(Type sceneType, bool deleteCurrentSceneValues)
        //{
        //    if (Scene.IsScene(sceneType))
        //    {
        //        if (deleteCurrentSceneValues)
        //        {
        //            _currentScene.RemoveAllToDrawGameObject();
        //            _currentScene.RemoveAllToDetectCollisionsGameObject();
        //        }

        //        _currentScene = Scene.GetLoadedSceneByType(sceneType); ;
        //        GC.Collect();
        //        _currentScene.Start();
        //        return;
        //    }
        //    throw new Exception("The type is not a scene");
        //}

        //public static void LoadScene(Type sceneType)
        //{
        //    if (Scene.IsScene(sceneType)) Scene.LoadScene(sceneType);
        //    else throw new Exception("The type is not a scene");
        //}

        //public static void UnloadScene(Type sceneType)
        //{
        //    if (Scene.IsScene(sceneType)) Scene.UnloadScene(sceneType);
        //    else throw new Exception("The type is not a scene");
        //}

        private static void ChangeScreenBuffer() //This method changes the current screen buffer to the next screen buffer
        {
            DllImporter.SetScreenBuffer(_currentScreenBufferID);

            _currentScreenBufferID = _currentScreenBufferID % DllImporter.P_NumberOfScreenBuffers;
            _currentScreenBufferID++;

            DllImporter.ClearConsole(_currentScreenBufferID);
        }

        private static void ResetValuesUpdate() //this method resets all values of the game so that it works correctly
        {
            DetectAllCollisions();

            DrawAllGameObjects();
            ChangeScreenBuffer();

            Input.ResetValues();

            Timer.ResetDeltaTime();
            Timer.StartDeltaTime();
        }

        private static void DetectAllCollisions()
        {
            for (int i = 0; i < Scene.P_CurrentScene.P_ICollideableToDetectCollisionsArray.Length; i++)
            {
                GameObject currentGameObjectCollision1 = (GameObject)Scene.P_CurrentScene.P_ICollideableToDetectCollisionsArray[i];

                if (currentGameObjectCollision1.P_Collision.P_DetectCollisions)
                {
                    for (int i2 = i + 1; i2 < Scene.P_CurrentScene.P_ICollideableToDetectCollisionsArray.Length; i2++)
                    {
                        GameObject currentGameObjectCollision2 = (GameObject)Scene.P_CurrentScene.P_ICollideableToDetectCollisionsArray[i2];

                        if (currentGameObjectCollision2.P_Collision.P_DetectCollisions)
                        {
                            if (Collision.IsCollision(currentGameObjectCollision1, currentGameObjectCollision2))
                            {
                                if (!currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.Contains(currentGameObjectCollision2))
                                {
                                    currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionEnterGameObjects.Add(currentGameObjectCollision2);
                                    currentGameObjectCollision2.P_Collision.P_CurrentOnCollisionEnterGameObjects.Add(currentGameObjectCollision1);
                                    currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.Add(currentGameObjectCollision2);
                                    currentGameObjectCollision2.P_Collision.P_CurrentOnCollisionStayGameObjects.Add(currentGameObjectCollision1);
                                }
                            }
                            else if (currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.Remove(currentGameObjectCollision2) &&
                                     currentGameObjectCollision2.P_Collision.P_CurrentOnCollisionStayGameObjects.Remove(currentGameObjectCollision1))
                            {
                                currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionExitGameObjects.Add(currentGameObjectCollision2);
                                currentGameObjectCollision2.P_Collision.P_CurrentOnCollisionExitGameObjects.Add(currentGameObjectCollision1);
                            }
                        }
                    }
                }


                if (currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionEnterGameObjects.Count != 0)
                {
                    ((ICollideable)currentGameObjectCollision1).OnCollisionEnter(currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionEnterGameObjects.ToArray());
                    ((ICollideable)currentGameObjectCollision1).OnCollisionStay(currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.ToArray());
                }

                else if (currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.Count != 0)
                    ((ICollideable)currentGameObjectCollision1).OnCollisionStay(currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.ToArray());

                if (currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionExitGameObjects.Count != 0)
                    ((ICollideable)currentGameObjectCollision1).OnCollisionExit(currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionExitGameObjects.ToArray());

                currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionEnterGameObjects.Clear();
                currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionExitGameObjects.Clear();
            }
        }

        #endregion

        #region ---PROPIERTIES;

        #endregion



        #region ---MAIN

        static void Main(string[] args)
        {
            julienfEngine.Initialize();

            Game.Initialize();

            while (true)
            {
                // Game Update();
                Scene.P_CurrentScene.Update();
                
                // Engine Update
                julienfEngine.ResetValuesUpdate();
            }
        }

        #endregion
    }
}
