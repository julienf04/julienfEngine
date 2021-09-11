using julienfEngine1;
using System;

namespace julienfEngine1
{
    class HardSinglePlayerMenu : GameObject, IClickable
    {
        #region ATRIBUTES

        private Figure _figureHard = new Figure
            (new string[6]
                {
                    @" _   _               _ ",
                    @"| | | |             | |",
                    @"| |_| | __ _ _ __ __| |",
                    @"|  _  |/ _` | '__/ _` |",
                    @"| | | | (_| | | | (_| |",
                    @"\_| |_/\__,_|_|  \__,_|"
                }, E_ForegroundColors.Gray
            );

        #endregion

        #region CONSTRUCTORS

        public HardSinglePlayerMenu(Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureHard };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            this._figureHard.ForegroundColor = E_ForegroundColors.Green;
        }

        public void OnDeselect()
        {
            this._figureHard.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {

        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureHard
        {
            get
            {
                return _figureHard;
            }
        }

        #endregion
    }
}