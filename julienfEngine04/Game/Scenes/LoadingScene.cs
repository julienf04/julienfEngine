using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace julienfEngine1
{
    class LoadingScene : Scene
    {
        // Declare every attributes of this scene
        #region ATTRIBUTES

        private const float _LOADING_ANIM_RELATIVE_POSX = 2f;
        private const float _LOADING_ANIM_RELATIVE_POSY = 4f;

        private LoadingAnim _loadingAnim;
        private LoadingSpinnerAnim _loadingSpinnerAnim;

        #endregion

        // Initialize every attribute and create a game logic for this scene
        #region GAME METHODS

        // This runs when this scene is loaded
        public override void Awake()
        {
            _loadingAnim = new LoadingAnim((int)(Screen.P_Width / _LOADING_ANIM_RELATIVE_POSX), (int)(Screen.P_Height / _LOADING_ANIM_RELATIVE_POSY), true, true, 0);
            _loadingAnim.P_PosX -= _loadingAnim.P_GameObjectFigures[_loadingAnim.P_GameObjectFigures.Length-1].P_Figure[0].Length / 2;

            _loadingSpinnerAnim = new LoadingSpinnerAnim(4, 4, true, true, 0);
        }

        // This runs when this scene is setted
        public override void Start()
        {
            if (_loadingAnim.P_Animation.P_IsRunning) _loadingAnim.P_Animation.StopAnimation(true);
            _loadingAnim.Animate();
        }

        // This runs every frame
        public override void Update()
        {

        }

        #endregion
    }
}
