using System;

namespace julienfEngine1
{
    class MultiplayerMenuScene : Scene
    {
        // Declare every attributes of this scene
        #region ATTRIBUTES

        private const double _BUTTONS_RELATIVE_POSX = 6;
        private const double _FIRST_BUTTON_RELATIVE_POSY = 10;
        private const int _DISTANCE_BETWEEN_BUTTONS_POSY = 13;

        private const double _COOLDOWN_TO_MOVE_ARROW = 0.55;
        private const double _ARROW_VELOCITY = 0.30;
        private const byte _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX = 10;
        private const ArrowMenu.E_PointSide _ARROW_POINT_SIDE = ArrowMenu.E_PointSide.PointLeft;

        private const byte _DISTANCE_BETWEEN_BUTTONS_AND_TUTORIALS_POSX = 110;
        private const byte _DISTANCE_BETWEEN_FIRST_BUTTON_AND_FIRST_TUTORIAL_POSY = 10;
        private const byte _DISTANCE_BETWEEN_TUTORIALS = 5;


        private static ArrowMenu _arrowMenu;

        private static IClickable[] _buttonsMainMenu;

        private static double _timerChangeArrowVelocity = 0;

        private static TextMessage _tutorialControls;
        private static TextMessage _tutorialDefeatYourOpponent;
        private static TextMessage _tutorialMovement;
        private static TextMessage _tutorialShoot;
        private static TextMessage _tutorialAvailableBullets;

        #endregion

        // Initialize every attribute and create a game logic for this scene
        #region GAME METHODS

        // This runs when this scene is loaded
        public override void Awake()
        {
            int buttonsPosX = (int)(Screen.P_Width / _BUTTONS_RELATIVE_POSX);
            int multiplayerOfflinePosY = (int)(Screen.P_Height / _FIRST_BUTTON_RELATIVE_POSY);
            int multiplayerOnlinePosY = multiplayerOfflinePosY + _DISTANCE_BETWEEN_BUTTONS_POSY;
            int backPosY = multiplayerOnlinePosY + _DISTANCE_BETWEEN_BUTTONS_POSY;

            MultiplayerOffline multiplayerOffline = new MultiplayerOffline(buttonsPosX, multiplayerOfflinePosY, true, true, 0);
            MultiplayerOnline multiplayerOnline = new MultiplayerOnline(buttonsPosX, multiplayerOnlinePosY, true, true, 0);
            Back back = new Back(buttonsPosX - 2, backPosY, true, true, 0); // -2 becouse this font is not the same, so it looks more aligned

            _buttonsMainMenu = new IClickable[3]
            {
                multiplayerOffline,
                multiplayerOnline,
                back
            };


            int arrowMenuPosX = buttonsPosX + multiplayerOffline.P_GameObjectFigures[0].P_Figure[0].Length + _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX;

            _arrowMenu = new ArrowMenu(_buttonsMainMenu, arrowMenuPosX, multiplayerOfflinePosY, true, true, 0, ArrowMenu.RO_FigureMenuArrow, (byte)ArrowMenu.E_ArrowSidesAndSizes.BigArrowPointLeft);
            _arrowMenu.P_PosY += (multiplayerOffline.P_GameObjectFigures[0].P_Figure.Length / 2) - (_arrowMenu.P_GameObjectFigures[0].P_Figure.Length / 2);
            _arrowMenu.P_CurrentSelectOption = 0;

            int tutorialsPosX = buttonsPosX + _DISTANCE_BETWEEN_BUTTONS_AND_TUTORIALS_POSX;
            int tutorialControlsPosY = multiplayerOfflinePosY + _DISTANCE_BETWEEN_FIRST_BUTTON_AND_FIRST_TUTORIAL_POSY;
            int tutorialDefeatYourOpponentPosY = tutorialControlsPosY + _DISTANCE_BETWEEN_TUTORIALS;
            int tutorialMovementPosY = tutorialDefeatYourOpponentPosY + _DISTANCE_BETWEEN_TUTORIALS;
            int tutorialShootPosY = tutorialMovementPosY + _DISTANCE_BETWEEN_TUTORIALS;
            int tutorialAvailableBulletsPosY = tutorialShootPosY + _DISTANCE_BETWEEN_TUTORIALS;

            _tutorialControls = new TextMessage("--- Controls:", tutorialsPosX, tutorialControlsPosY, true, true, 0);
            _tutorialDefeatYourOpponent = new TextMessage("Defeat your opponent", tutorialsPosX, tutorialDefeatYourOpponentPosY, true, true, 0);
            _tutorialMovement = new TextMessage("Move with:  { W - S }  or  { Up arrow - Down arrow }", tutorialsPosX, tutorialMovementPosY, true, true, 0);
            _tutorialShoot = new TextMessage("Shoot with:  { D }  or  { Right arrow }", tutorialsPosX, tutorialShootPosY, true, true, 0);
            _tutorialAvailableBullets = new TextMessage("Your available bullets are shown up", tutorialsPosX, tutorialAvailableBulletsPosY, true, true, 0);
        }

        // This runs when this scene is setted
        public override void Start()
        {
            _arrowMenu.SetArrowAt(_ARROW_POINT_SIDE, _DISTANCE_BETWEEN_BUTTONS_AND_ARROW_POSX, 0);
        }

        // This runs every frame
        public override void Update()
        {
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