using System;
using julienfEngine1;

namespace julienfEngine1
{
    class Square : GameObject
    {
        private bool _allowInput;
        private double velocity = 10;
        public int ID = 0;

        public Square(bool allowInput, Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
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
                if (Input.GetKey(E_Keyboard.A)) this.P_PosX -= velocity * Timer.P_DeltaTime;
                if (Input.GetKey(E_Keyboard.D)) this.P_PosX += velocity * Timer.P_DeltaTime;
                if (Input.GetKey(E_Keyboard.W)) this.P_PosY -= velocity * Timer.P_DeltaTime;
                if (Input.GetKey(E_Keyboard.S)) this.P_PosY += velocity * Timer.P_DeltaTime;
            }
        }
    }
}