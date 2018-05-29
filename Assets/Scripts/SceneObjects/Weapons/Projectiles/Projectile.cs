using System.Collections;
using System.Collections.Generic;
using Game3D.Interfaces;
using UnityEngine;


namespace Game3D.SceneObjects.Weapon
{

    public abstract class Projectile : BaseObjectScene, IDamageDealer
    {

        /// <summary>
        /// Урон снаряда на единицу скорости
        /// </summary>
        [SerializeField] private float _damage;

        /// <summary>
        /// Время жизни снаряда
        /// </summary>
        [SerializeField] private float _TTL = 10;

        /// <summary>
        /// Сопротивление воздуха (сила, которая действует в обратном направлении вектора скорости)
        /// </summary>
        [SerializeField] private float _airResistance = 0;


        private Vector3 _prevVelocity;
        private float _currentDamage;


        public float Damage
        {
            get
            {
                return _currentDamage;
            }
        }

        public Vector3 PrevVelocity
        {
            get
            {
                return _prevVelocity;
            }
        }




        protected virtual void Start()
        {
            StartCoroutine(Destroy());
        }


        protected virtual void FixedUpdate()
        {
            // Добавляем объекту силу сопротивления воздуха, дифференцированную по времени. Она будет обратна вектору скорости
            Rigidbody.AddForce(Rigidbody.velocity.normalized * -1 * _airResistance * Time.fixedDeltaTime);
        }


        protected virtual void Update()
        {
            CalculateDamage();
        }
        

        protected virtual void LateUpdate()
        {
            _prevVelocity = Rigidbody.velocity;
        }


        protected virtual void OnCollisionEnter(Collision collision)
        {
            DoDamage(collision.gameObject.GetComponent<IDamagable>(), collision);
            Deactivate();
        }




        /// <summary>
        /// Отключение снаряда
        /// </summary>
        protected virtual void Deactivate()
        {
            _damage = 0;
        }


        protected virtual void CalculateDamage()
        {
            _currentDamage = _damage * Rigidbody.velocity.magnitude;
        }


        public void DoDamage(IDamagable damagableObj, Collision collision)
        {
            if(damagableObj != null)
                damagableObj.TakeDamage(this, collision);
        }


        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(_TTL);
            if(gameObject != null)
                Destroy(gameObject);
        }
       
    }

}