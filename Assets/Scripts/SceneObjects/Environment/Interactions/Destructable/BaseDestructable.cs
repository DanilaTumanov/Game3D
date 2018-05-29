using System.Collections;
using System.Collections.Generic;
using Game3D.Interfaces;
using UnityEngine;


namespace Game3D.SceneObjects.Environment
{

    public abstract class BaseDestructable : BaseObjectScene, IDamagable
    {

        /// <summary>
        /// Максимальный угол входа снаряда в разрушаемый объект. Нужно для рассчетов урона, зависящего от угла соприкосновения
        /// </summary>
        public const float MAX_COLLISION_ANGLE = 100;



        [SerializeField] private float _MaxHP;


        private float _hp;




        private void Start()
        {
            _hp = _MaxHP;
        }




        public virtual void TakeDamage(IDamageDealer damageDealer, Collision collision)
        {
            Damage(damageDealer.Damage);
        }


        public virtual void Damage(float damage)
        {
            _hp -= damage;

            if(_hp <= 0)
            {
                _hp = 0;
                Die();
            }
        }





        protected virtual void Die()
        {
            if (gameObject != null)
                Destroy(gameObject);
        }

    }

}