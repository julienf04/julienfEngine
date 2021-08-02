using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class Scene
    {
        #region ---ATRIBUTES

        private Camera _mainCamera = new Camera(0, 0); //This Camera is the main camera, the camera displayed 

        private List<GameObject> _gameObjectsToDraw = new List<GameObject>();

        private List<GameObject> _gameObjectsToDetectCollisions = new List<GameObject>();

        #endregion


        #region PROPERTIES

        public Camera P_MainCamera
        {
            get
            {
                return _mainCamera;
            }

            set
            {
                _mainCamera = value;
            }
        }

        public List<GameObject> P_GameObjectsToDraw
        {
            get
            {
                return _gameObjectsToDraw;
            }
        }

        public List<GameObject> P_GameObjectsToDetectCollisions
        {
            get
            {
                return _gameObjectsToDetectCollisions;
            }
        }

        #endregion
    }
}
