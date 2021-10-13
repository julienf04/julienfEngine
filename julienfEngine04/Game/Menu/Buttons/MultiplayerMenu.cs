using julienfEngine1;
using System;

namespace julienfEngine1
{
    class MultiplayerMenu : GameObject, IClickable
    {
        #region ATRIBUTES

        private readonly Figure _figureMultiplayer = new Figure
            (new string[8]
                {
                    @"___  ___        _  _    _         _                           ",
                    @"|  \/  |       | || |  (_)       | |                          ",
                    @"| .  . | _   _ | || |_  _  _ __  | |  __ _  _   _   ___  _ __ ",
                    @"| |\/| || | | || || __|| || '_ \ | | / _` || | | | / _ \| '__|",
                    @"| |  | || |_| || || |_ | || |_) || || (_| || |_| ||  __/| |   ",
                    @"\_|  |_/ \__,_||_| \__||_|| .__/ |_| \__,_| \__, | \___||_|   ",
                    @"                          | |                __/ |            ",
                    @"                          |_|               |___/             "
                }, E_ForegroundColors.Gray
            );

        #endregion

        #region CONSTRUCTORS

        public MultiplayerMenu(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureMultiplayer };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            _figureMultiplayer.ForegroundColor = E_ForegroundColors.Green;
        }

        public void OnDeselect()
        {
            _figureMultiplayer.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {
            Scene.SetLoadedScene(typeof(MultiplayerMenuScene), false);
        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureMultiplayer
        {
            get
            {
                return _figureMultiplayer;
            }
        }

        #endregion
    }
}