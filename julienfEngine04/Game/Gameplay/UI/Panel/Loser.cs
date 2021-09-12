﻿using julienfEngine1;
using System;

namespace julienfEngine1
{
    class Loser : GameObject
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private Figure _figureLoser = new Figure
           (new string[7]
               {
                    @" _       ___   _____   ___  ____  ",
                    @"| |     /   \ / ___/  /  _]|    \ ",
                    @"| |    |     (   \_  /  [_ |  D  )",
                    @"| |___ |  O  |\__  ||    _]|    / ",
                    @"|     ||     |/  \ ||   [_ |    \ ",
                    @"|     ||     |\    ||     ||  .  \",
                    @"|_____| \___/  \___||_____||__|\_|"
               }, E_ForegroundColors.Gray
           );

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Loser(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureLoser };
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigureLoser
        {
            get
            {
                return _figureLoser;
            }
        }

        #endregion
    }
}