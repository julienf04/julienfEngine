using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace julienfEngine1
{
    class SpaceshipAINormal : SpaceshipAI
    {
        #region ATTRIBUTES

        private const int _WEIGHT_MARGIN = -10;
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

        public SpaceshipAINormal(Spaceship mySpaceship, IEnumerable<IDodgeable> objectsToDodge) : base(mySpaceship, objectsToDodge)
        {
            _lastMinBulletPosY = this.P_SpaceshipAttached.P_MinPosY;
            _lastMaxBulletPosY = this.P_SpaceshipAttached.P_MaxPosY;
            _timerImmovable.StartMyTimer(0);
        }

        #endregion

        #region METHODS

        public override void Run()
        {
            int? fixedDirection;
            IDodgeable targetBullet = FindTargetBullet(0, this.P_SpaceshipAttached.P_MaxPosY, out fixedDirection);

            if (targetBullet is not null)
            {
                int destiny = (int)targetBullet.P_Transform.P_PosY;
                int direction = 0;
                if (fixedDirection is null)
                {
                    MoveToDestiny(destiny, 0, this.P_SpaceshipAttached.P_MaxPosY, out direction);

                    if (direction == 1)
                    {
                        _lastMinBullet = targetBullet;
                        _lastMinBulletPosY = (int)_lastMinBullet.P_Transform.P_PosY;
                    }
                    else
                    {
                        _lastMaxBullet = targetBullet;
                        _lastMaxBulletPosY = (int)_lastMaxBullet.P_Transform.P_PosY;
                    }
                }
                else MoveToDestiny((int)fixedDirection);

                _timerImmovable.ResetMyTimer();
                _timerImmovable.StartMyTimer(0);
                _timeImmovable = new Random().Next(_MIN_TIME_TO_SLEEP, _MAX_TIME_TO_SLEEP);
            }
            else if (_timerImmovable.P_MyTimer >= _timeImmovable)
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

        private IDodgeable FindTargetBullet(int spaceshipMinPosY, int spaceshipMaxPosY, out int? fixedDirection)
        {
            bool CheckDanger(int dodgeablePosY, int spaceshipFigureLength)
            {
                return dodgeablePosY >= (int)this.P_SpaceshipAttached.P_PosY && dodgeablePosY < (int)this.P_SpaceshipAttached.P_PosY + spaceshipFigureLength;
            }

            IDodgeable[] dodgeables = this.P_ObjectsToDodge.ToArray();

            int spaceshipFigureLengthY = this.P_SpaceshipAttached.P_GameObjectFigures[0].P_Figure.Length;
            int spaceshipFigureLengthX = this.P_SpaceshipAttached.P_GameObjectFigures[0].P_Figure[0].Length;
            fixedDirection = null;

            if (dodgeables.Length <= 0) return null;

            IDodgeable bulletMaxPosY = dodgeables[0];
            IDodgeable bulletMinPosY = dodgeables[0];
            IDodgeable lastBullet = dodgeables[0];


            bool danger = CheckDanger((int)dodgeables[0].P_Transform.P_PosY, spaceshipFigureLengthY);
            bool weightPassed = false;
            for (int i = 1; i < dodgeables.Length && !weightPassed; i++)
            {
                int velocityDifference = (int)MathF.Round(dodgeables[i].P_Velocity / this.P_SpaceshipAttached.P_Velocity, MidpointRounding.ToPositiveInfinity);

                int weightToPass = (spaceshipFigureLengthY + spaceshipFigureLengthX * velocityDifference) + _WEIGHT_MARGIN;
                int weightPosY = ((int)lastBullet.P_Transform.P_PosY - (int)dodgeables[i].P_Transform.P_PosY) * velocityDifference;
                int weightPosX = ((int)lastBullet.P_Transform.P_PosX - (int)dodgeables[i].P_Transform.P_PosX);

                weightPassed = weightPosX + weightPosY > weightToPass;

                if (!weightPassed)
                {
                    if ((int)dodgeables[i].P_Transform.P_PosY >= (int)bulletMaxPosY.P_Transform.P_PosY) bulletMaxPosY = dodgeables[i];
                    else if ((int)dodgeables[i].P_Transform.P_PosY < (int)bulletMaxPosY.P_Transform.P_PosY) bulletMinPosY = dodgeables[i];

                    if (!danger) danger = CheckDanger((int)dodgeables[i].P_Transform.P_PosY, spaceshipFigureLengthY);
                    lastBullet = dodgeables[i];
                }
            }

            if (!danger) return null;

            int destinyDistanceMaxPosY = Math.Abs((int)bulletMaxPosY.P_Transform.P_PosY - (int)this.P_SpaceshipAttached.P_PosY + 1);
            int destinyDistanceMinPosY = Math.Abs(((int)this.P_SpaceshipAttached.P_PosY + spaceshipFigureLengthY) - (int)bulletMaxPosY.P_Transform.P_PosY + 1);

            //if (performanceDestinyMaxPosY >= performanceDestinyMinPosY)
            //    if ((int)bulletMinPosY.P_Transform.P_PosY > spaceshipMinPosY) return bulletMinPosY;
            //    else if ((int)bulletMaxPosY.P_Transform.P_PosY < spaceshipMaxPosY) return bulletMaxPosY;
            //    else return bulletMinPosY;


            IDodgeable bulletToReturn = destinyDistanceMaxPosY <= destinyDistanceMinPosY && (int)bulletMaxPosY.P_Transform.P_PosY <= spaceshipMaxPosY ? bulletMaxPosY
                : (int)bulletMinPosY.P_Transform.P_PosY >= spaceshipMinPosY ? bulletMinPosY
                : null;

            if ((int)bulletMaxPosY.P_Transform.P_PosY >= spaceshipMaxPosY)
            {
                bulletToReturn = bulletMinPosY;
                fixedDirection = -1;
            }
            else if ((int)bulletMinPosY.P_Transform.P_PosY <= spaceshipMinPosY)
            {
                bulletToReturn = bulletMaxPosY;
                fixedDirection = 1;
            }

            return bulletToReturn;
        }

        private void MoveToDestiny(int fixedDirection)
        {
            this.P_SpaceshipAttached.P_PosY += fixedDirection * this.P_SpaceshipAttached.P_Velocity * Timer.P_DeltaTime;
        }

        private void MoveToDestiny(int targetBulletPosY, int spaceshipMinPosY, int spaceshipMaxPosY, out int outDirection)
        {
            int spaceshipFigureLength = this.P_SpaceshipAttached.P_GameObjectFigures[0].P_Figure.Length;
            int halfSpaceshipFigureLength = spaceshipFigureLength / 2;

            //int direction = bulletPosition < (int)this.P_SpaceshipAttached.P_PosY + halfSpaceshipFigureLength ? 1
            //    : bulletPosition > (int)this.P_SpaceshipAttached.P_PosY - spaceshipFigureLength ? -1
            //    : bulletPosition == (int)this.P_SpaceshipAttached.P_PosY + halfSpaceshipFigureLength ? (new Random()).Next(0,2) == 1 ? 1 : -1
            //    : 0;
            int direction = 0;

            if (targetBulletPosY <= (int)this.P_SpaceshipAttached.P_PosY + halfSpaceshipFigureLength) direction = 1;
            else if (targetBulletPosY >= (int)this.P_SpaceshipAttached.P_PosY + spaceshipFigureLength - halfSpaceshipFigureLength) direction = -1;

            if (targetBulletPosY >= spaceshipMaxPosY) direction = -1;
            else if (targetBulletPosY - spaceshipFigureLength <= spaceshipMinPosY) direction = 1;

            outDirection = direction;

            this.P_SpaceshipAttached.P_PosY += direction * this.P_SpaceshipAttached.P_Velocity * Timer.P_DeltaTime;
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
        #endregion
    }
}