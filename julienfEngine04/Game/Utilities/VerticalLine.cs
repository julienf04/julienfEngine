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

        public VerticalLine(byte length, int posX, int posY, bool visible, bool isUI, byte layer)
            : base(posX, posY, visible, isUI, layer)
        {
            string[] line = new string[length];
            for (int i = 0; i < length; i++) line[i] = "|";

            this.P_GameObjectFigures[0].P_Figure = line;
            //figures[0].P_Figure = line;
        }

        public VerticalLine(byte length, E_CurveDirection curveDirection, int posX, int posY, bool visible, bool isUI, byte layer)
            : base(posX, posY, visible, isUI, layer)
        {
            length--;
            string[] line = new string[length];
            bool directionLeft = curveDirection == E_CurveDirection.Left;
            line[0] = directionLeft ? @"\" : "/";
            for (int i = 1; i < length; i++) line[i] = "|";
            line[length - 1] = directionLeft ? "/" : @"\";

            this.P_GameObjectFigures[0].P_Figure = line;
            //figures[0].P_Figure = line;
        }

        #endregion

        #region METHODS

        #endregion

        #region PROPERTIES

        #endregion
    }
}