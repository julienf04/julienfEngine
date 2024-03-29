﻿using julienfEngine1;
using System;

namespace julienfEngine1
{
    class SinglePlayerMenu : GameObject, IClickable
    {
        #region ATRIBUTES

        public Figure _figureSinglePlayer = new Figure
            (new string[8]
                {
                    @" _____  _                _                _                           ",
                    @"/  ___|(_)              | |              | |                          ",
                    @"\ `--.  _  _ __    __ _ | |  ___   _ __  | |  __ _  _   _   ___  _ __ ",
                    @" `--. \| || '_ \  / _` || | / _ \ | '_ \ | | / _` || | | | / _ \| '__|",
                    @"/\__/ /| || | | || (_| || ||  __/ | |_) || || (_| || |_| ||  __/| |   ",
                    @"\____/ |_||_| |_| \__, ||_| \___| | .__/ |_| \__,_| \__, | \___||_|   ",
                    @"                   __/ |          | |                __/ |            ",
                    @"                  |___/           |_|               |___/             "
                }, E_ForegroundColors.Gray
            );

        #endregion

        #region CONSTRUCTORS

        public SinglePlayerMenu(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureSinglePlayer };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            _figureSinglePlayer.ForegroundColor = E_ForegroundColors.Green;
        }

        public void OnDeselect()
        {
            _figureSinglePlayer.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {
            Scene.SetLoadedScene(typeof(SinglePlayerMenuScene), false);
        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureSinglePlayer
        {
            get
            {
                return _figureSinglePlayer;
            }
        }

        #endregion
    }
}