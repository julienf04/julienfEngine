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

        protected override IDodgeable FindTargetBullet(int spaceshipMinPosY, int spaceshipMaxPosY, out int? fixedDirection)
        {
            throw new NotImplementedException();
        }

        protected override void MoveToDestiny(int fixedDirection)
        {
            throw new NotImplementedException();
        }

        protected override void MoveToDestiny(int destiny, int spaceshipMinPosY, int spaceshipMaxPosY)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
