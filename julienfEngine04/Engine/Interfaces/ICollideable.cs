using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    interface ICollideable
    {
        public void OnCollisionEnter(GameObject[] collisions);

        public void OnCollisionStay(GameObject[] collisions);

        public void OnCollisionExit(GameObject[] collisions);
    }
}
