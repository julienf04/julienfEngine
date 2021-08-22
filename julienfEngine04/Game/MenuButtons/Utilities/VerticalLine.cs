using julienfEngine1;
using System;

namespace julienfEngine1
{
    class VerticalLine : GameObject
    {
        #region ATRIBUTES

        #endregion

        #region ENUMS

        public enum E_CurveDirection : byte
        {
            Right,
            Left
        }

        #endregion

        #region CONSTRUCTORS

        public VerticalLine(byte length, Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, myScene, baseFigure, visible, isUI, layer, posX, posY)
        {
            string[] line = new string[length];
            for (int i = 0; i < length; i++) line[i] = "|";

            figures[0].P_Figure = line;
        }

        public VerticalLine(byte length, E_CurveDirection curveDirection, Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, myScene, baseFigure, visible, isUI, layer, posX, posY)
        {
            length--;
            string[] line = new string[length];
            bool directionLeft = curveDirection == E_CurveDirection.Left;
            line[0] = directionLeft ? @"\" : "/";
            for (int i = 1; i < length; i++) line[i] = "|";
            line[length - 1] = directionLeft ? "/" : @"\";

            figures[0].P_Figure = line;
        }

        #endregion

        #region METHODS

        #endregion

        #region PROPERTIES

        #endregion
    }
}