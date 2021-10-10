using julienfEngine1;
using System;

namespace julienfEngine1
{
    class Back : GameObject, IClickable
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private readonly Figure _figureBack = new Figure
           (new string[5]
               {
                    @"    ____             __  ",
                    @"   / __ )____ ______/ /__",
                    @"  / __  / __ `/ ___/ //_/",
                    @" / /_/ / /_/ / /__/ ,<   ",
                    @"/_____/\__,_/\___/_/|_|  "

               }, E_ForegroundColors.Gray
           );

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Back(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureBack };
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        public void OnSelect()
        {
            this._figureBack.ForegroundColor = E_ForegroundColors.Red;
        }

        public void OnDeselect()
        {
            this._figureBack.ForegroundColor = E_ForegroundColors.White;
        }

        public void OnClick()
        {
            Scene.SetLoadedScene(typeof(MainMenuScene), false);
        }

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigureArrowBack
        {
            get
            {
                return _figureBack;
            }
        }

        #endregion
    }
}