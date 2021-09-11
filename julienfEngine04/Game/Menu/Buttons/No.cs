using julienfEngine1;
using System;

namespace julienfEngine1
{
    class No : GameObject, IClickable
    {
        #region ATRIBUTES

        public Figure _figureNo = new Figure
            (new string[6]
                {
                    @" _   _  _____ ",
                    @"| \ | ||  _  |",
                    @"|  \| || | | |",
                    @"| . ` || | | |",
                    @"| |\  |\ \_/ /",
                    @"\_| \_/ \___/ "
                }, E_ForegroundColors.Gray
            );

        #endregion

        #region CONSTRUCTORS

        public No(Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureNo };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            _figureNo.ForegroundColor = E_ForegroundColors.Green;
        }

        public void OnDeselect()
        {
            _figureNo.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {
            Scene.SetLoadedScene(typeof(MainMenuScene), true);
        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureNo
        {
            get
            {
                return _figureNo;
            }
        }

        #endregion
    }
}