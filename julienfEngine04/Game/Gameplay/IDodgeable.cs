using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace julienfEngine1
{
    interface IDodgeable
    {
        public Transform P_Transform
        {
            get;
        }

        public float P_Velocity
        {
            get;
        }
    }
}
