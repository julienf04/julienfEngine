using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    interface ICollidable : ICollidableOnCollisionEnter, ICollidableOnCollisionStay, ICollidableOnCollisionExit
    {
    
    }
}
