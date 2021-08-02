using System;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Threading;
using System.Text;

namespace julienfEngine1
{
    class Game
    {
        #region GAME ATRIBUTES;

        static Figure[] figuresGameObject0 = new Figure[5];
        static Figure[] figuresGameObject1 = new Figure[1];
        static Area colliderSquare;
        static Square square0;
        static Square square1;
        static Square square2;
        static Square square3;
        static Square gameObject1;

        static Timer generalTimer = new Timer();

        static Scene firstScene = julienfEngine.P_CurrentScene;
        static Scene secondScene = new Scene();

        static double deltaTimeGameObject1 = 0;

        #endregion

        #region MAIN METHOD;

        #endregion;

        #region SUPPORT METHODS;

        public static void Start()
        {
            for (int i = 0; i < figuresGameObject0.Length; i++)
            {
                figuresGameObject0[i] = new Figure();
            }
            figuresGameObject0[0].P_Figure = new string[11]
            {
                "000000000000000000000000",
                "000000000000000000000000",
                "000000000000000000000000",
                "000000000000000000000000",
                "000000000000000000000000",
                "000000000000000000000000",
                "000000000000000000000000",
                "000000000000000000000000",
                "000000000000000000000000",
                "000000000000000000000000",
                "000000000000000000000000"
            };
            figuresGameObject0[0].ForegroundColor = ForegroundColors.Violet;

            figuresGameObject0[1].P_Figure = new string[11]
           {
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX",
                "XXXXXXXXXXXXXXXXXXXXXXXX"
           };
            figuresGameObject0[1].ForegroundColor = ForegroundColors.Blue;

            figuresGameObject0[2].P_Figure = new string[11]
           {
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS",
                "SSSSSSSSSSSSSSSSSSSSSSSS"
           };
            figuresGameObject0[2].ForegroundColor = ForegroundColors.White;

            figuresGameObject0[3].P_Figure = new string[11]
           {
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY",
                "YYYYYYYYYYYYYYYYYYYYYYYY"
           };
            figuresGameObject0[3].ForegroundColor = ForegroundColors.Green;

            figuresGameObject0[4].P_Figure = new string[11]
           {
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO",
                "OOOOOOOOOOOOOOOOOOOOOOOO"
           };
            figuresGameObject0[4].ForegroundColor = ForegroundColors.Yellow;


            for (int i = 0; i < figuresGameObject1.Length; i++)
            {
                figuresGameObject1[i] = new Figure();
            }
            figuresGameObject1[0].P_Figure = new string[1]
            {
                "DeltaTime: "
            };
















            colliderSquare = new Area(0, figuresGameObject0[0].P_Figure[0].Length, 0, figuresGameObject0[0].P_Figure.Length);
            square0 = new Square(true, figuresGameObject0, 0, true, false, 0, 0, 0, colliderSquare, true);
            square0.P_Animation.P_AnimationState = AnimationStates.RepeatReverse;
            square0.P_Animation.RunAnimation();
            square0.ID = 1;

            square1 = new Square(false, figuresGameObject0, 0, true, false, 0, 136, 36, colliderSquare, true);
            square1.ID = 2;

            square2 = new Square(false, figuresGameObject0, 0, true, false, 0, 132, 40, colliderSquare, true);
            square2.ID = 3;

            square3 = new Square(false, figuresGameObject0, 0, true, false, 0, 134, 42, colliderSquare, true);
            square3.ID = 4;

            gameObject1 = new Square(false, figures: figuresGameObject1, isUI: false, layer: 0, posX: 20, posY: 20);

            julienfEngine.DrawConsole(square0);
            julienfEngine.DrawConsole(square0);
            julienfEngine.DrawConsole(square0);
            julienfEngine.DrawConsole(square0);

        }

        public static void Update()
        {
            square0.Update();




            //if (Input.GetKeyUp(Keyboard.D)) gameObject0.MovePosition(gameObject0.P_PosX + 1, gameObject0.P_PosY + 1);

            deltaTimeGameObject1 = Timer.P_DeltaTime > 0.010 ? Timer.P_DeltaTime : deltaTimeGameObject1;
            figuresGameObject1[0].P_Figure[0] = Convert.ToString(deltaTimeGameObject1);




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
    }

    #endregion
}