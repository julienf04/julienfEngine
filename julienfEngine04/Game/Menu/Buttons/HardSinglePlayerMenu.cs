using julienfEngine1;
using System;

namespace julienfEngine1
{
    class HardSinglePlayerMenu : GameObject, IClickable
    {
        #region ATRIBUTES

        private readonly Figure _figureHard = new Figure
            (new string[6]
                {
                    @" _   _               _ ",
                    @"| | | |             | |",
                    @"| |_| | __ _ _ __ __| |",
                    @"|  _  |/ _` | '__/ _` |",
                    @"| | | | (_| | | | (_| |",
                    @"\_| |_/\__,_|_|  \__,_|"
                }, E_ForegroundColors.Gray
            );

        #endregion

        #region CONSTRUCTORS

        public HardSinglePlayerMenu(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureHard };
        }

        #endregion

        #region METHODS

        public void OnSelect()
        {
            this._figureHard.ForegroundColor = E_ForegroundColors.Green;
        }

        public void OnDeselect()
        {
            this._figureHard.ForegroundColor = E_ForegroundColors.Gray;
        }

        public void OnClick()
        {
            Scene.UnloadScene(typeof(MainMenuScene));
            Scene.UnloadScene(typeof(ExitMenuScene));
            Scene.UnloadScene(typeof(SinglePlayerMenuScene));
            Scene.UnloadScene(typeof(MultiplayerMenuScene));

            Scene.LoadScene(typeof(GameScene), new object[] { GameScene.E_GameType.SinglePlayerHard });
            Scene.SetLoadedScene(typeof(GameScene), true);
        }

        #endregion

        #region PROPERTIES

        public Figure P_FigureHard
        {
            get
            {
                return _figureHard;
            }
        }

        #endregion
    }
}