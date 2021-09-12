using julienfEngine1;
using System;
using System.Linq;

namespace julienfEngine1
{
    class ArrowMenu : GameObject
    {
        #region ATRIBUTES

        public static readonly Figure[] RO_FigureMenuArrow = new Figure[8]
        {
            new Figure(new string[6]
                      {
                          @"  /|",
                          @" / |",
                          @"/  |",
                          @"\  |",
                          @" \ |",
                          @"  \|"
                      }),

            new Figure(new string[6]
                      {
                          @"|\  ",
                          @"| \ ",
                          @"|  \",
                          @"|  /",
                          @"| / ",
                          @"|/  "
                      }),

            new Figure(new string[4]
                      {
                          @"   /\   ",
                          @"  /  \  ",
                          @" /    \ ",
                          @"/------\"
                      }),

            new Figure(new string[4]
                      {
                          @"\------/",
                          @" \    / ",
                          @"  \  /  ",
                          @"   \/   "
                      }),

            new Figure(new string[4]
                      {
                          @" /",
                          @"/ ",
                          @"\ ",
                          @" \"
                      }),

             new Figure(new string[4]
                      {
                          @"\ ",
                          @" \",
                          @" /",
                          @"/ "
                      }),

             new Figure(new string[2]
                      {
                          @" /\ ",
                          @"/  \"
                      }),

              new Figure(new string[2]
                      {
                          @"\  /",
                          @" \/ "
                      }),
        };




        private uint _currentSelectOption = 0; //Current select option dimension 1

        private IClickable[] _currentMenu;


        // Support variables
        private double _timerChangeArrowVelocity = 0;

        private Func<E_Keyboard, bool> _predicateForCheckArrowMovement;
        private E_Keyboard _currentKeyPressed;

        #endregion

        #region ENUMS

        public enum E_PointSide : byte
        {
            PointLeft,
            PointRight,
            PointUp,
            PointDown
        }

        public enum E_ArrowSidesAndSizes : byte
        {
            BigArrowPointLeft,
            BigArrowPointRight,
            BigArrowPointUp,
            BigArrowPointDown,
            SmallArrowPointLeft,
            SmallArrowPointRight,
            SmallArrowPointUp,
            SmallArrowPointDown
        }

        #endregion

        #region CONSTRUCTORS

        public ArrowMenu(IClickable[] currentMenu, int posX, int posY, bool visible, bool isUI, byte layer, Figure[] figures, byte baseFigure)
            : base(posX, posY, visible, isUI, layer, figures, baseFigure)
        {
            _currentMenu = currentMenu;
            _predicateForCheckArrowMovement = new Func<E_Keyboard, bool>(IsPressed);
        }

        #endregion

        #region METHODS

        private bool IsPressed(E_Keyboard key)
        {
            if (Input.GetKey(key))
            {
                _currentKeyPressed = key;
                return true;
            }
            return false;
        }

        public void MoveArrowToCurrentMenu(double cooldownToMove, double arrowVelocity, int buttonDistance, E_PointSide arrowPointSide, E_Keyboard keyToMoveLeft, E_Keyboard keyToMoveRight)
        {
            if (Input.GetKey(keyToMoveRight))
            {
                if (Input.GetKeyDown(keyToMoveRight))
                {
                    this.MoveOneStepRight(arrowPointSide, buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMove)
                {
                    this.MoveOneStepRight(arrowPointSide, buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else if (Input.GetKey(keyToMoveLeft))
            {
                if (Input.GetKeyDown(keyToMoveLeft))
                {
                    this.MoveOneStepLeft(arrowPointSide, buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMove)
                {
                    this.MoveOneStepLeft(arrowPointSide, buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else _timerChangeArrowVelocity = 0;
        }

        public void MoveArrowToCurrentMenu(double cooldownToMove, double arrowVelocity, int buttonDistance,E_PointSide arrowPointSide, E_Keyboard[] keysToMoveLeft, E_Keyboard[] keysToMoveRight)
        {
            if (keysToMoveRight.Any(_predicateForCheckArrowMovement) && keysToMoveRight.Length >= 1)
            {
                if (Input.GetKeyDown(_currentKeyPressed))
                {
                    this.MoveOneStepRight(arrowPointSide, buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMove)
                {
                    this.MoveOneStepRight(arrowPointSide, buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else if (keysToMoveLeft.Any(_predicateForCheckArrowMovement) && keysToMoveLeft.Length >= 1)
            {
                if (Input.GetKeyDown(_currentKeyPressed))
                {
                    this.MoveOneStepLeft(arrowPointSide, buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMove)
                {
                    this.MoveOneStepLeft(arrowPointSide, buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else _timerChangeArrowVelocity = 0;

            //if (keysToMoveRight.Any(currentKey => Input.GetKey(currentKey)) && keysToMoveRight.Length >= 1)
            //{
            //    if (!_keyDownPressed || _timerChangeArrowVelocity > cooldownToMove)
            //    {
            //        this.MoveOneStepRight(arrowPointSide, buttonDistance);

            //        if (_keyDownPressed) _timerChangeArrowVelocity = arrowVelocity;
            //        _keyDownPressed = true;
            //    }
                
            //    _timerChangeArrowVelocity += Timer.P_DeltaTime;
            //}
            //else if (keysToMoveLeft.Any(currentKey => Input.GetKey(currentKey) && keysToMoveLeft.Length >= 1))
            //{
            //    if (!_keyUpPressed || _timerChangeArrowVelocity > cooldownToMove)
            //    {
            //        this.MoveOneStepLeft(arrowPointSide, buttonDistance);

            //        if (_keyUpPressed) _timerChangeArrowVelocity = arrowVelocity;
            //        _keyUpPressed = true;
            //    }

            //    _timerChangeArrowVelocity += Timer.P_DeltaTime;
            //}
            //else
            //{
            //    _keyUpPressed = false;
            //    _keyDownPressed = false;
            //    _timerChangeArrowVelocity = 0;
            //}
        }

        public void MoveArrowToCurrentMenu(double cooldownToMove, double arrowVelocity, int buttonDistance, E_PointSide[] arrowPointSides, E_Keyboard keyToMoveLeft, E_Keyboard keyToMoveRight)
        {
            if (Input.GetKey(keyToMoveRight))
            {
                if (Input.GetKeyDown(keyToMoveRight))
                {
                    this.MoveOneStepRight(arrowPointSides[_currentSelectOption], buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMove)
                {
                    this.MoveOneStepRight(arrowPointSides[_currentSelectOption], buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else if (Input.GetKey(keyToMoveLeft))
            {
                if (Input.GetKeyDown(keyToMoveLeft))
                {
                    this.MoveOneStepLeft(arrowPointSides[_currentSelectOption], buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMove)
                {
                    this.MoveOneStepLeft(arrowPointSides[_currentSelectOption], buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else _timerChangeArrowVelocity = 0;
        }

        public void MoveArrowToCurrentMenu(double cooldownToMove, double arrowVelocity, int buttonDistance, E_PointSide[] arrowPointSides, E_Keyboard[] keysToMoveLeft, E_Keyboard[] keysToMoveRight)
        {
            if (keysToMoveRight.Any(_predicateForCheckArrowMovement) && keysToMoveRight.Length >= 1)
            {
                if (Input.GetKeyDown(_currentKeyPressed))
                {
                    this.MoveOneStepRight(arrowPointSides[_currentSelectOption], buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMove)
                {
                    this.MoveOneStepRight(arrowPointSides[_currentSelectOption], buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else if (keysToMoveLeft.Any(_predicateForCheckArrowMovement) && keysToMoveLeft.Length >= 1)
            {
                if (Input.GetKeyDown(_currentKeyPressed))
                {
                    this.MoveOneStepLeft(arrowPointSides[_currentSelectOption], buttonDistance);
                }
                else if (_timerChangeArrowVelocity > cooldownToMove)
                {
                    this.MoveOneStepLeft(arrowPointSides[_currentSelectOption], buttonDistance);
                    _timerChangeArrowVelocity = arrowVelocity;
                }
                _timerChangeArrowVelocity += Timer.P_DeltaTime;
            }
            else _timerChangeArrowVelocity = 0;
        }


        public void DoClick()
        {
            _currentMenu[this._currentSelectOption].OnClick();
        }



        public void MoveOneStepLeft(E_PointSide pointSide, int buttonDistance)
        {
            _currentMenu[_currentSelectOption].OnDeselect();
            _currentSelectOption += (uint)(_currentMenu.Length - 1);
            _currentSelectOption %= (uint)_currentMenu.Length;
            GameObject currentButton = (GameObject)_currentMenu[_currentSelectOption];
            _currentMenu[_currentSelectOption].OnSelect();

            switch (pointSide)
            {
                case E_PointSide.PointLeft:
                    this.P_PosY = currentButton.P_PosY + (currentButton.P_GameObjectFigures[0].P_Figure.Length / 2) - (this.P_GameObjectFigures[0].P_Figure.Length / 2);
                    this.P_PosX = currentButton.P_PosX + currentButton.P_GameObjectFigures[0].P_Figure[0].Length + buttonDistance;
                    break;
                case E_PointSide.PointRight:
                    this.P_PosY = currentButton.P_PosY + (currentButton.P_GameObjectFigures[0].P_Figure.Length / 2) - (this.P_GameObjectFigures[0].P_Figure.Length / 2);
                    this.P_PosX = currentButton.P_PosX - this.P_GameObjectFigures[0].P_Figure[0].Length - buttonDistance;
                    break;
                case E_PointSide.PointUp:
                    this.P_PosY = currentButton.P_PosY + currentButton.P_GameObjectFigures[0].P_Figure.Length + buttonDistance;
                    this.P_PosX = currentButton.P_PosX + (currentButton.P_GameObjectFigures[0].P_Figure[0].Length / 2) - (this.P_GameObjectFigures[0].P_Figure[0].Length / 2);
                    break;
                case E_PointSide.PointDown:
                    this.P_PosY = currentButton.P_PosY - buttonDistance;
                    this.P_PosX = currentButton.P_PosX + (currentButton.P_GameObjectFigures[0].P_Figure[0].Length / 2) - (this.P_GameObjectFigures[0].P_Figure[0].Length / 2);
                    break;
            }
        }
        
        public void MoveOneStepRight(E_PointSide pointSide, int buttonDistance)
        {
            _currentMenu[_currentSelectOption].OnDeselect();
            _currentSelectOption++;
            _currentSelectOption %= (uint)_currentMenu.Length;
            GameObject currentButton = (GameObject)_currentMenu[_currentSelectOption];
            _currentMenu[_currentSelectOption].OnSelect();

            switch (pointSide)
            {
                case E_PointSide.PointLeft:
                    this.P_PosY = currentButton.P_PosY + (currentButton.P_GameObjectFigures[0].P_Figure.Length / 2) - (this.P_GameObjectFigures[0].P_Figure.Length / 2);
                    this.P_PosX = currentButton.P_PosX + currentButton.P_GameObjectFigures[0].P_Figure[0].Length + buttonDistance;
                    break;
                case E_PointSide.PointRight:
                    this.P_PosY = currentButton.P_PosY + (currentButton.P_GameObjectFigures[0].P_Figure.Length / 2) - (this.P_GameObjectFigures[0].P_Figure.Length / 2);
                    this.P_PosX = currentButton.P_PosX - this.P_GameObjectFigures[0].P_Figure[0].Length - buttonDistance;
                    break;
                case E_PointSide.PointUp:
                    this.P_PosY = currentButton.P_PosY + currentButton.P_GameObjectFigures[0].P_Figure.Length + buttonDistance;
                    this.P_PosX = currentButton.P_PosX + (currentButton.P_GameObjectFigures[0].P_Figure[0].Length / 2) - (this.P_GameObjectFigures[0].P_Figure[0].Length / 2);
                    break;
                case E_PointSide.PointDown:
                    this.P_PosY = currentButton.P_PosY - buttonDistance;
                    this.P_PosX = currentButton.P_PosX + (currentButton.P_GameObjectFigures[0].P_Figure[0].Length / 2) - (this.P_GameObjectFigures[0].P_Figure[0].Length / 2);
                    break;
            }
        }

        #endregion

        #region PROPERTIES

        public uint P_CurrentSelectOption
        {
            get
            {
                return _currentSelectOption;
            }
            set
            {
                _currentMenu[_currentSelectOption].OnDeselect();
                _currentSelectOption = value;
                _currentMenu[_currentSelectOption].OnSelect();
            }
        }

        #endregion
    }
}