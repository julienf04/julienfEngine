using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class Transform
    {

        #region ---ATRIBUTES;

        private int _posX = 0; //position X of the gameObject
        private int _posY = 0; //position Y of the gameObject

        private ConsoleColor _color = 0; //color of the gameObject

        #endregion

        #region ---CONSTRUCTORS;

        public Transform(int posX = 0, int posY = 0)
        {
            P_posX = posX;
            P_posY = posY;
        }

        #endregion

        #region ---METHODS;

        public void MovePosition(int x, int y)
        {
            P_posX = x;
            P_posY = y;
        }

        #endregion

        #region ---PROPIERTIES;

        public int P_posX
        {
            get
            {
                return _posX;
            }

            set
            {
                _posX = value;
            }
        }

        public int P_posY
        {
            get
            {
                return _posY;
            }

            set
            {
                _posY = value;
            }
        }

        public ConsoleColor P_consoleColor
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        #endregion

    }
}
