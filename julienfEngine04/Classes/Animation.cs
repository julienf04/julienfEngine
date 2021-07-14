using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class Animation
    {
        #region ---ATRIBUTES

        private int[] _sequenceOfFigures;

        #endregion

        #region ---ENUMS

        public enum AnimationStates
        {
            OneShot,
            Repeat,
            RepeatReverse,
            PingPong
        }

        #endregion
    }
}
