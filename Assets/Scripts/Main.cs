using Game3D.Managers;
using Game3D.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game3D {

    public class Main : MonoBehaviour
    {

        [Header("Сцены игры")]
        [Tooltip("Для того, чтобы добавить файл со списком сцен выберите Assets/Create/Game3D/Settings/ScenesList")]
        [SerializeField]
        private ScenesList scenesList;


        private GameObject _controllerGO;
        
        public static Main Instance { get; private set; }
        public GameObject Player { get; private set; }

        public GameSceneManager SceneManager { get; private set; }



        void Start()
        {
            Instance = this;

            Player = GameObject.FindGameObjectWithTag("Player");

            _controllerGO = new GameObject(name = "Controllers");

            SceneManager = new GameSceneManager(scenesList);


            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_controllerGO);
        }

    }

}