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

        private Spaceship _spaceshipAttached;
        private Queue<Transform> _objectsToDodge;
        private Task _taskRunAI;
        private bool _runAI = false;

        #endregion

        #region CONSTRUCTORS

        public SpaceshipAI(Spaceship mySpaceship, Queue<Transform> objectsToDodge)
        {
            _spaceshipAttached = mySpaceship;
            _objectsToDodge = objectsToDodge;

            _taskRunAI = new Task(RunAsync);
        }

        #endregion

        #region METHODS

        public void RunAsync()
        {
            _runAI = true;
            while (_runAI && _taskRunAI.IsCompleted)
            {
                AlgoritmAI();
            }
        }

        public abstract void AlgoritmAI();

        public void StopRunning()
        {
            _runAI = false;
        }

        #endregion

        #region PROPERTIES

        protected Spaceship P_SpaceshipAttached
        {
            get
            {
                return _spaceshipAttached;
            }
        }

        protected Queue<Transform> P_ObjectsToDodge
        {
            get
            {
                return _objectsToDodge;
            }
        }

        #endregion
    }
}