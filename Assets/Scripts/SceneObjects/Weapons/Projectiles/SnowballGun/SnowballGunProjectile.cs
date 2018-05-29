using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.SceneObjects.Weapon
{

    public class SnowballGunProjectile : Projectile
    {

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            if(gameObject != null)
                Destroy(gameObject);
        }

    }

}
