using julienfEngine1;
using System;
using System.Diagnostics;

namespace julienfEngine1
{
    class ExitMenu : GameObject, IClickable
    {
        #region ATRIBUTES

        public Figure _figureExit = new Figure
            (new string[6]
                {
                    @" _____        _  _   ",
                    @"|  ___|      (_)| |  ",
                    @"| |__  __  __ _ | |_ ",
                    @"|  __| \ \/ /| || __|",
                    @"| |___  >  < | || |_ ",
                    @"\____/ /_/\_\|_| \__|",
                }, E_ForegroundColors.Gray
            );

        #endregion

        #region CONSTRUCTORS

        public ExitMenu(Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureExit };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            _figureExit.ForegroundColor = E_ForegroundColors.Red;
        }

        public void OnDeselect()
        {
            _figureExit.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {
            Scene.SetLoadedScene(typeof(ExitMenuScene), true);
        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureExit
        {
            get
            {
                return _figureExit;
            }
        }

        #endregion
    }
}