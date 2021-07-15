using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class GameObject : Transform //This class has all necesary for objets
    {
        #region ---ATRIBUTES;

        private Figure[] _figures = new Figure[1]; //Figures of the gameObject
        private int _baseFigure = 0;

        private bool _visible = false; // return true if the gameObject must be drawed, false otherwise

        private bool _isUI = false;

        private Animation _animation = null;

        #endregion

        #region ---CONSTRUCTORS;

        public GameObject(Figure[] figures, int baseFigure = 0, bool visible = true, bool isUI = false, int posX = 0, int posY = 0) : base(posX, posY)
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

        #endregion
    }
}
