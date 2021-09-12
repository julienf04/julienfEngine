using julienfEngine1;
using System;
using System.Diagnostics;

namespace julienfEngine1
{
    class HorizontalLine : GameObject
    {
        #region ATRIBUTES

        #endregion

        #region ENUMS

        public enum E_CurveDirection: byte
        {
            Up,
            Down
        }

        #endregion

        #region CONSTRUCTORS

        public HorizontalLine(byte length, int posX, int posY, bool visible, bool isUI, byte layer)
            : base(posX, posY, visible, isUI, layer)
        {
            string line = "";
            for (int i = 0; i < length; i++) line = line + "-";

            this.P_GameObjectFigures[0].P_Figure = new string[1] { line };
            //figures[0].P_Figure = new string[1] { line };
        }

        public HorizontalLine(byte length, E_CurveDirection curveDirection, int posX, int posY, bool visible, bool isUI, byte layer)
            : base(posX, posY, visible, isUI, layer)
        {
            length--;
            bool directionUp = curveDirection == E_CurveDirection.Up;
            string line = directionUp ? @"\" : "/";
            for (int i = 1; i < length; i++) line += "-";
            line += directionUp ? "/" : @"\";

            this.P_GameObjectFigures[0].P_Figure = new string[1] { line };
            //figures[0].P_Figure = new string[1] { line };
        }

        #endregion

        #region METHODS

        #endregion

        #region PROPERTIES

        #endregion
    }
}