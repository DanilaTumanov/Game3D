using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game3D.Adapters;
using System.Xml.Serialization;
using System;
using System.IO;

namespace Game3D.Editor {

	public class SerializableToXML {


        private static XmlSerializer _formatter;


        static SerializableToXML()
        {
            _formatter = new XmlSerializer(typeof(SerializableGameObject[]));
        }


        public static void Save(SerializableGameObject[] gameObjectArray, string path)
        {
            if (gameObjectArray == null && !String.IsNullOrEmpty(path))
                return;

            if (gameObjectArray.Length <= 0)
                return;

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                _formatter.Serialize(fs, gameObjectArray);
            }
        }


        public static SerializableGameObject[] Load(string path)
        {
            SerializableGameObject[] result;

            if (!File.Exists(path))
                return default(SerializableGameObject[]);

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                result = (SerializableGameObject[])_formatter.Deserialize(fs);
            }

            return result;
        }

    }
	
}