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

        #endregion

        #region ---CONSTRUCTORS;

        public GameObject(Figure figure = null, bool visible = true, int posX = 0, int posY = 0) : base(posX, posY)
        {
            this._figure = figure;
            _visible = visible;
        }

        #endregion

        #region ---METHODS;

        #endregion

        #region ---PROPIERTIES;

        public Figure P_figure
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

        public bool P_visible
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

        #endregion
    }
}
