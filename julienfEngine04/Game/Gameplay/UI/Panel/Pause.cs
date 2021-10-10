using julienfEngine1;
using System;

namespace julienfEngine1
{
    class Pause : GameObject
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private readonly Figure _figurePause = new Figure
           (new string[6]
               {
                    @"  _____                               ",
                    @" |  __ \                              ",
                    @" | |__) |   __ _   _   _   ___    ___ ",
                    @" |  ___/   / _` | | | | | / __|  / _ \",
                    @" | |      | (_| | | |_| | \__ \ |  __/",
                    @" |_|       \__,_|  \__,_| |___/  \___|"
               }, E_ForegroundColors.Gray
           );

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Pause(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figurePause };
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigurePause
        {
            get
            {
                return _figurePause;
            }
        }

        #endregion
    }
}