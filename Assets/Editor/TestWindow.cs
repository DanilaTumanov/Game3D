using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Game3D.Editor {

	public class TestWindow: EditorWindow {

        private GameObject _obj;
        private int _count = 1;

        private void OnGUI()
        {
            _obj = (GameObject) EditorGUILayout.ObjectField("Добавляемый объект", _obj, typeof(GameObject), false);
            _count = EditorGUILayout.IntSlider("Количество", _count, 1, 50);
        }

    }
	
}