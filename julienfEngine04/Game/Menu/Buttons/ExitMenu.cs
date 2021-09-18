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

        public ExitMenu(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
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
            Scene.SetLoadedScene(typeof(ExitMenuScene), false);
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