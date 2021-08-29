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

        private double cooldownToMoveArrow = 0.55;
        private double arrowVelocity = 0.30;
        private byte buttonDistance = 2;
        private const ArrowMenu.E_PointSide _arrowPointSide = ArrowMenu.E_PointSide.PointDown;

        private double _timerChangeArrowVelocity = 0;

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

            arrowMenu = new ArrowMenu(buttonsExitMenu, ArrowMenu.RO_FigureMenuArrow, this, 7, true, true, 0, 122, 14);
            arrowMenu.P_CurrentSelectOption = 1;
        }

        public override void Update()
        {
            //arrowMenu.MoveArrowToCurrentMenu(cooldownToMoveArrow, arrowVelocity, buttonDistance, ArrowMenu.E_PointSide.PointDown, keysToMoveArrowToLeft, keysToMoveArrowToRight);

            if (Input.GetKey(E_Keyboard.RightArrow) || Input.GetKey(E_Keyboard.D))
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
            else if (Input.GetKey(E_Keyboard.LeftArrow) || Input.GetKey(E_Keyboard.A))
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

            if (Input.GetKeyDown(E_Keyboard.Enter) || Input.GetKeyDown(E_Keyboard.SpaceBar)) arrowMenu.DoClick();
        }

        #endregion
    }
}
