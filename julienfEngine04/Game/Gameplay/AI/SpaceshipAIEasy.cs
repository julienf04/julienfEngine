using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace julienfEngine1
{
    class SpaceshipAIEasy: SpaceshipAI
    {
        #region CONSTRUCTORS

        public SpaceshipAIEasy(Spaceship mySpaceship, IEnumerable<IDodgeable> objectsToDodge) : base(mySpaceship, objectsToDodge)
        {

        }

        #endregion

        #region METHODS

        public override void Run()
        {

        }

        #endregion
    }
}
