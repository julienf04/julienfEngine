using System;
using System.Collections.Generic;
using System.Linq;

namespace julienfEngine1
{
    class SinglePlayerGameScene : Scene
    {
        // Declare every attributes of this scene
        #region ATTRIBUTES

        private Spaceship spaceshipPlayer1;
        private Spaceship spaceshipPlayer2;

        private const byte _WALL_DISTANCE = 3;

        private const byte _CEILING_DISTANCE_UI = 2;
        private const byte _WALL_DISTANCE_UI = 30;

        private bulletsAvailibleUI bulletsAvailibleUI;

        private TextMessage pauseText;

        //private HorizontalLine horizontalLineUp;
        //private HorizontalLine horizontalLineDown;
        //private VerticalLine verticalLineLeft;
        //private VerticalLine verticalLineRight;

        private Winner winner;
        private Loser loser;
        private Pause pause;

        private TextMessage pressAnyKeyToContinue;

        #endregion

        // Initialize every attribute and create a game logic for this
        // 

        #region GAME METHODS

        // This runs when this scene is loaded
        public SinglePlayerGameScene()
        {

        }

        // This runs when this scene is setted
        public override void Start()
        {
            spaceshipPlayer1 = new Spaceship(E_ForegroundColors.Green, _WALL_DISTANCE, Screen.P_Height / 2, true);
            spaceshipPlayer1.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.White;
            bulletsAvailibleUI = new bulletsAvailibleUI(spaceshipPlayer1, E_BackgroundColors.Green, _WALL_DISTANCE_UI, _CEILING_DISTANCE_UI, true, true, 0);

            spaceshipPlayer2 = new Spaceship(E_ForegroundColors.White, Screen.P_Width - _WALL_DISTANCE, Screen.P_Height / 2, true);
            spaceshipPlayer2.P_PosX -= spaceshipPlayer2.P_GameObjectFigures[0].P_Figure[0].Length;
            spaceshipPlayer2.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.White;


            pauseText = new TextMessage("Pause: P", (int)bulletsAvailibleUI.P_PosX, (int)bulletsAvailibleUI.P_PosY - 1, true, true, 0);

            //horizontalLineUp = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Down, null, 0, true, true, 0, 77, 9);
            //horizontalLineDown = new HorizontalLine(60, HorizontalLine.E_CurveDirection.Up, null, 0, true, true, 0, 77, 24);
            //verticalLineLeft = new VerticalLine(15, VerticalLine.E_CurveDirection.Right, null, 0, true, true, 0, 76, 10);
            //verticalLineRight = new VerticalLine(15, VerticalLine.E_CurveDirection.Left, null, 0, true, true, 0, 137, 10);

            winner = new Winner(Screen.P_Width / 2, 10, true, true, 0);
            winner.P_PosX -= winner.P_GameObjectFigures[0].P_Figure[0].Length / 2;

            pressAnyKeyToContinue = new TextMessage("Press any key to continue", 3, 3, true, true, 0);
        }

        // This runs every frame
        public override void Update()
        {
            int oldPosY = (int)spaceshipPlayer1.P_PosY;

            if (Input.GetKey(E_Keyboard.W) || Input.GetKey(E_Keyboard.UpArrow)) spaceshipPlayer1.P_PosY -= spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            if (Input.GetKey(E_Keyboard.S) || Input.GetKey(E_Keyboard.DownArrow)) spaceshipPlayer1.P_PosY += spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            //if (Input.GetKey(E_Keyboard.D) || Input.GetKey(E_Keyboard.RightArrow)) spaceshipPlayer1.P_PosX += spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            //if (Input.GetKey(E_Keyboard.A) || Input.GetKey(E_Keyboard.LeftArrow)) spaceshipPlayer1.P_PosX -= spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            if (spaceshipPlayer1.P_PosY < spaceshipPlayer1.P_MinPosY || spaceshipPlayer1.P_PosY >= spaceshipPlayer1.P_MaxPosY) spaceshipPlayer1.P_PosY = oldPosY;

            if (Input.GetKey(E_Keyboard.D) || Input.GetKey(E_Keyboard.RightArrow) || Input.GetKey(E_Keyboard.SpaceBar))
                spaceshipPlayer1.Shoot();

            spaceshipPlayer1.MoveBulletsAttached();
            spaceshipPlayer1.RechargeBullets();
            bulletsAvailibleUI.UpdateBulletsUI();
        }

        #endregion
    }
}