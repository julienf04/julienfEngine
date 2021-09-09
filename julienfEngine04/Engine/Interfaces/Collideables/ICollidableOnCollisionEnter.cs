using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    interface ICollidableOnCollisionEnter : ICanCollide
    {
        public void OnCollisionEnter(GameObject[] collisions);
    }
}
