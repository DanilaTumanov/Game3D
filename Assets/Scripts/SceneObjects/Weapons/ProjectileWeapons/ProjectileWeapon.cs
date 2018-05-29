using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.SceneObjects.Weapon
{

    public abstract class ProjectileWeapon : Weapon
    {

        /// <summary>
        /// Скорость полета снаряда (юнитов в секунду)
        /// </summary>
        [Tooltip("Скорость полета снаряда (юнитов в секунду)")]
        [SerializeField] protected float _speed;

        /// <summary>
        /// Снаряд
        /// </summary>
        [Tooltip("Префаб снаряда")]
        [SerializeField] protected Projectile _projectile;



        public override bool Fire()
        {
            if (!base.Fire())
                return false;

            RaycastHit target;
            Quaternion rotation = _barrel.rotation;

            if (Physics.Raycast(Camera.main.GetCenter(), Camera.main.transform.forward, out target, MAX_RAYCAST_DISTANCE))
            {
                rotation = Quaternion.LookRotation(target.point - _barrel.transform.position, _barrel.up);
            }            

            var proj = Instantiate(_projectile, _barrel.position, rotation);
            proj.Rigidbody.AddForce(proj.transform.forward * _speed);

            return true;
        }

    }

}