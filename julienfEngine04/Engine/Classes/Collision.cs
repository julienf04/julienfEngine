using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class Collision
    {
        #region ATTRIBUTES

        private Area[] _colliders;

        private bool _detectCollisions = true;

        private Stack<GameObject> _currentOnCollisionEnterGameObjects;
        private Dictionary<uint, GameObject> _currentOnCollisionStayGameObjects;
        private Stack<GameObject> _currentOnCollisionExitGameObjects;

        private GameObject _gameObjectAttached;

        private uint _ID = 0;
        private static uint _countOfExistingCollisions = 0;

        #endregion

        #region CONSTRUCTORS

        public Collision(GameObject gameObjectAttached)
        {
            this._gameObjectAttached = gameObjectAttached;

            _currentOnCollisionEnterGameObjects = new Stack<GameObject>();
            _currentOnCollisionStayGameObjects = new Dictionary<uint, GameObject>();
            _currentOnCollisionExitGameObjects = new Stack<GameObject>();

            _ID = _countOfExistingCollisions;
            _countOfExistingCollisions++;
        }

        #endregion

        #region METHODS

        public static bool IsCollision(GameObject gameObject1, GameObject gameObject2)
        {
            if (gameObject1.P_Collision._colliders == null || gameObject2.P_Collision._colliders == null) return false;


            int GO1PosX = (int)gameObject1.P_PosX;
            int GO1PosY = (int)gameObject1.P_PosY;
            int GO2PosX = (int)gameObject2.P_PosX;
            int GO2PosY = (int)gameObject2.P_PosY;

            int objectMinPosXAndColliderLastPointX;
            int objectMaxPosXAndColliderFirstPointX;

            int objectMinPosYAndColliderLastPointY;
            int objectMaxPosYAndColliderFirstPointY;


            for (int iGO1 = 0; iGO1 < gameObject1.P_Collision._colliders.Length; iGO1++)
            {
                Area currentColliderGO1 = gameObject1.P_Collision._colliders[iGO1];
                if (currentColliderGO1 != null)
                {
                    for (int iGO2 = 0; iGO2 < gameObject2.P_Collision._colliders.Length; iGO2++)
                    {
                        Area currentColliderGO2 = gameObject2.P_Collision._colliders[iGO2];
                        if (currentColliderGO2 != null)
                        {
                            if (GO1PosX + currentColliderGO1.P_FirstPointCollisionX <= GO2PosX + currentColliderGO2.P_FirstPointCollisionX)
                            {
                                objectMinPosXAndColliderLastPointX = GO1PosX + currentColliderGO1.P_LastPointCollisionX;
                                objectMaxPosXAndColliderFirstPointX = GO2PosX + currentColliderGO2.P_FirstPointCollisionX;
                            }
                            else
                            {
                                objectMinPosXAndColliderLastPointX = GO2PosX + currentColliderGO2.P_LastPointCollisionX;
                                objectMaxPosXAndColliderFirstPointX = GO1PosX + currentColliderGO1.P_FirstPointCollisionX;
                            }


                            if (GO1PosY + currentColliderGO1.P_FirstPointCollisionY <= GO2PosY + currentColliderGO2.P_FirstPointCollisionY)
                            {
                                objectMinPosYAndColliderLastPointY = GO1PosY + currentColliderGO1.P_LastPointCollisionY;
                                objectMaxPosYAndColliderFirstPointY = GO2PosY + currentColliderGO2.P_FirstPointCollisionY;
                            }
                            else
                            {
                                objectMinPosYAndColliderLastPointY = GO2PosY + currentColliderGO2.P_LastPointCollisionY;
                                objectMaxPosYAndColliderFirstPointY = GO1PosY + currentColliderGO1.P_FirstPointCollisionY;
                            }


                            if (objectMinPosXAndColliderLastPointX >= objectMaxPosXAndColliderFirstPointX && objectMinPosYAndColliderLastPointY >= objectMaxPosYAndColliderFirstPointY) return true;
                        }
                    }
                }
            }

            return false;
        }

        public static void AddToDetectCollisions(GameObject gameObject)
        {
            Scene.P_CurrentScene.AddToDetectCollisionsGameObject(gameObject);
        }

        public void AddToDetectCollisions()
        {
            Scene.P_CurrentScene.AddToDetectCollisionsGameObject(this._gameObjectAttached);
        }

        #endregion

        #region PROPERTIES

        public Area[] P_Colliders
        {
            get
            {
                return _colliders;
            }
            set
            {
                _colliders = value;
            }
        }

        public bool P_DetectCollisions
        {
            get
            {
                return _detectCollisions;
            }
            set
            {
                if (value != _detectCollisions)
                {
                    if (value) Scene.P_CurrentScene.AddToDetectCollisionsGameObject(_gameObjectAttached);
                    else Scene.P_CurrentScene.RemoveToDetectCollisionsGameObject(_gameObjectAttached);

                    _detectCollisions = value;
                }
            }
        }

        internal Stack<GameObject> P_CurrentOnCollisionEnterGameObjects
        {
            get
            {
                return _currentOnCollisionEnterGameObjects;
            }
        }

        internal Dictionary<uint, GameObject> P_CurrentOnCollisionStayGameObjects
        {
            get
            {
                return _currentOnCollisionStayGameObjects;
            }
        }

        internal Stack<GameObject> P_CurrentOnCollisionExitGameObjects
        {
            get
            {
                return _currentOnCollisionExitGameObjects;
            }
        }

        internal uint P_ID
        {
            get
            {
                return _ID;
            }
        }

        #endregion
    }
}
