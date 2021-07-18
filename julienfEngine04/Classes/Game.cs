﻿using System;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Threading;

namespace julienfEngine1
{
    class Game
    {
        #region GAME ATRIBUTES;

        static Figure[] figures = new Figure[5];
        static GameObject gameObject0;

        static Timer generalTimer = new Timer();

        static Scene firstScene = julienfEngine.P_CurrentScene;
        static Scene secondScene = new Scene();

        #endregion

        #region MAIN METHOD;

        #endregion;

        #region SUPPORT METHODS;

        public static void Start()
        {
            for (int i = 0; i < figures.Length; i++)
            {
                figures[i] = new Figure();
            }
            figures[0].P_Figure = new string[11]
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
            figures[0].ForegroundColor = ForegroundColors.Red;

            figures[1].P_Figure = new string[11]
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
            figures[1].ForegroundColor = ForegroundColors.Blue;

            figures[2].P_Figure = new string[11]
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
            figures[2].ForegroundColor = ForegroundColors.White;

            figures[3].P_Figure = new string[11]
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
            figures[3].ForegroundColor = ForegroundColors.Green;

            figures[4].P_Figure = new string[11]
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
            figures[4].ForegroundColor = ForegroundColors.Yellow;





















            gameObject0 = new GameObject(figures, 0, true, false, 0, 0, 0);
            gameObject0.P_Animation.P_AnimationState = AnimationStates.RepeatReverse;
            gameObject0.P_Animation.RunAnimation();
        }

        public static void Update()
        {
            
            while (true)
            {
                if (Timer.P_Time > 600)
                {
                    julienfEngine.SetScene(firstScene, true);
                }
                else if (Timer.P_Time > 9)
                {
                    julienfEngine.SetScene(firstScene, true);
                }
                else if (Timer.P_Time > 8)
                {
                    julienfEngine.SetScene(secondScene, true);
                }


                //julienfEngine.DrawConsole(gameObject0);
                //gameObject0.Draw();

                //gameObject0.MovePosition(20, 20);
                //gameObject0.MovePosition((int)Timer.P_Time * 14, (int)Timer.P_Time * 4);
                //julienfEngine.P_MainCamera.MovePosition(-(int)Timer.P_Time * 14, -(int)Timer.P_Time * 4);

                julienfEngine.ResetValuesUpdate();
            }
        }
    }

    #endregion
}