using System;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Threading;
using System.Text;
using System.Collections.Generic;

namespace julienfEngine1
{
    static class Game
    {
        //////////////////////////////////////////////////////////////////////////////////////////////
        #region GAME ATRIBUTES;

        //private static Scene[] allScenes = new Scene[1]
        //{
        //    new MainMenuScene("MainMenu")
        //};


        //private static IClickable[] buttonsMainMenu = new IClickable[4]
        //    {
        //    new SinglePlayerMenu(new Figure[]{ SinglePlayerMenu.RO_figureSinglePlayer }, julienfEngine.P_CurrentScene, 0, true, true, 0, 35, 5),
        //    new MultiplayerMenu(new Figure[]{ MultiplayerMenu.RO_figureMultiplayer }, julienfEngine.P_CurrentScene, 0, true, true, 0, 35, 14),
        //    new CustomizeMenu(new Figure[]{ CustomizeMenu.RO_figureCustomize }, julienfEngine.P_CurrentScene, 0, true, true, 0, 35, 22),
        //    new ExitMenu(new Figure[]{ ExitMenu.RO_figureExit }, julienfEngine.P_CurrentScene, 0, true, true, 0, 35, 30),
        //    };


        //private static ArrowMenu _arrowMenu = new ArrowMenu(ArrowMenu.RO_FigureMenuArrow, julienfEngine.P_CurrentScene, (byte)ArrowMenu.E_ArrowSidesAndSizes.BigArrowPointLeft, true, true, 0, 108, 6);

        //private static double _timerChangeArrowVelocity = 0;
        //private static double _cooldownToKeyDownMovement = 0.60;
        //private static double _cooldownToKeyPressedMovement = 0.40;

        //private static bool _keyUpPressed = false;
        //private static bool _keyDownPressed = false;

        //private static Scene sceneExitGame = new Scene();
        //private static HorizontalLine horizontalLineUp = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Down, new Figure[1], sceneExitGame, 0, true, true, 0, 77, 9);
        //private static HorizontalLine horizontalLineDown = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Up, new Figure[1], sceneExitGame, 0, true, true, 0, 77, 24);
        //private static VerticalLine verticalLineLeft = new VerticalLine(15, VerticalLine.E_CurveDirection.Right, new Figure[1], sceneExitGame, 0, true, true, 0, 76, 10);
        //private static VerticalLine verticalLineRight = new VerticalLine(15, VerticalLine.E_CurveDirection.Left, new Figure[1], sceneExitGame, 0, true, true, 0, 137, 10);
        //private static TextMessage messageAreYouSureYouWantToCloseMe = new TextMessage("Are you sure you want to close me?", new Figure[1], sceneExitGame, 0, true, true, 0, 89, 12);
        //private static Yes yesExit = new Yes(new Figure[1] { Yes.RO_figureYes }, sceneExitGame, 0, true, true, 0, 83, 16);
        //private static No noExit = new No(new Figure[1] { No.RO_figureNo }, sceneExitGame, 0, true, false, 0, 117, 16);





        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////static Figure[] figuresGameObject0 = new Figure[5];
        //static Figure[] figuresGameObject1 = new Figure[1];
        ////static Area colliderSquare;
        ////static Square square0;
        ////static Square square1;
        ////static Square square2;
        ////static Square square3;
        //static Square gameObject1;

        ////static Timer generalTimer = new Timer();

        ////static Scene firstScene = julienfEngine.P_CurrentScene;
        ////static Scene secondScene = new Scene();

        //static double deltaTimeGameObject1 = 0;

        #endregion

        #region SUPPORT METHODS;

        public static void Start()
        {

















            //////////////////////////////////////////////////////////////////////////////////////////////



            // for (int i = 0; i < figuresGameObject0.Length; i++)
            // {
            //     figuresGameObject0[i] = new Figure();
            // }
            // figuresGameObject0[0].P_Figure = new string[11]
            // {
            //     "000000000000000000000000",
            //     "000000000000000000000000",
            //     "000000000000000000000000",
            //     "000000000000000000000000",
            //     "000000000000000000000000",
            //     "000000000000000000000000",
            //     "000000000000000000000000",
            //     "000000000000000000000000",
            //     "000000000000000000000000",
            //     "000000000000000000000000",
            //     "000000000000000000000000"
            // };
            // figuresGameObject0[0].ForegroundColor = E_ForegroundColors.Violet;

            // figuresGameObject0[1].P_Figure = new string[11]
            //{
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX",
            //     "XXXXXXXXXXXXXXXXXXXXXXXX"
            //};
            // figuresGameObject0[1].ForegroundColor = E_ForegroundColors.Blue;

            // figuresGameObject0[2].P_Figure = new string[11]
            //{
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS",
            //     "SSSSSSSSSSSSSSSSSSSSSSSS"
            //};
            // figuresGameObject0[2].ForegroundColor = E_ForegroundColors.White;

            // figuresGameObject0[3].P_Figure = new string[11]
            //{
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY",
            //     "YYYYYYYYYYYYYYYYYYYYYYYY"
            //};
            // figuresGameObject0[3].ForegroundColor = E_ForegroundColors.Green;

            // figuresGameObject0[4].P_Figure = new string[11]
            //{
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO",
            //     "OOOOOOOOOOOOOOOOOOOOOOOO"
            //};
            // figuresGameObject0[4].ForegroundColor = E_ForegroundColors.Yellow;


            //////// haciendo un gameobject Time.deltaTime
            //for (int i = 0; i < figuresGameObject1.Length; i++)
            //{
            //    figuresGameObject1[i] = new Figure();
            //}
            //figuresGameObject1[0].P_Figure = new string[1]
            //{
            //    "DeltaTime: "
            //};














            //colliderSquare = new Area(0, figuresGameObject0[0].P_Figure[0].Length, 0, figuresGameObject0[0].P_Figure.Length);
            //square0 = new Square(true, figuresGameObject0, julienfEngine.P_CurrentScene, 0, true, false, 0, 0, 0, colliderSquare, true);
            //square0.P_Animation.P_AnimationState = E_AnimationStates.RepeatReverse;
            //square0.P_Animation.RunAnimation();
            //square0.ID = 1;

            //square1 = new Square(false, figuresGameObject0, julienfEngine.P_CurrentScene, 0, true, false, 0, 136, 36, colliderSquare, true);
            //square1.ID = 2;

            //square2 = new Square(false, figuresGameObject0, julienfEngine.P_CurrentScene, 0, true, false, 0, 132, 40, colliderSquare, true);
            //square2.ID = 3;

            //square3 = new Square(false, figuresGameObject0, julienfEngine.P_CurrentScene, 0, true, false, 0, 134, 42, colliderSquare, true);
            //square3.ID = 4;

            //gameObject1 = new Square(false, figures: figuresGameObject1, julienfEngine.P_CurrentScene, isUI: false, layer: 0, posX: 20, posY: 20);

            //julienfEngine.DrawConsole(square0);
            //julienfEngine.DrawConsole(square0);
            //julienfEngine.DrawConsole(square0);
            //julienfEngine.DrawConsole(square0);

        }

        public static void Update()
        {



            //IClickable[] currentMenu = buttonsMainMenu;
            //if (Input.GetKey(E_Keyboard.DownArrow) || Input.GetKey(E_Keyboard.S))
            //{
            //    if (!_keyDownPressed || _timerChangeArrowVelocity > _cooldownToKeyDownMovement)
            //    {
            //        _arrowMenu.MoveOneStepDown(currentMenu, ArrowMenu.E_PointSide.PointLeft, 3);

            //        if (_keyDownPressed) _timerChangeArrowVelocity = _cooldownToKeyPressedMovement;
            //        _keyDownPressed = true;
            //    }

            //    _timerChangeArrowVelocity += Timer.P_DeltaTime;
            //}
            //else if (Input.GetKey(E_Keyboard.UpArrow) || Input.GetKey(E_Keyboard.W))
            //{
            //    if (!_keyUpPressed || _timerChangeArrowVelocity > _cooldownToKeyDownMovement)
            //    {
            //        _arrowMenu.MoveOneStepUp(currentMenu, ArrowMenu.E_PointSide.PointLeft, 3);

            //        if (_keyUpPressed) _timerChangeArrowVelocity = _cooldownToKeyPressedMovement;
            //        _keyUpPressed = true;
            //    }

            //    _timerChangeArrowVelocity += Timer.P_DeltaTime;
            //}
            //else
            //{
            //    _keyUpPressed = false;
            //    _keyDownPressed = false;
            //    _timerChangeArrowVelocity = 0;
            //}



            //if (Input.GetKey(E_Keyboard.Enter)) currentMenu[_arrowMenu.P_CurrentSelectOptionDimension1].OnClick();






            /////////////////////////////////////////////////////////////////////////////////////////////////////








            //square0.Update();





            //if (Input.GetKeyUp(Keyboard.D)) gameObject0.MovePosition(gameObject0.P_PosX + 1, gameObject0.P_PosY + 1);

            ////////actualiando la figura del Time.deltaTime
            //deltaTimeGameObject1 = Timer.P_DeltaTime > 0.010 ? Timer.P_DeltaTime : deltaTimeGameObject1;
            //figuresGameObject1[0].P_Figure[0] = Convert.ToString(deltaTimeGameObject1);




            //if (Timer.P_Time > 600)
            //{
            //    julienfEngine.SetScene(firstScene, true);
            //}
            //else if (Timer.P_Time > 9)
            //{
            //    julienfEngine.SetScene(firstScene, true);
            //}
            //else if (Timer.P_Time > 8)
            //{
            //    julienfEngine.SetScene(secondScene, true);
            //}


            //julienfEngine.DrawConsole(gameObject0);
            //gameObject0.Draw();

            //gameObject0.MovePosition(20, 20);
            //gameObject0.MovePosition((int)Timer.P_Time * 14, (int)Timer.P_Time * 4);
            //julienfEngine.P_MainCamera.MovePosition(-(int)Timer.P_Time * 14, -(int)Timer.P_Time * 4);
        }

        #endregion

        #region GAME PROPERTIES

        #endregion
        //////////////////////////////////////////////////////////////////////////////////////////////





        #region INITIALIZE METHOD

        public static void Initialize()
        {
            // Step 1: Initialize all scenes you have created --> YourScene.Initialize();
            julienfEngine.InitializeScene(typeof(MainMenuScene));
            julienfEngine.InitializeScene(typeof(ExitMenuScene));
            julienfEngine.InitializeScene(typeof(SinglePlayerMenuScene));
            julienfEngine.InitializeScene(typeof(SinglePlayerGameScene));

            // Step 2: Load the start scenes and set the first scene for your game --> julienfEngine.LoadScene(typeof(YourFirstScene)); julienfEngine.SetLoadedScene(typeof(YourFirstScene));
            julienfEngine.LoadScene(typeof(MainMenuScene));
            julienfEngine.LoadScene(typeof(ExitMenuScene));
            julienfEngine.LoadScene(typeof(SinglePlayerMenuScene));
            julienfEngine.SetLoadedScene(typeof(MainMenuScene), false);
        }

        #endregion
    }
}