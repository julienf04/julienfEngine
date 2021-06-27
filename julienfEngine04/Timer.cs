using System.Diagnostics;

namespace julienfEngine1
{
    class Timer //This class is a class that manages the time
    {
        #region ---ATRIBUTES;

        private static Stopwatch _stTime = new Stopwatch(); //this variable is a variable that shows the time elapsed since the game started
        private static Stopwatch _stDeltaTime = new Stopwatch(); //This variable is a variable that shows the time elapsed since de last frame

        private Stopwatch _stMyTimer = new Stopwatch(); //This variable is for managing a timer given to the user
        private double _myTimer = 0; //This timer is a final timer that going to be showed to the user. The user can set a start timer value

        #endregion

        #region ---METHODS;

        public static void StartTime()
        {
            _stTime.Start();
        }

        public static void StartDeltaTime()
        {
            _stDeltaTime.Start();
        }

        public static void ResetDeltaTime()
        {
            _stDeltaTime.Reset();
        }

        public void StartMyTimer(double startInTime)
        {
            _myTimer = startInTime;
            _stMyTimer.Start();
        }

        public void StopMyTimer()
        {
            _stMyTimer.Stop();
        }

        public void ResetAndStopMyTimer()
        {
            _stMyTimer.Reset();
        }


        #endregion

        #region ---PROPIERTIES;

        public static double P_TimeElapsed
        {
            get
            {
                return (double)_stTime.ElapsedMilliseconds / 1000;
            }
        }

        public static double P_DeltaTime
        {
            get
            {
                return _stDeltaTime.ElapsedMilliseconds / 1000;
            }
        }

        public double P_MyTimer
        {
            get
            {
                return _myTimer + _stMyTimer.ElapsedMilliseconds / 1000;
            }
        }

        public double P_MyTimerReverse
        {
            get
            {
                return _myTimer - _stMyTimer.ElapsedMilliseconds / 1000;
            }
        }

        #endregion
    }
}