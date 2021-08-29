using julienfEngine1;
using System;

namespace julienfEngine1
{
    class EasySinglePlayerMenu : GameObject, IClickable
    {
        #region ATRIBUTES

        private Figure _figureEasy = new Figure
            (new string[8]
                {
                    @" _____                    ",
                    @"|  ___|                   ",
                    @"| |__    __ _  ___  _   _ ",
                    @"|  __|  / _` |/ __|| | | |",
                    @"| |___ | (_| |\__ \| |_| |",
                    @"\____/  \__,_||___/ \__, |",
                    @"                     __/ |",
                    @"                    |___/ "
                }, E_ForegroundColors.Gray
            );

        #endregion

        #region CONSTRUCTORS

        public EasySinglePlayerMenu(Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                    int posX = 0, int posY = 0) : base(figures, myScene, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureEasy };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            _figureEasy.ForegroundColor = E_ForegroundColors.Green;
        }

        public void OnDeselect()
        {
            _figureEasy.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {
            Scene.UnloadScene(typeof(MainMenuScene));
            Scene.UnloadScene(typeof(ExitMenuScene));
            Scene.UnloadScene(typeof(SinglePlayerMenuScene));

            Scene.LoadScene(typeof(SinglePlayerGameScene));
            Scene.SetLoadedScene(typeof(SinglePlayerGameScene), true);
        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureEasy
        {
            get
            {
                return _figureEasy;
            }
        }

        #endregion
    }
}