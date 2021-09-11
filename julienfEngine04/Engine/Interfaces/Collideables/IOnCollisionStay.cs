using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    interface IOnCollisionStay : ICanCollide
    {
        public void OnCollisionStay(GameObject[] collisions);
    }
}
