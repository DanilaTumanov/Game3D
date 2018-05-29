using System;
using System.Collections;
using System.Collections.Generic;
using Game3D.SceneObjects.Weapon;
using UnityEngine;


namespace Game3D.Managers
{

    public class ObjectManager : MonoBehaviour
    {

        Weapon[] _weaponCollection;



        public Weapon[] WeaponCollection
        {
            get
            {
                return _weaponCollection;
            }
        }



        private void Start()
        {
            SetWeaponCollection();
        }

        

        private void SetWeaponCollection()
        {
            Transform weapons = GameObject.FindGameObjectWithTag("PlayerWeapons").transform;
            
            _weaponCollection = new Weapon[10];
            var i = 0;
            
            foreach (Transform weapon in weapons)
            {
                _weaponCollection[i++] = weapon.GetComponent<Weapon>();
            }
        }

    }

}