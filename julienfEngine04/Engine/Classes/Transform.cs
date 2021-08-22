using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class Transform
    {

        #region ---ATRIBUTES;

        private double _posX = 0; //position X of the gameObject
        private double _posY = 0; //position Y of the gameObject

        #endregion

        #region ---CONSTRUCTORS;

        public Transform(double posX = 0, double posY = 0)
        {
            P_PosX = posX;
            P_PosY = posY;
        }

        #endregion

        #region ---METHODS;

        public void MovePosition(double x, double y)
        {
            P_PosX = x;
            P_PosY = y;
        }

        #endregion

        #region ---PROPIERTIES;

        public double P_PosX
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

        public double P_PosY
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
