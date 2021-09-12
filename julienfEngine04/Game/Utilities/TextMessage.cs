using julienfEngine1;
using System;

namespace julienfEngine1
{
    class TextMessage : GameObject
    {
        #region ATRIBUTES

        #endregion

        #region CONSTRUCTORS

        public TextMessage(string message, int posX, int posY, bool visible, bool isUI, byte layer)
            : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures[0].P_Figure = new string[1] { message };
            //figures[0].P_Figure = new string[1] { message };
        }

        #endregion

        #region METHODS

        #endregion

        #region PROPERTIES

        #endregion
    }
}