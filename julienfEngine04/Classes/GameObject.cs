using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class GameObject : Transform //This class has all necesary for objets
    {
        #region ---ATRIBUTES;

        private Figure _figure = new Figure(); //Figure of the gameObject

        private bool _visible = false; // return true if the gameObject must be drawed, false otherwise

        private bool _isUI = false;

        #endregion

        #region ---CONSTRUCTORS;

        public GameObject(Figure figure, bool visible = true, bool isUI = false, int posX = 0, int posY = 0) : base(posX, posY)
        {
            if (figure != null) _figure = figure;
            _visible = visible;
            _isUI = isUI;
        }

        #endregion

        #region ---METHODS;

        #endregion

        #region ---PROPIERTIES;

        public Figure P_GameObjectFigure
        {
            get
            {
                return _figure;
            }

            set
            {
                _figure = value;
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

        #endregion
    }
}
