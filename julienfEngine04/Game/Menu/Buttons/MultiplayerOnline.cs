using julienfEngine1;
using System;

namespace julienfEngine1
{
    class MultiplayerOnline : GameObject, IClickable
    {
        // Declare every attributes for this GameObject
        #region ATRIBUTES

        // Declare and initialize figure/s for this GameObject
        private readonly Figure _figureMultiplayerOffline = new Figure
           (new string[8]
               {
                    @"___  ___      _ _   _       _                                     _ _            ",
                    @"|  \/  |     | | | (_)     | |                                   | (_)           ",
                    @"| .  . |_   _| | |_ _ _ __ | | __ _ _   _  ___ _ __    ___  _ __ | |_ _ __   ___ ",
                    @"| |\/| | | | | | __| | '_ \| |/ _` | | | |/ _ \ '__|  / _ \| '_ \| | | '_ \ / _ \",
                    @"| |  | | |_| | | |_| | |_) | | (_| | |_| |  __/ |    | (_) | | | | | | | | |  __/",
                    @"\_|  |_/\__,_|_|\__|_| .__/|_|\__,_|\__, |\___|_|     \___/|_| |_|_|_|_| |_|\___|",
                    @"                     | |             __/ |                                       ",
                    @"                     |_|            |___/                                        "
               }, E_ForegroundColors.Gray
           );

        #endregion

        // Constructors for this GameObject
        #region CONSTRUCTORS

        // Use these constructors or create new ones. You can delete unused constructors
        public MultiplayerOnline(int posX, int posY, bool visible) : base(posX, posY, visible)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureMultiplayerOffline };
        }

        public MultiplayerOnline(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureMultiplayerOffline };
        }

        public MultiplayerOnline(int posX, int posY, bool visible, bool isUI, byte layer, Figure[] figures, byte baseFigure)
            : base(posX, posY, visible, isUI, layer, figures, baseFigure)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureMultiplayerOffline };
        }

        #endregion

        // Create actions for this GameObject
        #region METHODS

        void IClickable.OnSelect()
        {
            this.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.Green;
        }

        void IClickable.OnDeselect()
        {
            this.P_GameObjectFigures[0].ForegroundColor = E_ForegroundColors.Gray;
        }

        void IClickable.OnClick()
        {
            Scene.SetLoadedScene(typeof(LoadingScene), false);
        }

        #endregion

        // Create properties for this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigureMultiplayerOffline
        {
            get
            {
                return _figureMultiplayerOffline;
            }
        }

        #endregion
    }
}