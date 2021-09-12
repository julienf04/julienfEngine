using System;
using julienfEngine1;

namespace julienfEngine1
{
    class Square : GameObject
    {
        private bool _allowInput;
        private double velocity = 10;
        public int ID = 0;

        public Square(bool allowInput, int posX, int posY, bool visible, bool isUI, byte layer, Figure[] figures, byte baseFigure)
            : base(posX, posY, visible, isUI, layer, figures, baseFigure)
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