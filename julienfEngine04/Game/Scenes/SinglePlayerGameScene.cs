using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks.Sources;

namespace julienfEngine1
{
    class SinglePlayerGameScene : Scene, IWinnable
    {
        // Declare every attributes of this scene
        #region ATTRIBUTES

        private Spaceship _spaceshipPlayer1;
        private Spaceship _spaceshipPlayer2;

        private const byte _WALL_DISTANCE = 3;

        private const byte _CEILING_DISTANCE_UI = 2;
        private const byte _WALL_DISTANCE_UI = 30;

        private bulletsAvailibleUI _bulletsAvailibleUI;

        private TextMessage _pauseText;

        private Winner _winner;
        private Loser _loser;
        private Pause _pause;

        private TextMessage _pressAnyKeyToContinue;
        private const byte _TEXT_ANY_KEY_FIXED_WINEER_POSX = 8;
        private const byte _TEXT_ANY_KEY_FIXED_LOSER_POSX = 5;
        private const byte _TEXT_ANY_KEY_FIXED_PAUSE_POSX = 8;

        private bool _onPause = false;
        private bool _gameOver = false;

        private Timer _timerToAllowAnyKeyPressed = new Timer();
        private const double _TIME_TO_ALLOW_ANY_KEY_PRESSED = 1;

        #endregion

        #region ENUMS

        private enum E_GameOverStates : byte
        {
            Winner,
            Loser
        }

        #endregion

        // Initialize every attribute and create a game logic for this scene
        #region GAME METHODS

        // This runs when this scene is loaded
        public SinglePlayerGameScene()
        {

        }

        // This runs when this scene is setted
        public override void Start()
        {
            _spaceshipPlayer1 = new Spaceship(E_ForegroundColors.Green, _WALL_DISTANCE, Screen.P_Height / 2, true);
            _spaceshipPlayer1.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.White;
            _bulletsAvailibleUI = new bulletsAvailibleUI(_spaceshipPlayer1, E_BackgroundColors.Green, _WALL_DISTANCE_UI, _CEILING_DISTANCE_UI, true, true, 0);

            _spaceshipPlayer2 = new Spaceship(E_ForegroundColors.Red, Screen.P_Width - _WALL_DISTANCE, Screen.P_Height / 2, true);
            _spaceshipPlayer2.P_PosX -= _spaceshipPlayer2.P_GameObjectFigures[0].P_Figure[0].Length;
            _spaceshipPlayer2.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.White;


            _pauseText = new TextMessage("Pause: P", (int)_bulletsAvailibleUI.P_PosX, (int)_bulletsAvailibleUI.P_PosY - 1, true, true, 0);

            //horizontalLineUp = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Down, null, 0, true, true, 0, 77, 9);
            //horizontalLineDown = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Up, null, 0, true, true, 0, 77, 24);
            //verticalLineLeft = new VerticalLine(15, VerticalLine.E_CurveDirection.Right, null, 0, true, true, 0, 76, 10);
            //verticalLineRight = new VerticalLine(15, VerticalLine.E_CurveDirection.Left, null, 0, true, true, 0, 137, 10);

            _winner = new Winner(Screen.P_Width / 2, Screen.P_Height / 5, false, true, 0);
            _winner.P_PosX -= _winner.P_GameObjectFigures[0].P_Figure[0].Length / 2;

            _loser = new Loser(Screen.P_Width / 2, Screen.P_Height / 5, false, true, 0);
            _loser.P_PosX -= _loser.P_GameObjectFigures[0].P_Figure[0].Length / 2;

            _pause = new Pause(Screen.P_Width / 2, Screen.P_Height / 5, false, true, 0);
            _pause.P_PosX -= _pause.P_GameObjectFigures[0].P_Figure[0].Length / 2;

            _pressAnyKeyToContinue = new TextMessage("Press any key to continue",
                (int)_pause.P_PosX + _TEXT_ANY_KEY_FIXED_PAUSE_POSX, (int)_pause.P_PosY + 10,
                false, true, 0);
        }

        // This runs every frame
        public override void Update()
        {
            void Pause(bool isPause)
            {
                _spaceshipPlayer1.OnPause(isPause);
                _spaceshipPlayer2.OnPause(isPause);

                _pause.P_Visible = isPause;
                _pressAnyKeyToContinue.P_Visible = isPause;

                _onPause = isPause;
            }

            if (_gameOver)
            {
                if (_timerToAllowAnyKeyPressed.P_MyTimer > _TIME_TO_ALLOW_ANY_KEY_PRESSED && Input.AnyKeyPressed())
                {
                    _timerToAllowAnyKeyPressed.ResetMyTimer();
                    Scene.UnloadScene(typeof(SinglePlayerGameScene));
                    Scene.LoadScene(typeof(MainMenuScene));
                    Scene.LoadScene(typeof(ExitMenuScene));
                    Scene.LoadScene(typeof(SinglePlayerMenuScene));
                    Scene.SetLoadedScene(typeof(MainMenuScene), true);

                    return;
                }
            }

            if (!_onPause)
            {
                if (Input.GetKeyDown(E_Keyboard.P))
                {
                    Pause(true);
                    return;
                }

                int spaceship1_OldPosY = (int)_spaceshipPlayer1.P_PosY;
                int spaceship2_OldPosY = (int)_spaceshipPlayer2.P_PosY;

                if (Input.GetKey(E_Keyboard.W) || Input.GetKey(E_Keyboard.UpArrow)) _spaceshipPlayer1.P_PosY -= _spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

                if (Input.GetKey(E_Keyboard.S) || Input.GetKey(E_Keyboard.DownArrow)) _spaceshipPlayer1.P_PosY += _spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;


                if (_spaceshipPlayer1.P_PosY < _spaceshipPlayer1.P_MinPosY || _spaceshipPlayer1.P_PosY >= _spaceshipPlayer1.P_MaxPosY) _spaceshipPlayer1.P_PosY = spaceship1_OldPosY;

                if (Input.GetKey(E_Keyboard.D) || Input.GetKey(E_Keyboard.RightArrow) || Input.GetKey(E_Keyboard.SpaceBar)) _spaceshipPlayer1.Shoot();



                _spaceshipPlayer1.MoveBulletsAttached();
                _spaceshipPlayer1.RechargeBullets();
                _bulletsAvailibleUI.UpdateBulletsUI();

                return;
            }

            if (Input.GetKeyDown(E_Keyboard.P)) Pause(false);
        }

        public void GameOver(Spaceship.E_PlayerID playerID)
        {
            switch (playerID)
            {
                case Spaceship.E_PlayerID.Player1:
                    _loser.P_Visible = true;
                    break;

                case Spaceship.E_PlayerID.Player2:
                    _winner.P_Visible = true;
                    break;
            }

            _pressAnyKeyToContinue.P_Visible = true;
            _timerToAllowAnyKeyPressed.StartMyTimer(0);
            _gameOver = true;
        }

        #endregion
    }
}