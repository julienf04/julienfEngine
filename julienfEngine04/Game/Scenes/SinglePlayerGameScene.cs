using System;

namespace julienfEngine1
{
    class SinglePlayerGameScene : Scene
    {
        // Declare every attributes of this scene
        #region ATTRIBUTES

        private Spaceship spaceshipPlayer1;
        private Spaceship spaceshipPlayer2;

        private double velocity = 25;

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
            spaceshipPlayer1 = new Spaceship(null, this, 0, true, false, 0, 50, 25);
            spaceshipPlayer2 = new Spaceship(null, this, 0, true, false, 0, 100, 20);
        }

        // This runs every frame
        public override void Update()
        {
            if (Input.GetKey(E_Keyboard.W) || Input.GetKey(E_Keyboard.UpArrow)) spaceshipPlayer1.P_PosY -= velocity * Timer.P_DeltaTime;

            if (Input.GetKey(E_Keyboard.S) || Input.GetKey(E_Keyboard.DownArrow)) spaceshipPlayer1.P_PosY += velocity * Timer.P_DeltaTime;

            //if (Input.GetKey(E_Keyboard.W) || Input.GetKey(E_Keyboard.UpArrow))
            //    if (Input.GetKeyDown(E_Keyboard.W) || Input.GetKeyDown(E_Keyboard.UpArrow)) spaceshipPlayer1.MoveUp(1);
            //    else spaceshipPlayer1.MoveUp(velocity * Timer.P_DeltaTime);

            //if (Input.GetKey(E_Keyboard.S) || Input.GetKey(E_Keyboard.DownArrow))
            //    if (Input.GetKeyDown(E_Keyboard.S) || Input.GetKeyDown(E_Keyboard.DownArrow)) spaceshipPlayer1.MoveDown(1);
            //    else spaceshipPlayer1.MoveDown(velocity * Timer.P_DeltaTime);

            //if (Input.GetKey(E_Keyboard.D) || Input.GetKey(E_Keyboard.RightArrow))
            //    if (Input.GetKeyDown(E_Keyboard.D) || Input.GetKeyDown(E_Keyboard.RightArrow)) spaceshipPlayer1.MoveRight(1);
            //    else spaceshipPlayer1.MoveRight(velocity * Timer.P_DeltaTime);

            //if (Input.GetKey(E_Keyboard.A) || Input.GetKey(E_Keyboard.LeftArrow))
            //    if (Input.GetKeyDown(E_Keyboard.A) || Input.GetKeyDown(E_Keyboard.LeftArrow)) spaceshipPlayer1.MoveLeft(1);
            //    else spaceshipPlayer1.MoveLeft(velocity * Timer.P_DeltaTime);
        }

        #endregion
    }
}