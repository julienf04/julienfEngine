using julienfEngine1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace julienfEngine1
{
    class Bullet : GameObject, ICollidable
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

        private int _velocity = 25;

        private bool _isBeenUsed = false;

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Bullet(Spaceship.E_PlayerID playerID, Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                                  int posX = 0, int posY = 0) : base(figures, myScene, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureBullet };

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

        void ICollidableOnCollisionEnter.OnCollisionEnter(GameObject[] collisions)
        {
            throw new NotImplementedException();
        }

        void ICollidableOnCollisionStay.OnCollisionStay(GameObject[] collisions)
        {
            throw new NotImplementedException();
        }

        void ICollidableOnCollisionExit.OnCollisionExit(GameObject[] collisions)
        {
            throw new NotImplementedException();
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
                _isBeenUsed = value;
            }
        }

        #endregion
    }
}