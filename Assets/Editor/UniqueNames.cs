using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Game3D.Editor {

	public class UniqueNames : UnityEditor.Editor {

        private static readonly Dictionary<string, int> _nameDictionary = new Dictionary<string, int>();

        
        public static void SetUniqueNames()
        {
            var sceneObj = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; // Находим все объекты на сцене

            if (sceneObj != null)
            {
                foreach (var obj in sceneObj)
                {
                    PlaceInNameDictionary(obj);
                }
            }

            foreach (var i in _nameDictionary)
            {
                for (var j = 0; j < i.Value; j++)
                {
                    var gameObj = GameObject.Find(i.Key);
                    if (gameObj)
                    {
                        gameObj.name += "(" + j + ")";
                    }
                }
            }
            _nameDictionary.Clear(); // Очищаем память
        }


        /// <summary>
        /// Собирает информацию об объекте (имя и индекс)
        /// </summary>
        /// <param name="sceneObj">Объект на сцене</param>
        private static void PlaceInNameDictionary(GameObject sceneObj)
        {
            string[] tempName = sceneObj.name.Split('(');

            tempName[0] = tempName[0].Trim(); // Убираем пробелы

            if (!_nameDictionary.ContainsKey(tempName[0]))
            {
                _nameDictionary.Add(tempName[0], 0);
            }
            else
            {
                _nameDictionary[tempName[0]]++;
            }

            sceneObj.name = tempName[0];
        }

    }

}