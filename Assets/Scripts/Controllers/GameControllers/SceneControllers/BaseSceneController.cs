using Game3D.SceneObjects;
using Game3D.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game3D.Controllers {
    
    public class BaseSceneController : BaseController {


        private BaseUI _sceneUI;



        public BaseUI SceneUI
        {
            get
            {
                return _sceneUI;
            }

            private set { }
        }



        public void SetSceneUI(BaseUI UIPrefab)
        {
            var currentScene = SceneManager.GetActiveScene();
            var ExistingUIObjects = GameObject.FindObjectsOfType<BaseUI>();

            foreach(var ExistingUIObject in ExistingUIObjects)
            {
                Destroy(ExistingUIObject.gameObject);
            }

            if (UIPrefab != null)
            {
                _sceneUI = MonoBehaviour.Instantiate(UIPrefab);
            }
        }

    }
	
}