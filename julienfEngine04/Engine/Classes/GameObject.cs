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

        private bool _visible = true; // return true if the gameObject must be drawed, false otherwise

        private bool _isUI = false;

        private readonly Animation _animation = null;

        private byte _layer = 0;

        private readonly Collision _collision;

        //private Scene _myScene = null;

        #endregion

        #region ---CONSTRUCTORS;

        public GameObject(int posX, int posY, bool visible) : base(posX, posY)
        {
            _figures[0] = new Figure();
            _animation = new Animation(_figures.Length);

            _visible = visible;
            if (_visible) Scene.P_CurrentScene.AddToDrawGameObject(this);

            if (this is ICanCollide)
            {
                _collision = new Collision(this);
                Scene.P_CurrentScene.AddToDetectCollisionsGameObject(this);
            }
        }

        public GameObject(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY)
        {
            _figures[0] = new Figure();
            _animation = new Animation(_figures.Length);

            _visible = visible;
            _isUI = isUI;
            _layer = layer;

            if (_visible) Scene.P_CurrentScene.AddToDrawGameObject(this);

            if (this is ICanCollide)
            {
                _collision = new Collision(this);
                Scene.P_CurrentScene.AddToDetectCollisionsGameObject(this);
            }
        }

        public GameObject(int posX, int posY, bool visible, bool isUI, byte layer, Figure[] figures, byte baseFigure) : base(posX, posY)
        {
            _figures = figures;
            for (int i = 0; i < figures.Length; i++)
            {
                if (figures[i] == null) figures[i] = new Figure();
            }

            if (baseFigure >= 0 && baseFigure < figures.Length) _baseFigure = baseFigure;

            _animation = new Animation(_figures.Length);

            _visible = visible;
            _isUI = isUI;
            _layer = layer;

            if (_visible) Scene.P_CurrentScene.AddToDrawGameObject(this);

            if (this is ICanCollide)
            {
                _collision = new Collision(this);
                Scene.P_CurrentScene.AddToDetectCollisionsGameObject(this);
            }
        }

        //public GameObject(Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
        //                  int posX = 0, int posY = 0) : base(posX, posY)
        //{
        //    if (figures != null)
        //    {
        //        _figures = figures;
        //        for (int i = 0; i < figures.Length; i++)
        //        {
        //            if (figures[i] == null) figures[i] = new Figure();
        //        }

        //        if (baseFigure >= 0 && baseFigure < figures.Length) _baseFigure = baseFigure;
        //    }
        //    else
        //    {
        //        _figures[0] = new Figure();
        //    }

        //    _animation = new Animation(_figures.Length);
        //    _visible = visible;
        //    _isUI = isUI;
        //    _layer = layer;

        //    //if (myScene != null) _myScene = myScene;
        //    //if (_visible && myScene != null) _myScene.AddToDrawGameObject(this);
        //    if (_visible) Scene.P_CurrentScene.AddToDrawGameObject(this);


        //    if (this is ICanCollide)
        //    {
        //        _collision = new Collision(this);

        //        //_myScene.AddToDetectCollisionsGameObject(this);
        //        Scene.P_CurrentScene.AddToDetectCollisionsGameObject(this);
        //    }
        //}

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
                if (value.Length != this._animation.P_SequenceOfFigures.Length) this._animation.P_NewSequenceOfFigures = value.Length;
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
