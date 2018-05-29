using UnityEngine;

namespace Game3D.Interfaces
{

    /// <summary>
    /// Интерфейс взаимодействия с объектом, наносящим урон
    /// </summary>
    public interface IDamageDealer
    {
        
        /// <summary>
        /// Урон
        /// </summary>
        float Damage { get; }

        /// <summary>
        /// Направление и величина скорости снаряда
        /// </summary>
        Vector3 Velocity { get; }

    }

}


