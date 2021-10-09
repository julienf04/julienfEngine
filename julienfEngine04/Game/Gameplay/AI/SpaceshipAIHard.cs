//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace julienfEngine1
//{
//    class SpaceshipAIHard : SpaceshipAI
//    {
//        #region ATTRIBUTES

//        private const byte _LIMIT_MARGIN_Y = 1;
//        private const float _MAX_RANDOMLY_BULLET_POSX_TO_START_MOVING = 3;

//        //private Transform _currentTransformToDodge;
//        private sbyte _direction = 1;
//        private sbyte _destiny;
//        private bool _changeDestination = true;
//        private Random _random = new Random();
//        private int? _bulletClosenessToStartMoving = int.MaxValue;

//        #endregion

//        #region CONSTRUCTORS

//        public SpaceshipAIHard(Spaceship mySpaceship, IEnumerable<IDodgeable> objectsToDodge) : base(mySpaceship, objectsToDodge)
//        {
//            _destiny = (sbyte)this.P_SpaceshipAttached.P_PosY;
//        }

//        #endregion

//        #region METHODS

//        public override void Run()
//        {
//            IDodgeable[] objectsToDodge = this.P_ObjectsToDodge.ToArray();
//            if (objectsToDodge.Length <= 0) return;

//            int posY = (int)this.P_SpaceshipAttached.P_PosY;
//            int posX = (int)this.P_SpaceshipAttached.P_PosX;

//            float differenceBetweenSpaceshipAndObjectPosY = float.MaxValue;
//            float differenceBetweenSpaceshipAndObjectPosX = float.MaxValue;
//            int spaceshipFigureLength = this.P_SpaceshipAttached.P_GameObjectFigures[0].P_Figure.Length;

//            int maxPosYWithMargin = this.P_SpaceshipAttached.P_MaxPosY - 1 - _LIMIT_MARGIN_Y; // -X to give it margin
//            int minPosYWithMargin = this.P_SpaceshipAttached.P_MinPosY + 0 + _LIMIT_MARGIN_Y; // +X to give it margin

//            IDodgeable mostDangerousObject = this.FindMostDangerousObject(minPosYWithMargin, maxPosYWithMargin);

//            if (mostDangerousObject != null) // Length of spaceship and 0
//            {
//                // La bala esta en una posY que chocaria contra la nave

//                differenceBetweenSpaceshipAndObjectPosX = posX - (int)objectsToDodge[0].P_Transform.P_PosX;
//                if (differenceBetweenSpaceshipAndObjectPosX <= _bulletClosenessToStartMoving || !_changeDestination)
//                {
//                    differenceBetweenSpaceshipAndObjectPosY = posY + spaceshipFigureLength - (int)mostDangerousObject.P_Transform.P_PosY;
//                    // La bala esta en una posX que chocaria contra la nave
//                    if (_changeDestination)
//                    {

//                        float halfSpaceshipFigureLength = MathF.Round((float)spaceshipFigureLength / 2, MidpointRounding.ToPositiveInfinity);
//                        _direction = differenceBetweenSpaceshipAndObjectPosY == halfSpaceshipFigureLength ? _random.Next(0, 2) == 0 ? (sbyte)-1 : (sbyte)1
//                            : differenceBetweenSpaceshipAndObjectPosY > spaceshipFigureLength / 2 ? (sbyte)1 : (sbyte)-1;

//                        bool positiveDirection = _direction == 1;

//                        _destiny = (sbyte)(posY + (positiveDirection ? spaceshipFigureLength : 0) - differenceBetweenSpaceshipAndObjectPosY);
//                        //_destiny = (sbyte)(posY + (spaceshipFigureLength - differenceBetweenSpaceshipAndObjectPosY * _direction);


//                        sbyte fixedDirection = _destiny >= maxPosYWithMargin ? (sbyte)-1 : _destiny
//                            < minPosYWithMargin ? (sbyte)1 : _direction;

//                        positiveDirection = fixedDirection == 1;

//                        //_destiny = (sbyte)(posY + (spaceshipFigureLength - differenceBetweenSpaceshipAndObjectPosY) * fixedDirection);
//                        _destiny = (sbyte)(posY + (positiveDirection ? spaceshipFigureLength : 0) - differenceBetweenSpaceshipAndObjectPosY);
//                        _direction = fixedDirection;
//                        _changeDestination = false;
//                        _bulletClosenessToStartMoving = null;
//                        //if (fixedDirection == -1)
//                        //{
//                        //    _destiny = (byte)(posY + differenceBetweenSpaceshipAndObjectPosY * -_direction);
//                        //}
//                        //_direction = _destiny >= maxPosYWithMargin ? (sbyte)-1 : _destiny 
//                        //    <= minPosYWithMargin ? (sbyte)1 : _direction;

//                        //int z;
//                        //if (_destiny <= minPosYWithMargin)
//                        //    z = 354;
//                    }


//                    this.P_SpaceshipAttached.P_PosY += this.P_SpaceshipAttached.P_Velocity * _direction * Timer.P_DeltaTime;
//                }
//            }
//            else
//            {
//               _bulletClosenessToStartMoving ??= _random.Next((int)objectsToDodge[0].P_Velocity, (int)(objectsToDodge[0].P_Velocity * _MAX_RANDOMLY_BULLET_POSX_TO_START_MOVING));

//                _changeDestination = true;
//            }
//        }


//        #endregion
//    }
//}