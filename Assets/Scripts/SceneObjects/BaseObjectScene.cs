using System.Collections;
using System.Collections.Generic;
using Game3D.Managers;
using UnityEngine;


namespace Game3D.SceneObjects
{

    /// <summary>
    /// Базовый класс
    /// </summary>
    public abstract partial class BaseObjectScene : MonoBehaviour
    {
        private Rigidbody _rigidbody;



        #region Свойства

        public Rigidbody Rigidbody
        {
            get
            {
                return _rigidbody;
            }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }

            set
            {
                transform.position = value;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return transform.localScale;
            }

            set
            {
                transform.localScale = value;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return transform.rotation;
            }

            set
            {
                transform.rotation = value;
            }
        }

        public Vector3 Velocity
        {
            get
            {
                return Rigidbody != null ? Rigidbody.velocity : Vector3.zero;
            }

            set
            {
                Rigidbody.velocity = value;
            }
        }

        #endregion



        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }



        public void SetLayer(int layer)
        {
            SetLayer(transform, layer);
        }


        static public void SetLayer(Transform transform, int layer)
        {
            transform.gameObject.layer = layer;

            if (transform.childCount > 0)
            {
                foreach(Transform childTransform in transform)
                {
                    SetLayer(childTransform, layer);
                }
            }
        }

        public List<SaveableField> GetSaveableFields()
        {
            return new List<SaveableField>();
        }
    }

}