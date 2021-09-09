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

        private E_ForegroundColors _foregroundColor = E_ForegroundColors.White;

        private E_BackgroundColors _backgroundColor = E_BackgroundColors.Black;

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

        public Figure(E_ForegroundColors foregroundColor)
        {
            _foregroundColor = foregroundColor;
        }

        public Figure(E_BackgroundColors backgroundColor)
        {
            _backgroundColor = backgroundColor;
        }

        public Figure(E_BackgroundColors backgroundColor, E_ForegroundColors foregroundColor)
        {
            _backgroundColor = backgroundColor;
            _foregroundColor = foregroundColor;
        }

        public Figure(string[] figure, E_ForegroundColors foregroundColor)
        {
            _figure = figure;
            _foregroundColor = foregroundColor;
        }

        public Figure(string[] figure, E_BackgroundColors backgroundColor)
        {
            _figure = figure;
            _backgroundColor = backgroundColor;
        }

        public Figure(string[] figure, E_ForegroundColors foregroundColor, E_BackgroundColors backgroundColor)
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

        public E_ForegroundColors ForegroundColor
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

        public E_BackgroundColors BackgroundColor
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