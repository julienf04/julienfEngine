using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    interface ICollidableOnCollisionExit : ICanCollide
    {
        public void OnCollisionExit(GameObject[] collisions);
    }
}
