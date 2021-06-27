using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    class Figure //This class is for drawing figures like a characters, enemies, obstacles, and anything
    {
        #region ---ATRIBUTES;

        private string[] _figure; //points of the figure in a matriz. This is for read this matriz and draw points of the figure on the screen. 1 if some point need to be drawn, 0 else

        #endregion

        #region ---CONSTRUCTORS;


        public Figure()
        {

        }

        public Figure(string[] figure)
        {
            this._figure = figure;
        }

        #endregion

        #region ---PROPERTIES;

        public string[] P_Figure
        {
            get
            {
                return this._figure;
            }

            set
            {
               this._figure = value; //If matriz in X and matriz in Y are less than screen, it is allowed, not else
            }
        }

        #endregion

        #region ---METHODS;








        #endregion
    }
}