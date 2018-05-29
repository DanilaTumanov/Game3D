using Game3D.SceneObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D {

    [RequireComponent(typeof(Animator))]
	public class HumanoidIKController : BaseObjectScene {

        [Tooltip("Слои, которые считаются землей")]
        public LayerMask groundLayersMask = 0;

        [Tooltip("На каком расстоянии ноги прилипают к земле")]
        public float maxGroundDistance = 2f;

        [Tooltip("Смещение ступней относительно земли")]
        [SerializeField]
        private Vector3 _footOffset = new Vector3(0, 0.1f, 0);

        [Tooltip("Корректировка поворота ступней")]
        [SerializeField]
        private Vector3 _footRotationAdjustment = new Vector3(0, 0, 0);

        protected Animator _animator;

        private Transform _rFootTransform, _lFootTransform;

        private float _rFootWeight, _lFootWeight;



        protected virtual void OnValidate()
        {
            if(groundLayersMask == 0)
            {
                groundLayersMask = new LayerMask()
                {
                    value = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Buildings")
                };
            }
        }



        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();
            
            _rFootTransform = _animator.GetBoneTransform(HumanBodyBones.RightFoot);
            _lFootTransform = _animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        }

        protected virtual void OnAnimatorIK(int layerIndex)
        {
            _rFootWeight = _animator.GetFloat("RightFootWeight");
            _lFootWeight = _animator.GetFloat("LeftFootWeight");

            ProcessFootIK();
        }

        private void ProcessFootIK()
        {            
            AttachToGround(_rFootTransform, AvatarIKGoal.RightFoot, _rFootWeight, _footOffset, _footRotationAdjustment);
            AttachToGround(_lFootTransform, AvatarIKGoal.LeftFoot, _lFootWeight, _footOffset, _footRotationAdjustment);
        }

        private void AttachToGround(Transform transform, AvatarIKGoal goal, float weight, Vector3 offset, Vector3 rotationAdjustment)
        {
            _animator.SetIKPositionWeight(goal, weight);
            _animator.SetIKRotationWeight(goal, weight);

            RaycastHit groundHit;

            if(Physics.Raycast(
                    _animator.GetIKPosition(goal) + Vector3.up,
                    Vector3.down,
                    out groundHit,
                    maxGroundDistance,
                    groundLayersMask
                ))
            {
                _animator.SetIKPosition(goal, groundHit.point + offset);
                //Debug.DrawRay(transform.position, Vector3.ProjectOnPlane(transform.forward, groundHit.normal), Color.red, 0.3f);
                _animator.SetIKRotation(
                    goal, 
                    Quaternion.LookRotation(
                        Vector3.ProjectOnPlane(
                            Quaternion.Euler(rotationAdjustment) * transform.forward, 
                            groundHit.normal
                        ), 
                        groundHit.normal
                    )
                );
            }
        }
    }
	
}