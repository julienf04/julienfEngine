using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class Camera : Transform
    {
        #region ---ATRIBUTES;

        private bool _isFirstCamera = true; //True if the first screen to draw gameObjects, and false if the second screen to draw gameObjects

        #endregion

        #region ---CONSTRUCTORS;

        public Camera(int posX, int posY) : base(posX, posY)
        {

        }

        #endregion

        #region ---METHODS;

        #endregion

        #region ---PROPIERTIES;

        public bool P_IsFirstCamera
        {
            get
            {
                return _isFirstCamera;
            }

            set
            {
                _isFirstCamera = value;
            }
        }

        #endregion
    }
}
