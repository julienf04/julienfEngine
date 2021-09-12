using julienfEngine1;
using System;

namespace julienfEngine1
{
    class SimpleBacgroundColor : GameObject
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private Figure _figureSimpleBacgroundColor = new Figure
           (new string[1]
               {
                    @" "
               }
           );

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public SimpleBacgroundColor(E_BackgroundColors backgroundColor, int posX, int posY, bool visible, bool isUI, byte layer)
            : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureSimpleBacgroundColor };
            this.P_GameObjectFigures[0].BackgroundColor = backgroundColor;
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigureSimpleBacgroundColor
        {
            get
            {
                return _figureSimpleBacgroundColor;
            }
        }

        #endregion
    }
}