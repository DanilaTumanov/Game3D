using Game3D.SceneObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D {

	public class BlueManIKController : HumanoidIKController
    {

        [Tooltip("Слои для вертикальных поверхностей, к которым будет притрагиваться персонаж при приближении")]
        public LayerMask touchableSurfaces;

        [Tooltip("Максимальное расстояние начиная с которого персонаж попробует коснуться вертикальной поверхности. Считается от центра персонажа (от груди)")]
        public float maxTouchDistance = 0.5f;

        [Tooltip("Корректировка поворота грудной клетки")]
        [SerializeField]
        private Vector3 _chestRotationAdjustment = new Vector3(0, 0, 0);

        [Tooltip("Корректировка поворота ладони (forward смотрит в направлении пальцев)")]
        [SerializeField]
        private Vector3 _palmRotationAdjustment = new Vector3(0, 0, 0);


        private Transform _chest;



        protected override void Start()
        {
            base.Start();

            _chest = _animator.GetBoneTransform(HumanBodyBones.Chest);
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (touchableSurfaces == 0)
            {
                touchableSurfaces = new LayerMask()
                {
                    value = 1 << LayerMask.NameToLayer("Walls")
                };
            }
        }

        protected override void OnAnimatorIK(int layerIndex)
        {
            base.OnAnimatorIK(layerIndex);

            ProcessWallTouch();
        }

        private void ProcessWallTouch()
        {
            Collider[] walls;

            //Debug.DrawRay(_chest.position, transform.forward * maxTouchDistance, Color.blue, 1);

            walls = Physics.OverlapSphere(_chest.position, maxTouchDistance, touchableSurfaces);

            if (walls.Length > 0)
            {
                Collider wall = walls[0];
                Vector3 closestPoint = wall.ClosestPointOnBounds(_chest.position);
                RaycastHit wallHit;
                Vector3 toClosestPosition = closestPoint - _chest.position;

                Physics.Raycast(
                    _chest.position,
                    toClosestPosition,
                    out wallHit,
                    toClosestPosition.magnitude + 0.1f,
                    touchableSurfaces
                );

                AvatarIKGoal goal = transform.InverseTransformPoint(closestPoint).x < 0 ? AvatarIKGoal.LeftHand : AvatarIKGoal.RightHand;
                HumanBodyBones transformBone = transform.InverseTransformPoint(closestPoint).x < 0 ? HumanBodyBones.LeftHand : HumanBodyBones.RightHand;

                Transform palmTransform = _animator.GetBoneTransform(transformBone);

                _animator.SetIKPositionWeight(goal, 1);
                _animator.SetIKRotationWeight(goal, 1);

                _animator.SetIKPosition(goal, closestPoint);
                _animator.SetIKRotation(
                    goal,
                    Quaternion.LookRotation(
                        Vector3.ProjectOnPlane(
                            Quaternion.Euler(_palmRotationAdjustment) * transform.up,
                            wallHit.normal
                        ),
                        wallHit.normal
                    ));

            }
        }
    }
	
}