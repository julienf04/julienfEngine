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
    class GameScene : Scene, IWinnable
    {
        // Declare every attributes of this scene
        #region ATTRIBUTES

        private const int _MAX_BULLETS_PLAYER1 = 10;
        private const float _TIME_TO_RECHARGE_PLAYER1 = 0.5f;

        private const int _MAX_BULLETS_PLAYER2 = 10;
        private const float _TIME_TO_RECHARGE_PLAYER2 = 0.5f;

        private Spaceship _spaceshipPlayer1;
        private Spaceship _spaceshipPlayer2;
        private SpaceshipAI _spaceshipAI;

        private const byte _WALL_DISTANCE = 3;

        private const byte _CEILING_DISTANCE_UI = 2;
        private const byte _WALL_DISTANCE_UI = 30;

        private bulletsAvailibleUI _bulletsAvailibleUIPlayer1;
        private bulletsAvailibleUI _bulletsAvailibleUIPlayer2;

        private TextMessage _pauseText;

        private Winner _winner;
        private Loser _loser;
        private Pause _pause;

        private TextMessage _pressToContinue;
        private const byte _TEXT_ANY_KEY_FIXED_PAUSE_POSX = 8;

        private bool _onPause = false;
        private bool _gameOver = false;

        private readonly Timer _timerToAllowAnyKeyPressed = new Timer();
        private const double _TIME_TO_ALLOW_ANY_KEY_PRESSED = 1;

        private E_GameType _gameType;
        private bool _shoot = false;
        private bool _myGameDataChanged = false;
        private Server<GameData> _remotePlayer2;
        private Client<GameData> _myRemotePlayer;
        private Task<string> _taskRemotePlayer2;

        #endregion

        #region ENUMS

        private enum E_GameOverStates : byte
        {
            Winner,
            Loser
        }
        
        public enum E_GameType
        {
            SinglePlayerEasy,
            SinglePlayerNormal,
            SinglePlayerHard,
            SinglePlayer,
            MultiplayerOffline,
            MultiplayerOnline
        }

        #endregion

        // Initialize every attribute and create a game logic for this scene
        #region GAME METHODS

        public GameScene(E_GameType gameType)
        {
            _gameType = gameType;
        }

        // This runs when this scene is loaded
        public override void Awake()
        {
            _spaceshipPlayer1 = new Spaceship(E_ForegroundColors.Green, _MAX_BULLETS_PLAYER1, _TIME_TO_RECHARGE_PLAYER1 ,_WALL_DISTANCE, Screen.P_Height / 2, true);
            _spaceshipPlayer1.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.White;
            _bulletsAvailibleUIPlayer1 = new bulletsAvailibleUI(_spaceshipPlayer1, E_BackgroundColors.Green, _WALL_DISTANCE_UI, _CEILING_DISTANCE_UI, true, true, 0);

            _spaceshipPlayer2 = new Spaceship(E_ForegroundColors.Red, _MAX_BULLETS_PLAYER2, _TIME_TO_RECHARGE_PLAYER2, Screen.P_Width - _WALL_DISTANCE, Screen.P_Height / 2, true);
            _spaceshipPlayer2.P_PosX -= _spaceshipPlayer2.P_GameObjectFigures[0].P_Figure[0].Length;
            _spaceshipPlayer2.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.White;

            switch (_gameType)
            {
                case E_GameType.SinglePlayerEasy:
                    _spaceshipAI = new SpaceshipAIEasy(_spaceshipPlayer2, _spaceshipPlayer1.P_Bullets);
                    _gameType = E_GameType.SinglePlayer;
                    break;
                case E_GameType.SinglePlayerNormal:
                    _spaceshipAI = new SpaceshipAINormal(_spaceshipPlayer2, _spaceshipPlayer1.P_Bullets);
                    _gameType = E_GameType.SinglePlayer;
                    break;
                case E_GameType.SinglePlayerHard:
                    _spaceshipAI = new SpaceshipAIHard(_spaceshipPlayer2, _spaceshipPlayer1.P_Bullets);
                    _gameType = E_GameType.SinglePlayer;
                    break;
                case E_GameType.MultiplayerOffline:
                    _bulletsAvailibleUIPlayer2 = new bulletsAvailibleUI(_spaceshipPlayer2, E_BackgroundColors.Red, Screen.P_Width - _WALL_DISTANCE_UI - (_spaceshipPlayer2.P_MaxBullets * bulletsAvailibleUI.BULLET_DISTANCE), _CEILING_DISTANCE_UI, true, true, 0);
                    break;
                case E_GameType.MultiplayerOnline:

                    break;
            }


            _pauseText = new TextMessage("Pause: P", (int)_bulletsAvailibleUIPlayer1.P_PosX, (int)_bulletsAvailibleUIPlayer1.P_PosY - 1, true, true, 0);

            //horizontalLineUp = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Down, null, 0, true, true, 0, 77, 9);
            //horizontalLineDown = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Up, null, 0, true, true, 0, 77, 24);
            //verticalLineLeft = new VerticalLine(15, VerticalLine.E_CurveDirection.Right, null, 0, true, true, 0, 76, 10);
            //verticalLineRight = new VerticalLine(15, VerticalLine.E_CurveDirection.Left, null, 0, true, true, 0, 137, 10);

            _loser = new Loser(Screen.P_Width / 2, Screen.P_Height / 5, false, true, 0);
            _loser.P_PosX -= _loser.P_GameObjectFigures[0].P_Figure[0].Length / 2;

            _pause = new Pause(Screen.P_Width / 2, Screen.P_Height / 5, false, true, 0);
            _pause.P_PosX -= _pause.P_GameObjectFigures[0].P_Figure[0].Length / 2;

            _pressToContinue = new TextMessage("Press the P key to continue",
                (int)_pause.P_PosX + _TEXT_ANY_KEY_FIXED_PAUSE_POSX, (int)_pause.P_PosY + 10,
                false, true, 0);
        }

        // This runs when this scene is setted
        public override void Start()
        {
            int posY = Screen.P_Height / 2;

            _spaceshipPlayer1.P_PosY = posY;
            _spaceshipPlayer1.SetBullets(_spaceshipPlayer1.P_MaxBullets);
            _bulletsAvailibleUIPlayer1.UpdateBulletsUI();

            _spaceshipPlayer2.P_PosY = posY;
            _spaceshipPlayer2.SetBullets(_spaceshipPlayer2.P_MaxBullets);
        }

        // This runs every frame
        public override void Update()
        {
            void Pause(bool isPause)
            {
                _spaceshipPlayer1.OnPause(isPause);
                _spaceshipPlayer2.OnPause(isPause);

                _pause.P_Visible = isPause;
                _pressToContinue.P_Visible = isPause;

                _onPause = isPause;
            }

            if (_gameOver)
            {
                if (_timerToAllowAnyKeyPressed.P_MyTimer > _TIME_TO_ALLOW_ANY_KEY_PRESSED && Input.AnyKeyPressed())
                {
                    _timerToAllowAnyKeyPressed.ResetMyTimer();
                    Scene.UnloadScene(typeof(GameScene));
                    Scene.LoadScene(typeof(MainMenuScene));
                    Scene.LoadScene(typeof(ExitMenuScene));
                    Scene.LoadScene(typeof(SinglePlayerMenuScene));
                    Scene.LoadScene(typeof(MultiplayerMenuScene));
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


                switch (_gameType)
                {
                    case E_GameType.SinglePlayer:
                        SinglePlayer();
                        break;
                    case E_GameType.MultiplayerOffline:
                        MultiplayerOffline();
                        break;
                    case E_GameType.MultiplayerOnline:
                        MultiplayerOnline();
                        break;
                }

                _spaceshipPlayer1.MoveBulletsAttached();
                _spaceshipPlayer1.RechargeBullets();
                _bulletsAvailibleUIPlayer1.UpdateBulletsUI();
                _myGameDataChanged = false;
                _shoot = false;

                return;
            }

            if (Input.GetKeyDown(E_Keyboard.P)) Pause(false);
        }

        public void GameOver(E_PlayerID playerID)
        {
            bool gameMultiplayerOffline = _gameType == E_GameType.MultiplayerOffline;

            switch (playerID)
            {
                case E_PlayerID.Player1:
                    if (gameMultiplayerOffline)
                    {
                        _winner = new Winner(Winner.E_WinnerTypes.WinnerP2, Screen.P_Width / 2, Screen.P_Height / 5, true, true, 0);
                        _winner.P_PosX -= _winner.P_GameObjectFigures[0].P_Figure[0].Length / 2;
                    }
                    else _loser.P_Visible = true;
                    break;

                case E_PlayerID.Player2:
                    _winner = new Winner(gameMultiplayerOffline ? Winner.E_WinnerTypes.WinnerP1 : Winner.E_WinnerTypes.Winner,
                        Screen.P_Width / 2, Screen.P_Height / 5, true, true, 0);
                    _winner.P_PosX -= _winner.P_GameObjectFigures[0].P_Figure[0].Length / 2;
                    break;
            }

            _pressToContinue.P_Message = "Press any key to continue";
            _pressToContinue.P_Visible = true;
            _timerToAllowAnyKeyPressed.StartMyTimer(0);
            _gameOver = true;
        }

        private void RunPlayer1FullKeys()
        {
            int spaceship1OldPosY = (int)_spaceshipPlayer1.P_PosY;

            if (Input.GetKey(E_Keyboard.W) || Input.GetKey(E_Keyboard.UpArrow))
            {
                _spaceshipPlayer1.P_PosY -= _spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;
                _myGameDataChanged = true;
            }

            if (Input.GetKey(E_Keyboard.S) || Input.GetKey(E_Keyboard.DownArrow))
            {
                _spaceshipPlayer1.P_PosY += _spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;
                _myGameDataChanged = true;
            }

            if (_spaceshipPlayer1.P_PosY < _spaceshipPlayer1.P_MinPosY || _spaceshipPlayer1.P_PosY >= _spaceshipPlayer1.P_MaxPosY)
                _spaceshipPlayer1.P_PosY = spaceship1OldPosY;

            if (Input.GetKey(E_Keyboard.D) || Input.GetKey(E_Keyboard.RightArrow) || Input.GetKey(E_Keyboard.SpaceBar))
            {
                _spaceshipPlayer1.Shoot();
                _myGameDataChanged = true;
                _shoot = true;
            }

        }

            private void RunPlayer1LimitedKeys()
        {
            int spaceship1OldPosY = (int)_spaceshipPlayer1.P_PosY;

            if (Input.GetKey(E_Keyboard.W))
                _spaceshipPlayer1.P_PosY -= _spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            if (Input.GetKey(E_Keyboard.S)) _spaceshipPlayer1.P_PosY += _spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            if (_spaceshipPlayer1.P_PosY < _spaceshipPlayer1.P_MinPosY || _spaceshipPlayer1.P_PosY >= _spaceshipPlayer1.P_MaxPosY) _spaceshipPlayer1.P_PosY = spaceship1OldPosY;

            if (Input.GetKey(E_Keyboard.D)) _spaceshipPlayer1.Shoot();
        }

        private void SinglePlayer()
        {
            RunPlayer1FullKeys();

            int spaceship2OldPosY = (int)_spaceshipPlayer2.P_PosY;

            if (_spaceshipPlayer2.P_IsAlive) _spaceshipAI.Run();

            if (_spaceshipPlayer2.P_PosY < _spaceshipPlayer2.P_MinPosY || _spaceshipPlayer2.P_PosY >= _spaceshipPlayer2.P_MaxPosY) _spaceshipPlayer2.P_PosY = spaceship2OldPosY;
        }

        private void MultiplayerOffline()
        {
            RunPlayer1LimitedKeys();

            int spaceship1OldPosY = (int)_spaceshipPlayer2.P_PosY;

            if (Input.GetKey(E_Keyboard.UpArrow))
                _spaceshipPlayer2.P_PosY -= _spaceshipPlayer2.P_Velocity * Timer.P_DeltaTime;

            if (Input.GetKey(E_Keyboard.DownArrow)) _spaceshipPlayer2.P_PosY += _spaceshipPlayer2.P_Velocity * Timer.P_DeltaTime;

            if (_spaceshipPlayer2.P_PosY < _spaceshipPlayer2.P_MinPosY || _spaceshipPlayer2.P_PosY >= _spaceshipPlayer2.P_MaxPosY) _spaceshipPlayer2.P_PosY = spaceship1OldPosY;

            if (Input.GetKey(E_Keyboard.LeftArrow)) _spaceshipPlayer2.Shoot();

            _spaceshipPlayer2.MoveBulletsAttached();
            _spaceshipPlayer2.RechargeBullets();
            _bulletsAvailibleUIPlayer2.UpdateBulletsUI();
        }

        private void MultiplayerOnline()
        {
            RunPlayer1FullKeys();

            if (_myGameDataChanged)
            {
                GameData myData = new GameData()
                {
                    P_PosY = (int)_spaceshipPlayer1.P_PosY,
                    P_Shoot = _shoot
                };
                string sendData = GameData.SerializeData(myData);
                Task.Run(()=> _myRemotePlayer.SendInfo(sendData));
                _myRemotePlayer.SendInfoAsync(sendData);
            }
            

            if (_taskRemotePlayer2.IsCompleted)
            {
                string remotePlayer2DataString = _taskRemotePlayer2.Result;
                GameData remotePlayer2Data = GameData.DeserializeData(remotePlayer2DataString);

                _spaceshipPlayer2.P_PosY = remotePlayer2Data.P_PosY;

                if (remotePlayer2Data.P_Shoot) _spaceshipPlayer2.Shoot();

                _taskRemotePlayer2 = _remotePlayer2.ReceiveInfoAsync();
            }
            
        }

        #endregion
    }
}