using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.UI.Modules {

	public class GameSceneMainMenuUIModule : BaseMenuUIModule {

		private void ExitUIButtonHandler()
        {
            Main.Instance.SceneManager.LoadMainMenu();
        }
		
	}
	
}