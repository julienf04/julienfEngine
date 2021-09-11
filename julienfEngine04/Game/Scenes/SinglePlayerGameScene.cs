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

        private HorizontalLine horizontalLinePanel;
        private VerticalLine verticalLinePanel;


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
            spaceshipPlayer1 = new Spaceship(E_ForegroundColors.Green, null, 0, true, false, 0, _WALL_DISTANCE, Screen.P_Height / 2);
            spaceshipPlayer1.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.White;
            bulletsAvailibleUI = new bulletsAvailibleUI(spaceshipPlayer1, E_BackgroundColors.Green, null, 0, true, true, 0, _WALL_DISTANCE_UI, _CEILING_DISTANCE_UI);

            spaceshipPlayer2 = new Spaceship(E_ForegroundColors.White, null, 0, true, false, 0, Screen.P_Width - _WALL_DISTANCE, Screen.P_Height / 2);
            spaceshipPlayer2.P_PosX -= spaceshipPlayer2.P_GameObjectFigures[0].P_Figure[0].Length;
            spaceshipPlayer2.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.White;




            horizontalLinePanel = new HorizontalLine(50, null, 0, false, true, 0, 100, 40);
            //verticalLinePanel = new VerticalLine(20, )
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