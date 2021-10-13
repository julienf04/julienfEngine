using julienfEngine1;
using System;

namespace julienfEngine1
{
    class LoadingAnim : GameObject
    {
        // Declare every attributes for this GameObject
        #region ATTRIBUTES

        private const double _ANIMATION_VELOCITY = 0.6;

        private readonly Figure[] _figuresLoadingAnim = new Figure[4]
        {
            new Figure
            (new string[8]
                {
                    @" _                     _ _             ",
                    @"| |                   | (_)            ",
                    @"| |     ___   __ _  __| |_ _ __   __ _ ",
                    @"| |    / _ \ / _` |/ _` | | '_ \ / _` |",
                    @"| |___| (_) | (_| | (_| | | | | | (_| |",
                    @"\_____/\___/ \__,_|\__,_|_|_| |_|\__, |",
                    @"                                  __/ |",
                    @"                                 |___/ "
                }, E_ForegroundColors.Gray
            ),

            new Figure
            (new string[8]
                {
                    @" _                     _ _                 ",
                    @"| |                   | (_)                ",
                    @"| |     ___   __ _  __| |_ _ __   __ _     ",
                    @"| |    / _ \ / _` |/ _` | | '_ \ / _` |    ",
                    @"| |___| (_) | (_| | (_| | | | | | (_| |  _ ",
                    @"\_____/\___/ \__,_|\__,_|_|_| |_|\__, | (_)",
                    @"                                  __/ |    ",
                    @"                                 |___/     "
                }, E_ForegroundColors.Gray
            ),

            new Figure
            (new string[8]
                {
                    @" _                     _ _                     ",
                    @"| |                   | (_)                    ",
                    @"| |     ___   __ _  __| |_ _ __   __ _         ",
                    @"| |    / _ \ / _` |/ _` | | '_ \ / _` |        ",
                    @"| |___| (_) | (_| | (_| | | | | | (_| |  _   _ ",
                    @"\_____/\___/ \__,_|\__,_|_|_| |_|\__, | (_) (_)",
                    @"                                  __/ |        ",
                    @"                                 |___/         "
                }, E_ForegroundColors.Gray
            ),

            new Figure
            (new string[8]
                {
                    @" _                     _ _                         ",
                    @"| |                   | (_)                        ",
                    @"| |     ___   __ _  __| |_ _ __   __ _             ",
                    @"| |    / _ \ / _` |/ _` | | '_ \ / _` |            ",
                    @"| |___| (_) | (_| | (_| | | | | | (_| |  _   _   _ ",
                    @"\_____/\___/ \__,_|\__,_|_|_| |_|\__, | (_) (_) (_)",
                    @"                                  __/ |            ",
                    @"                                 |___/             "
                }, E_ForegroundColors.Gray
            )
    };

        #endregion

        // Constructors for this GameObject
        #region CONSTRUCTORS

        public LoadingAnim(int posX, int posY, bool visible, bool isUI, byte layer) : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = _figuresLoadingAnim;
            this.P_Animation.P_AnimationState = E_AnimationStates.Repeat;
            this.P_Animation.P_TimeBetweenFigures = _ANIMATION_VELOCITY;
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
