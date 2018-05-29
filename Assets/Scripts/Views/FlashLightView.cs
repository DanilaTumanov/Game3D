using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game3D.Views
{

    public class FlashLightView : MonoBehaviour
    {


        private Image _batteryLevel;


        // Use this for initialization
        void Start()
        {
            _batteryLevel = GameObject.Find("BatteryLevel").GetComponent<Image>();
        }


        /// <summary>
        /// Установить отображение уровня батареи
        /// </summary>
        /// <param name="level"></param>
        public void SetBatteryLevel(float level)
        {
            _batteryLevel.fillAmount = level;
        }

    }
}


