using julienfEngine1;
using System;

namespace julienfEngine1
{
    class Winner : GameObject
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject

        private string[] _textWinner = new string[8]
        {
            @"           (        )     )       (    ",
            @" (  (      )\ )  ( /(  ( /(       )\ ) ",
            @" )\))(   '(()/(  )\()) )\()) (   (()/( ",
            @"((_)()\ )  /(_))((_)\ ((_)\  )\   /(_))",
            @"_(())\_)()(_))   _((_) _((_)((_) (_))  ",
            @"\ \((_)/ /|_ _| | \| || \| || __|| _ \ ",
            @" \ \/\/ /  | |  | .` || .` || _| |   / ",
            @"  \_/\_/  |___| |_|\_||_|\_||___||_|_\ "
        };

        private string[] _textWinnerP1 = new string[8]
        {
            @"           (        )     )       (        (          ",
            @" (  (      )\ )  ( /(  ( /(       )\ )     )\ )    )  ",
            @" )\))(   '(()/(  )\()) )\()) (   (()/(    (()/( ( /(  ",
            @"((_)()\ )  /(_))((_)\ ((_)\  )\   /(_))    /(_)))\()) ",
            @"_(())\_)()(_))   _((_) _((_)((_) (_))     (_)) ((_)\  ",
            @"\ \((_)/ /|_ _| | \| || \| || __|| _ \    | _ \ / (_) ",
            @" \ \/\/ /  | |  | .` || .` || _| |   /    |  _/ | |   ",
            @"  \_/\_/  |___| |_|\_||_|\_||___||_|_\    |_|   |_|   "
        };

        private string[] _textWinnerP2 = new string[8]
       {
            @"           (        )     )       (      (          ",
            @" (  (      )\ )  ( /(  ( /(       )\ )   )\ )    )  ",
            @" )\))(   '(()/(  )\()) )\()) (   (()/(  (()/( ( /(  ",
            @"((_)()\ )  /(_))((_)\ ((_)\  )\   /(_))  /(_)))(_)) ",
            @"_(())\_)()(_))   _((_) _((_)((_) (_))   (_)) ((_)   ",
            @"\ \((_)/ /|_ _| | \| || \| || __|| _ \  | _ \|_  )  ",
            @" \ \/\/ /  | |  | .` || .` || _| |   /  |  _/ / /   ",
            @"  \_/\_/  |___| |_|\_||_|\_||___||_|_\  |_|  /___|  "
       };

        private Figure _figureWinner = new Figure(E_ForegroundColors.Yellow);

        #endregion

        #region ENUMS

        public enum E_WinnerTypes
        {
            Winner,
            WinnerP1,
            WinnerP2
        }

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Winner(E_WinnerTypes winnerType, int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            string[] textFigure = null;

            switch (winnerType)
            {
                case E_WinnerTypes.Winner:
                    textFigure = _textWinner;
                    break;
                case E_WinnerTypes.WinnerP1:
                    textFigure = _textWinnerP1;
                    break;
                case E_WinnerTypes.WinnerP2:
                    textFigure = _textWinnerP2;
                    break;
            }

            _figureWinner.P_Figure = textFigure;
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