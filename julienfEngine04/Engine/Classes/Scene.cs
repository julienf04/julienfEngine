using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace julienfEngine1
{
    abstract class Scene
    {
        #region ---ATRIBUTES

        private Camera _mainCamera = new Camera(0, 0); //This Camera is the main camera, the camera displayed 

        private List<GameObject> _gameObjectsToDraw = new List<GameObject>();

        private List<ICanCollide> _ICollideableToDetectCollisions = new List<ICanCollide>();

        private static List<IntPtr> _allScenePointers = new List<IntPtr>();
        private static List<Scene> _allLoadedScenes = new List<Scene>();

        private static bool _onLoadScene = false;

        private static Scene _currentScene;

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

                _onLoadScene = false;
                return;
            }

            throw new Exception("You cannot create an instance of your scenes. Use Scene.LoadScene(Type sceneType) for load scene, and use Scene.SetLoadedScene(Type sceneType) for run loaded scene");
        
        }

        #endregion

        #region METHODS

        public void AddToDrawGameObject(GameObject gameObject)
        {
            if (_gameObjectsToDraw.Count != 0)
            {
                if (!_gameObjectsToDraw.Contains(gameObject))
                {
                    int indexFirstUI = _gameObjectsToDraw.Any(gameObjectOfList => gameObjectOfList.P_IsUI)
                        ? _gameObjectsToDraw.FindIndex(gameObjectOfList => gameObjectOfList.P_IsUI)
                        : 0;
                    int startIndex = gameObject.P_IsUI ? indexFirstUI : 0;
                    int count = gameObject.P_IsUI ? _gameObjectsToDraw.Count - indexFirstUI : indexFirstUI + 1;

                    List<GameObject> gameObjectsUI = _gameObjectsToDraw.GetRange(startIndex, count);

                    if (gameObjectsUI.Max(gameObjectOfList => gameObjectOfList.P_Layer) > gameObject.P_Layer)
                        _gameObjectsToDraw.Insert(_gameObjectsToDraw.FindIndex(startIndex, count, gameObjectOfList => gameObjectOfList.P_Layer > gameObject.P_Layer), gameObject);
                    else _gameObjectsToDraw.Add(gameObject);
                }
            }
            else _gameObjectsToDraw.Add(gameObject);

        }

        public void RemoveToDrawGameObject(GameObject gameObject)
        {
            _gameObjectsToDraw.Remove(gameObject);
        }

        public void RemoveAllToDrawGameObject()
        {
            _gameObjectsToDraw.Clear();
        }

        public void AddToDetectCollisionsGameObject(GameObject gameObject)
        {
            if (!(gameObject is ICanCollide)) throw new Exception("gamobject must be 'ICollideable'");

            ICanCollide collision = (ICanCollide)gameObject;
            if (!_ICollideableToDetectCollisions.Contains(collision)) _ICollideableToDetectCollisions.Add(collision);
        }

        public void RemoveToDetectCollisionsGameObject(GameObject gameObject)
        {
            if (!(gameObject is ICanCollide)) throw new Exception("gamobject must be 'ICollideable'");

            _ICollideableToDetectCollisions.Remove((ICanCollide)gameObject);
        }

        public void RemoveAllToDetectCollisionsGameObject()
        {
            _ICollideableToDetectCollisions.Clear();
        }

        public static void InitializeScene(Type sceneType)
        {
            if (!IsScene(sceneType)) throw new Exception("The type is not a scene");

            IntPtr scenePointer = sceneType.TypeHandle.Value;
            if (_allScenePointers.Contains(scenePointer)) throw new Exception("Scene type was already initialized. You cannot initialize twice the same scene");
            _allScenePointers.Add(scenePointer);
        }

        public static bool IsScene(Type sceneType)
        {
            return sceneType.BaseType == typeof(Scene);
        }

        public static void SetLoadedScene(Type sceneType, bool deleteCurrentSceneValues)
        {
            if (IsScene(sceneType))
            {
                if (deleteCurrentSceneValues)
                {
                    _currentScene.RemoveAllToDrawGameObject();
                    _currentScene.RemoveAllToDetectCollisionsGameObject();
                }

                _currentScene = Scene.GetLoadedSceneByType(sceneType);
                //GC.Collect();

                #if DEBUG
                julienfEngine.SetDebugGameObject();
                #endif

                _currentScene.Start();
                return;
            }
            throw new Exception("The type is not a scene");
        }

        public static Scene GetLoadedSceneByType(Type sceneType)
        {
            Scene sceneToReturn = _allLoadedScenes.Find(currentScene => currentScene.GetType() == sceneType);

            if (sceneToReturn == null) throw new Exception("The scene you are trying to set is not loaded");

            return sceneToReturn;
        }

        public static void LoadScene(Type sceneType)
        {
            if (!IsScene(sceneType)) throw new Exception("The type is not a scene");

            if (_allLoadedScenes.All(currentScene => currentScene.GetType() == sceneType) && _allLoadedScenes.Count >= 1) throw new Exception("The scene you are trying to load is already loaded");

            _onLoadScene = true;
            Scene sceneToLoad = (Scene)Activator.CreateInstance(sceneType);
            _allLoadedScenes.Add(sceneToLoad);

            if (_currentScene == null) _currentScene = sceneToLoad;

            Scene currentScene = _currentScene;
            _currentScene = sceneToLoad;
            sceneToLoad.Awake();
            _currentScene = currentScene;
        }

        public static void UnloadScene(Type sceneType)
        {
            if (!IsScene(sceneType)) throw new Exception("The type is not a scene");

            Scene sceneToUnload = _allLoadedScenes.Find(currentScene => currentScene.GetType() == sceneType);
            _allLoadedScenes.Remove(sceneToUnload);
        }


        public abstract void Awake();
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

        public GameObject[] P_GameObjectsToDraw
        {
            get
            {
                return _gameObjectsToDraw.ToArray();
            }
        }

        public ICanCollide[] P_ICollideableToDetectCollisions
        {
            get
            {
                return _ICollideableToDetectCollisions.ToArray();
            }
        }

        public static Scene P_CurrentScene
        {
            get
            {
                return _currentScene;
            }
        }

        #endregion
    }
}