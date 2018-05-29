using Game3D.SceneObjects.Weapon;
using UnityEngine;

namespace Game3D.Interfaces
{

    /// <summary>
    /// Интерфейс взаимодействия с объектом, умеющим обрабатывать попадание в ловушку
    /// </summary>
    public interface ITrapable
    {

        /// <summary>
        /// Установка состояния при попадании в ловушку.
        /// </summary>
        /// <param name="trap">Ловушка, в которую попал объект</param>
        void SetTrapped(Trap trap);

    }

}