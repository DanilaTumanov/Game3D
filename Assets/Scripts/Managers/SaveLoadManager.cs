#if UNITY_EDITOR
using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Game3D.Managers {


    /// <summary>
    /// Менеджер сохранения и загрузки игры
    /// </summary>
	public static class SaveLoadManager {

        /// <summary>
        /// Сохранить игру
        /// </summary>
		public static void SaveGame()
        {
            DataManager.Save(AggregateSaveableData());
        }

        /// <summary>
        /// Загрузить игру
        /// </summary>
        public static void LoadGame()
        {
            SetLoadedData(DataManager.Load());
        }




        /// <summary>
        /// Агрегировать сохраняемые данные
        /// </summary>
        /// <returns></returns>
        private static SaveableSceneObjects AggregateSaveableData()
        {
            // Находим все GO, имеющие в компонентах класс SaveableObject, т.е. отмеченные для сохранения
            SaveableObject[] objs = GameObject.FindObjectsOfType<SaveableObject>();

            SaveableSceneObjects aggregatedData = new SaveableSceneObjects();

            // Проходимся по найденным объектам
            foreach (var obj in objs)
            {
                GameObject GO = obj.gameObject;

                // Добавляем в собранные данные информацию о каждом из GO в формате, необходимом для сохранения
                aggregatedData.Add(GO.GetSaveableSceneObject());
            }

            return aggregatedData;
        }


        /// <summary>
        /// Изменить состояние сцены в соответствии с загруженной информацией
        /// </summary>
        /// <param name="objects"></param>
        private static void SetLoadedData(SaveableSceneObjects objects)
        {

            // Сначала очистим сцену от сохраняемых объектов. Не сохраняемые объекты мы не имеем права трогать, т.к. на них не могут влиять
            // механизмы системы сохранения/загрузки
            ClearScene();

            // Проходимся по всем загруженным объектам
            foreach (var obj in objects)
            {
                // Вот так будем подгружать ассеты
                //MonoBehaviour.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefabs/Characters/Robots/Robo3.prefab"), Vector3.zero, Quaternion.identity);

                // Создаем новый объект в нулевой точке на сцене, на основании ассета, загруженного из ресурсов.
                GameObject GO = MonoBehaviour.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(obj.PrefabResourcePath), Vector3.zero, Quaternion.identity);

                // Проходимся по всем компонентам загруженного объекта
                foreach (var component in obj.Components)
                {
                    // TODO: Сейчас не поддерживается наличие нескольких компонентов одного типа. Нужно переделать. Возможно стоит разделять разные компоненты по ID...

                    // Находим соответствующие компоненты на созданном объекте
                    var GOComp = GO.GetComponent(component.Type);
                    
                    // Проходимся по всем полям (свойства там тоже могут быть, просто поле надо было как-то назвать понятно и вот поэтому поля)
                    foreach (var field in component.Fields)
                    {
                        var GOMembers = GOComp.GetType().GetMember(field.Name);
                        
                        // Устанавливаем значение полей в соответствии с загруженными значениями
                        switch (GOMembers[0].MemberType)
                        {
                            case MemberTypes.Field:
                                (GOMembers[0] as FieldInfo).SetValue(GOComp, field.Value);
                                break;
                            case MemberTypes.Property:
                                (GOMembers[0] as PropertyInfo).GetSetMethod().Invoke(GOComp, new object[] { field.Value });
                                break;
                        }
                    }
                }
            }
        }



        /// <summary>
        /// Очищение сцены от сохраняемых объектов.
        /// </summary>
        private static void ClearScene()
        {
            SaveableObject[] objs = GameObject.FindObjectsOfType<SaveableObject>();
            foreach (var obj in objs)
            {
                GameObject.Destroy(obj.gameObject);
            }
        }

    }
	
}

#endif