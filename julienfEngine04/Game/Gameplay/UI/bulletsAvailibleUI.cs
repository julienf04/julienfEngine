using julienfEngine1;
using System;
using System.Collections.Generic;

namespace julienfEngine1
{
    class bulletsAvailibleUI : GameObject
    {
        // Declare every attributes of this GameObject
        #region ATRIBUTES

        // Declare and initialize a figure/s of this GameObject
        private Figure _figurebulletsAvailible = new Figure
           (new string[1]
               {
                    @"Bullets:"
               }, E_ForegroundColors.Yellow
           );

        private Spaceship _spaceshipAttached;
        private SimpleBacgroundColor[] _bullets;
        private byte _countOfBulletsUI;

        private const byte _BULLET_DISTANCE = 2;

        #endregion

        // Constructors of this GameObject
        #region CONSTRUCTORS

        // Create a constructor/s of tthis GameObject
        public bulletsAvailibleUI(Spaceship player, E_BackgroundColors bulletsColor, int posX, int posY, bool visible, bool isUI, byte layer)
            : base(posX, posY, visible, isUI, layer)
        {
            this.P_GameObjectFigures = new Figure[1] { _figurebulletsAvailible };

            _bullets = new SimpleBacgroundColor[player.P_MaxBullets];
            for (int i = 0, x = 0; x < _bullets.Length * _BULLET_DISTANCE; i++, x += _BULLET_DISTANCE)
                _bullets[i] = new SimpleBacgroundColor(bulletsColor, (int)this.P_PosX + _figurebulletsAvailible.P_Figure[0].Length + 1 + x, (int)this.P_PosY,
                    true, true, 0);

            this._spaceshipAttached = player;
            this._countOfBulletsUI = player.P_MaxBullets;
        }

        #endregion

        // Create actions of this GameObject
        #region METHODS

        public void UpdateBulletsUI()
        {
            if (_spaceshipAttached.P_CountOfBullets - 1 != _countOfBulletsUI - 1)
            {
                bool moreBulletsUI = _spaceshipAttached.P_CountOfBullets - 1 > _countOfBulletsUI - 1; // More bullets = true. Less bullets = false
                _bullets[moreBulletsUI ? _spaceshipAttached.P_CountOfBullets - 1 : _countOfBulletsUI - 1].P_Visible = moreBulletsUI;

                _countOfBulletsUI = _spaceshipAttached.P_CountOfBullets;
            }
        }

        #endregion

        // Create properties of this GameObject
        #region PROPERTIES

        // Figure property of this GameObject
        public Figure P_FigurebulletsAvailible
        {
            get
            {
                return _figurebulletsAvailible;
            }
        }

        #endregion
    }
}