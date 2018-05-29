using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.Controllers
{

    public class FlashLightController : BaseController
    {
        
        private SceneObjects.FlashLight _flashLight;
        private Views.FlashLightView _view;


        public void Start()
        {
            _flashLight = FindObjectOfType<SceneObjects.FlashLight>();
            _view = FindObjectOfType<Views.FlashLightView>();
        }


        private void Update()
        {
            if (!Enabled)
                return;

            ProcessVerticalTracking();
        }


        public override void Enable()
        {
            base.Enable();
            InvokeRepeating("ProcessView", 0, 0.5f);
        }


        public override void Disable()
        {
            base.Enable();
            CancelInvoke();
        }


        public void SetFlashLightActive(bool active)
        {
            if (active)
            {
                _flashLight.On();
            }
            else
            {
                _flashLight.Off();
            }
        }

        public void ToggleFlashLight()
        {
            SetFlashLightActive(!_flashLight.Enabled);
        }


        private void ProcessView()
        {
            _view.SetBatteryLevel(_flashLight.Charge / _flashLight.maxCharge);
        }


        private void ProcessVerticalTracking()
        {
            _flashLight.transform.rotation = Quaternion.Slerp(_flashLight.transform.rotation, Camera.main.transform.rotation, 0.1f);
        }
    }
}