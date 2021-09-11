using julienfEngine1;
using System;

namespace julienfEngine1
{
    class Winner : GameObject
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private Figure _figureWinner = new Figure
           (new string[8]
               {
                    @"           (        )     )       (     ",
                    @" (  (      )\ )  ( /(  ( /(       )\ )  ",
                    @" )\))(   '(()/(  )\()) )\()) (   (()/(  ",
                    @"((_)()\ )  /(_))((_)\ ((_)\  )\   /(_)) ",
                    @"_(())\_)()(_))   _((_) _((_)((_) (_))   ",
                    @"\ \((_)/ /|_ _| | \| || \| || __|| _ \  ",
                    @" \ \/\/ /  | |  | .` || .` || _| |   /  ",
                    @"  \_/\_/  |___| |_|\_||_|\_||___||_|_\  "
               }, E_ForegroundColors.Gray
           );

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Winner(Figure[] figures, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                                  int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureWinner };
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigureWinner
        {
            get
            {
                return _figureWinner;
            }
        }

        #endregion
    }
}