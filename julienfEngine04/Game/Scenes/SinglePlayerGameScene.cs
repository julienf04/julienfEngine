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
            spaceshipPlayer1 = new Spaceship(null, this, 0, true, false, 0, 50, 25);
            spaceshipPlayer2 = new Spaceship(null, this, 0, true, false, 0, 100, 20);
        }

        // This runs every frame
        public override void Update()
        {
            int oldPosY = (int)spaceshipPlayer1.P_PosY;

            if (Input.GetKey(E_Keyboard.W) || Input.GetKey(E_Keyboard.UpArrow)) spaceshipPlayer1.P_PosY -= spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            if (Input.GetKey(E_Keyboard.S) || Input.GetKey(E_Keyboard.DownArrow)) spaceshipPlayer1.P_PosY += spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            if (Input.GetKey(E_Keyboard.D) || Input.GetKey(E_Keyboard.RightArrow)) spaceshipPlayer1.P_PosX += spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            if (Input.GetKey(E_Keyboard.A) || Input.GetKey(E_Keyboard.LeftArrow)) spaceshipPlayer1.P_PosX -= spaceshipPlayer1.P_Velocity * Timer.P_DeltaTime;

            if (spaceshipPlayer1.P_PosY < spaceshipPlayer1.P_MinPosY || spaceshipPlayer1.P_PosY >= spaceshipPlayer1.P_MaxPosY) spaceshipPlayer1.P_PosY = oldPosY;

            if (Input.GetKey(E_Keyboard.D) || Input.GetKey(E_Keyboard.LeftArrow))
                spaceshipPlayer1.InstantiateBullet();

            spaceshipPlayer1.MoveBulletsAttached();

            //Scene.P_CurrentScene.P_MainCamera.P_PosX -= velocity * Timer.P_DeltaTime * 0.5;
        }

        #endregion
    }
}