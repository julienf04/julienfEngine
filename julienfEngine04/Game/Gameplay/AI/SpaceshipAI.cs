//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace julienfEngine1
//{
//    abstract class SpaceshipAI
//    {
//        #region ATTRIBUTES

//        private const byte _WEIGHT_MARGIN = 2;

//        private Spaceship _spaceshipAttached;
//        private IEnumerable<IDodgeable> _objectsToDodge;


//        //private Task _taskRunAI;
//        //private bool _runAI = false;

//        #endregion

//        #region CONSTRUCTORS

//        protected SpaceshipAI(Spaceship mySpaceship, IEnumerable<IDodgeable> objectsToDodge)
//        {
            

//            _spaceshipAttached = mySpaceship;
//            _objectsToDodge = objectsToDodge;
//            //_taskRunAI = new Task(RunAsync);
//        }

//        #endregion

//        #region METHODS

//        //public void Run()
//        //{

//        //}

//        //public void RunAsync()
//        //{
//        //    _runAI = true;
//        //    while (_runAI && _taskRunAI.IsCompleted)
//        //    {
//        //        AlgoritmAI();
//        //    }
//        //}

//        public abstract void Run();

//        //public void StopRunning()
//        //{
//        //    _runAI = false;
//        //}

//        protected IDodgeable FindMostDangerousObject(int minPosY, int maxPosY)
//        {
//            IDodgeable[] objectsToDodge = this._objectsToDodge.ToArray();

//            if (objectsToDodge.Length <= 0) return null;

//            int posY = (int)this.P_SpaceshipAttached.P_PosY;
//            int posX = (int)this.P_SpaceshipAttached.P_PosX;
//            int spaceshipFigureLength = this.P_SpaceshipAttached.P_GameObjectFigures[0].P_Figure.Length;
//            int objectWithMinPosY = (int)objectsToDodge[0].P_Transform.P_PosY;
//            int objectWithMaxPosY = objectWithMinPosY;
//            int weightPosY = (int)(objectsToDodge[0].P_Velocity / this.P_SpaceshipAttached.P_Velocity);

//            int totalWeightToOvercome = weightPosY * spaceshipFigureLength;
//            totalWeightToOvercome += weightPosY;

//            IDodgeable mostDangerousObjectMinPosY = null;
//            IDodgeable mostDangerousObjectMaxPosY = null;
//            bool danger = false;
//            IDodgeable lastDodgeable = null;

//            bool safe = false;
//            for (int i = 0; i < objectsToDodge.Length && !safe; i++)
//            {
//                int differenceBetweenSpaceshipAndObjectPosY = posY + spaceshipFigureLength - (int)objectsToDodge[i].P_Transform.P_PosY;
//                int differenceBetweenSpaceshipAndObjectPosX = posX - (int)objectsToDodge[i].P_Transform.P_PosX;

//                if (i <= 0)
//                {
//                    if (!danger)
//                    {
//                        danger = differenceBetweenSpaceshipAndObjectPosY <= spaceshipFigureLength && differenceBetweenSpaceshipAndObjectPosY > 0;
//                        objectWithMaxPosY = (int)objectsToDodge[0].P_Transform.P_PosY;
//                        objectWithMinPosY = objectWithMaxPosY;
//                        mostDangerousObjectMaxPosY = objectsToDodge[0];
//                        mostDangerousObjectMinPosY = mostDangerousObjectMaxPosY;
//                    }

//                    lastDodgeable = objectsToDodge[i];
//                    continue;
//                }

//                int currentWeightPosY;
//                int currentWeightPosX;

//                if (objectsToDodge.Length >= 5)
//                    currentWeightPosX = 0;


//                currentWeightPosY = Math.Abs((int)lastDodgeable.P_Transform.P_PosY - (int)objectsToDodge[i].P_Transform.P_PosY) * weightPosY;
//                currentWeightPosX = Math.Abs((int)lastDodgeable.P_Transform.P_PosX - (int)objectsToDodge[i].P_Transform.P_PosX);

//                if (!danger) danger = differenceBetweenSpaceshipAndObjectPosY <= spaceshipFigureLength && differenceBetweenSpaceshipAndObjectPosY > 0;

//                if (currentWeightPosX + currentWeightPosY <= totalWeightToOvercome)
//                {
//                    if (objectWithMaxPosY < (int)objectsToDodge[i].P_Transform.P_PosY)
//                    {
//                        objectWithMaxPosY = (int)objectsToDodge[i].P_Transform.P_PosY;
//                        mostDangerousObjectMaxPosY = objectsToDodge[i];
//                    }
//                    else if (objectWithMinPosY > (int)objectsToDodge[i].P_Transform.P_PosY)
//                    {
//                        objectWithMinPosY = (int)objectsToDodge[i].P_Transform.P_PosY;
//                        mostDangerousObjectMinPosY = objectsToDodge[i];
//                    }
//                }
//                else safe = true;

//                lastDodgeable = objectsToDodge[i];
//            }

//            lastDodgeable = null;

//            if (!danger) return null;

//            float halfSpaceshipFigureLength = MathF.Round((float)spaceshipFigureLength / 2, MidpointRounding.ToPositiveInfinity);
//            int spaceshipPosY = (int)this._spaceshipAttached.P_PosY + (int)halfSpaceshipFigureLength;

//            int differenceBetweenMostDangerousMaxPosY = Math.Abs(spaceshipPosY - objectWithMaxPosY);
//            int differenceBetweenMostDangerousMinPosY = Math.Abs(spaceshipPosY - objectWithMinPosY);

//            IDodgeable mostDangerous = (differenceBetweenMostDangerousMaxPosY < differenceBetweenMostDangerousMinPosY) && (objectWithMaxPosY + spaceshipFigureLength < maxPosY) ? mostDangerousObjectMaxPosY :
//                (differenceBetweenMostDangerousMinPosY < differenceBetweenMostDangerousMaxPosY) && (objectWithMinPosY - spaceshipFigureLength > minPosY) ? mostDangerousObjectMinPosY :
//                new Random().Next(0, 2) == 0 ? mostDangerousObjectMaxPosY : mostDangerousObjectMinPosY;

//            return mostDangerous;
//        }

//        #endregion

//        #region PROPERTIES

//        protected Spaceship P_SpaceshipAttached
//        {
//            get
//            {
//                return _spaceshipAttached;
//            }
//        }

//        protected IEnumerable<IDodgeable> P_ObjectsToDodge
//        {
//            get
//            {
//                return _objectsToDodge;
//            }
//        }

//        #endregion
//    }
//}