using julienfEngine1;
using System;

namespace julienfEngine1
{
    class LoadingSpinnerAnim : GameObject
    {
        // Declare every attributes for this GameObject
        #region ATTRIBUTES

        private readonly Figure _figureLoadingSpinnerAnim = new Figure
            (new string[6]
                {
                    @"     ¼¼¼¼¼ ",
                    @"     ¼¼¼¼¼ ",
                    @"     ¼¼¼¼¼ ",
                    @"     ¼¼¼¼¼ ",
                    @"     ¼¼¼¼¼ ",
                    @"     ¼¼¼¼¼ "
                }, E_BackgroundColors.Violet
            );

        #endregion

        // Constructors for this GameObject
        #region CONSTRUCTORS

        // Use these constructors or create new ones. You can delete unused constructors
        public LoadingSpinnerAnim(int posX, int posY, bool visible) : base(posX, posY, visible)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureLoadingSpinnerAnim };
        }

        public LoadingSpinnerAnim(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureLoadingSpinnerAnim };
        }

        public LoadingSpinnerAnim(int posX, int posY, bool visible, bool isUI, byte layer, Figure[] figures, byte baseFigure)
            : base(posX, posY, visible, isUI, layer, figures, baseFigure)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureLoadingSpinnerAnim };
        }

        #endregion

        // Create actions for this GameObject
        #region METHODS

        #endregion

        // Create properties for this GameObject
        #region PROPERTIES

        #endregion

    }
}
