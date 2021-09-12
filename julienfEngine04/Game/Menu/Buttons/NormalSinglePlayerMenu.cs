using julienfEngine1;
using System;

namespace julienfEngine1
{
    class NormalSinglePlayerMenu : GameObject, IClickable
    {
        #region ATRIBUTES

        private Figure _figureNormal = new Figure
          (new string[6]
              {
                    @" _   _                            _ ",
                    @"| \ | |                          | |",
                    @"|  \| | ___  _ __ _ __ ___   __ _| |",
                    @"| . ` |/ _ \| '__| '_ ` _ \ / _` | |",
                    @"| |\  | (_) | |  | | | | | | (_| | |",
                    @"\_| \_/\___/|_|  |_| |_| |_|\__,_|_|"
              }, E_ForegroundColors.Gray
          );

        #endregion

        #region CONSTRUCTORS

        public NormalSinglePlayerMenu(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureNormal };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            this._figureNormal.ForegroundColor = E_ForegroundColors.Green;
        }

        public void OnDeselect()
        {
            this._figureNormal.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {

        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureNormal
        {
            get
            {
                return _figureNormal;
            }
        }

        #endregion
    }
}