using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace julienfEngine1
{
    class SpaceshipAIHard : SpaceshipAI
    {
        #region ATTRIBUTES

        private const byte _LIMIT_MARGIN_Y = 1;
        private const byte _MIN_BULLET_CLOSENESS_TO_START_MOVING_POSX = 20;
        private const byte _MAX_BULLET_CLOSENESS_TO_START_MOVING_POSX = 60;

        //private Transform _currentTransformToDodge;
        private sbyte _direction = 1;
        private sbyte _destiny;
        private bool _changeDestination = true;
        private Random _random = new Random();
        private int? _bulletClosenessToStartMoving = _MAX_BULLET_CLOSENESS_TO_START_MOVING_POSX;

        #endregion

        #region CONSTRUCTORS

        public SpaceshipAIHard(Spaceship mySpaceship, IEnumerable<IDodgeable> objectsToDodge) : base(mySpaceship, objectsToDodge)
        {
            _destiny = (sbyte)this.P_SpaceshipAttached.P_PosY;
        }

        #endregion

        #region METHODS

        public override void Run()
        {
            int posY = (int)this.P_SpaceshipAttached.P_PosY;
            int posX = (int)this.P_SpaceshipAttached.P_PosX;

            IDodgeable[] objectsToDodge = this.P_ObjectsToDodge.ToArray();
            float differenceBetweenSpaceshipAndObjectPosY = float.MaxValue;
            float differenceBetweenSpaceshipAndObjectPosX = float.MaxValue;
            float spaceshipFigureLength = this.P_SpaceshipAttached.P_GameObjectFigures[0].P_Figure.Length;



            if (objectsToDodge.Length > 0)
            {
                differenceBetweenSpaceshipAndObjectPosY = posY + spaceshipFigureLength - (int)objectsToDodge[0].P_Transform.P_PosY;
                differenceBetweenSpaceshipAndObjectPosX = posX - (int)objectsToDodge[0].P_Transform.P_PosX;
            }

            if (differenceBetweenSpaceshipAndObjectPosY <= spaceshipFigureLength && differenceBetweenSpaceshipAndObjectPosY > 0) // Length of spaceship and 0
            {
                // La bala esta en una posY que chocaria contra la nave
                if (differenceBetweenSpaceshipAndObjectPosX <= _bulletClosenessToStartMoving || !_changeDestination)
                {
                    // La bala esta en una posX que chocaria contra la nave
                    if (_changeDestination)
                    {
                        int maxPosYWithMargin = this.P_SpaceshipAttached.P_MaxPosY - 1 - _LIMIT_MARGIN_Y; // -X to give it margin
                        int minPosYWithMargin = this.P_SpaceshipAttached.P_MinPosY + 0 + _LIMIT_MARGIN_Y; // +X to give it margin

                        float halfSpaceshipFigureLength = MathF.Round(spaceshipFigureLength / 2, MidpointRounding.ToPositiveInfinity);
                        _direction = differenceBetweenSpaceshipAndObjectPosY == halfSpaceshipFigureLength ? _random.Next(0, 2) == 0 ? (sbyte)-1 : (sbyte)1
                            : differenceBetweenSpaceshipAndObjectPosY > spaceshipFigureLength / 2 ? (sbyte)1 : (sbyte)-1;

                        bool positiveDirection = _direction == 1;

                        _destiny = (sbyte)(posY + (positiveDirection ? spaceshipFigureLength : 0) - differenceBetweenSpaceshipAndObjectPosY);
                        //_destiny = (sbyte)(posY + (spaceshipFigureLength - differenceBetweenSpaceshipAndObjectPosY * _direction);


                        sbyte fixedDirection = _destiny >= maxPosYWithMargin ? (sbyte)-1 : _destiny
                            < minPosYWithMargin ? (sbyte)1 : _direction;

                        positiveDirection = fixedDirection == 1;

                        //_destiny = (sbyte)(posY + (spaceshipFigureLength - differenceBetweenSpaceshipAndObjectPosY) * fixedDirection);
                        _destiny = (sbyte)(posY + (positiveDirection ? spaceshipFigureLength : 0) - differenceBetweenSpaceshipAndObjectPosY);
                        _direction = fixedDirection;
                        _changeDestination = false;
                        _bulletClosenessToStartMoving = null;
                        //if (fixedDirection == -1)
                        //{
                        //    _destiny = (byte)(posY + differenceBetweenSpaceshipAndObjectPosY * -_direction);
                        //}
                        //_direction = _destiny >= maxPosYWithMargin ? (sbyte)-1 : _destiny 
                        //    <= minPosYWithMargin ? (sbyte)1 : _direction;

                        //int z;
                        //if (_destiny <= minPosYWithMargin)
                        //    z = 354;
                    }


                    this.P_SpaceshipAttached.P_PosY += this.P_SpaceshipAttached.P_Velocity * _direction * Timer.P_DeltaTime;
                }
            }
            else
            {
               _bulletClosenessToStartMoving ??= _random.Next(_MIN_BULLET_CLOSENESS_TO_START_MOVING_POSX, _MAX_BULLET_CLOSENESS_TO_START_MOVING_POSX);

                _changeDestination = true;
            }
        }


        #endregion
    }
}