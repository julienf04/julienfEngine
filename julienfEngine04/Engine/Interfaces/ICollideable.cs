using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    interface ICollideable
    {
        public Area P_Collider
        {
            get;
        }

        public void OnCollisionEnter(GameObject[] gameObjects);

        public void OnCollisionStay(GameObject[] gameObjects);

        public void OnCollisionExit(GameObject[] gameObjects);
    }
}
