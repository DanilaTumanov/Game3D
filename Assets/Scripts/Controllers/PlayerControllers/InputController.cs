using Game3D.Managers;
using Game3D.SceneObjects.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.Controllers
{

    public class InputController : BaseController
    {

        
        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                var flashLightController = PlayerController.Instance.FlashLightController;
                if (flashLightController.Enabled)
                    flashLightController.ToggleFlashLight();                
            }


            //if (Input.GetKeyDown(KeyCode.F5))
            //{
            //    SaveLoadManager.SaveGame();
            //}

            //if (Input.GetKeyDown(KeyCode.F9))
            //{
            //    SaveLoadManager.LoadGame();
            //}
        }
    }

}
