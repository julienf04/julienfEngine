using julienfEngine1;
using System;

namespace julienfEngine1
{
    class TextMessage : GameObject
    {
        #region ATRIBUTES

        #endregion

        #region CONSTRUCTORS

        public TextMessage(string message, Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, myScene, baseFigure, visible, isUI, layer, posX, posY)
        {
            figures[0].P_Figure = new string[1] { message };
        }

        #endregion

        #region METHODS

        #endregion

        #region PROPERTIES

        #endregion
    }
}