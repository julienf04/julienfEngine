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

        public HorizontalLine(byte length, Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
        {
            string line = "";
            for (int i = 0; i < length; i++) line = line + "-";

            this.P_GameObjectFigures[0].P_Figure = new string[1] { line };
            //figures[0].P_Figure = new string[1] { line };
        }

        public HorizontalLine(byte length, E_CurveDirection curveDirection, Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
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