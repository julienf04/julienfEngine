using System;
using System.Collections.Generic;
using System.Text;

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

        private Area _collider;

        private bool _detectCollisions = false;

        private List<GameObject> _currentOnCollisionEnterGameObjects;

        private List<GameObject> _currentOnCollisionStayGameObjects;

        private List<GameObject> _currentOnCollisionExitGameObjects;

        private Scene _myScene = null;

        #endregion

        #region ---CONSTRUCTORS;

        public GameObject(Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
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

            if (myScene != null) _myScene = myScene;
            if (_visible && myScene != null) _myScene.AddToDrawGameObject(this);


            if (this is ICollideable)
                if (((ICollideable)this).P_Collider == null) throw new NullReferenceException();
                else _myScene.AddToDetectCollisionsGameObject(this);
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

        public bool CollisionWith(GameObject gameObject)
        {
            int objectMinPosXAndColliderLength;
            int objectMaxPosX;
            if (this.P_PosX <= gameObject.P_PosX)
            {
                objectMinPosXAndColliderLength = (int)this.P_PosX + this._collider.P_FirstPointCollisionX + this._collider.P_LastPointCollisionX;
                objectMaxPosX = (int)gameObject.P_PosX;
            }
            else
            {
                objectMinPosXAndColliderLength = (int)gameObject.P_PosX + gameObject._collider.P_FirstPointCollisionX + gameObject._collider.P_LastPointCollisionX;
                objectMaxPosX = (int)this.P_PosX;
            }


            int objectMinPosYAndColliderLength;
            int objectMaxPosY;
            if (this.P_PosY <= gameObject.P_PosY)
            {
                objectMinPosYAndColliderLength = (int)this.P_PosY + this._collider.P_FirstPointCollisionY + this._collider.P_LastPointCollisionY;
                objectMaxPosY = (int)gameObject.P_PosY;
            }
            else
            {
                objectMinPosYAndColliderLength = (int)gameObject.P_PosY + gameObject._collider.P_FirstPointCollisionY + gameObject._collider.P_LastPointCollisionY;
                objectMaxPosY = (int)this.P_PosY;
            }


            return objectMinPosXAndColliderLength >= objectMaxPosX && objectMinPosYAndColliderLength >= objectMaxPosY;
        }

        internal void DetectCollisions(ICollideable collision)
        {
            _collider = collision.P_Collider;
            _currentOnCollisionEnterGameObjects = new List<GameObject>();
            _currentOnCollisionStayGameObjects = new List<GameObject>();
            _currentOnCollisionExitGameObjects = new List<GameObject>();
            _detectCollisions = true;
        }

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
                _visible = value;
                if (_visible) _myScene.AddToDrawGameObject(this);
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

        public bool P_DetectCollisions
        {
            get
            {
                return _detectCollisions;
            }
        }

        internal List<GameObject> P_CurrentOnCollisionEnterGameObjects
        {
            get
            {
                return _currentOnCollisionEnterGameObjects;
            }
        }

        internal List<GameObject> P_CurrentOnCollisionStayGameObjects
        {
            get
            {
                return _currentOnCollisionStayGameObjects;
            }
        }

        internal List<GameObject> P_CurrentOnCollisionExitGameObjects
        {
            get
            {
                return _currentOnCollisionExitGameObjects;
            }
        }

        #endregion

        #region DESTRUCTOR

        //~GameObject()
        //{
        //    this._myScene.RemoveToDrawGameObject(this);
        //}

        #endregion
    }
}
