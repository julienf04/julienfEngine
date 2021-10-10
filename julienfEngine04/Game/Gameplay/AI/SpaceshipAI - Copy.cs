using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace julienfEngine1
{
    abstract class SpaceshipAI
    {
        #region ATTRIBUTES

        private const byte _WEIGHT_MARGIN = 2;

        private Spaceship _spaceshipAttached;
        private IEnumerable<IDodgeable> _objectsToDodge;


        //private Task _taskRunAI;
        //private bool _runAI = false;

        #endregion

        #region CONSTRUCTORS

        protected SpaceshipAI(Spaceship mySpaceship, IEnumerable<IDodgeable> objectsToDodge)
        {
            _spaceshipAttached = mySpaceship;
            _objectsToDodge = objectsToDodge;
            //_taskRunAI = new Task(RunAsync);
            _spaceshipAttached.P_MaxPosY--;
            //_spaceshipAttached.P_MinPosY++;
        }

        #endregion

        #region METHODS

        //public void Run()
        //{

        //}

        //public void RunAsync()
        //{
        //    _runAI = true;
        //    while (_runAI && _taskRunAI.IsCompleted)
        //    {
        //        AlgoritmAI();
        //    }
        //}

        public abstract void Run();

        //public void StopRunning()
        //{
        //    _runAI = false;
        //}

        #endregion

        #region PROPERTIES

        protected Spaceship P_SpaceshipAttached
        {
            get
            {
                return _spaceshipAttached;
            }
        }

        protected IEnumerable<IDodgeable> P_ObjectsToDodge
        {
            get
            {
                return _objectsToDodge;
            }
        }

        #endregion
    }
}