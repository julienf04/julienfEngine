using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class MainMenuScene : Scene
    {
        #region ATRIBUTES

        private const int _BUTTONS_RELATIVE_POSX = 6;
        private const int _FIRST_BUTTON_RELATIVE_POSY = 10;
        private const int _DISTANCE_BETWEEN_BUTTONS_POSY = 13;

        private const double _COOLDOWN_TO_MOVE_ARROW = 0.55;
        private const double _ARROW_VELOCITY = 0.30;
        private const byte _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX = 10;

        private const ArrowMenu.E_PointSide _ARROW_POINT_SIDE = ArrowMenu.E_PointSide.PointLeft;


        private static ArrowMenu _arrowMenu;

        private static IClickable[] _buttonsMainMenu;

        private static double _timerChangeArrowVelocity = 0;

        #endregion

        #region METHODS

        #endregion

        #region GAME METHODS

        public override void Awake()
        {
            int allButtonsPosX = Screen.P_Width / _BUTTONS_RELATIVE_POSX;
            int singlePlayerMenuPosY = Screen.P_Height / _FIRST_BUTTON_RELATIVE_POSY;
            int multiplayerMenuPosY = singlePlayerMenuPosY + _DISTANCE_BETWEEN_BUTTONS_POSY;
            int exitMenuPosY = multiplayerMenuPosY + _DISTANCE_BETWEEN_BUTTONS_POSY;

            SinglePlayerMenu singlePlayerMenu = new SinglePlayerMenu(allButtonsPosX, singlePlayerMenuPosY, true, true, 0);
            MultiplayerMenu multiplayerMenu = new MultiplayerMenu(allButtonsPosX, multiplayerMenuPosY, true, true, 0);
            ExitMenu exitMenu = new ExitMenu(allButtonsPosX, exitMenuPosY, true, true, 0);
            _buttonsMainMenu = new IClickable[3]
            {
                singlePlayerMenu,
                multiplayerMenu,
                exitMenu
            };

            int arrowMenuPosX = allButtonsPosX + singlePlayerMenu.P_GameObjectFigures[0].P_Figure[0].Length + _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX;
            _arrowMenu = new ArrowMenu(_buttonsMainMenu, arrowMenuPosX, singlePlayerMenuPosY,
                true, true, 0, ArrowMenu.RO_FigureMenuArrow, (byte)ArrowMenu.E_ArrowSidesAndSizes.BigArrowPointLeft);
            _arrowMenu.P_PosY += (singlePlayerMenu.P_GameObjectFigures[0].P_Figure.Length / 2) - (_arrowMenu.P_GameObjectFigures[0].P_Figure.Length / 2);
            _arrowMenu.P_CurrentSelectOption = 0;
        }

        public override void Start()
        {
            _arrowMenu.SetArrowAt(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX, 0);
        }

        public override void Update()
        {
            //arrowMenu.MoveArrowToCurrentMenu(cooldownToMoveArrow, arrowVelocity, buttonDistance, ArrowMenu.E_PointSide.PointLeft, keysToMoveArrowToLeft, keysToMoveArrowToRight);

            if (Input.GetKey(E_Keyboard.DownArrow) || Input.GetKey(E_Keyboard.S))
            {
                if (Input.GetKeyDown(Input.P_LastKeyPressed))
                {
                    _arrowMenu.MoveOneStepRight(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX);
                }
                else if (_timerChangeArrowVelocity > _COOLDOWN_TO_MOVE_ARROW)
                {
                    _arrowMenu.MoveOneStepRight(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX);
                    _timerChangeArrowVelocity = _ARROW_VELOCITY;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else if (Input.GetKey(E_Keyboard.UpArrow) || Input.GetKey(E_Keyboard.W))
            {
                if (Input.GetKeyDown(Input.P_LastKeyPressed))
                {
                    _arrowMenu.MoveOneStepLeft(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX);
                }
                else if (_timerChangeArrowVelocity > _COOLDOWN_TO_MOVE_ARROW)
                {
                    _arrowMenu.MoveOneStepLeft(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX);
                    _timerChangeArrowVelocity = _ARROW_VELOCITY;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else _timerChangeArrowVelocity = 0;


            if (Input.GetKeyDown(E_Keyboard.Enter) || Input.GetKeyDown(E_Keyboard.SpaceBar)) _arrowMenu.DoClick();
        }

        #endregion
    }
}
