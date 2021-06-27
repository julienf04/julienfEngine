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





















            gameObject0 = new GameObject(figure0, true);
        }

        static void Update()
        {
            while (true)
            {
                julienfEngine.DrawConsole(gameObject0);

                gameObject0.MovePosition(205,20);

                julienfEngine.ResetValuesUpdate();
            }
        }
    }

    #endregion
}