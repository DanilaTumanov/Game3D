using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.SceneObjects.Weapon
{

    public class GravitationMine : Trap
    {

        [SerializeField]
        private float _freezeHeight = 1;

        [SerializeField]
        private float _actionTime = 3;

        private Vector3 _freezePosition;
        private Coroutine _actionTimer;

        public Coroutine ActionTimer
        {
            get
            {
                return _actionTimer;
            }
        }

        protected override void Start()
        {
            base.Start();
            _freezePosition = new Vector3(0, _freezeHeight, 0);
        }


        protected override void OnCollisionEnter(Collision collision)
        {
            if (!CanTrap(collision.gameObject))
                return;

            base.OnCollisionEnter(collision);
            
            Rigidbody rbObj = collision.gameObject.GetComponent<Rigidbody>();
            if (rbObj != null)
            {
                StartCoroutine(ThrowObj(rbObj));
            }
        }


        private IEnumerator ThrowObj(Rigidbody rbObj)
        {
            _actionTimer = StartCoroutine(SetActionTimer());

            // Запоминаем было ли твердое тело кинематическое
            var wasKinematic = rbObj.isKinematic;

            // Делаем его кинематическим, т.к. воздействует сторонняя сила
            rbObj.isKinematic = true;

            while (_triggered)
            {
                //rbObj.transform.position = transform.position + _freezePosition;
                rbObj.position = transform.position + _freezePosition;
                yield return new WaitForFixedUpdate();   
            }
            
            // Возвращаем настройку кинематики в исходное состояние
            rbObj.isKinematic = wasKinematic;

            // TODO: Нужно более конкретное место для уничтожения объекта. Нужна система работы ловушек с длительным воздействием
            Destroy(gameObject);
        }

        private IEnumerator SetActionTimer()
        {
            yield return new WaitForSeconds(_actionTime);
            _triggered = false;
        }

    }

}