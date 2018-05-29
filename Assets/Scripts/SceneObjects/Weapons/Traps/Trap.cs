using Game3D.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.SceneObjects.Weapon
{

    public abstract class Trap : BaseObjectScene
    {

        [SerializeField]
        protected float _activationTime;

        [SerializeField]
        protected bool _active = false;


        protected bool _triggered;
        protected int _trapableLayerMask;

        protected bool Triggered
        {
            get
            {
                return _triggered;
            }
        }

        protected virtual void Start()
        {
            StartCoroutine(DelayedActivation());
            _trapableLayerMask = GetTrapableLayerMask();
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!CanTrap(collision.gameObject))
                return;

            _triggered = true;
            
            ITrapable trapableObj = collision.gameObject.GetComponent<ITrapable>();

            if (trapableObj != null)
            {
                trapableObj.SetTrapped(this);
            }
        }


        protected virtual bool CanTrap(GameObject GO)
        {
            return _active &&
                !_triggered &&
                ((_trapableLayerMask | (1 << GO.layer)) == _trapableLayerMask);
        }


        protected virtual IEnumerator DelayedActivation()
        {
            yield return new WaitForSeconds(_activationTime);
            _active = true;
        }


        // TODO: вот тут нарушение DRY, но у меня совсем нет времени :( надо сделать хэлпер
        private int GetTrapableLayerMask()
        {

            // TODO: Нужно отдельно задавать слои. Тут это делать плохо.
            int includeLayers = int.MaxValue;

            int excludeLayers = 1 << LayerMask.NameToLayer("Ground");

            return includeLayers ^ excludeLayers;
        }
    }

}