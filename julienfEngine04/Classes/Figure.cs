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

        private ForegroundColors _foregroundColor = ForegroundColors.White;

        private BackgroundColors _backgroundColor = BackgroundColors.Black;

        #endregion

        #region ---CONSTRUCTORS;


        public Figure()
        {
            _figure = new string[0];
        }

        public Figure(string[] figure)
        {
            _figure = figure;
        }

        public Figure(ForegroundColors foregroundColor)
        {
            _foregroundColor = foregroundColor;
        }

        public Figure(BackgroundColors backgroundColor)
        {
            _backgroundColor = backgroundColor;
        }

        public Figure(string[] figure, ForegroundColors foregroundColor)
        {
            _figure = figure;
            _foregroundColor = foregroundColor;
        }

        public Figure(string[] figure, BackgroundColors backgroundColor)
        {
            _figure = figure;
            _backgroundColor = backgroundColor;
        }

        public Figure(string[] figure, ForegroundColors foregroundColor, BackgroundColors backgroundColor)
        {
            _figure = figure;
            _foregroundColor = foregroundColor;
            _backgroundColor = backgroundColor;
        }

        #endregion

        #region ---METHODS;

        #endregion

        #region ---PROPERTIES;

        public string[] P_Figure
        {
            get
            {
                return _figure;
            }

            set
            {
                _figure = value; //If matriz in X and matriz in Y are less than screen, it is allowed, not else
            }
        }

        public ForegroundColors ForegroundColor
        {
            get
            {
                return _foregroundColor;
            }

            set
            {
                _foregroundColor = value;
            }
        }

        public BackgroundColors BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }

            set
            {
                _backgroundColor = value;
            }
        }

        #endregion
    }
}