using Game3D.Controllers;
using Game3D.Helpers;
using Game3D.SceneObjects;
using Game3D.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;
using System;
using Game3D.UI;
using System.Linq;

namespace Game3D.Managers {

	public class GameSceneManager {

        private const string SCENE_CONTROLLER_POSTFIX = "SceneController";

        private ScenesList _scenes;
        private Dictionary<string, SceneInfo> _gameScenes = new Dictionary<string, SceneInfo>();
        private BaseSceneController _currentSceneController;
        private SceneInfo _currentScene;
        private SceneInfo _loadingScene;
        private GameObject _sceneControllerGO;

        private Type[] _assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
        private Dictionary<string, Type> _sceneControllers = new Dictionary<string, Type>();

        public GameSceneManager(ScenesList scenes)
        {
            _scenes = scenes;
            ConvertGameScenes(scenes.gameScenes);
            SetCurrentScene(_scenes.mainMenu);
        }


        private void ConvertGameScenes(SceneInfo[] gameScenes)
        {
            int gameScenesCount = gameScenes.Length;
            
            foreach(var sceneInfo in gameScenes)
            {
                _gameScenes.Add(sceneInfo, sceneInfo);
            }
        }

        private void OnLoadSceneCompleted(AsyncOperation sceneLoading)
        {
            SetCurrentScene(_loadingScene);
        }


        private Type GetSceneController(string sceneName)
        {
            if (_sceneControllers.ContainsKey(sceneName))
            {
                return _sceneControllers[sceneName];
            }

            var sceneControllers = _assemblyTypes.Where(t => t.Name == sceneName + SCENE_CONTROLLER_POSTFIX);
            
            if (sceneControllers.Count() != 0)
            {
                _sceneControllers.Add(sceneName, sceneControllers.First());
                return _sceneControllers[sceneName];
            }

            return null;
        }

        

        private void SetCurrentScene(SceneInfo scene)
        {
            _currentScene = scene;
            InitCurrentSceneController();
        }



        private void InitCurrentSceneController()
        {
            Type currentSceneController = GetSceneController(_currentScene);
            
            if (currentSceneController != null)
            {
                _sceneControllerGO = new GameObject("SceneController");
                BaseSceneController sceneController = (BaseSceneController) _sceneControllerGO.AddComponent(currentSceneController);

                if (_currentScene.SceneUIPrefab != null)
                {
                    sceneController.SetSceneUI(_currentScene.SceneUIPrefab);
                }
            }
                
        }









        private void LoadScene(SceneInfo sceneInfo)
        {

            var sceneLoading = SceneManager.LoadSceneAsync(sceneInfo);

            _loadingScene = sceneInfo;

            sceneLoading.completed += OnLoadSceneCompleted;
        }



        public void LoadMainMenu()
        {
            LoadScene(_scenes.mainMenu);
        }

        public void LoadGameScene(string name)
        {
            if (_gameScenes.ContainsKey(name))
            {
                LoadScene(_gameScenes[name]);
            }
        }

    }
	
}