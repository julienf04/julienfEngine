using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace julienfEngine1
{
    internal class julienfEngine : DllImporter //This class manages console and all relationed with how to behaves the
                                               //engine
    {
        #region ---ATRIBUTES;

        private static int _currentScreenBufferID = 1; //This variable is a variable that contains the number of the current screen buffer that should be displayed

        private static double _limitTimePerFrame = 0;

        private static bool _limitFPSbyAverage = false;

        private static readonly Stack<Task> _tasksToWait = new Stack<Task>();

        private const byte _COUNT_OF_METHODS_TO_RESET_ALL_ENGINE_VALUES = 3;

        public const char SPECIAL_ASCII_CHARACTER = '¼';

        #if DEBUG
        private static Debug _debugGameObject;
        #endif

        #endregion

        #region ---METHODS;

        private static void Initialize() //Initialize the engine
        {
            Screen.P_Width = Console.LargestWindowWidth;
            Screen.P_Height = Console.LargestWindowHeight + 3;

            // TRABAJO PENDIENTE: ACCEDER A LA WINAPI Y HACER QUE EL CURSOR DE LA CONSOLA NO SEA VISIBLE. CREO QUE EL METODO SE LLAMA "CursorVisible(bool)"

            DllImporter.RemoveBorders();

            DllImporter.MaximizeWindow();

            //DllController.ConfineCursor(true);

            DllImporter.CreateScreenBuffer();

            DllImporter.HideConsoleCursor();

            _currentScreenBufferID = DllImporter.P_NumberOfScreenBuffers;

            Timer.StartTime();
            Timer.StartDeltaTime();
        }

        private static void DrawAllGameObjects()
        {
            for (int iGameObject = 0; iGameObject < Scene.P_CurrentScene.P_GameObjectsToDraw.Length; iGameObject++)
            {
                GameObject currentGameObjectToDraw = Scene.P_CurrentScene.P_GameObjectsToDraw[iGameObject];

                int figureIndex = currentGameObjectToDraw.P_Animation.P_IsRunning ? currentGameObjectToDraw.P_Animation.P_CurrentFigure : currentGameObjectToDraw.P_BaseFigure;

                int y = currentGameObjectToDraw.P_IsUI ? (int)currentGameObjectToDraw.P_PosY : (int)(currentGameObjectToDraw.P_PosY - Scene.P_CurrentScene.P_MainCamera.P_PosY);
                int xStart = currentGameObjectToDraw.P_IsUI ? (int)currentGameObjectToDraw.P_PosX : (int)(currentGameObjectToDraw.P_PosX - Scene.P_CurrentScene.P_MainCamera.P_PosX);
                for (int iFigure = 0; y < (y - iFigure + currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure.Length); y++, iFigure++)
                {
                    if (y >= 0 && y <= Screen.P_Height)
                    {
                        int xEnd = xStart + currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Length;
                        bool hasSpecialChars = currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_HasSpecialCharacters is not null && currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_HasSpecialCharacters[iFigure];

                        if (xStart >= 0 && xEnd <= Screen.P_Width)
                            if (!hasSpecialChars)
                                DllImporter.WriteConsole(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure], new DllImporter.COORD((short)(xStart), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor);
                            else
                                DllImporter.WriteConsoleWithSpecialCharacters(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure], new DllImporter.COORD((short)(xStart), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_SpecialCharactersStartIndexes[iFigure], currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_SpecialCharactersLengthToPaint[iFigure]);

                        else if (xStart < Screen.P_Width && xEnd >= Screen.P_Width)
                            if (!hasSpecialChars)
                                DllImporter.WriteConsole(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Substring(0, Screen.P_Width - xStart), new DllImporter.COORD((short)(xStart), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor);
                            else
                                DllImporter.WriteConsoleWithSpecialCharacters(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Substring(0, Screen.P_Width - xStart), new DllImporter.COORD((short)(xStart), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_SpecialCharactersStartIndexes[iFigure], currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_SpecialCharactersLengthToPaint[iFigure]);

                        else if (xStart < 0 && xEnd > 0)
                            if (!hasSpecialChars)
                                DllImporter.WriteConsole(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Substring(-xStart, xEnd), new DllImporter.COORD((short)(0), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor);
                            else
                                DllImporter.WriteConsoleWithSpecialCharacters(_currentScreenBufferID, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_Figure[iFigure].Substring(-xStart, xEnd), new DllImporter.COORD((short)(0), (short)y), currentGameObjectToDraw.P_GameObjectFigures[figureIndex].ForegroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].BackgroundColor, currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_SpecialCharactersStartIndexes[iFigure], currentGameObjectToDraw.P_GameObjectFigures[figureIndex].P_SpecialCharactersLengthToPaint[iFigure]);
                    }
                }

                if (currentGameObjectToDraw.P_Animation.P_IsRunning) currentGameObjectToDraw.P_Animation.NextFigure();
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

            _currentScreenBufferID %= DllImporter.P_NumberOfScreenBuffers;
            _currentScreenBufferID++;

            DllImporter.ClearConsole(_currentScreenBufferID);
        }

        private static void ResetValuesUpdate() //this method resets all values of the game so that it works correctly
        {
            WaitPendingTasks();

            Task[] tasksToResetValues = new Task[_COUNT_OF_METHODS_TO_RESET_ALL_ENGINE_VALUES];

            tasksToResetValues[0] = Task.Run(DetectAllCollisions);
            tasksToResetValues[1] = Task.Run(DrawAllGameObjects);
            tasksToResetValues[2] = Task.Run(Input.ResetValues);

            Task.WaitAll(tasksToResetValues);

            ChangeScreenBuffer();

            LimitFPSResetValues();                                                                                                                                                                                                                                                                                                                                                                                                                       

            Timer.ResetDeltaTime();
            Timer.StartDeltaTime();
        }

        private static void DetectAllCollisions()
        {
            for (int i = 0; i < Scene.P_CurrentScene.P_ICollideableToDetectCollisions.Length; i++)
            {
                GameObject currentGameObjectCollision1 = (GameObject)Scene.P_CurrentScene.P_ICollideableToDetectCollisions[i];

                if (i + 1 < Scene.P_CurrentScene.P_ICollideableToDetectCollisions.Length)
                {
                    for (int i2 = i + 1; i2 < Scene.P_CurrentScene.P_ICollideableToDetectCollisions.Length; i2++)
                    {
                        GameObject currentGameObjectCollision2 = (GameObject)Scene.P_CurrentScene.P_ICollideableToDetectCollisions[i2];

                        if (Collision.IsCollision(currentGameObjectCollision1, currentGameObjectCollision2))
                        {
                            if (!currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.Contains(currentGameObjectCollision2))
                            {
                                currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionEnterGameObjects.Push(currentGameObjectCollision2);
                                currentGameObjectCollision2.P_Collision.P_CurrentOnCollisionEnterGameObjects.Push(currentGameObjectCollision1);
                                currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.Add(currentGameObjectCollision2);
                                currentGameObjectCollision2.P_Collision.P_CurrentOnCollisionStayGameObjects.Add(currentGameObjectCollision1);
                            }
                        }
                        else if (currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.Remove(currentGameObjectCollision2) &&
                                  currentGameObjectCollision2.P_Collision.P_CurrentOnCollisionStayGameObjects.Remove(currentGameObjectCollision1))
                        {
                            currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionExitGameObjects.Push(currentGameObjectCollision2);
                            currentGameObjectCollision2.P_Collision.P_CurrentOnCollisionExitGameObjects.Push(currentGameObjectCollision1);
                        }
                    }
                }

                if (currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionEnterGameObjects.Count != 0)
                {
                    if (currentGameObjectCollision1 is IOnCollisionEnter colEnter) colEnter.OnCollisionEnter(currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionEnterGameObjects.ToArray());
                    if (currentGameObjectCollision1 is IOnCollisionStay colStay) colStay.OnCollisionStay(currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.ToArray());
                }

                else if (currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.Count != 0 && currentGameObjectCollision1 is IOnCollisionStay colStay)
                    colStay.OnCollisionStay(currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionStayGameObjects.ToArray());

                if (currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionExitGameObjects.Count != 0 && currentGameObjectCollision1 is IOnCollisionExit colExit)
                    colExit.OnCollisionExit(currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionExitGameObjects.ToArray());

                currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionEnterGameObjects.Clear();
                currentGameObjectCollision1.P_Collision.P_CurrentOnCollisionExitGameObjects.Clear();
            }
        }

        private static void LimitFPSResetValues()
        {
            double timeToWait = _limitFPSbyAverage ? Timer.P_AverageFPS : _limitTimePerFrame;

            while (Timer.P_CurrentTimeOfDeltaTime < timeToWait) { }
        }

        public static void LimitFPS(uint fps)
        {
            _limitTimePerFrame = 1 / (double)fps;
        }

        public static void LimitFPSbyAverage(uint limitFpsToReset)
        {
            _limitFPSbyAverage = true;
            Timer.P_LimitFPS = (int)limitFpsToReset;
        }

        public static void WaitToTasksAtEndFrame(Task taskToWait)
        {
            _tasksToWait.Push(taskToWait);
        }

        private static void WaitPendingTasks()
        {
            int count = _tasksToWait.Count;
            for (int i = 0; i < count; i++)
            {
                _tasksToWait.Pop().Wait();
            }
        }


        #if DEBUG
        internal static void SetDebugGameObject()
        {
            _debugGameObject = new Debug(0, 0, true, true, byte.MaxValue);
            _debugGameObject.P_GameObjectFigures[0].P_Figure = new string[1] { "" };
        }
        #endif

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


                #if DEBUG
                _debugGameObject.UpdateValues();
                #endif
            }
        }

        #endregion
    }
}
