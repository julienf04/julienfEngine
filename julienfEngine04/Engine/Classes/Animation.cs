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

        private double _timeBetweenFigures = 1; //time in seconds

        private readonly Timer _sequenceTimer = new Timer();

        private bool _isNextFigureFrame = false;

        #endregion

        #region ---CONSTRUCTORS

        public Animation(int sequenceOfFiguresLength)
        {
            _sequenceOfFigures = new int[sequenceOfFiguresLength];

            for (int i = 0; i < _sequenceOfFigures.Length; i++) _sequenceOfFigures[i] = i;
        }

        #endregion

        #region ---METHODS

        public void RunAnimation()
        {
            if (this._isRunning) throw new Exception("Animation is already running");
            _sequenceTimer.StartMyTimer(0);
            _isRunning = true;
        }

        public void StopAnimation(bool resetAnimation)
        {
            if (resetAnimation)
            {
                _currentFigureIndex = 0;
                _nextFigureIndexForPingPong = 1;

                _sequenceTimer.ResetMyTimer();
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
                        _currentFigureIndex %= (_sequenceOfFigures.Length);
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

                _sequenceTimer.ResetMyTimer();
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
                if (value.Length <= 0 || value.Length > _sequenceOfFigures.Length)
                    throw new Exception("You cannot set a sequence of figures with different length than the total figures of this GameObject");
                if (value.Min() < 0 || value.Max() > _sequenceOfFigures.Max())
                    throw new Exception("You cannot set figure values greater or less than the maximum or minimum number of figures in the GameObject");

                _sequenceOfFigures = value;
            }
        }

        internal int P_NewSequenceOfFigures
        {
            set
            {
                _sequenceOfFigures = new int[value];
                for (int i = 0; i < _sequenceOfFigures.Length; i++) _sequenceOfFigures[i] = i;
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

        public double P_TimeBetweenFigures
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