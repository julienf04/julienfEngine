#if DEBUG
namespace julienfEngine1
{
    internal class Debug : GameObject
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Debug(Figure[] figures = null, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                                  int posX = 0, int posY = 0) : base(figures, baseFigure, visible, isUI, layer, posX, posY)
        {

        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        internal void UpdateValues()
        {
            this.P_GameObjectFigures[0].P_Figure[0] = "fps: " + Timer.P_DeltaTime.ToString();
        }

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        #endregion
    }
}
#endif