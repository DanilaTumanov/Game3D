using Game3D.SceneObjects.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.Controllers
{

    public class WeaponController : BaseController
    {

        const int DEFAULT_WEAPON_INDEX = 0;
        const int MAX_WEAPON_COUNT = 2;

        private SceneObjects.Weapon.Weapon _weapon;
        private Views.WeaponView _view;
        int _weaponIndex = DEFAULT_WEAPON_INDEX;

        private void Start()
        {
            _view = FindObjectOfType<Views.WeaponView>();
            SetActiveWeapon(_weaponIndex);            
        }


        private void Update()
        {
            if (!Enabled)
                return;

            ProcessFire();
            ProcessChangeWeapon();
        }






        public override void Enable()
        {
            base.Enable();

            ShowWeapon();
        }


        public override void Disable()
        {
            base.Disable();

            HideWeapon();
        }



        public void ShowWeapon()
        {
            if(_weapon != null)
            {
                _weapon.SetActive(true);
            }
        }

        public void HideWeapon()
        {
            if (_weapon != null)
            {
                _weapon.SetActive(false);
            }
        }






        private void ProcessFire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                
                if(_weapon != null)
                {
                    if(_weapon.Fire())
                        _view.SetAmmoCount(_weapon.AmmoLeft);
                }
            }
        }


        private void ProcessChangeWeapon()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if(scroll > 0)
            {
                SetNextWeapon();
            }
            else if(scroll < 0)
            {
                SetPrevWeapon();
            }
        }


        private void SetNextWeapon()
        {
            _weaponIndex = Math.Abs(++_weaponIndex % MAX_WEAPON_COUNT);
            SetActiveWeapon(_weaponIndex);
        }

        private void SetPrevWeapon()
        {
            _weaponIndex = Math.Abs(--_weaponIndex % MAX_WEAPON_COUNT);
            SetActiveWeapon(_weaponIndex);
        }


        private void SetActiveWeapon(int weaponIndex)
        {
            var weapon = PlayerController.Instance.ObjectManager.WeaponCollection[weaponIndex];

            if (_weapon != null)
            {
                _weapon.SetActive(false);
            }

            _weapon = weapon;

            if (_weapon != null)
            {
                _weapon.SetActive(true);
                _view.SetAmmoCount(_weapon.AmmoLeft);
            }
            
        }
        
    }

}