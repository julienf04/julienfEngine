using System;
using julienfEngine1;

namespace julienfEngine1
{
    class Square : GameObject
    {
        private bool _allowInput;
        private double velocity = 10;
        public int ID = 0;

        public Square(bool allowInput, Figure[] figures, int baseFigure = 0, bool visible = true, bool isUI = false, int layer = 0, int posX = 0, int posY = 0,
                      Area collider = null, bool detectCollisions = false) : base(figures, baseFigure, visible, isUI, layer, posX, posY, collider, detectCollisions)
        {
            _allowInput = allowInput;
        }

        public void Start()
        {

        }

        public void Update()
        {
            if (_allowInput)
            {
                if (Input.GetKey(Keyboard.A)) this.P_PosX -= velocity * Timer.P_DeltaTime;
                if (Input.GetKey(Keyboard.D)) this.P_PosX += velocity * Timer.P_DeltaTime;
                if (Input.GetKey(Keyboard.W)) this.P_PosY -= velocity * Timer.P_DeltaTime;
                if (Input.GetKey(Keyboard.S)) this.P_PosY += velocity * Timer.P_DeltaTime;
            }
        }

        public override void OnCollisionEnter(GameObject[] gameObjects)
        {

        }

        public override void OnCollisionStay(GameObject[] gameObjects)
        {

        }

        public override void OnCollisionExit(GameObject[] gameObjects)
        {

        }
    }
}