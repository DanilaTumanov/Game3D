using Game3D.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Game3D.Drivers {

    /// <summary>
    /// Тестовый драйвер данных. Для проверки работы DataManager
    /// </summary>
    public class TestDataDriver : ExternalDataDriver
    {

        Dictionary<string, SaveableSceneObjects> _savedObjects;


        public TestDataDriver()
        {
            _savedObjects = new Dictionary<string, SaveableSceneObjects>();
        }


        public override SaveableSceneObjects Load(string path)
        {
            return _savedObjects[path];
        }
        
        public override void Save(SaveableSceneObjects saveableSceneObjects, string path)
        {
            _savedObjects.Add(path, saveableSceneObjects);
        }
    }

}