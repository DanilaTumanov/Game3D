using UnityEngine;

namespace Game3D.Interfaces
{

    /// <summary>
    /// Интерфейс взаимодействия с объектом, принимающим урон
    /// </summary>
    public interface IDamagable
    {

        /// <summary>
        /// Принять урон от объекта, способного нанести урон
        /// </summary>
        /// <param name="damageDealer">Объект, нанесший урон</param>
        /// <param name="collision">Коллизия, вызвывшая урон</param>
        void TakeDamage(IDamageDealer damageDealer, Collision collision);

    }

}
