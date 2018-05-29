using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;
using UnityEngine.Events;

namespace Game3D.UI.Modules {

	public class BaseUIModule : BaseObjectScene {

        const string UI_BUTTON_HANDLER_POSTFIX = "UIButtonHandler";


        [Tooltip("Определяет видимость модуля после загрузки на сцену")]
        public bool VisibleOnLoad = false;



        protected virtual void Start()
        {
            gameObject.SetActive(VisibleOnLoad);
            SetButtonsHandlers();
        }



        private void SetButtonsHandlers()
        {
            var buttons = gameObject.GetComponentsInChildren<Button>();

            foreach(var button in buttons)
            {
                var methodInfo = GetType().GetMethod(button.name + UI_BUTTON_HANDLER_POSTFIX, 
                    BindingFlags.NonPublic | 
                    BindingFlags.Public | 
                    BindingFlags.Instance
                );

                if(methodInfo != null)
                {
                    button.onClick.AddListener((UnityAction) Delegate.CreateDelegate(typeof(UnityAction), this, methodInfo));                    
                }
            }
        }




        public virtual void Show()
        {
            SetVisible(true);
        }

        public virtual void Hide()
        {
            SetVisible(false);
        }

        public virtual void SetVisible(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
        
    }
	
}