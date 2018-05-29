using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game3D.Views
{

    public class WeaponView : MonoBehaviour
    {


        private Image _fade;
        private Text _ammo;



        void Start()
        {
            _fade = GameObject.Find("Fade").GetComponent<Image>();
            _ammo = GameObject.Find("Ammo").GetComponent<Text>();
        }

        

        public void SetReloadStatus(float status)
        {
            _fade.fillAmount = status;
        }


        public void SetAmmoCount(int count)
        {
            _ammo.text = count.ToString();
        }

    }

}
