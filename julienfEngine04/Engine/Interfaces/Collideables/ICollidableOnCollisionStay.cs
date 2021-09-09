using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    interface ICollidableOnCollisionStay : ICanCollide
    {
        public void OnCollisionStay(GameObject[] collisions);
    }
}
