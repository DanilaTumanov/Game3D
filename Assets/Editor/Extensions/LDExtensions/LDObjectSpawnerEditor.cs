using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game3D.Utils;

namespace Game3D.Editor {

    [CustomEditor(typeof(LDObjectSpawner))]
    public class LDObjectSpawnerEditor : UnityEditor.Editor {


        private LDObjectSpawner _target;


        

        public override void OnInspectorGUI()
        {
            _target = (LDObjectSpawner) target;

            DrawDefaultInspector();
            

        }


        private void OnSceneGUI()
        {
            
            ProcessPlaceObject();

        }



        private void ProcessPlaceObject()
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            Event currentEvent = Event.current;

            // Если нажата ЛКМ
            if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
            {
                // Перехватываем событие своим контролом
                GUIUtility.hotControl = controlID;


                RaycastHit hit;
                // TODO: Сделать возможность выбирать слои на которых размещать и вынести в отдельный метод. Не надо каждый раз считать маску
                LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");

                
                // TODO: 1000 - волшебная константа - плохо.
                if (Physics.Raycast(HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition), out hit, 10000))
                {
                    int hitLayerMask = 1 << hit.transform.gameObject.layer;
                    
                    // Проверяем кликнули ли мы на слой, на котором можно размещать наш GO.
                    if ((hitLayerMask & layerMask) == hitLayerMask)
                    {
                        SpawnAtPoint(hit.point);
                    }                        
                }

                currentEvent.Use();
            }
            else if (currentEvent.type == EventType.MouseUp)
            {
                // По отжатию кнопки мыши отпускаем перехват событий
                GUIUtility.hotControl = 0;
            }
        }


        // Расположить объект(ы) относительно точки по заданным настройкам
        private void SpawnAtPoint(Vector3 point)
        {
            GameObject instance = Instantiate(_target.prefab, point, Quaternion.identity, _target.parent);
        }

        
    }
	
}