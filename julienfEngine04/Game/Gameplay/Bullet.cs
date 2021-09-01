using julienfEngine1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace julienfEngine1
{
    class Bullet : GameObject, ICollideable
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

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Bullet(Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                                  int posX = 0, int posY = 0) : base(figures, myScene, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureBullet };
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        void ICollideable.OnCollisionEnter(GameObject[] collisions)
        {
            throw new NotImplementedException();
        }

        void ICollideable.OnCollisionStay(GameObject[] collisions)
        {
            throw new NotImplementedException();
        }

        void ICollideable.OnCollisionExit(GameObject[] collisions)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {

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

        #endregion
    }
}