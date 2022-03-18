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

        private bool[] _hasSpecialCharacters = null;
        private short[][] _specialCharactersStartIndexes = null;
        private int[][] _specialCharactersLengthToPaint = null;

        #endregion

        #region ---CONSTRUCTORS;


        public Figure()
        {
            _figure = Array.Empty<string>();
        }

        public Figure(string[] figure)
        {
            _figure = figure;
            _hasSpecialCharacters = FigureFilter.GetSpecialCharacters(figure, out _specialCharactersStartIndexes, out _specialCharactersLengthToPaint);
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
            _hasSpecialCharacters = FigureFilter.GetSpecialCharacters(figure, out _specialCharactersStartIndexes, out _specialCharactersLengthToPaint);
        }

        public Figure(string[] figure, E_BackgroundColors backgroundColor)
        {
            _figure = figure;
            _backgroundColor = backgroundColor;
            _hasSpecialCharacters = FigureFilter.GetSpecialCharacters(figure, out _specialCharactersStartIndexes, out _specialCharactersLengthToPaint);
        }

        public Figure(string[] figure, E_ForegroundColors foregroundColor, E_BackgroundColors backgroundColor)
        {
            _figure = figure;
            _foregroundColor = foregroundColor;
            _backgroundColor = backgroundColor;
            _hasSpecialCharacters = FigureFilter.GetSpecialCharacters(figure, out _specialCharactersStartIndexes, out _specialCharactersLengthToPaint);
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
                _hasSpecialCharacters = FigureFilter.GetSpecialCharacters(value, out _specialCharactersStartIndexes, out _specialCharactersLengthToPaint);
                _figure = value;
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

        public bool[] P_HasSpecialCharacters
        {
            get
            {
                return _hasSpecialCharacters;
            }
        }

        public short[][] P_SpecialCharactersStartIndexes
        {
            get
            {
                return _specialCharactersStartIndexes;
            }
        }

        public int[][] P_SpecialCharactersLengthToPaint
        {
            get
            {
                return _specialCharactersLengthToPaint;
            }
        }

        #endregion
    }
}