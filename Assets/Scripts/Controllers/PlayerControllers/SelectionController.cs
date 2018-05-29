using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.Controllers
{

    public class SelectionController : BaseController
    {
        /// <summary>
        /// Расстояние на котором срабатывает выделение
        /// </summary>
        const float SELECTION_DISTANCE = 2;

        /// <summary>
        /// Цвет выделения
        /// </summary>
        readonly private Color SELECTION_COLOR = Color.green;

        /// <summary>
        /// Слои, в которых ищем выделяемые предметы
        /// </summary>
        readonly private string[] SELECTION_LAYERS = new string[1] { "Interactions" };



        /// <summary>
        /// Кэш hitInfo
        /// </summary>
        private RaycastHit _hitInfo;

        /// <summary>
        /// Сформированная маска слоев
        /// </summary>
        private int _layerMask;

        /// <summary>
        /// Выделенный объект
        /// </summary>
        private Transform _selectedObj;

        /// <summary>
        /// Предыдущий цвет выделенного объекта (нужен, чтобы вернуть его после отключения выделения)
        /// </summary>
        private Color _oldColor;




        /// <summary>
        /// Выделенный объект
        /// </summary>
        public Transform SelectedObj
        {
            get
            {
                return _selectedObj;
            }
        }



        // Use this for initialization
        void Start()
        {
            _layerMask = GetLayerMask(SELECTION_LAYERS);
        }

        // Update is called once per frame
        void Update()
        {
            if (!Enabled)
                return;

            ProcessSelection();
        }



        /// <summary>
        /// Получить маску слоев по набору слоев
        /// </summary>
        /// <param name="layers">Массив с набором названий слоев</param>
        /// <returns></returns>
        private int GetLayerMask(string[] layers)
        {
            int mask = 0;
            for(var i = 0; i < layers.Length; i++)
            {
                mask |= 1 << LayerMask.NameToLayer(layers[i]);
            }
            return mask;
        }


        /// <summary>
        /// Обработка выделения
        /// </summary>
        private void ProcessSelection()
        {
            var hit = Physics.Raycast(
                Camera.main.transform.position, 
                Camera.main.transform.forward, 
                out _hitInfo, 
                SELECTION_DISTANCE, 
                _layerMask);
            
            if (hit)
            {
                if(!IsSelected(_hitInfo.transform))
                {
                    RemoveSelection();
                    SetSelection(_hitInfo.transform);
                }                
            }
            else if(IsSelected())
            {
                RemoveSelection();
            }
        }



        /// <summary>
        /// Выделение объекта
        /// </summary>
        /// <param name="obj"></param>
        private void SetSelection(Transform obj)
        {
            var renderer = obj.transform.GetComponent<MeshRenderer>();

            _selectedObj = obj;
            _oldColor = renderer.materials[0].color;

            if (renderer != null)
            {
                renderer.materials[0].color = SELECTION_COLOR;
            }            
        }


        /// <summary>
        /// Сброс активного выделения
        /// </summary>
        private void RemoveSelection()
        {
            if (_selectedObj == null)
                return;

            var renderer = _selectedObj.GetComponent<MeshRenderer>();
            
            if (renderer != null)
            {
                renderer.materials[0].color = _oldColor;
            }

            _selectedObj = null;
        }


        /// <summary>
        /// Есть ли хоть один выделенный объект
        /// </summary>
        /// <returns></returns>
        private bool IsSelected()
        {
            return _selectedObj != null;
        }


        /// <summary>
        /// Выделен ли объект obj
        /// </summary>
        /// <param name="obj">Объект, проверяемый на выделение</param>
        /// <returns></returns>
        private bool IsSelected(Transform obj)
        {
            return IsSelected() && (_selectedObj == _hitInfo.transform);
        }

    }

}