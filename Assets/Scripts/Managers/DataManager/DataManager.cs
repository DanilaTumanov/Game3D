using Game3D.Drivers;
using Game3D.SceneObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;

namespace Game3D.Managers {

    
    /// <summary>
    /// Класс работы с внешними данными.
    /// </summary>
	public static class DataManager {


        /// <summary>
        /// Драйвер для чтения и записи внешних данных
        /// </summary>
        private static ExternalDataDriver _dataDriver;

        
        //private Dictionary<string _fieldsList;


        static DataManager()
        {
            _dataDriver = new TestDataDriver();
        }

        /// <summary>
        /// Сохранить игру
        /// </summary>
	    public static void Save(SaveableSceneObjects saveableObjects)
        {
            _dataDriver.Save(saveableObjects, "test");
        }

        
        /// <summary>
        /// Загрузить игру
        /// </summary>
        public static SaveableSceneObjects Load()
        {
            return _dataDriver.Load("test");
        }
        
    }




    // НИЖЕ ОПИСАНА СТРУКТУРА ДАННЫХ СОХРАНЕНИЯ И ЗАГРУЗКИ ИГРЫ ДЛЯ ОБЕСПЕЧЕНИЯ ПРОТОКОЛА ВЗАИМОДЕЙСТВИЯ С ДРАЙВЕРАМИ ДАННЫХ
    // TODO: Это нужно бы вынести в отдельный файл, но уже 4 часа ночи :(


    public class SaveableSceneObjects : List<SaveableSceneObject> { }


    public struct SaveableSceneObject
    {
        public string Name { get; set; }
        public string PrefabResourcePath { get; set; }
        public List<SaveableComponent> Components { get; set; }
    }


    public struct SaveableComponent
    {
        public Type Type { get; set; }
        public List<SaveableField> Fields { get; set; }
    }


    public struct SaveableField
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
