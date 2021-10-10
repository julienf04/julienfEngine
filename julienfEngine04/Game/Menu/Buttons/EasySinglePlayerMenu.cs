﻿using julienfEngine1;
using System;

namespace julienfEngine1
{
    class EasySinglePlayerMenu : GameObject, IClickable
    {
        #region ATRIBUTES

        private readonly Figure _figureEasy = new Figure
            (new string[8]
                {
                    @" _____                    ",
                    @"|  ___|                   ",
                    @"| |__    __ _  ___  _   _ ",
                    @"|  __|  / _` |/ __|| | | |",
                    @"| |___ | (_| |\__ \| |_| |",
                    @"\____/  \__,_||___/ \__, |",
                    @"                     __/ |",
                    @"                    |___/ "
                }, E_ForegroundColors.Gray
            );

        #endregion

        #region CONSTRUCTORS

        public EasySinglePlayerMenu(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureEasy };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            _figureEasy.ForegroundColor = E_ForegroundColors.Green;
        }

        public void OnDeselect()
        {
            _figureEasy.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {
            Scene.UnloadScene(typeof(MainMenuScene));
            Scene.UnloadScene(typeof(ExitMenuScene));
            Scene.UnloadScene(typeof(SinglePlayerMenuScene));
            Scene.UnloadScene(typeof(MultiplayerMenuScene));

            Scene.LoadScene(typeof(GameScene), new object[] { GameScene.E_GameType.SinglePlayerEasy });
            Scene.SetLoadedScene(typeof(GameScene), true);
        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureEasy
        {
            get
            {
                return _figureEasy;
            }
        }

        #endregion
    }
}