using julienfEngine1;
using System;
using System.Diagnostics;

namespace julienfEngine1
{
    class Yes : GameObject, IClickable
    {
        #region ATRIBUTES

        public Figure _figureYes = new Figure
            (new string[6]
                {
                    @"__   _______ _____ ",
                    @"\ \ / /  ___/  ___|",
                    @" \ V /| |__ \ `--. ",
                    @"  \ / |  __| `--. \",
                    @"  | | | |___/\__/ /",
                    @"  \_/ \____/\____/ "
                }, E_ForegroundColors.Gray
            );
        #endregion

        #region CONSTRUCTORS

        public Yes(Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureYes };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            _figureYes.ForegroundColor = E_ForegroundColors.Red;
        }

        public void OnDeselect()
        {
            _figureYes.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {
            Process.GetCurrentProcess().Kill();
        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureYes
        {
            get
            {
                return _figureYes;
            }
        }

        #endregion
    }
}