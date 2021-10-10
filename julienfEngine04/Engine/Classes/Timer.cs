using System;
using System.Diagnostics;

namespace julienfEngine1
{
    class Timer //This class is a class that manages the time
    {
        #region ---ATRIBUTES;

        private static Stopwatch _stTime = new Stopwatch(); //this variable is a variable that counts the time elapsed since the game started
        private static double _time = 0; //This variable is a variable that shows the time elapsed since de last frame
        private static Stopwatch _stDeltaTime = new Stopwatch(); //This variable is a variable that counts the time elapsed since de last frame
        private static float _deltaTime = 0; //This variable is a variable that shows the time elapsed since de last frame

        private Stopwatch _stMyTimer = new Stopwatch(); //This variable is for managing a timer given to the user
        private double _myTimer = 0; //This timer is a final timer that going to be showed to the user. The user can set a start timer value


        private static int _limitFPS = 60;
                 
        private static float _averageFPS = 0; // Average fps taken by this lap

        private static float _countOfDeltaTime = 0; // Average fps taken each 60 frames
        private static int _countOfFPS = 0; // This variable count the fps until _averageFPS, in loop
        private double _timeScale = 1;

        #endregion

        #region ---METHODS;

        internal static void StartTime()
        {
            _stTime.Start();
        }

        internal static void StartDeltaTime()
        {
            _stDeltaTime.Start();
        }

        internal static void ResetDeltaTime()
        {
            _deltaTime = (float)_stDeltaTime.ElapsedMilliseconds / 1000;
            
            if (_countOfFPS >= _limitFPS)
            {
                _averageFPS = _countOfDeltaTime / _limitFPS;

                _countOfFPS = 0;
                _countOfDeltaTime = 0;
            }
            else
            {
                _countOfFPS++;
                _countOfDeltaTime += _deltaTime;
            }

            _stDeltaTime.Reset();
        }

        public void StartMyTimer(float startInTime)
        {
            if (_stMyTimer.IsRunning) throw new Exception("You cannot start a timer already started");

            _timeScale = 1;
            _myTimer = startInTime;
            _stMyTimer.Start();
        }

        public void StartMyTimer(float startInTime, double timeScale)
        {
            if (_stMyTimer.IsRunning) throw new Exception("You cannot start a timer already started");

            _timeScale = timeScale;
            _myTimer = startInTime;
            _stMyTimer.Start();
        }

        public void StopMyTimer()
        {
            _stMyTimer.Stop();
        }

        public void ContinueMyTimer()
        {
            if (_stMyTimer.IsRunning) throw new Exception("You cannot continue a timer that is already running");

            _stMyTimer.Start();
        }

        public void ResetMyTimer()
        {
            _stMyTimer.Reset();
        }


        #endregion

        #region ---PROPIERTIES;

        public static double P_Time
        {
            get
            {
                _time = _stTime.ElapsedMilliseconds / 1000;
                return _time;
            }
        }

        public static float P_DeltaTime
        {
            get
            {
                return _deltaTime;
            }
        }

        public static double P_CurrentTimeOfDeltaTime
        {
            get
            {
                return (double)_stDeltaTime.ElapsedMilliseconds / 1000;
            }
        }

        public double P_MyTimer
        {
            get
            {
                return _myTimer + ((double)_stMyTimer.ElapsedMilliseconds / 1000) * _timeScale;
            }
        }

        //public double P_MyTimerReverse
        //{
        //    get
        //    {
        //        return _myTimer - (double)_stMyTimer.ElapsedMilliseconds / 1000;
        //    }
        //}

        public static float P_AverageFPS
        {
            get
            {
                return _averageFPS;
            }
        }

        internal static int P_LimitFPS
        {
            get
            {
                return _limitFPS;
            }
            set
            {
                _limitFPS = value;
            }
        }


        #endregion
    }
}