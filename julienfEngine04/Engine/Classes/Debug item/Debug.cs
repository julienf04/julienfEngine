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
        public Debug(int posX, int posY, bool visible, bool isUI, byte layer)
            : base(posX, posY, visible, isUI, layer)
        {

        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        internal void UpdateValues()
        {
            UpdateFigure();
            BreakThread(E_Keyboard.B);
        }

        private void UpdateFigure()
        {
            this.P_GameObjectFigures[0].P_Figure[0] = "fps: " + Timer.P_DeltaTime.ToString();
        }

        private static void BreakThread(E_Keyboard key)
        {
            if (Input.GetKey(key))
            { }
        }

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        #endregion
    }
}
#endif