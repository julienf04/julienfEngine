﻿using System;

namespace julienfEngine1
{
    class SinglePlayerMenuScene : Scene
    {
        // Declare every attributes of this scene
        #region ATTRIBUTES

        private IClickable[] buttonsMainMenu;


        private static ArrowMenu arrowMenu;

        private const double cooldownToMoveArrow = 0.55;
        private const double arrowVelocity = 0.30;
        private const byte buttonDistance = 3;
        private const ArrowMenu.E_PointSide _arrowPointSide = ArrowMenu.E_PointSide.PointLeft;

        private double _timerChangeArrowVelocity = 0;
        private byte _arrowDimensionX = 1;

        private const byte _dimensionBack = 0;
        private const byte _dimensionButtons = 1;

        #endregion

        // Initialize every attribute and create a game logic for this scene
        #region GAME METHODS

        // This runs when this scene is loaded
        public SinglePlayerMenuScene()
        {

        }

        // This runs when this scene is setted
        public override void Start()
        {
            buttonsMainMenu = new IClickable[4]
            {
                new EasySinglePlayerMenu(35, 5, true, true, 0),
                new NormalSinglePlayerMenu(35, 17, true, true, 0),
                new HardSinglePlayerMenu(35, 30, true, true, 0),
                new Back(15, 43, true, true, 0)
            };

            arrowMenu = new ArrowMenu(buttonsMainMenu, 64, 6, true, true, 0, ArrowMenu.RO_FigureMenuArrow, (byte)ArrowMenu.E_ArrowSidesAndSizes.BigArrowPointLeft);
            arrowMenu.P_CurrentSelectOption = 0;
        }

        // This runs every frame
        public override void Update()
        {
            switch (_arrowDimensionX)
            {
                case _dimensionBack:

                    if (Input.GetKeyDown(E_Keyboard.RightArrow))
                    {
                        arrowMenu.P_CurrentSelectOption = 1;
                        arrowMenu.P_Visible = true;
                    }

                    break;

                case _dimensionButtons:

                    if (Input.GetKey(E_Keyboard.DownArrow) || Input.GetKey(E_Keyboard.S))
                    {
                        if (Input.GetKeyDown(Input.P_LastKeyPressed))
                        {
                            arrowMenu.MoveOneStepRight(_arrowPointSide, buttonDistance);
                        }
                        else if (_timerChangeArrowVelocity > cooldownToMoveArrow)
                        {
                            arrowMenu.MoveOneStepRight(_arrowPointSide, buttonDistance);
                            _timerChangeArrowVelocity = arrowVelocity;
                        }
                        _timerChangeArrowVelocity += Timer.P_DeltaTime;
                    }
                    else if (Input.GetKey(E_Keyboard.UpArrow) || Input.GetKey(E_Keyboard.W))
                    {
                        if (Input.GetKeyDown(Input.P_LastKeyPressed))
                        {
                            arrowMenu.MoveOneStepLeft(_arrowPointSide, buttonDistance);
                        }
                        else if (_timerChangeArrowVelocity > cooldownToMoveArrow)
                        {
                            arrowMenu.MoveOneStepLeft(_arrowPointSide, buttonDistance);
                            _timerChangeArrowVelocity = arrowVelocity;
                        }
                        _timerChangeArrowVelocity += Timer.P_DeltaTime;
                    }
                    else _timerChangeArrowVelocity = 0;

                    break;
            }

            if (Input.GetKeyDown(E_Keyboard.Enter) || Input.GetKeyDown(E_Keyboard.SpaceBar)) arrowMenu.DoClick();
        }

        #endregion
    }
}