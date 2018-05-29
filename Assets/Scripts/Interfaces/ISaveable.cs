using Game3D.Managers;
using System.Collections.Generic;

namespace Game3D.Interfaces
{

    /// <summary>
    /// Интерфейс для обеспечения агрегации данных для сохранения
    /// </summary>
    public interface ISaveable
    {
        List<SaveableField> GetSaveableFields();
    }

}