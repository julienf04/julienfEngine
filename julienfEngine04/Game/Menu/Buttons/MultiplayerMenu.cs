using julienfEngine1;
using System;

namespace julienfEngine1
{
    class MultiplayerMenu : GameObject, IClickable
    {
        #region ATRIBUTES

        public Figure _figureMultiplayer = new Figure
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

        public MultiplayerMenu(Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
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
            throw new NotImplementedException();
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