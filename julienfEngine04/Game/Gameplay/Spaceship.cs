using julienfEngine1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace julienfEngine1
{
    class Spaceship : GameObject, IOnCollisionEnter
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private Figure _figureSpaceshipLeftSide = new Figure
           (new string[9]
               {
                    @"|====;      ",
                    @"|    |      ",
                    @"|    ====;  ",
                    @"|        |  ",
                    @"|        ===",
                    @"|        |  ",
                    @"|    ====;  ",
                    @"|    |      ",
                    @"|====;      "
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

        private float _velocity = 25;
        private int _minPosY = 0;
        private int _maxPosY = 46;

        private Queue<Bullet> _bullets = new Queue<Bullet>();
        private int _posXToInstantiateBullets;
        private byte _halfFigureY;

        private int _maxBullets = 5;
        private float _timeToRecharge = 1;
        private int _countOfBullets;
        private Timer _timerToRechargeBullets = new Timer();

        private E_ForegroundColors _bulletsColor;

        private bool _isAlive = true;

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Spaceship(E_ForegroundColors bulletsColor, int maxBullets, float timeToRecharge, int posX, int posY, bool visible) : base(posX, posY, visible)
        {
            _playerID = (E_PlayerID)_numberOfPlayers;
            _numberOfPlayers++;

            _bulletsColor = bulletsColor;
            Bullet firstBullet = new Bullet(bulletsColor, _playerID, 0, 0, false);
            firstBullet.P_Collision.P_DetectCollisions = false;
            _bullets.Enqueue(firstBullet);

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

                this._posXToInstantiateBullets = (int)this.P_PosX + this.P_GameObjectFigures[0].P_Figure[0].Length;
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

                this._posXToInstantiateBullets = (int)this.P_PosX - this.P_GameObjectFigures[0].P_Figure[0].Length - firstBullet.P_GameObjectFigures[0].P_Figure[0].Length;
            }

            _halfFigureY = (byte)(this.P_GameObjectFigures[0].P_Figure.Length / 2);

            _maxBullets = maxBullets;
            _timeToRecharge = timeToRecharge;
            _countOfBullets = _maxBullets;
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        public void SetBullets(int bullets)
        {
            this._countOfBullets = bullets <= _maxBullets ? bullets : _maxBullets;
        }

        public void MoveBulletsAttached()
        {
            foreach (Bullet bullet in _bullets) bullet.MoveBullet();
        }

        public void Shoot()
        {
            void SetBullet(Bullet bullet)
            {
                // Hacer que la bullet aparezca en el canion de la nave, y poner su propiedad isBeenUsed en true
                bullet.P_PosX = _posXToInstantiateBullets;
                bullet.P_PosY = this.P_PosY + _halfFigureY;
                bullet.P_IsBeenUsed = true;
                _bullets.Enqueue(bullet);
            }

            if (_countOfBullets > 0 && _isAlive)
            {
                if (!_bullets.Peek().P_IsBeenUsed) SetBullet(_bullets.Dequeue());
                else SetBullet(new Bullet(_bulletsColor, _playerID, 0, 0, true));

                //// Decimal reset = false;
                _countOfBullets--;
                float timerDecimalsElapsed = (float)_timerToRechargeBullets.P_MyTimer >= _countOfBullets ? (float)_timerToRechargeBullets.P_MyTimer - (int)_timerToRechargeBullets.P_MyTimer : 0;
                _timerToRechargeBullets.ResetMyTimer();
                _timerToRechargeBullets.StartMyTimer(_countOfBullets + timerDecimalsElapsed, 1 / _timeToRecharge);
            }
        }

        public void RechargeBullets()
        {
            _countOfBullets = _countOfBullets < _maxBullets && _timerToRechargeBullets.P_MyTimer < _maxBullets
                ? (int)_timerToRechargeBullets.P_MyTimer : _maxBullets;

            //_countOfBullets = (byte)_timerToRechargeBullets.P_MyTimer < _maxBullets
            //   ? (byte)_timerToRechargeBullets.P_MyTimer : _maxBullets;
        }

        public void OnPause(bool isPause)
        {
            if (isPause) _timerToRechargeBullets.StopMyTimer();
            else _timerToRechargeBullets.ContinueMyTimer();
        }

        void IOnCollisionEnter.OnCollisionEnter(GameObject[] collisions)
        {
            this.P_Visible = false;
            this.P_Collision.P_DetectCollisions = false;
            int queueLength = _bullets.Count;
            for (int i = 0; i < queueLength; i++) _bullets.Dequeue().P_IsBeenUsed = false;
            _numberOfPlayers = 0;
            _isAlive = false;
            ((IWinnable)Scene.P_CurrentScene).GameOver(_playerID);
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

        public float P_Velocity
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
            set
            {
                _minPosY = value;
            }
        }

        public int P_MaxPosY
        {
            get
            {
                return _maxPosY;
            }
            set
            {
                _maxPosY = value;
            }
        }

        public int P_MaxBullets
        {
            get
            {
                return _maxBullets;
            }
        }

        public int P_CountOfBullets
        {
            get
            {
                return _countOfBullets;
            }
        }

        public double P_TimeToRecharge
        {
            get
            {
                return _timeToRecharge;
            }
        }

        public E_ForegroundColors P_BulletsColor
        {
            get
            {
                return _bulletsColor;
            }
        }

        public IEnumerable<IDodgeable> P_Bullets
        {
            get
            {
                return _bullets.Where(currentBullet => currentBullet.P_IsBeenUsed);
            }
        }

        public E_PlayerID P_PlayerID
        {
            get
            {
                return _playerID;
            }
        }

        public bool P_IsAlive
        {
            get
            {
                return _isAlive;
            }
        }

        #endregion
    }
}