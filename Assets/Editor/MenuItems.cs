using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Game3D.Editor {

	public class MenuItems {
        

        [MenuItem("Game3D/Тестовый пункт %#a", false)]
		private static void TestMenuOption()
        {
            EditorWindow.GetWindow(typeof(TestWindow), false, "Добавить объект");
        }

        [MenuItem("Game3D/Установить уникальные имена %#u", false)]
        private static void SetUniqueNames()
        {
            UniqueNames.SetUniqueNames();
        }

        [MenuItem("Game3D/Сохранить сцену", false)]
        private static void SaveScene()
        {
            LevelManager.Save();
        }

        [MenuItem("Game3D/Очистить сцену", false)]
        private static void ClearScene()
        {
            LevelManager.Save();
            if(EditorUtility.DisplayDialog("Очистка сцены", "Вы действительно хотите очистить сцену?", "Да", "Отмена"))
            {
                LevelManager.Clear();
            }
        }
    }
	
}