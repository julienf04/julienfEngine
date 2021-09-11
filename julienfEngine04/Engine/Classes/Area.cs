using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class Area
    {
        #region ATRIBUTES

        private int _firstPointCollisionX;
        private int _lastPointCollisionX;
        private int _firstPointCollisionY;
        private int _lastPointCollisionY;

        #endregion

        #region CONSTRUCTORS

        public Area(int firstPointCollisionX, int lastPointCollisionX, int firstPointCollisionY, int lastPointCollisionY)
        {
            _firstPointCollisionX = firstPointCollisionX;
            _lastPointCollisionX = lastPointCollisionX;
            _firstPointCollisionY = firstPointCollisionY;
            _lastPointCollisionY = lastPointCollisionY;
        }

        #endregion

        #region METHODS

        private int ChangeValues(int value, ref int otherPoint)
        {
            int point = otherPoint;
            otherPoint = value;
            return point;
        }

        #endregion

        #region PROPERTIES

        public int P_FirstPointCollisionX
        {
            get
            {
                return _firstPointCollisionX;
            }
            set
            {
                _firstPointCollisionX = value <= _lastPointCollisionX ? value : ChangeValues(value, ref _lastPointCollisionX);
            }
        }

        public int P_LastPointCollisionX
        {
            get
            {
                return _lastPointCollisionX;
            }
            set
            {
                _lastPointCollisionX = value >= _firstPointCollisionX ? value : ChangeValues(value, ref _firstPointCollisionX);
            }
        }

        public int P_FirstPointCollisionY
        {
            get
            {
                return _firstPointCollisionY;
            }
            set
            {
                _firstPointCollisionY = value <= _lastPointCollisionY ? value : ChangeValues(value, ref _lastPointCollisionY);
            }
        }

        public int P_LastPointCollisionY
        {
            get
            {
                return _lastPointCollisionY;
            }
            set
            {
                _lastPointCollisionY = value <= _firstPointCollisionY ? value : ChangeValues(value, ref _firstPointCollisionY);
            }
        }

        #endregion
    }
}
