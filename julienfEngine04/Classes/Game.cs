using System;
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

        static Figure figure0 = new Figure();
        static GameObject gameObject0;

        static Timer generalTimer = new Timer();

        #endregion

        #region MAIN METHOD;

        static void Main(string[] args)
        {
            julienfEngine.Initialize();

            Start();

            Update();
        }

        #endregion;

        #region SUPPORT METHODS;

        static void Start()
        {
            figure0.P_Figure = new string[11]
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
            figure0.ForegroundColor = ForegroundColors.Red;





















            gameObject0 = new GameObject(null, true, false, 0, 0);
        }

        static void Update()
        {
            while (true)
            {
                julienfEngine.DrawConsole(gameObject0);

                //gameObject0.MovePosition(20, 20);
                //gameObject0.MovePosition((int)Timer.P_Time * 14, (int)Timer.P_Time * 4);
                //julienfEngine.P_MainCamera.MovePosition(-(int)Timer.P_Time * 14, -(int)Timer.P_Time * 4);

                julienfEngine.ResetValuesUpdate();
            }
        }
    }

    #endregion
}