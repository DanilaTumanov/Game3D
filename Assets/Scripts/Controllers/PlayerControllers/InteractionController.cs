using Game3D.SceneObjects.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.Controllers
{

    public class InteractionController : BaseController
    {

        const float THROW_FORCE = 3000;


        private Transform _interactedObject;
        private Rigidbody _interactedRigidbody;

        /// <summary>
        /// Расстояние, на котором переносится предмет
        /// </summary>
        private float _carryDistance = 1.5f;

        /// <summary>
        /// Максимальное скалярное значение вектора скорости, действующей на RB переносимого предмета. 
        /// Если скорость превышает максимальную, то предмет падает
        /// </summary>
        private float _maxCarryResistance = 2f;

        private Vector3 _carryPosMod;



        private void Start()
        {
            _carryPosMod = new Vector3(0, -0.5f, 0);
        }



        private void FixedUpdate()
        {
            if (_interactedObject != null)
            {
                Carry(_interactedObject);
            }
        }


        private void Update()
        {
            if (!Enabled)
                return;

            ProcessInteraction();
            ProcessThrow();
        }




        private void ProcessInteraction()
        {
            if (Input.GetButtonDown("Interact"))
                ToggleInteraction();
        }



        private void ProcessThrow()
        {
            if (_interactedRigidbody != null && Input.GetButtonDown("Fire1"))
            {
                var rb = _interactedRigidbody;
                CancelInteraction();
                rb.AddForce(Camera.main.transform.forward * THROW_FORCE);
            }
        }



        private void ToggleInteraction()
        {
            if(_interactedObject != null)
            {
                CancelInteraction();
            }
            else
            {
                var selectedObj = PlayerController.Instance.SelectionController.SelectedObj;

                if (selectedObj != null)
                {
                    Interact(selectedObj);
                }
            }            
        }


        private void Interact(Transform interaction)
        {
            _interactedObject = interaction;
            _interactedRigidbody = _interactedObject.GetComponent<Rigidbody>();

            if(_interactedRigidbody != null)
                _interactedRigidbody.useGravity = false;

            PlayerController.Instance.WeaponController.Disable();
            PlayerController.Instance.SelectionController.Disable();
        }


        private void CancelInteraction()
        {
            if (_interactedRigidbody != null)
                _interactedRigidbody.useGravity = true;

            _interactedObject = null;
            _interactedRigidbody = null;

            PlayerController.Instance.WeaponController.Enable();
            PlayerController.Instance.SelectionController.Enable();
        }


        private void Carry(Transform interaction)
        {
            Vector3 nextPosition = Camera.main.transform.position + _carryPosMod + Camera.main.transform.forward * _carryDistance;
            Quaternion nextRotation = Camera.main.transform.rotation;
            

            
            if (_interactedRigidbody != null)
            {

                if (_interactedRigidbody.velocity.magnitude > _maxCarryResistance)
                {
                    CancelInteraction();
                    return;
                }

                _interactedRigidbody.position = nextPosition;
                _interactedRigidbody.rotation = nextRotation;

                //_interactedRigidbody.MovePosition(nextPosition);
                //_interactedRigidbody.MoveRotation(nextRotation);

            }
            else
            {
                interaction.position = nextPosition;
                interaction.rotation = nextRotation;
            }            
        }

    }

}
