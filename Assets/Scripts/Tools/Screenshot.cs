using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D
{

    public class Screenshot : BaseObjectScene
    {
        const string DEFAULT_SCREENSHOT_NAME = @"D:\Game3DScreenshot.png";

        public string path = DEFAULT_SCREENSHOT_NAME;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                print("screenshot");
                ScreenCapture.CaptureScreenshot(path != string.Empty ? path : DEFAULT_SCREENSHOT_NAME);
            }
            
        }

    }

}