using Game3D.SceneObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.UI.Modules {
    
	public class BaseMenuUIModule : BaseUIModule {

        [SerializeField]
        public BaseMenuUIModule[] _submenuGameObjects;


        private Dictionary<Type, BaseMenuUIModule> _submenues = new Dictionary<Type, BaseMenuUIModule>();
        private BaseMenuUIModule _parent;



        protected override void Start()
        {
            SetSubmenuDictionary();

            base.Start();
        }


        protected virtual void Update()
        {
            ProcessInput();
        }



        private void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Return();
            }
        }


        private void SetSubmenuDictionary()
        {
            foreach(var menu in _submenuGameObjects)
            {
                _submenues.Add(menu.GetType(), menu);
            }
        }






        public virtual BaseMenuUIModule GetSubmenu<T>()
        {
            return _submenues.ContainsKey(typeof(T)) ? _submenues[typeof(T)] : null;
        }


        public virtual void Return()
        {
            if (_parent == null)
                return;

            var parent = _parent;
            Hide();
            parent.Show();
        }


        public virtual void EnterSubmenu(BaseMenuUIModule submenu)
        {
            Hide();
            submenu.Show(this);
        }


        public virtual void Show(BaseMenuUIModule parent)
        {
            _parent = parent;
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public virtual void SetVisible(bool isVisible, BaseMenuUIModule parent)
        {
            _parent = parent;
            base.SetVisible(isVisible);
        }

    }
	
}