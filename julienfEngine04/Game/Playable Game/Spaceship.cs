using julienfEngine1;
using System;

namespace julienfEngine1
{
    class Spaceship : GameObject, ICollideable
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private Figure _figureSpaceshipLeftSide = new Figure
           (new string[9]
               {
                    @"|====;",
                    @"|    |",
                    @"|    ====;",
                    @"|        |",
                    @"|        ===",
                    @"|        |",
                    @"|    ====;",
                    @"|    |",
                    @"|====;"
               }, E_ForegroundColors.Gray
           );

        private Figure _figureSpaceshipRightSide = new Figure
          (new string[9]
              {
                    @"      ;====|",
                    @"      |    |",
                    @"  ;====    |",
                    @"  |        |",
                    @"===        |",
                    @"  |        |",
                    @"  ;====    |",
                    @"      |    |",
                    @"      ;====|"
              }, E_ForegroundColors.Gray
          );


        private static byte _playerID = 0;

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Spaceship(Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                                  int posX = 0, int posY = 0) : base(figures, myScene, baseFigure, visible, isUI, layer, posX, posY)
        {
            this.P_GameObjectFigures = new Figure[1] { _figureSpaceshipLeftSide };

            if (_playerID == 0)
            {
                this.P_GameObjectFigures = new Figure[1] { _figureSpaceshipLeftSide };
                this.P_Layer = 1;

                this.P_Collision.P_Colliders = new Area[3]
                {
                    new Area(0, 5, 0, 8),
                    new Area(6, 9, 2, 6),
                    new Area(10, 11, 4, 4)
                };
            }
            else
            {
                this.P_GameObjectFigures = new Figure[1] { _figureSpaceshipRightSide };

                this.P_Collision.P_Colliders = new Area[3]
                {
                    new Area(0, 1, 4, 4),
                    new Area(2, 5, 2, 6),
                    new Area(6, 11, 0, 8)
                };
            }

            _playerID++;
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        void ICollideable.OnCollisionEnter(GameObject[] collisions)
        {
            this._figureSpaceshipLeftSide.ForegroundColor = E_ForegroundColors.Red;
        }

        void ICollideable.OnCollisionStay(GameObject[] collisions)
        {
            //throw new Exception();
        }

        void ICollideable.OnCollisionExit(GameObject[] collisions)
        {
            this._figureSpaceshipLeftSide.ForegroundColor = E_ForegroundColors.Gray;
        }

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigureSpaceshipLeftSide
        {
            get
            {
                return _figureSpaceshipLeftSide;
            }
        }

        public Figure P_FigureSpaceshipRightSide
        {
            get
            {
                return _figureSpaceshipRightSide;
            }
        }

        #endregion
    }
}