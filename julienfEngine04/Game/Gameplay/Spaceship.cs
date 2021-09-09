using julienfEngine1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace julienfEngine1
{
    class Spaceship : GameObject, ICollidable
    {
        #region ENUMS

        public enum E_PlayerID : byte
        {
            Player1,
            Player2
        }

        #endregion

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


        private static byte _numberOfPlayers = 0;
        private E_PlayerID _playerID = E_PlayerID.Player1;

        private double _velocity = 25;
        private int _minPosY = 0;
        private int _maxPosY = 46;

        private Queue<Bullet> _bullets = new Queue<Bullet>();
        private int _posXToInstantiateBullets;
        private byte _halfFigureY;

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Spaceship(Figure[] figures, Scene myScene, byte baseFigure = 0, bool visible = true, bool isUI = false, byte layer = 0,
                                  int posX = 0, int posY = 0) : base(figures, myScene, baseFigure, visible, isUI, layer, posX, posY)
        {
            _playerID = (E_PlayerID)_numberOfPlayers;
            _numberOfPlayers++;

            if (_playerID == E_PlayerID.Player1)
            {
                this.P_GameObjectFigures = new Figure[1] { _figureSpaceshipLeftSide };
                this.P_Layer = 1;

                this.P_Collision.P_Colliders = new Area[3]
                {
                    new Area(0, 5, 0, 8),
                    new Area(6, 9, 2, 6),
                    new Area(10, 11, 4, 4)
                };

                this._posXToInstantiateBullets = 15;
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

                this._posXToInstantiateBullets = 200;
            }

            _posXToInstantiateBullets = _playerID == E_PlayerID.Player1 ? 20 : 200;
            _halfFigureY = (byte)(this.P_GameObjectFigures[0].P_Figure.Length / 2);

            Bullet firstBullet = new Bullet(_playerID, null, Scene.P_CurrentScene, 0, false, false, 0, 0, 0);
            firstBullet.P_Collision.P_DetectCollisions = false;
            _bullets.Enqueue(firstBullet);
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        public void MoveBulletsAttached()
        {
            foreach (Bullet bullet in _bullets) bullet.MoveBullet();

        }

        public void InstantiateBullet()
        {
            void SetBullet(Bullet bullet)
            {
                // Hacer que la bullet aparezca en el canion de la nave, y poner su propiedad isBeenUsed en true
                bullet.P_PosX = _posXToInstantiateBullets;
                bullet.P_PosY = this.P_PosY + _halfFigureY;
                bullet.P_IsBeenUsed = true;
                bullet.P_Collision.P_DetectCollisions = true;
                bullet.P_Visible = true;

                _bullets.Enqueue(bullet);
            }

            if (!_bullets.Peek().P_IsBeenUsed) SetBullet(_bullets.Dequeue());
            else SetBullet(new Bullet(_playerID, null, Scene.P_CurrentScene, 0, true, false, 0, 0, 0));
        }

        void ICollidableOnCollisionEnter.OnCollisionEnter(GameObject[] collisions)
        {
            this._figureSpaceshipLeftSide.ForegroundColor = E_ForegroundColors.Red;
        }

        void ICollidableOnCollisionStay.OnCollisionStay(GameObject[] collisions)
        {
            //throw new Exception();
        }

        void ICollidableOnCollisionExit.OnCollisionExit(GameObject[] collisions)
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

        public double P_Velocity
        {
            get
            {
                return _velocity;
            }
        }

        public int P_MinPosY
        {
            get
            {
                return _minPosY;
            }
        }

        public int P_MaxPosY
        {
            get
            {
                return _maxPosY;
            }
        }

        #endregion
    }
}