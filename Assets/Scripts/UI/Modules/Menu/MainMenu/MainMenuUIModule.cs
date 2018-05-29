using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.UI.Modules {

	public class MainMenuUIModule : BaseMenuUIModule {

		private void StartGameUIButtonHandler()
        {
            Main.Instance.SceneManager.LoadGameScene("SciFi");
        }

        private void SettingsUIButtonHandler()
        {
            EnterSubmenu(GetSubmenu<MainMenuSettingsUIModule>());
        }

    }
	
}