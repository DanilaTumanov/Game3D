using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Game3D.Helpers
{

    [Serializable]
    public class SceneField
    {

        [SerializeField] private Object sceneAsset;

        [SerializeField]
        [HideInInspector]
        private string sceneName;

        public Object SceneAsset
        {
            get { return sceneAsset; }
        }

        public static implicit operator string(SceneField sceneField)
        {
            return sceneField.sceneName;
        }

        public override string ToString()
        {
            return this;
        }

    }


#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var sceneAsset = property.FindPropertyRelative("sceneAsset");
            var sceneName = property.FindPropertyRelative("sceneName");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var value = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);

            sceneAsset.objectReferenceValue = value;
            sceneName.stringValue = value != null ? value.name : String.Empty;
        }
    }

#endif

}
