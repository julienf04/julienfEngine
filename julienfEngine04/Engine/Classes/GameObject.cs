using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace julienfEngine1
{
    abstract class GameObject : Transform //This class has all necesary for objets
    {
        #region ---ATRIBUTES;

        private Figure[] _figures = new Figure[1]; //Figures of the gameObject
        private byte _baseFigure = 0;

        private bool _visible = false; // return true if the gameObject must be drawed, false otherwise

        private bool _isUI = false;

        private Animation _animation = null;

        private byte _layer = 0;

        private Collision _collision;

        //private Scene _myScene = null;

        #endregion

        #region ---CONSTRUCTORS;

        public GameObject(Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                          int posX = 0, int posY = 0) : base(posX, posY)
        {
            if (figures != null)
            {
                _figures = figures;
                for (int i = 0; i < figures.Length; i++)
                {
                    if (figures[i] == null) figures[i] = new Figure();
                }

                if (baseFigure >= 0 && baseFigure < figures.Length) _baseFigure = baseFigure;
            }
            else
            {
                _figures[0] = new Figure();
            }

            _animation = new Animation(_figures.Length);
            _visible = visible;
            _isUI = isUI;
            _layer = layer;

            //if (myScene != null) _myScene = myScene;
            //if (_visible && myScene != null) _myScene.AddToDrawGameObject(this);
            if (_visible) Scene.P_CurrentScene.AddToDrawGameObject(this);


            if (this is ICanCollide)
            {
                _collision = new Collision(this);

                //_myScene.AddToDetectCollisionsGameObject(this);
                Scene.P_CurrentScene.AddToDetectCollisionsGameObject(this);
            }
        }

        #endregion

        #region ---METHODS;

        public void Animate()
        {
            _animation.RunAnimation();
        }

        public void StopAnimation(bool resetAnimation)
        {
            _animation.StopAnimation(resetAnimation);
        }

        //public bool CollisionWith(GameObject gameObject)
        //{
        //    if (this._colliders == null || gameObject._colliders == null) return false;


        //    int thisPosX = (int)this.P_PosX;
        //    int thisPosY = (int)this.P_PosY;
        //    int otherPosX = (int)gameObject.P_PosX;
        //    int otherPosY = (int)gameObject.P_PosY;

        //    int? objectMinPosXAndColliderLastPointX = null;
        //    int? objectMaxPosXAndColliderFirstPointX = null;

        //    int? objectMinPosYAndColliderLastPointY = null;
        //    int? objectMaxPosYAndColliderFirstPointY = null;


        //    // This code is experimental. I recommend not use it
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    //if (_collisionMode == E_CollisionModes.Discrete && gameObject._collisionMode == E_CollisionModes.Discrete)
        //    //{
        //    //    // Discrete

        //    //    if (thisPosX <= otherPosX)
        //    //    {
        //    //        if (thisPosY <= otherPosY)
        //    //        {
        //    //            for (int i = 0; i < this._colliders.Length; i++)
        //    //            {
        //    //                if (this._colliders[i] != null)
        //    //                {
        //    //                    //if ((this._colliders[i].P_LastPointCollisionX + thisPosX >= objectMinPosXAndColliderLastPointX || objectMinPosXAndColliderLastPointX == null) &&
        //    //                    //    (this._colliders[i].P_LastPointCollisionY + thisPosY >= objectMinPosYAndColliderLastPointY || objectMinPosYAndColliderLastPointY == null))
        //    //                    //{
        //    //                    //    objectMinPosXAndColliderLastPointX = this._colliders[i].P_LastPointCollisionX + thisPosX;
        //    //                    //    objectMinPosYAndColliderLastPointY = this._colliders[i].P_LastPointCollisionY + thisPosY;
        //    //                    //}


        //    //                    if (this._colliders[i].P_LastPointCollisionX + this._colliders[i].P_LastPointCollisionY >
        //    //                        objectMinPosXAndColliderLastPointX + objectMinPosYAndColliderLastPointY ||
        //    //                        objectMinPosXAndColliderLastPointX == null)
        //    //                    {
        //    //                        objectMinPosXAndColliderLastPointX = this._colliders[i].P_LastPointCollisionX;
        //    //                        objectMinPosYAndColliderLastPointY = this._colliders[i].P_LastPointCollisionY;
        //    //                    }


        //    //                    //if (this._colliders[i].P_LastPointCollisionX + thisPosX > objectMinPosXAndColliderLastPointX || objectMinPosXAndColliderLastPointX == null)
        //    //                    //    objectMinPosXAndColliderLastPointX = this._colliders[i].P_LastPointCollisionX + thisPosX;

        //    //                    //if (this._colliders[i].P_LastPointCollisionY + thisPosY > objectMinPosYAndColliderLastPointY || objectMinPosYAndColliderLastPointY == null)
        //    //                    //    objectMinPosYAndColliderLastPointY = this._colliders[i].P_LastPointCollisionY + thisPosY;
        //    //                }
        //    //            }

        //    //            if (objectMinPosXAndColliderLastPointX == null) return false;

        //    //            for (int i = 0; i < gameObject._colliders.Length; i++)
        //    //            {
        //    //                if (gameObject._colliders[i] != null)
        //    //                {
        //    //                    //if ((gameObject._colliders[i].P_FirstPointCollisionX + otherPosX <= objectMaxPosXAndColliderFirstPointX || objectMaxPosXAndColliderFirstPointX == null) &&
        //    //                    //    (gameObject._colliders[i].P_FirstPointCollisionY + otherPosY <= objectMaxPosYAndColliderFirstPointY || objectMaxPosYAndColliderFirstPointY == null))
        //    //                    //{
        //    //                    //    objectMaxPosXAndColliderFirstPointX = gameObject._colliders[i].P_FirstPointCollisionX + otherPosX;
        //    //                    //    objectMaxPosYAndColliderFirstPointY = gameObject._colliders[i].P_FirstPointCollisionY + otherPosY;
        //    //                    //}


        //    //                    if (gameObject._colliders[i].P_FirstPointCollisionX + gameObject._colliders[i].P_FirstPointCollisionY <
        //    //                        objectMaxPosXAndColliderFirstPointX + objectMaxPosYAndColliderFirstPointY||
        //    //                        objectMaxPosXAndColliderFirstPointX == null)
        //    //                    {
        //    //                        objectMaxPosXAndColliderFirstPointX = gameObject._colliders[i].P_FirstPointCollisionX;
        //    //                        objectMaxPosYAndColliderFirstPointY = gameObject._colliders[i].P_FirstPointCollisionY;
        //    //                    }


        //    //                    //if (gameObject._colliders[i].P_FirstPointCollisionX + otherPosX < objectMaxPosXAndColliderFirstPointX || objectMaxPosXAndColliderFirstPointX == null)
        //    //                    //    objectMaxPosXAndColliderFirstPointX = gameObject._colliders[i].P_FirstPointCollisionX + otherPosX;

        //    //                    //if (gameObject._colliders[i].P_FirstPointCollisionY + otherPosY < objectMaxPosYAndColliderFirstPointY || objectMaxPosYAndColliderFirstPointY == null)
        //    //                    //    objectMaxPosYAndColliderFirstPointY = gameObject._colliders[i].P_FirstPointCollisionY + otherPosY;
        //    //                }

        //    //            }

        //    //            if (objectMaxPosXAndColliderFirstPointX == null) return false;

        //    //            objectMinPosXAndColliderLastPointX += thisPosX;
        //    //            objectMinPosYAndColliderLastPointY += thisPosY;
        //    //            objectMaxPosXAndColliderFirstPointX += otherPosX;
        //    //            objectMaxPosYAndColliderFirstPointY += otherPosY;
        //    //        }
        //    //        else
        //    //        {
        //    //            for (int i = 0; i < this._colliders.Length; i++)
        //    //            {
        //    //                if (this._colliders[i] != null)
        //    //                {
        //    //                    //if ((this._colliders[i].P_LastPointCollisionX + thisPosX >= objectMinPosXAndColliderLastPointX || objectMinPosXAndColliderLastPointX == null) &&
        //    //                    //    (this._colliders[i].P_FirstPointCollisionY + thisPosY <= objectMaxPosYAndColliderFirstPointY || objectMaxPosYAndColliderFirstPointY == null))
        //    //                    //{
        //    //                    //    objectMinPosXAndColliderLastPointX = this._colliders[i].P_LastPointCollisionX + thisPosX;
        //    //                    //    objectMaxPosYAndColliderFirstPointY = this._colliders[i].P_FirstPointCollisionY + thisPosY;
        //    //                    //}


        //    //                    if (this._colliders[i].P_LastPointCollisionX + this._colliders[i].P_FirstPointCollisionY <
        //    //                        objectMinPosXAndColliderLastPointX + objectMaxPosYAndColliderFirstPointY ||
        //    //                        objectMinPosXAndColliderLastPointX == null)
        //    //                    {
        //    //                        objectMinPosXAndColliderLastPointX = this._colliders[i].P_LastPointCollisionX;
        //    //                        objectMaxPosYAndColliderFirstPointY = this._colliders[i].P_FirstPointCollisionY;
        //    //                    }


        //    //                    //if (this._colliders[i].P_LastPointCollisionX + thisPosX > objectMinPosXAndColliderLastPointX || objectMinPosXAndColliderLastPointX == null)
        //    //                    //    objectMinPosXAndColliderLastPointX = this._colliders[i].P_LastPointCollisionX + thisPosX;

        //    //                    //if (this._colliders[i].P_FirstPointCollisionY + thisPosY < objectMaxPosYAndColliderFirstPointY || objectMaxPosYAndColliderFirstPointY == null)
        //    //                    //    objectMaxPosYAndColliderFirstPointY = this._colliders[i].P_FirstPointCollisionY + thisPosY;
        //    //                }
        //    //            }

        //    //            if (objectMinPosXAndColliderLastPointX == null) return false;

        //    //            for (int i = 0; i < gameObject._colliders.Length; i++)
        //    //            {
        //    //                if (gameObject._colliders != null)
        //    //                {
        //    //                    //if ((gameObject._colliders[i].P_FirstPointCollisionX + otherPosX <= objectMaxPosXAndColliderFirstPointX || objectMaxPosXAndColliderFirstPointX == null) &&
        //    //                    //    (gameObject._colliders[i].P_LastPointCollisionY + otherPosY >= objectMinPosYAndColliderLastPointY || objectMinPosYAndColliderLastPointY == null))
        //    //                    //{
        //    //                    //    objectMaxPosXAndColliderFirstPointX = gameObject._colliders[i].P_FirstPointCollisionX + otherPosX;
        //    //                    //    objectMinPosYAndColliderLastPointY = gameObject._colliders[i].P_LastPointCollisionY + otherPosY;
        //    //                    //}


        //    //                    if (gameObject._colliders[i].P_FirstPointCollisionX + gameObject._colliders[i].P_LastPointCollisionY <
        //    //                        objectMaxPosXAndColliderFirstPointX + objectMinPosYAndColliderLastPointY ||
        //    //                        objectMaxPosXAndColliderFirstPointX == null)
        //    //                    {
        //    //                        objectMaxPosXAndColliderFirstPointX = gameObject._colliders[i].P_FirstPointCollisionX;
        //    //                        objectMinPosYAndColliderLastPointY = gameObject._colliders[i].P_LastPointCollisionY;
        //    //                    }


        //    //                    //if (gameObject._colliders[i].P_FirstPointCollisionX + otherPosX < objectMaxPosXAndColliderFirstPointX || objectMaxPosXAndColliderFirstPointX == null)
        //    //                    //    objectMaxPosXAndColliderFirstPointX = gameObject._colliders[i].P_FirstPointCollisionX + otherPosX;

        //    //                    //if (gameObject._colliders[i].P_LastPointCollisionY + otherPosY > objectMinPosYAndColliderLastPointY || objectMinPosYAndColliderLastPointY == null)
        //    //                    //    objectMinPosYAndColliderLastPointY = gameObject._colliders[i].P_LastPointCollisionY + otherPosY;
        //    //                }
        //    //            }

        //    //            if (objectMaxPosXAndColliderFirstPointX == null) return false;

        //    //            objectMinPosXAndColliderLastPointX += thisPosX;
        //    //            objectMinPosYAndColliderLastPointY += otherPosY;
        //    //            objectMaxPosXAndColliderFirstPointX += otherPosX;
        //    //            objectMaxPosYAndColliderFirstPointY += thisPosY;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        if (thisPosY <= otherPosY)
        //    //        {
        //    //            for (int i = 0; i < this._colliders.Length; i++)
        //    //            {
        //    //                if (this._colliders[i] != null)
        //    //                {
        //    //                    //if ((this._colliders[i].P_FirstPointCollisionX + thisPosX <= objectMaxPosXAndColliderFirstPointX || objectMaxPosXAndColliderFirstPointX == null) &&
        //    //                    //    (this._colliders[i].P_LastPointCollisionY + thisPosY >= objectMinPosYAndColliderLastPointY || objectMinPosYAndColliderLastPointY == null))
        //    //                    //{
        //    //                    //    objectMaxPosXAndColliderFirstPointX = this._colliders[i].P_FirstPointCollisionX + thisPosX;
        //    //                    //    objectMinPosYAndColliderLastPointY = this._colliders[i].P_LastPointCollisionY + thisPosY;
        //    //                    //}


        //    //                    if (this._colliders[i].P_FirstPointCollisionX + this._colliders[i].P_LastPointCollisionY <
        //    //                        objectMaxPosXAndColliderFirstPointX + objectMinPosYAndColliderLastPointY ||
        //    //                        objectMaxPosXAndColliderFirstPointX == null)
        //    //                    {
        //    //                        objectMaxPosXAndColliderFirstPointX = this._colliders[i].P_FirstPointCollisionX;
        //    //                        objectMinPosYAndColliderLastPointY = this._colliders[i].P_LastPointCollisionY;
        //    //                    }


        //    //                    //if (this._colliders[i].P_FirstPointCollisionX + thisPosX < objectMaxPosXAndColliderFirstPointX || objectMaxPosXAndColliderFirstPointX == null)
        //    //                    //    objectMaxPosXAndColliderFirstPointX = this._colliders[i].P_FirstPointCollisionX + thisPosX;

        //    //                    //if (this._colliders[i].P_LastPointCollisionY + thisPosY > objectMinPosYAndColliderLastPointY || objectMinPosYAndColliderLastPointY == null)
        //    //                    //    objectMinPosYAndColliderLastPointY = this._colliders[i].P_LastPointCollisionY + thisPosY;
        //    //                }
        //    //            }

        //    //            if (objectMaxPosXAndColliderFirstPointX == null) return false;

        //    //            for (int i = 0; i < gameObject._colliders.Length; i++)
        //    //            {
        //    //                if (gameObject._colliders[i] != null)
        //    //                {
        //    //                    //if ((gameObject._colliders[i].P_LastPointCollisionX + otherPosX >= objectMinPosXAndColliderLastPointX || objectMinPosXAndColliderLastPointX == null) &&
        //    //                    //    (gameObject._colliders[i].P_FirstPointCollisionY + otherPosY <= objectMaxPosYAndColliderFirstPointY || objectMaxPosYAndColliderFirstPointY == null))
        //    //                    //{
        //    //                    //    objectMinPosXAndColliderLastPointX = gameObject._colliders[i].P_LastPointCollisionX + otherPosX;
        //    //                    //    objectMaxPosYAndColliderFirstPointY = gameObject._colliders[i].P_FirstPointCollisionY + otherPosY;
        //    //                    //}


        //    //                    if (gameObject._colliders[i].P_LastPointCollisionX + gameObject._colliders[i].P_FirstPointCollisionY <
        //    //                        objectMinPosXAndColliderLastPointX + objectMaxPosYAndColliderFirstPointY ||
        //    //                        objectMinPosXAndColliderLastPointX == null)
        //    //                    {
        //    //                        objectMinPosXAndColliderLastPointX = gameObject._colliders[i].P_LastPointCollisionX;
        //    //                        objectMaxPosYAndColliderFirstPointY = gameObject._colliders[i].P_FirstPointCollisionY;
        //    //                    }


        //    //                    //if (gameObject._colliders[i].P_LastPointCollisionX + otherPosX > objectMinPosXAndColliderLastPointX || objectMinPosXAndColliderLastPointX == null)
        //    //                    //    objectMinPosXAndColliderLastPointX = gameObject._colliders[i].P_LastPointCollisionX + otherPosX;

        //    //                    //if (gameObject._colliders[i].P_FirstPointCollisionY + otherPosY < objectMaxPosYAndColliderFirstPointY || objectMaxPosYAndColliderFirstPointY == null)
        //    //                    //    objectMaxPosYAndColliderFirstPointY = gameObject._colliders[i].P_FirstPointCollisionY + otherPosY;
        //    //                }
        //    //            }

        //    //            if (objectMinPosXAndColliderLastPointX == null) return false;

        //    //            objectMinPosXAndColliderLastPointX += otherPosX;
        //    //            objectMinPosYAndColliderLastPointY += thisPosY;
        //    //            objectMaxPosXAndColliderFirstPointX += thisPosX;
        //    //            objectMaxPosYAndColliderFirstPointY += otherPosY;
        //    //        }
        //    //        else
        //    //        {
        //    //            for (int i = 0; i < this._colliders.Length; i++)
        //    //            {
        //    //                if (this._colliders[i] != null)
        //    //                {
        //    //                    if ((this._colliders[i].P_FirstPointCollisionX + thisPosX <= objectMaxPosXAndColliderFirstPointX || objectMaxPosXAndColliderFirstPointX == null) &&
        //    //                        (this._colliders[i].P_FirstPointCollisionY + thisPosY <= objectMaxPosYAndColliderFirstPointY || objectMaxPosYAndColliderFirstPointY == null))
        //    //                    {
        //    //                        objectMaxPosXAndColliderFirstPointX = this._colliders[i].P_FirstPointCollisionX + thisPosX;
        //    //                        objectMaxPosYAndColliderFirstPointY = this._colliders[i].P_FirstPointCollisionY + thisPosY;
        //    //                    }


        //    //                    if (this._colliders[i].P_FirstPointCollisionX + this._colliders[i].P_FirstPointCollisionY <
        //    //                        objectMaxPosXAndColliderFirstPointX + objectMaxPosYAndColliderFirstPointY ||
        //    //                        objectMaxPosXAndColliderFirstPointX == null)
        //    //                    {
        //    //                        objectMaxPosXAndColliderFirstPointX = this._colliders[i].P_FirstPointCollisionX;
        //    //                        objectMaxPosYAndColliderFirstPointY = this._colliders[i].P_FirstPointCollisionY;
        //    //                    }


        //    //                    //if (this._colliders[i].P_FirstPointCollisionX + thisPosX < objectMaxPosXAndColliderFirstPointX || objectMaxPosXAndColliderFirstPointX == null)
        //    //                    //    objectMaxPosXAndColliderFirstPointX = this._colliders[i].P_FirstPointCollisionX + thisPosX;

        //    //                    //if (this._colliders[i].P_FirstPointCollisionY + thisPosY < objectMaxPosYAndColliderFirstPointY || objectMaxPosYAndColliderFirstPointY == null)
        //    //                    //    objectMaxPosYAndColliderFirstPointY = this._colliders[i].P_FirstPointCollisionY + thisPosY;
        //    //                }
        //    //            }

        //    //            if (objectMaxPosXAndColliderFirstPointX == null) return false;

        //    //            for (int i = 0; i < gameObject._colliders.Length; i++)
        //    //            {
        //    //                if (gameObject._colliders != null)
        //    //                {
        //    //                    //if ((gameObject._colliders[i].P_LastPointCollisionX + otherPosX >= objectMinPosXAndColliderLastPointX || objectMinPosXAndColliderLastPointX == null) &&
        //    //                    //    (gameObject._colliders[i].P_LastPointCollisionY + otherPosY >= objectMinPosYAndColliderLastPointY || objectMinPosYAndColliderLastPointY == null))
        //    //                    //{
        //    //                    //    objectMinPosXAndColliderLastPointX = gameObject._colliders[i].P_LastPointCollisionX + otherPosX;
        //    //                    //    objectMinPosYAndColliderLastPointY = gameObject._colliders[i].P_LastPointCollisionY + otherPosY;
        //    //                    //}


        //    //                    if (gameObject._colliders[i].P_LastPointCollisionX + gameObject._colliders[i].P_LastPointCollisionY >
        //    //                        objectMinPosXAndColliderLastPointX + objectMinPosYAndColliderLastPointY ||
        //    //                        objectMinPosXAndColliderLastPointX == null)
        //    //                    {
        //    //                        objectMinPosXAndColliderLastPointX = gameObject._colliders[i].P_LastPointCollisionX;
        //    //                        objectMinPosYAndColliderLastPointY = gameObject._colliders[i].P_LastPointCollisionY;
        //    //                    }


        //    //                    //if (gameObject._colliders[i].P_LastPointCollisionX + otherPosX > objectMinPosXAndColliderLastPointX || objectMinPosXAndColliderLastPointX == null)
        //    //                    //    objectMinPosXAndColliderLastPointX = gameObject._colliders[i].P_LastPointCollisionX + otherPosX;

        //    //                    //if (gameObject._colliders[i].P_LastPointCollisionY + otherPosY > objectMinPosYAndColliderLastPointY || objectMinPosYAndColliderLastPointY == null)
        //    //                    //    objectMinPosYAndColliderLastPointY = gameObject._colliders[i].P_LastPointCollisionY + otherPosY;
        //    //                }
        //    //            }

        //    //            if (objectMinPosXAndColliderLastPointX == null) return false;

        //    //            objectMinPosXAndColliderLastPointX += otherPosY;
        //    //            objectMinPosYAndColliderLastPointY += otherPosY;
        //    //            objectMaxPosXAndColliderFirstPointX += thisPosX;
        //    //            objectMaxPosYAndColliderFirstPointY += thisPosY;
        //    //        }
        //    //    }

        //    //    if (objectMinPosXAndColliderLastPointX >= objectMaxPosXAndColliderFirstPointX
        //    //        && objectMinPosYAndColliderLastPointY >= objectMaxPosYAndColliderFirstPointY) return true;

        //    //}


        //    //else
        //    //{
        //    //    // Hard

        //    //    for (int iThis = 0; iThis < this._colliders.Length; iThis++)
        //    //    {
        //    //        Area thisCurrentCollider = this._colliders[iThis];
        //    //        if (thisCurrentCollider != null)
        //    //        {
        //    //            for (int iOther = 0; iOther < gameObject._colliders.Length; iOther++)
        //    //            {
        //    //                Area otherCurrentCollider = gameObject._colliders[iOther];
        //    //                if (otherCurrentCollider != null)
        //    //                {
        //    //                    if (thisPosX + thisCurrentCollider.P_FirstPointCollisionX <= otherPosX + otherCurrentCollider.P_FirstPointCollisionX)
        //    //                    {
        //    //                        objectMinPosXAndColliderLastPointX = thisPosX + thisCurrentCollider.P_LastPointCollisionX;
        //    //                        objectMaxPosXAndColliderFirstPointX = otherPosX + otherCurrentCollider.P_FirstPointCollisionX;
        //    //                    }
        //    //                    else
        //    //                    {
        //    //                        objectMinPosXAndColliderLastPointX = otherPosX + otherCurrentCollider.P_LastPointCollisionX;
        //    //                        objectMaxPosXAndColliderFirstPointX = thisPosX + thisCurrentCollider.P_FirstPointCollisionX;
        //    //                    }


        //    //                    if (thisPosY + thisCurrentCollider.P_FirstPointCollisionY <= otherPosY + otherCurrentCollider.P_FirstPointCollisionY)
        //    //                    {
        //    //                        objectMinPosYAndColliderLastPointY = thisPosY + thisCurrentCollider.P_LastPointCollisionY;
        //    //                        objectMaxPosYAndColliderFirstPointY = otherPosY + otherCurrentCollider.P_FirstPointCollisionY;
        //    //                    }
        //    //                    else
        //    //                    {
        //    //                        objectMinPosYAndColliderLastPointY = otherPosY + otherCurrentCollider.P_LastPointCollisionY;
        //    //                        objectMaxPosYAndColliderFirstPointY = thisPosY + thisCurrentCollider.P_FirstPointCollisionY;
        //    //                    }


        //    //                    if (objectMinPosXAndColliderLastPointX >= objectMaxPosXAndColliderFirstPointX && objectMinPosYAndColliderLastPointY >= objectMaxPosYAndColliderFirstPointY) return true;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        //    for (int iThis = 0; iThis < this._colliders.Length; iThis++)
        //    {
        //        Area thisCurrentCollider = this._colliders[iThis];
        //        if (thisCurrentCollider != null)
        //        {
        //            for (int iOther = 0; iOther < gameObject._colliders.Length; iOther++)
        //            {
        //                Area otherCurrentCollider = gameObject._colliders[iOther];
        //                if (otherCurrentCollider != null)
        //                {
        //                    if (thisPosX + thisCurrentCollider.P_FirstPointCollisionX <= otherPosX + otherCurrentCollider.P_FirstPointCollisionX)
        //                    {
        //                        objectMinPosXAndColliderLastPointX = thisPosX + thisCurrentCollider.P_LastPointCollisionX;
        //                        objectMaxPosXAndColliderFirstPointX = otherPosX + otherCurrentCollider.P_FirstPointCollisionX;
        //                    }
        //                    else
        //                    {
        //                        objectMinPosXAndColliderLastPointX = otherPosX + otherCurrentCollider.P_LastPointCollisionX;
        //                        objectMaxPosXAndColliderFirstPointX = thisPosX + thisCurrentCollider.P_FirstPointCollisionX;
        //                    }


        //                    if (thisPosY + thisCurrentCollider.P_FirstPointCollisionY <= otherPosY + otherCurrentCollider.P_FirstPointCollisionY)
        //                    {
        //                        objectMinPosYAndColliderLastPointY = thisPosY + thisCurrentCollider.P_LastPointCollisionY;
        //                        objectMaxPosYAndColliderFirstPointY = otherPosY + otherCurrentCollider.P_FirstPointCollisionY;
        //                    }
        //                    else
        //                    {
        //                        objectMinPosYAndColliderLastPointY = otherPosY + otherCurrentCollider.P_LastPointCollisionY;
        //                        objectMaxPosYAndColliderFirstPointY = thisPosY + thisCurrentCollider.P_FirstPointCollisionY;
        //                    }


        //                    if (objectMinPosXAndColliderLastPointX >= objectMaxPosXAndColliderFirstPointX && objectMinPosYAndColliderLastPointY >= objectMaxPosYAndColliderFirstPointY) return true;
        //                }
        //            }
        //        }
        //    }

        //    return false;
        //}

        #endregion

        #region ---PROPIERTIES;

        public Figure[] P_GameObjectFigures
        {
            get
            {
                return _figures;
            }

            set
            {
                _figures = value;
            }
        }

        public byte P_BaseFigure
        {
            get
            {
                return _baseFigure;
            }

            set
            {
                if (value >= 0 && value < _figures.Length) _baseFigure = value;
            }
        }

        public bool P_Visible
        {
            get
            {
                return _visible;
            }

            set
            {
                //if (_visible) _myScene.AddToDrawGameObject(this);
                if (value != _visible)
                {
                    if (value) Scene.P_CurrentScene.AddToDrawGameObject(this);
                    else Scene.P_CurrentScene.RemoveToDrawGameObject(this);

                    _visible = value;
                }
            }
        }

        public bool P_IsUI
        {
            get
            {
                return _isUI;
            }

            set
            {
                _isUI = value;
            }
        }

        public Animation P_Animation
        {
            get
            {
                return _animation;
            }
        }

        public byte P_Layer
        {
            get
            {
                return _layer;
            }

            set
            {
                _layer = value;
            }
        }

        public Collision P_Collision
        {
            get
            {
                return _collision;
            }
        }

        #endregion
    }
}
