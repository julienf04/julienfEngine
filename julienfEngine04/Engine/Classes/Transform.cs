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

        //public void MoveUp(double step)
        //{
        //    //int nextIntCell = (int)this.P_PosY;
        //    //double nextStep = this.P_PosY - step;

        //    //this.P_PosY = nextStep >= nextIntCell ? nextStep : nextIntCell;

        //    int oldPosY = (int)this.P_PosY - 1;
        //    this.P_PosY -= step;
        //    if (this.P_PosY < oldPosY) this.P_PosY = (int)this.P_PosY;

        //    //if (this.ppo)
        //}

        //public void MoveDown(double step)
        //{
        //    int nextIntCell = (int)this.P_PosY + 1;
        //    double nextStep = this.P_PosY + step;

        //    this.P_PosY = nextStep < nextIntCell ? nextStep : nextIntCell;
        //}

        //public void MoveRight(double step)
        //{
        //    int nextIntCell = (int)this.P_PosX + 1;
        //    double nextStep = this.P_PosX + step;

        //    this.P_PosX = nextStep < nextIntCell ? nextStep : nextIntCell;
        //}

        //public void MoveLeft(double step)
        //{
        //    int nextIntCell = (int)this.P_PosX;
        //    double nextStep = this.P_PosX - step;

        //    this.P_PosX = nextStep > nextIntCell ? nextStep : nextIntCell - 0.0001;
        //}

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
