using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game3D.SceneObjects {

    public class FlashLight : BaseObjectScene {

        /// <summary>
        /// Максимальный заряд фонарика
        /// </summary>
        public float maxCharge = 10f;

        /// <summary>
        /// Расход батареи в секунду
        /// </summary>
        public float consumption = 1f;

        /// <summary>
        /// Восстановление батареи при выключении фонарика
        /// </summary>
        public float recovery = 2f;



        private Light _light;
        private float _charge;
        private float _maxIntensity;
        

        public bool Enabled {
            get {
                return _light.enabled;
            }
        }

        public float Charge
        {
            get
            {
                return _charge;
            }
        }

        // Use this for initialization
        void Start() {
            _light = GetComponent<Light>();
            _charge = maxCharge;
            _maxIntensity = _light.intensity;

            Off();
        }

        // Update is called once per frame
        void Update() {

            ProcessBattery();
            ProcessPower();

        }
        


        public void On()
        {
            if (_light.enabled)
                return;

            _light.enabled = true;
        }


        public void Off()
        {
            if (!_light.enabled)
                return;

            _light.enabled = false;
        }



        /// <summary>
        /// Обработка батареи
        /// </summary>
        private void ProcessBattery()
        {
            if (Enabled)
            {
                _charge -= consumption * Time.deltaTime;
                if(_charge < 0)
                {
                    _charge = 0;
                    Off();
                }
            }
            else if(_charge < maxCharge)
            {
                _charge += recovery * Time.deltaTime;
                if(_charge > maxCharge)
                {
                    _charge = maxCharge;
                }
            }
        }


        /// <summary>
        /// Обработка мощности луча
        /// </summary>
        private void ProcessPower()
        {
            if (Enabled)
            {
                _light.intensity = _maxIntensity * (_charge / maxCharge);
            }
        }
        
    }

}