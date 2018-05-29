using Game3D.SceneObjects;
using Game3D.UI.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.UI {

	public abstract class BaseUI : MonoBehaviour {

        private Dictionary<Type, BaseUIModule> _UImodules = new Dictionary<Type, BaseUIModule>();


        private void Start()
        {
            SetUIModules();
        }


        private void SetUIModules()
        {
            var modules = gameObject.GetComponentsInChildren<BaseUIModule>();

            foreach(var module in modules)
            {
                if(!_UImodules.ContainsKey(module.GetType()))
                    _UImodules.Add(module.GetType(), module);                
            }
        }


        public void ShowModule<T>()
        {
            SetModuleVisibility<T>(true);
        }


        public void HideModule<T>()
        {
            SetModuleVisibility<T>(false);
        }


        public void SetModuleVisibility<T>(bool visible)
        {
            if (!_UImodules.ContainsKey(typeof(T)))
                return;

            _UImodules[typeof(T)].SetVisible(visible);
        }

    }
	
}