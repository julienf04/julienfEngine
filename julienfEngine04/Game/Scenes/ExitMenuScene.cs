using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class ExitMenuScene : Scene
    {
        #region ATRIBUTES

        private IClickable[] buttonsExitMenu;

        private HorizontalLine horizontalLineUp;
        private HorizontalLine horizontalLineDown;
        private VerticalLine verticalLineLeft;
        private VerticalLine verticalLineRight;
        private TextMessage messageAreYouSureYouWantToCloseMe;
        private ArrowMenu arrowMenu;

        private double cooldownToMoveArrow = 0.60;
        private double arrowVelocity = 0.40;
        private byte buttonDistance = 2;

        private E_Keyboard[] keysToMoveArrowToRight = new E_Keyboard[2]
        {
            E_Keyboard.RightArrow, E_Keyboard.D
        };

        private E_Keyboard[] keysToMoveArrowToLeft = new E_Keyboard[2]
       {
            E_Keyboard.LeftArrow, E_Keyboard.A
       };

        #endregion

        #region CONSTRUCTORS

        public ExitMenuScene()
        {

        }

        #endregion

        #region METHODS

        #endregion

        #region GAME METHODS

        public override void Start()
        {
            horizontalLineUp = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Down, new Figure[1], this, 0, true, true, 0, 77, 9);
            horizontalLineDown = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Up, new Figure[1], this, 0, true, true, 0, 77, 24);
            verticalLineLeft = new VerticalLine(15, VerticalLine.E_CurveDirection.Right, new Figure[1], this, 0, true, true, 0, 76, 10);
            verticalLineRight = new VerticalLine(15, VerticalLine.E_CurveDirection.Left, new Figure[1], this, 0, true, true, 0, 137, 10);
            messageAreYouSureYouWantToCloseMe = new TextMessage("Are you sure you want to close me?", new Figure[1], this, 0, true, true, 0, 89, 12);

            buttonsExitMenu = new IClickable[2]
            {
                new Yes(null, this, 0, true, true, 0, 83, 16),
                new No(null, this, 0, true, false, 0, 117, 16)
            };

            arrowMenu = new ArrowMenu(buttonsExitMenu, ArrowMenu.RO_FigureMenuArrow, this, 7, true, true, 0, 92, 14);
        }

        public override void Update()
        {
            arrowMenu.MoveArrowToCurrentMenu(cooldownToMoveArrow, arrowVelocity, buttonDistance, ArrowMenu.E_PointSide.PointDown, keysToMoveArrowToLeft, keysToMoveArrowToRight);

            if (Input.GetKeyDown(E_Keyboard.Enter) || Input.GetKeyDown(E_Keyboard.SpaceBar)) arrowMenu.DoClick();
        }

        #endregion
    }
}
