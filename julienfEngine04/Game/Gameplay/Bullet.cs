using julienfEngine1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace julienfEngine1
{
    class Bullet : GameObject, ICanCollide
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private Figure _figureBullet = new Figure
           (new string[1]
               {
                    @"==>"
               }, E_ForegroundColors.Gray
           );

        private int _velocity = 50;

        private bool _isBeenUsed = false;

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Bullet(E_ForegroundColors color, Spaceship.E_PlayerID playerID, Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                                  int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureBullet };
            this.P_GameObjectFigures[0].ForegroundColor = color;

            this.P_Collision.P_Colliders = new Area[1]
            {
                new Area(1,1,0,0)
            };

            this._velocity *= playerID == Spaceship.E_PlayerID.Player1 ? 1 : -1;
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        public void MoveBullet()
        {
            if (this._isBeenUsed) this.P_PosX += _velocity * Timer.P_DeltaTime;

            this._isBeenUsed = this.P_PosX < Screen.P_Width && this.P_PosX > 0;
        }

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigureBullet
        {
            get
            {
                return _figureBullet;
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

        #endregion
    }
}