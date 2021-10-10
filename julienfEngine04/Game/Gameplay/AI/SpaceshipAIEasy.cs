using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace julienfEngine1
{
    class SpaceshipAIEasy: SpaceshipAI
    {
        #region ATTRIBUTES

        private const int _MIN_TIME_TO_SLEEP = 1;
        private const int _MAX_TIME_TO_SLEEP = 5;
        private const int _POSSIBILITY_OF_SLEEP = 4;

        //private Transform _currentTransformToDodge;
        private int _lastRandomDestiny = 1;
        private IDodgeable _lastMinBullet;
        private int _lastMinBulletPosY;
        private IDodgeable _lastMaxBullet;
        private int _lastMaxBulletPosY;
        private bool _operatorGreaterRandomDestiny = true;
        private readonly Timer _timerImmovable = new Timer();
        private int _timeImmovable = 1;

        #endregion

        #region CONSTRUCTORS

        public SpaceshipAIEasy(Spaceship mySpaceship, IEnumerable<IDodgeable> objectsToDodge) : base(mySpaceship, objectsToDodge)
        {
            _lastMinBulletPosY = this.P_SpaceshipAttached.P_MinPosY;
            _lastMaxBulletPosY = this.P_SpaceshipAttached.P_MaxPosY;
            _timerImmovable.StartMyTimer(0);
        }

        #endregion

        #region METHODS

        public override void Run()
        {
            if (_timerImmovable.P_MyTimer >= _timeImmovable)
            {
                int direction = FindRandomDirection(_lastMinBulletPosY, _lastMaxBulletPosY, ref _lastRandomDestiny);
                MoveToDestiny(direction);
            }

            if (!this.P_ObjectsToDodge.Contains(_lastMinBullet))
            {
                _lastMinBullet = null;
                _lastMinBulletPosY = this.P_SpaceshipAttached.P_MinPosY;
            }
            if (!this.P_ObjectsToDodge.Contains(_lastMaxBullet))
            {
                _lastMaxBullet = null;
                _lastMaxBulletPosY = this.P_SpaceshipAttached.P_MaxPosY;
            }

            this.P_SpaceshipAttached.Shoot();
            this.P_SpaceshipAttached.RechargeBullets();
            this.P_SpaceshipAttached.MoveBulletsAttached();
        }

        private int FindRandomDirection(int minRange, int maxRange, ref int lastRandomDestiny)
        {
            Random random = new Random();
            if (_operatorGreaterRandomDestiny ? this.P_SpaceshipAttached.P_PosY >= lastRandomDestiny : this.P_SpaceshipAttached.P_PosY <= lastRandomDestiny)
            {
                if (random.Next(0, _POSSIBILITY_OF_SLEEP) == 0)
                {
                    _timerImmovable.ResetMyTimer();
                    _timerImmovable.StartMyTimer(0);
                    _timeImmovable = random.Next(_MIN_TIME_TO_SLEEP, _MAX_TIME_TO_SLEEP);
                }

                lastRandomDestiny = random.Next(minRange, maxRange);
            }

            _operatorGreaterRandomDestiny = this.P_SpaceshipAttached.P_PosY <= lastRandomDestiny;

            int direction = _operatorGreaterRandomDestiny ? 1 : -1;
            return direction;
        }

        private void MoveToDestiny(int fixedDirection)
        {
            this.P_SpaceshipAttached.P_PosY += fixedDirection * this.P_SpaceshipAttached.P_Velocity * Timer.P_DeltaTime;
        }

        #endregion
    }
}
