using System.Collections;
using System.Collections.Generic;
using Game3D.Interfaces;
using Game3D.SceneObjects.Weapon;
using UnityEngine;


namespace Game3D.SceneObjects.Environment
{

    public class Sphere : BaseDestructable
    {

        public override void TakeDamage(IDamageDealer damageDealer, Collision collision)
        {
            var damage = damageDealer.Damage;
            
            if (damageDealer is Projectile)
            {
                // Рассчитываем модификатов. Находим угол последнего значения вектора скорости до столкновения и 
                // обратной нормали касания (направленной к центру сферы).
                // вычитаем угол из максимального угла вхождения и рассчитываем процентное отношение, чтобы модифицировать урон
                // Пример: при вхождении снаряда строго в направлении центра сферы угол будет равн 0, значит урон в обратной пропорции
                // будет иметь модификатор 1, а при вхождении снаряда по-касательной угол будет стремиться к максимальному углу и 
                // модификатор соответственно будет стремиться к 0, а с ним и урон :)
                var mod = (MAX_COLLISION_ANGLE - Vector3.Angle(collision.contacts[0].normal * -1, (damageDealer as Projectile).PrevVelocity)) / MAX_COLLISION_ANGLE;

                damage *= mod;
            }
            
            Damage(damage);
        }

    }

}


