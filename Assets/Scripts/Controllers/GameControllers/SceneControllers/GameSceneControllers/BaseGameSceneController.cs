using Game3D.SceneObjects;
using Game3D.UI.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Game3D.Controllers {

	public class BaseGameSceneController : BaseSceneController {

        private FirstPersonController _playerController;
        private bool _paused = false;


        private FirstPersonController PlayerController
        {
            get
            {
                return _playerController ?? (_playerController = FindObjectOfType<FirstPersonController>());
            }
        }




        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                print("SciFiScene switch to MainMenu");
                Main.Instance.SceneManager.LoadMainMenu();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }


        protected virtual void TogglePause()
        {
            _paused = !_paused;
            Time.timeScale = _paused ? 0 : 1;
            PlayerController.enabled = !_paused;
            Cursor.lockState = _paused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = _paused;
            SceneUI.SetModuleVisibility<GameSceneMainMenuUIModule>(_paused);
        }

    }
	
}