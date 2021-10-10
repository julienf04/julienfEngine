using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class ExitMenuScene : Scene
    {
        #region ATRIBUTES

        private const double _YES_BUTTON_RELATIVE_POSX = 2.56;
        private const double _YES_AND_NO_BUTTONS_RELATIVE_POSY = 3.31;

        private const int _DISTANCE_BETWEEN_YES_AND_NO_POSX = 34;

        private const int _DISTANCE_BETWEEN_YES_AND_LEFT_LINE_POSX = 6;
        private const int _DISTANCE_BETWEEN_YES_AND_TOP_LINE_POSY = 7;
        private const int _DISTANCE_BETWEEN_YES_AND_DOWN_LINE_POSY = 8;
        private const int _DISTANCE_BETWEEN_YES_AND_RIGHT_LINE_POSX = 54;
        private const int _HORIZONTAL_LINES_LENGTH = 60;
        private const int _VERTICAL_LINES_LENGTH = 15;

        private const int _DISTANCE_BETWEEN_YES_AND_MESSAGE_POSX = 6;
        private const int _DISTANCE_BETWEEN_YES_AND_MESSAGE_POSY = 4;
        private const int _DISTANCE_BETWEEN_YES_AND_ARROW_POSX = 39;
        private const byte _DISTANCE_BETWEEN_YES_AND_ARROW_POSY = 2;

        private const double _COOLDOWN_TO_MOVE_ARROW = 0.55;
        private const double _ARROW_VELOCITY = 0.30;

        private const ArrowMenu.E_PointSide _ARROW_POINT_SIDE = ArrowMenu.E_PointSide.PointDown;


        private static IClickable[] _buttonsExitMenu;

        private static HorizontalLine _horizontalLineUp;
        private static HorizontalLine _horizontalLineDown;
        private static VerticalLine _verticalLineLeft;
        private static VerticalLine _verticalLineRight;
        private static TextMessage _messageAreYouSureYouWantToCloseMe;
        private static ArrowMenu _arrowMenu;

        private static double _timerChangeArrowVelocity = 0;

        #endregion


        #region METHODS

        #endregion

        #region GAME METHODS

        public override void Awake()
        {
            int yesPosX = (int)(Screen.P_Width / _YES_BUTTON_RELATIVE_POSX);
            int noPosX = yesPosX + _DISTANCE_BETWEEN_YES_AND_NO_POSX;
            int yesAndNoPosY = (int)(Screen.P_Height / _YES_AND_NO_BUTTONS_RELATIVE_POSY);
            Yes yes = new Yes(yesPosX, yesAndNoPosY, true, true, 0);
            No no = new No(noPosX, yesAndNoPosY, true, false, 0);

            _buttonsExitMenu = new IClickable[2]
           {
                yes,
                no
           };

            int linesLeftPosX = yesPosX - _DISTANCE_BETWEEN_YES_AND_LEFT_LINE_POSX;
            int linesTopPosY = yesAndNoPosY - _DISTANCE_BETWEEN_YES_AND_TOP_LINE_POSY;
            int horizontalLineDownPosY = yesAndNoPosY + _DISTANCE_BETWEEN_YES_AND_DOWN_LINE_POSY;
            int verticalLineRightPosX = yesPosX + _DISTANCE_BETWEEN_YES_AND_RIGHT_LINE_POSX;

            _horizontalLineUp = new HorizontalLine(_HORIZONTAL_LINES_LENGTH, HorizontalLine.E_CurveDirection.Down, linesLeftPosX, linesTopPosY, true, true, 0);
            _horizontalLineDown = new HorizontalLine(_HORIZONTAL_LINES_LENGTH, HorizontalLine.E_CurveDirection.Up, linesLeftPosX, horizontalLineDownPosY, true, true, 0);
            _verticalLineLeft = new VerticalLine(_VERTICAL_LINES_LENGTH, VerticalLine.E_CurveDirection.Right, linesLeftPosX - 1, linesTopPosY + 1, true, true, 0);
            _verticalLineRight = new VerticalLine(_VERTICAL_LINES_LENGTH, VerticalLine.E_CurveDirection.Left, verticalLineRightPosX, linesTopPosY + 1, true, true, 0);

            int messagePosX = yesPosX + _DISTANCE_BETWEEN_YES_AND_MESSAGE_POSX;
            int messagePosY = yesAndNoPosY - _DISTANCE_BETWEEN_YES_AND_MESSAGE_POSY;
            _messageAreYouSureYouWantToCloseMe = new TextMessage("Are you sure you want to close me?", messagePosX, messagePosY, true, true, 0);

            int arrowPosX = yesPosX + _DISTANCE_BETWEEN_YES_AND_ARROW_POSX;
            int arrowPosY = yesAndNoPosY - _DISTANCE_BETWEEN_YES_AND_ARROW_POSY;
            _arrowMenu = new ArrowMenu(_buttonsExitMenu, arrowPosX, arrowPosY, true, true, 0, ArrowMenu.RO_FigureMenuArrow, 7)
            {
                P_CurrentSelectOption = 1
            };
        }

        public override void Start()
        {
            _arrowMenu.SetArrowAt(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_YES_AND_ARROW_POSY, 1);
        }

        public override void Update()
        {
            //arrowMenu.MoveArrowToCurrentMenu(cooldownToMoveArrow, arrowVelocity, buttonDistance, ArrowMenu.E_PointSide.PointDown, keysToMoveArrowToLeft, keysToMoveArrowToRight);

            if (Input.GetKey(E_Keyboard.RightArrow) || Input.GetKey(E_Keyboard.D))
            {
                if (Input.GetKeyDown(Input.P_LastKeyPressed))
                {
                    _arrowMenu.MoveOneStepRight(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_YES_AND_ARROW_POSY);
                }
                else if (_timerChangeArrowVelocity > _COOLDOWN_TO_MOVE_ARROW)
                {
                    _arrowMenu.MoveOneStepRight(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_YES_AND_ARROW_POSY);
                    _timerChangeArrowVelocity = _ARROW_VELOCITY;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else if (Input.GetKey(E_Keyboard.LeftArrow) || Input.GetKey(E_Keyboard.A))
            {
                if (Input.GetKeyDown(Input.P_LastKeyPressed))
                {
                    _arrowMenu.MoveOneStepLeft(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_YES_AND_ARROW_POSY);
                }
                else if (_timerChangeArrowVelocity > _COOLDOWN_TO_MOVE_ARROW)
                {
                    _arrowMenu.MoveOneStepLeft(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_YES_AND_ARROW_POSY);
                    _timerChangeArrowVelocity = _ARROW_VELOCITY;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else _timerChangeArrowVelocity = 0;


            if (Input.GetKeyDown(E_Keyboard.Enter) || Input.GetKeyDown(E_Keyboard.SpaceBar)) _arrowMenu.DoClick();

            if (Input.GetKeyDown(E_Keyboard.Enter) || Input.GetKeyDown(E_Keyboard.SpaceBar)) _arrowMenu.DoClick();
        }

        #endregion
    }
}
