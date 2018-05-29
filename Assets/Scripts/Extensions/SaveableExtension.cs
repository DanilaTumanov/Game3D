#if UNITY_EDITOR
using Game3D;
using Game3D.Interfaces;
using Game3D.Managers;
using Game3D.SceneObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Методы расширения GameObject, позволяющие ему агрегировать информацию о своих компонентах и переводить в формат для сохранения
/// </summary>
public static class SaveableExtension {

    /// <summary>
    /// Делегат, описывающий метод агрегирования сохраняемых полей компонента
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    delegate List<SaveableField> SFAggregator(Component component);


    /// <summary>
    /// Набор агрегаторов в зависимости от типов компонентов
    /// </summary>
    private static Dictionary<Type, SFAggregator> _aggregators;




    static SaveableExtension()
    {
        // Собираем информацию о методах в данном классе
        var selfMethods = typeof(SaveableExtension).GetMethods();
        
        _aggregators = new Dictionary<Type, SFAggregator>();

        // Проходимся по методам
        foreach (var method in selfMethods)
        {
            // Проверяем наличие атрибута, показывающего компонент какого типа агрегирует метод
            var attr = Attribute.GetCustomAttribute(method, typeof(SaveableFieldsAggregator));

            // Если атрибут присутствует, то..
            if (attr != null)
            {
                // Регистрируем агрегатор для типа компонента, указанного в атрибуте
                var type = (attr as SaveableFieldsAggregator).Type;
                SFAggregator action = (SFAggregator) Delegate.CreateDelegate(typeof(SFAggregator), method);
                _aggregators.Add(type, action);
            }
        }
    }



    /// <summary>
    /// Метод расширения GameObject, который возвращает информацию о сохраняемом GO в формате, необходимом для сохранения
    /// </summary>
    /// <param name="GO"></param>
    /// <returns></returns>
    public static SaveableSceneObject GetSaveableSceneObject(this GameObject GO)
    {
        SaveableSceneObject sceneObj = new SaveableSceneObject
        {
            Name = GO.name,
            PrefabResourcePath = AssetDatabase.GetAssetPath(PrefabUtility.GetPrefabParent(GO)),
            Components = GO.GetSaveableComponents()
        };

        return sceneObj;
    }


    /// <summary>
    /// Получение списка сохраняемых компонентов в формате, необходимом для сохранения
    /// </summary>
    /// <param name="GO"></param>
    /// <returns></returns>
    public static List<SaveableComponent> GetSaveableComponents(this GameObject GO)
    {
        Component[] components = GO.GetComponents<Component>();
        List<SaveableComponent> saveableComponents = new List<SaveableComponent>();
        
        // Проходимся по компонентам GO
        foreach (Component comp in components)
        {
            // Получаем тип компонента
            Type compType = comp.GetType();
            // Получаем тип соответствия для агрегатора
            Type aggrType = GetAggregatorType(compType);
            
            // Если есть агрегатор, подходящий для компонента, то..
            if (aggrType != null)
            {
                //Debug.Log(String.Format("{0} -- {1} -- {2}", comp.name, comp.GetInstanceID(), comp.GetType().Name));

                // Добавляем компонент в список, используя найденный агрегатор для получения списка полей и свойств
                saveableComponents.Add(
                    new SaveableComponent
                    {
                        Type = compType,
                        Fields = _aggregators[aggrType](comp)
                    }
                );
            }
        }

        return saveableComponents;
    }


    /// <summary>
    /// Получить тип соответствия агрегатора для типа type компонента. Данный метод будет искать агрегатор для компонента по его типу,
    /// начиная с самого типа и далее по типам родительских классов, пока не найдет наиболее близкий агрегатор или не поймет, что такого нет.
    /// В случае, если агрегатора нет, вернет null
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static Type GetAggregatorType(Type type)
    {
        if (type == typeof(Component))
            return null;

        return _aggregators.ContainsKey(type) ? type : GetAggregatorType(type.BaseType);
    }







    // ДАЛЕЕ ИДУТ АГРЕГАТОРЫ ДЛЯ НЕКОТОРЫХ КОМПОНЕНТОВ.
    // TODO: Их бы тоже вынести в отдельный класс, но уже почти 5 утра :(




    /// <summary>
    /// Агрегатор полей для компонента типа Transform
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    [SaveableFieldsAggregator(typeof(Transform))]
    public static List<SaveableField> GetTransformSaveableFields(Component component)
    {
        var transform = (Transform)component;

        List<SaveableField> saveableFields = new List<SaveableField>
        {
            new SaveableField
            {
                Type = transform.position.GetType(),
                Name = "position",
                Value = transform.position
            },

            new SaveableField
            {
                Type = transform.rotation.GetType(),
                Name = "rotation",
                Value = transform.rotation
            },

            new SaveableField
            {
                Type = transform.localScale.GetType(),
                Name = "localScale",
                Value = transform.localScale
            }
        };
        
        return saveableFields;
    }



    /// <summary>
    /// Агрегатор полей для базового компонента сцены
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    [SaveableFieldsAggregator(typeof(BaseObjectScene))]
    public static List<SaveableField> GetBAseObjectSceneSaveableFields(Component component)
    {
        List<SaveableField> saveableFields = new List<SaveableField>();

        // Получаем список членов класса
        var members = component.GetType().GetMembers();

        // Выбираем только те члены класса, которые могут быть сохранены, т.е. которые помечены атрибутом SaveableAttribute
        var saveableMembers = members.Where(member => Attribute.GetCustomAttribute(member, typeof(SaveableAttribute), true) != null);

        // Проходимся по сохраняемым членам класса
        foreach(var member in saveableMembers)
        {
            object value = null;

            // Получаем значение члена класса в зависимости от его типа
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    value = (member as FieldInfo).GetValue(component);
                    break;
                case MemberTypes.Property:
                    value = (member as PropertyInfo).GetGetMethod().Invoke(component, new object[] { });
                    break;
            }

            // Генерируем для каждого члена класса объект, по протоколу сохранения
            saveableFields.Add(new SaveableField
            {
                Type = member.GetType(),
                Name = member.Name,
                Value = value
            });
        }

        return saveableFields;
    }
}

#endif