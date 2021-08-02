using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    abstract class GameObject : Transform //This class has all necesary for objets
    {
        #region ---ATRIBUTES;

        private Figure[] _figures = new Figure[1]; //Figures of the gameObject
        private int _baseFigure = 0;

        private bool _visible = false; // return true if the gameObject must be drawed, false otherwise

        private bool _isUI = false;

        private Animation _animation = null;

        private int _layer = 0;

        private Area _collider;

        private bool _detectCollisions;

        private List<GameObject> _currentOnCollisionEnterGameObjects = new List<GameObject>();

        private List<GameObject> _currentOnCollisionStayGameObjects = new List<GameObject>();

        private List<GameObject> _currentOnCollisionExitGameObjects = new List<GameObject>();

        #endregion

        #region ---CONSTRUCTORS;

        public GameObject(Figure[] figures, int baseFigure = 0, bool visible = true, bool isUI = false, int layer = 0, int posX = 0, int posY = 0,
                          Area collider = null, bool detectCollisions = false) : base(posX, posY)
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

            if (_visible) julienfEngine.DrawConsole(this);


            _collider = collider;

            _detectCollisions = _collider != null ? detectCollisions : false;

            if (_detectCollisions) julienfEngine.AllowCollisions(this);
        }

        #endregion

        #region ---METHODS;

        public void Draw()
        {
            julienfEngine.DrawConsole(this);
        }

        public void Animate()
        {
            _animation.RunAnimation();
        }

        public void StopAnimation(bool resetAnimation)
        {
            _animation.StopAnimation(resetAnimation);
        }

        public abstract void OnCollisionEnter(GameObject[] gameObject);
        public abstract void OnCollisionStay(GameObject[] gameObject);
        public abstract void OnCollisionExit(GameObject[] gameObject);

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

        public int P_BaseFigure
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
                if (_visible) julienfEngine.DrawConsole(this);
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

        public int P_Layer
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
            set
            {
                if (_collider != null && value)
                {
                    _detectCollisions = value;
                    julienfEngine.AllowCollisions(this);
                }
                else
                {
                    _detectCollisions = value;
                    _currentOnCollisionStayGameObjects.Clear();
                    julienfEngine.NotAllowCollisions(this);
                }
            }
        }

        public List<GameObject> P_CurrentOnCollisionEnterGameObjects
        {
            get
            {
                return _currentOnCollisionEnterGameObjects;
            }
        }

        public List<GameObject> P_CurrentOnCollisionStayGameObjects
        {
            get
            {
                return _currentOnCollisionStayGameObjects;
            }
        }

        public List<GameObject> P_CurrentOnCollisionExitGameObjects
        {
            get
            {
                return _currentOnCollisionExitGameObjects;
            }
        }

        #endregion
    }
}
