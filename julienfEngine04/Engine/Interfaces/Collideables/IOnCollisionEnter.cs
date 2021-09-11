using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    interface IOnCollisionEnter : ICanCollide
    {
        public void OnCollisionEnter(GameObject[] collisions);
    }
}
