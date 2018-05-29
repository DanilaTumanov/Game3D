using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game3D.Utils {


    /// <summary>
    /// Компонент для удобной расстановки объектов
    /// </summary>
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class LDObjectSpawner : BaseObjectScene {
        
		public GameObject prefab;
        public Transform parent;

    }
	
}