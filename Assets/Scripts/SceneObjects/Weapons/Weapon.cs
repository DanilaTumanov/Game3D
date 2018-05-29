using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.SceneObjects.Weapon
{

    public abstract class Weapon : BaseObjectScene
    {

        protected const float MAX_RAYCAST_DISTANCE = 1000;

        /// <summary>
        /// Время перезарядки обоймы в секундах
        /// </summary>
        [Range(0, 60)]
        [Header("Время перезарядки обоймы в секундах")]
        [Tooltip("Время перезарядки обоймы в секундах")]
        [SerializeField] protected float _recharge;

        /// <summary>
        /// Время зарядки следующего снаряда
        /// </summary>
        [Range(0, 10)]
        [Header("Время зарядки следующего снаряда")]
        [Tooltip("Время зарядки следующего снаряда")]
        [SerializeField] protected float _cooldown;

        /// <summary>
        /// Емкость обоймы
        /// </summary>
        [Range(1, 500)]
        [Header("Емкость обоймы")]
        [Tooltip("Емкость обоймы")]
        [SerializeField] protected int _collarDensity;



        protected Transform _barrel;
        protected bool _onCooldown = false;
        protected bool _recharging = false;
        protected int _ammoLeft;


        public int AmmoLeft
        {
            get
            {
                return _ammoLeft;
            }
        }



        public event Action OnBeforeRecharge;
        public event Action OnAfterRecharge;



        protected virtual void Awake()
        {
            _barrel = transform.Find("Barrel");
            _ammoLeft = _collarDensity;
        }


        public virtual bool Fire()
        {
            if (_recharging || _onCooldown)
                return false;
            
            _ammoLeft--;

            if(_ammoLeft == 0)
            {
                StartCoroutine(Recharge());
            }
            else
            {
                StartCoroutine(Cooldown());
            }

            return true;
        }

        

        private IEnumerator Cooldown()
        {
            _onCooldown = true;
            yield return new WaitForSeconds(_cooldown);
            _onCooldown = false;
        }


        private IEnumerator Recharge()
        {
            _recharging = true;

            if (OnBeforeRecharge != null)
            {
                OnBeforeRecharge.Invoke();
            }

            yield return new WaitForSeconds(_recharge);

            _ammoLeft = _collarDensity;
            _recharging = false;

            if(OnAfterRecharge != null)
            {
                OnAfterRecharge.Invoke();
            }
        }


        public void SetActive(bool active)
        {
            if (!active)
            {
                StopAllCoroutines();
            }
                            
            gameObject.SetActive(active);

            if (active)
            {
                if(_recharging)
                    StartCoroutine(Recharge());
                else if (_onCooldown)
                    StartCoroutine(Cooldown());
            }
        }

    }

}
