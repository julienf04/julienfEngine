using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace julienfEngine1
{
    class Animation
    {
        #region ---ATRIBUTES

        private int[] _sequenceOfFigures;

        private E_AnimationStates _animationState = E_AnimationStates.OneShot;

        private int _currentFigureIndex = 0;

        private bool _isRunning = false;

        private int _nextFigureIndexForPingPong = 1;

        private int _timeBetweenFigures = 1; //time in seconds

        private Timer _sequenceTimer = new Timer();

        private bool _isNextFigureFrame = false;

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

        public void NextFigure() //Return true if next figure should be processed
        {
            if (_sequenceTimer.P_MyTimer >= _timeBetweenFigures)
            {
                switch (_animationState)
                {
                    case E_AnimationStates.OneShot:
                        _currentFigureIndex++;
                        if (_currentFigureIndex >= _sequenceOfFigures.Length) StopAnimation(true);
                        break;

                    case E_AnimationStates.Repeat:
                        _currentFigureIndex++;
                        _currentFigureIndex = _currentFigureIndex % (_sequenceOfFigures.Length);
                        break;

                    case E_AnimationStates.RepeatReverse:
                        ////Here there are 2 options to do it this. Both of options are the same optimization
                        //Option 1:
                        _currentFigureIndex = _currentFigureIndex == 0 ? _sequenceOfFigures.Length - 1 : _currentFigureIndex >= _sequenceOfFigures.Length ? _sequenceOfFigures.Length - (_currentFigureIndex % _sequenceOfFigures.Length) : _currentFigureIndex - 1;

                        //Option 2:
                        //totalIterations++;
                        //_currentFigureIndex = (_sequenceOfFigures.Length - 1) - (totalIterations % _sequenceOfFigures.Length);
                        break;
                    case E_AnimationStates.PingPong:
                        if (_currentFigureIndex % (_sequenceOfFigures.Length - 1) == 0) _nextFigureIndexForPingPong = -_nextFigureIndexForPingPong;
                        _currentFigureIndex += _nextFigureIndexForPingPong;
                        break;
                }

                _sequenceTimer.ResetMyTimerValueAndStop();
                _sequenceTimer.StartMyTimer(0);

                _isNextFigureFrame = true;
            }

            _isNextFigureFrame = false;
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

        public E_AnimationStates P_AnimationState
        {
            get
            {
                return _animationState;
            }

            set
            {
                if (value == E_AnimationStates.RepeatReverse && _animationState != value) _currentFigureIndex = _sequenceOfFigures.Length - 1;
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

        public bool P_IsNextFigureFrame
        {
            get
            {
                return _isNextFigureFrame;
            }
        }

        #endregion
    }
}