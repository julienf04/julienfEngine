﻿using julienfEngine1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace julienfEngine1
{
    class Spaceship : GameObject, IOnCollisionEnter
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

        private byte _maxBullets = 5;
        private double _timeToRecharge = 1;
        private byte _countOfBullets;
        private Timer _timerToRechargeBullets = new Timer();

        private E_ForegroundColors _bulletsColor;

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public Spaceship(E_ForegroundColors bulletsColor, int posX, int posY, bool visible) : base(posX, posY, visible)
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

            _bulletsColor = bulletsColor;
            Bullet firstBullet = new Bullet(bulletsColor, _playerID, 0, 0, false);
            firstBullet.P_Collision.P_DetectCollisions = false;
            _bullets.Enqueue(firstBullet);

            _countOfBullets = _maxBullets;
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

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

            if (_countOfBullets > 0)
            {
                if (!_bullets.Peek().P_IsBeenUsed) SetBullet(_bullets.Dequeue());
                else SetBullet(new Bullet(_bulletsColor, _playerID, 0, 0, true));

                //// Decimal reset = false;
                _countOfBullets--;
                double timerDecimalsElapsed = _timerToRechargeBullets.P_MyTimer >= _countOfBullets ? _timerToRechargeBullets.P_MyTimer - (int)_timerToRechargeBullets.P_MyTimer : 0;
                _timerToRechargeBullets.ResetMyTimer();
                _timerToRechargeBullets.StartMyTimer(_countOfBullets + timerDecimalsElapsed);

                //// Decimal reset = true;
                //_countOfBullets--;
                //_timerToRechargeBullets.ResetMyTimer();
                //_timerToRechargeBullets.StartMyTimer(_countOfBullets);
            }
        }

        public void RechargeBullets()
        {
            _countOfBullets = _countOfBullets < _maxBullets ? (byte)_timerToRechargeBullets.P_MyTimer : _maxBullets;
        }

        void IOnCollisionEnter.OnCollisionEnter(GameObject[] collisions)
        {
            this._figureSpaceshipLeftSide.ForegroundColor = E_ForegroundColors.Red;
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

        public byte P_MaxBullets
        {
            get
            {
                return _maxBullets;
            }
        }

        public byte P_CountOfBullets
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

        //public E_BackgroundColors P_BulletsColorUI
        //{
        //    get
        //    {
        //        return _bulletsColorUI;
        //    }
        //}

        #endregion
    }
}