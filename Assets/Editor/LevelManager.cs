using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Game3D.Adapters;
using UnityEngine.SceneManagement;

namespace Game3D.Editor {

	public class LevelManager {

        
        public static void Save()
        {
            string savePath = Path.Combine(Application.dataPath, SceneManager.GetActiveScene().name + "Data.xml");
            GameObject[] gameObjectArray = GameObject.FindObjectsOfType<GameObject>();
            List<SerializableGameObject> levelsList = new List<SerializableGameObject>();
            int goCount = gameObjectArray.Length;
            
            for (int i = 0; i < goCount; i++)
            {
                var trans = gameObjectArray[i].transform;
                levelsList.Add(new SerializableGameObject
                {
                    Name = gameObjectArray[i].name,
                    Pos = trans.position,
                    Rot = trans.rotation,
                    Scale = trans.localScale
                });
            }
            
            SerializableToXML.Save(levelsList.ToArray(), savePath);
        }
	
        
        public static void Clear()
        {
            GameObject[] gameObjectArray = SceneManager.GetActiveScene().GetRootGameObjects();
            int len = gameObjectArray.Length;


            for (int i = 0; i < len; i++)
            {
                MonoBehaviour.DestroyImmediate(gameObjectArray[i]);
            }
        }

	}
	
}