using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.SceneObjects.Controllers
{

    [RequireComponent(typeof(AIMoveToController))]
    public class CatController : BaseCharacterController
    {

        [Saveable]
        public float maxTargetDistance = 1000f;
        
        private AIMoveToController _moveTo;
        
        private void Start()
        {
            _moveTo = GetComponent<AIMoveToController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                RaycastHit hitInfo;
                int layerMask = 1 << LayerMask.NameToLayer("Ground");
                
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, maxTargetDistance, layerMask))
                {
                    _moveTo.SetTarget(hitInfo.point);
                }                
            }
        }
    }

}