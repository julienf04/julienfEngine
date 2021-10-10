using julienfEngine1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace julienfEngine1
{
    class Bullet : GameObject, ICanCollide, IDodgeable
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private readonly Figure _figureBulletPlayer1 = new Figure
           (new string[1]
               {
                    @"==>"
               }, E_ForegroundColors.Gray
           );

        private readonly Figure _figureBulletPlayer2 = new Figure
         (new string[1]
             {
                    @"<=="
             }, E_ForegroundColors.Gray
         );

        private readonly byte _velocity = 50;
        private readonly sbyte _direction;

        private bool _isBeenUsed = false;

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Bullet(E_ForegroundColors color, E_PlayerID playerID, int posX, int posY, bool visible) : base(posX, posY, visible)
        {
            //this.P_GameObjectFigures = new Figure[1] { _figureBulletPlayer1 };
            //this.P_GameObjectFigures[0].ForegroundColor = color;

            switch (playerID)
            {
                case E_PlayerID.Player1:
                    this.P_Collision.P_Colliders = new Area[1] { new Area(1, 1, 0, 0) };
                    this.P_GameObjectFigures = new Figure[1] { _figureBulletPlayer1 };
                    this.P_GameObjectFigures[0].ForegroundColor = color;
                    break;
                case E_PlayerID.Player2:
                    this.P_Collision.P_Colliders = new Area[1] { new Area(0, 0, 0, 0) };
                    this.P_GameObjectFigures = new Figure[1] { _figureBulletPlayer2 };
                    this.P_GameObjectFigures[0].ForegroundColor = color;
                    break;
            }

            this._direction = playerID == E_PlayerID.Player1 ? (sbyte)1 : (sbyte)-1;
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        public void MoveBullet()
        {
            if (this._isBeenUsed) this.P_PosX += _velocity * _direction * Timer.P_DeltaTime;

            this._isBeenUsed = this.P_PosX < Screen.P_Width && this.P_PosX > 0 - this.P_GameObjectFigures[0].P_Figure[0].Length;
        }

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigureBulletPlayer1
        {
            get
            {
                return _figureBulletPlayer1;
            }
        }

        public Figure P_FigureBulletPlayer2
        {
            get
            {
                return _figureBulletPlayer2;
            }
        }

        public bool P_IsBeenUsed
        {
            get
            {
                return _isBeenUsed;
            }
            set
            {
                if (value != _isBeenUsed)
                {
                    this.P_Visible = value;
                    this.P_Collision.P_DetectCollisions = value;
                    _isBeenUsed = value;
                }
            }
        }

        Transform IDodgeable.P_Transform
        {
            get
            {
                return this;
            }
        }

        float IDodgeable.P_Velocity
        {
            get
            {
                return _velocity;
            }
        }

        #endregion
    }
}