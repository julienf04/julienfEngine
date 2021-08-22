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

        private double cooldownToMoveArrow = 0.60;
        private double arrowVelocity = 0.40;
        private byte buttonDistance = 3;

        private E_Keyboard[] keysToMoveArrowToRight = new E_Keyboard[2]
        {
            E_Keyboard.DownArrow, E_Keyboard.S
        };

        private E_Keyboard[] keysToMoveArrowToLeft = new E_Keyboard[2]
       {
            E_Keyboard.UpArrow, E_Keyboard.W
       };
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
                 new SinglePlayerMenu(null, this, 0, true, true, 0, 35, 5),
                 new MultiplayerMenu(null, this, 0, true, true, 0, 35, 17),
                 new ExitMenu(null, this, 0, true, true, 0, 35, 30),
            };

            arrowMenu = new ArrowMenu(buttonsMainMenu, ArrowMenu.RO_FigureMenuArrow, julienfEngine.P_CurrentScene, (byte)ArrowMenu.E_ArrowSidesAndSizes.BigArrowPointLeft, true, true, 0, 108, 6);
        }

        public override void Update()
        {
            arrowMenu.MoveArrowToCurrentMenu(cooldownToMoveArrow, arrowVelocity, buttonDistance, ArrowMenu.E_PointSide.PointLeft, keysToMoveArrowToLeft, keysToMoveArrowToRight);

            if (Input.GetKeyDown(E_Keyboard.Enter) || Input.GetKeyDown(E_Keyboard.SpaceBar)) arrowMenu.DoClick();

            //IClickable[] currentMenu = buttonsMainMenu;
            //if (Input.GetKey(E_Keyboard.DownArrow) || Input.GetKey(E_Keyboard.S))
            //{
            //    if (!keyDownPressed || timerChangeArrowVelocity > cooldownToKeyDownMovement)
            //    {
            //        arrowMenu.MoveOneStepRight(currentMenu, ArrowMenu.E_PointSide.PointLeft, 3);

            //        if (keyDownPressed) timerChangeArrowVelocity = cooldownToKeyPressedMovement;
            //        keyDownPressed = true;
            //    }

            //    timerChangeArrowVelocity += Timer.P_DeltaTime;
            //}
            //else if (Input.GetKey(E_Keyboard.UpArrow) || Input.GetKey(E_Keyboard.W))
            //{
            //    if (!keyUpPressed || timerChangeArrowVelocity > cooldownToKeyDownMovement)
            //    {
            //        arrowMenu.MoveOneStepLeft(currentMenu, ArrowMenu.E_PointSide.PointLeft, 3);

            //        if (keyUpPressed) timerChangeArrowVelocity = cooldownToKeyPressedMovement;
            //        keyUpPressed = true;
            //    }

            //    timerChangeArrowVelocity += Timer.P_DeltaTime;
            //}
            //else
            //{
            //    keyUpPressed = false;
            //    keyDownPressed = false;
            //    timerChangeArrowVelocity = 0;
            //}



            //if (Input.GetKey(E_Keyboard.Enter) || Input.GetKey(E_Keyboard.SpaceBar)) currentMenu[arrowMenu.P_CurrentSelectOptionDimension1].OnClick();
        }

        #endregion
    }
}
