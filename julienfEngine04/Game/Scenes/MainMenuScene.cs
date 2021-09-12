using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class MainMenuScene : Scene
    {
        #region ATRIBUTES

        private IClickable[] buttonsMainMenu;


        private static ArrowMenu arrowMenu;

        private const double cooldownToMoveArrow = 0.55;
        private const double arrowVelocity = 0.30;
        private const byte buttonDistance = 3;
        private const ArrowMenu.E_PointSide _arrowPointSide = ArrowMenu.E_PointSide.PointLeft;

        private double _timerChangeArrowVelocity = 0;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        #endregion

        #region GAME METHODS

        public override void Start()
        {
            buttonsMainMenu = new IClickable[3]
            {
                new SinglePlayerMenu(35, 5, true, true, 0),
                new MultiplayerMenu(35, 17, true, true, 0),
                new ExitMenu(35, 30, true, true, 0),
            };

            arrowMenu = new ArrowMenu(buttonsMainMenu, 108, 6, true, true, 0, ArrowMenu.RO_FigureMenuArrow, (byte)ArrowMenu.E_ArrowSidesAndSizes.BigArrowPointLeft);
            arrowMenu.P_CurrentSelectOption = 0;
        }

        public override void Update()
        {
            //arrowMenu.MoveArrowToCurrentMenu(cooldownToMoveArrow, arrowVelocity, buttonDistance, ArrowMenu.E_PointSide.PointLeft, keysToMoveArrowToLeft, keysToMoveArrowToRight);

            if (Input.GetKey(E_Keyboard.DownArrow) || Input.GetKey(E_Keyboard.S))
            {
                if (Input.GetKeyDown(Input.P_LastKeyPressed))
                {
                    arrowMenu.MoveOneStepRight(_arrowPointSide, buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMoveArrow)
                {
                    arrowMenu.MoveOneStepRight(_arrowPointSide, buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else if (Input.GetKey(E_Keyboard.UpArrow) || Input.GetKey(E_Keyboard.W))
            {
                if (Input.GetKeyDown(Input.P_LastKeyPressed))
                {
                    arrowMenu.MoveOneStepLeft(_arrowPointSide, buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMoveArrow)
                {
                    arrowMenu.MoveOneStepLeft(_arrowPointSide, buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else _timerChangeArrowVelocity = 0;


            if (Input.GetKeyDown(E_Keyboard.Enter) || Input.GetKeyDown(E_Keyboard.SpaceBar)) arrowMenu.DoClick();
        }

        #endregion
    }
}
