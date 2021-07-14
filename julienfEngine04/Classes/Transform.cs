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

        #endregion

        #region ---CONSTRUCTORS;

        public Transform(int posX = 0, int posY = 0)
        {
            P_PosX = posX;
            P_PosY = posY;
        }

        #endregion

        #region ---METHODS;

        public void MovePosition(int x, int y)
        {
            P_PosX = x;
            P_PosY = y;
        }

        #endregion

        #region ---PROPIERTIES;

        public int P_PosX
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

        public int P_PosY
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

        #endregion

    }
}
