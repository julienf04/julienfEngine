using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace julienfEngine1
{
    abstract class Scene
    {
        #region ---ATRIBUTES

        private Camera _mainCamera = new Camera(0, 0); //This Camera is the main camera, the camera displayed 

        private List<GameObject> _gameObjectsToDraw = new List<GameObject>();

        private List<ICollideable> _ICollideableToDetectCollisions = new List<ICollideable>();

        private GameObject[] _gameObjectsToDrawArray;
        private ICollideable[] _ICollideableToDetectCollisionsArray;

        private static List<IntPtr> _allScenePointers = new List<IntPtr>();
        private static List<Scene> _allLoadedScenes = new List<Scene>();

        private static bool _onLoadScene = false;

        #endregion

        #region CONSTRUCTORS

        public Scene()
        {
            //Type typeOfThisScene = this.GetType();
            //if (_allScenePointers.All(currentScenePointer => currentScenePointer == typeOfThisScene.TypeHandle.Value) && _allScenePointers.Count >= 1 && !_onLoadScene) throw new Exception("An instance of class " + this.GetType() + " already exists. There can only be one Scene of the same type");

            //_gameObjectsToDrawArray = _gameObjectsToDraw.ToArray();
            //_ICollideableToDetectCollisionsArray = _ICollideableToDetectCollisions.ToArray();

            //_allScenePointers.Add(typeOfThisScene.TypeHandle.Value);

            //if (_allScenePointers.Count == 1)
            //{
            //    julienfEngine.LoadScene(typeOfThisScene);
            //    julienfEngine.SetLoadedScene(typeOfThisScene);
            //}

            if (_onLoadScene)
            {
                if (!_allScenePointers.Contains(this.GetType().TypeHandle.Value)) throw new Exception("The scene you are trying to load is not initialized");

                _gameObjectsToDrawArray = _gameObjectsToDraw.ToArray();
                _ICollideableToDetectCollisionsArray = _ICollideableToDetectCollisions.ToArray();

                _onLoadScene = false;
                return;
            }

            throw new Exception("You cannot create an instance of your scenes. Use julienfEngine.LoadScene(Type sceneType) for load a scene, and use julienfEngine.SetLoadedScene(Type sceneType) for run a loaded scene");
        
        }

        #endregion

        #region METHODS

        public void AddToDrawGameObject(GameObject gameObject)
        {
            if (_gameObjectsToDraw.Count != 0)
            {
                if (!_gameObjectsToDraw.Contains(gameObject))
                {
                    if (_gameObjectsToDraw.Max(gameObjectOfList => gameObjectOfList.P_Layer) > gameObject.P_Layer) _gameObjectsToDraw.Insert(_gameObjectsToDraw.FindIndex(gameObjectOfList => gameObjectOfList.P_Layer > gameObject.P_Layer), gameObject);
                    else _gameObjectsToDraw.Add(gameObject);
                }
            }
            else
            {
                _gameObjectsToDraw.Add(gameObject);
            }

            _gameObjectsToDrawArray = _gameObjectsToDraw.ToArray();
        }

        public void RemoveToDrawGameObject(GameObject gameObject)
        {
            if (_gameObjectsToDraw.Remove(gameObject)) _gameObjectsToDrawArray = _gameObjectsToDraw.ToArray();
        }

        public void RemoveAllToDrawGameObject()
        {
            _gameObjectsToDraw.Clear();
            _gameObjectsToDrawArray = _gameObjectsToDraw.ToArray();
        }

        public void AddToDetectCollisionsGameObject(GameObject gameObject)
        {
            if (!(gameObject is ICollideable)) throw new Exception("gamobject must be 'ICollideable'");

            ICollideable collision = (ICollideable)gameObject;
            if (!P_ICollideableToDetectCollisions.Contains(collision))
            {
                _ICollideableToDetectCollisions.Add(collision);
                _ICollideableToDetectCollisionsArray = _ICollideableToDetectCollisions.ToArray();
            }
        }

        public void RemoveToDetectCollisionsGameObject(GameObject gameObject)
        {
            if (!(gameObject is ICollideable)) throw new Exception("gamobject must be 'ICollideable'");

            if (_ICollideableToDetectCollisions.Remove((ICollideable)gameObject)) _ICollideableToDetectCollisionsArray = _ICollideableToDetectCollisions.ToArray();
        }

        public void RemoveAllToDetectCollisionsGameObject()
        {
            _ICollideableToDetectCollisions.Clear();
            _ICollideableToDetectCollisionsArray = _ICollideableToDetectCollisions.ToArray();
        }

        internal static void Initialize(Type sceneType)
        {
            IntPtr scenePointer = sceneType.TypeHandle.Value;
            if (_allScenePointers.Contains(scenePointer)) throw new Exception("Scene type was already initialized. You cannot initialize twice the same scene");
            _allScenePointers.Add(scenePointer);
        }

        internal static bool IsScene(Type sceneType)
        {
            return sceneType.BaseType == typeof(Scene);
        }

        internal static Scene GetLoadedSceneByType(Type sceneType)
        {
            Scene sceneToReturn = _allLoadedScenes.Find(currentScene => currentScene.GetType() == sceneType);

            if (sceneToReturn == null) throw new Exception("The scene you are trying to set is not loaded");

            return sceneToReturn;
        }

        internal static void LoadScene(Type sceneType)
        {
            if (_allLoadedScenes.All(currentScene => currentScene.GetType() == sceneType) && _allLoadedScenes.Count >= 1) throw new Exception("The scene you are trying to load is already loaded");

            _onLoadScene = true;
            Scene sceneToLoad = (Scene)Activator.CreateInstance(sceneType);
            _allLoadedScenes.Add(sceneToLoad);
        }

        internal static void UnloadScene(Type sceneType)
        {
            Scene sceneToUnload = _allLoadedScenes.Find(currentScene => currentScene.GetType() == sceneType);
            _allLoadedScenes.Remove(sceneToUnload);
        }


        public abstract void Start();
        public abstract void Update();

        #endregion

        #region PROPERTIES

        public Camera P_MainCamera
        {
            get
            {
                return _mainCamera;
            }

            set
            {
                _mainCamera = value;
            }
        }

        public List<GameObject> P_GameObjectsToDraw
        {
            get
            {
                return _gameObjectsToDraw;
            }
        }

        public List<ICollideable> P_ICollideableToDetectCollisions
        {
            get
            {
                return _ICollideableToDetectCollisions;
            }
        }

        public GameObject[] P_GameObjectsToDrawArray
        {
            get
            {
                return _gameObjectsToDrawArray;
            }
        }

        public ICollideable[] P_ICollideableToDetectCollisionsArray
        {
            get
            {
                return _ICollideableToDetectCollisionsArray;
            }
        }

        #endregion
    }
}