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

        public No(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
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
            Scene.SetLoadedScene(typeof(MainMenuScene), false);
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