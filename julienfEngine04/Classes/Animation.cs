using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace julienfEngine1
{
    class Animation
    {
        #region ---ATRIBUTES

        private int[] _sequenceOfFigures;

        private AnimationStates _animationState = AnimationStates.OneShot;

        private int _currentFigureIndex = 0;

        private bool _isRunning = false;

        private int _nextFigureIndexForPingPong = 1;

        private int _timeBetweenFigures = 1; //time in seconds

        private Timer _sequenceTimer = new Timer();

        #endregion

        #region ---CONSTRUCTORS

        public Animation(int sequenceOfFiguresLength)
        {
            _sequenceOfFigures = new int[sequenceOfFiguresLength];

            for (int i = 0; i < _sequenceOfFigures.Length; i++)
            {
                _sequenceOfFigures[i] = i;
            }
        }

        #endregion

        #region ---METHODS

        public void RunAnimation()
        {
            _sequenceTimer.StartMyTimer(0);
            _isRunning = true;
        }

        public void StopAnimation(bool resetAnimation)
        {
            if (resetAnimation)
            {
                _currentFigureIndex = 0;
                _nextFigureIndexForPingPong = 1;

                _sequenceTimer.ResetMyTimerValueAndStop();
            }
            else _sequenceTimer.StopMyTimer();

            _isRunning = false;
        }

        public void NextFigure()
        {
            if (_sequenceTimer.P_MyTimer >= _timeBetweenFigures)
            {
                switch (_animationState)
                {
                    case AnimationStates.OneShot:
                        _currentFigureIndex++;
                        if (_currentFigureIndex == _sequenceOfFigures.Length) StopAnimation(true);
                        break;

                    case AnimationStates.Repeat:
                        _currentFigureIndex++;
                        _currentFigureIndex = _currentFigureIndex % (_sequenceOfFigures.Length);
                        break;

                    case AnimationStates.RepeatReverse:
                        _currentFigureIndex = _currentFigureIndex == 0 ? _sequenceOfFigures.Length - 1 : --_currentFigureIndex;
                        break;

                    case AnimationStates.PingPong:
                        _currentFigureIndex += _nextFigureIndexForPingPong;
                        if (_currentFigureIndex % (_sequenceOfFigures.Length - 1) == 0) _nextFigureIndexForPingPong = -_nextFigureIndexForPingPong;
                        break;
                }

                _sequenceTimer.ResetMyTimerValueAndStop();
                _sequenceTimer.StartMyTimer(0);
            }
        }

        #endregion

        #region ---PROPERTIES

        public int[] P_SequenceOfFigures
        {
            get
            {
                return _sequenceOfFigures;
            }

            set
            {
                if (value.Length >= 0 && value.Length <= _sequenceOfFigures.Length &&
                    value.Min() >= 0 && value.Max() <= _sequenceOfFigures.Max()) _sequenceOfFigures = value;
            }
        }

        public AnimationStates P_AnimationState
        {
            get
            {
                return _animationState;
            }

            set
            {
                if (value == AnimationStates.RepeatReverse) _currentFigureIndex = _sequenceOfFigures.Length - 1;
                _animationState = value;
            }
        }

        public int P_CurrentFigure
        {
            get
            {
                return _currentFigureIndex;
            }
        }

        public bool P_IsRunning
        {
            get
            {
                return _isRunning;
            }

            private set
            {

            }
        }

        public int P_TimeBetweenFigures
        {
            get
            {
                return _timeBetweenFigures;
            }

            set
            {
                _timeBetweenFigures = value;
            }
        }

        #endregion
    }
}