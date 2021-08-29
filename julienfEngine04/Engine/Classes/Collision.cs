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

        private List<GameObject> _currentOnCollisionEnterGameObjects;
        private List<GameObject> _currentOnCollisionStayGameObjects;
        private List<GameObject> _currentOnCollisionExitGameObjects;

        #endregion

        #region CONSTRUCTORS

        public Collision()
        {
            _currentOnCollisionEnterGameObjects = new List<GameObject>();
            _currentOnCollisionStayGameObjects = new List<GameObject>();
            _currentOnCollisionExitGameObjects = new List<GameObject>();
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
                _detectCollisions = value;
            }
        }

        internal List<GameObject> P_CurrentOnCollisionEnterGameObjects
        {
            get
            {
                return _currentOnCollisionEnterGameObjects;
            }
        }

        internal List<GameObject> P_CurrentOnCollisionStayGameObjects
        {
            get
            {
                return _currentOnCollisionStayGameObjects;
            }
        }

        internal List<GameObject> P_CurrentOnCollisionExitGameObjects
        {
            get
            {
                return _currentOnCollisionExitGameObjects;
            }
        }

        #endregion
    }
}
