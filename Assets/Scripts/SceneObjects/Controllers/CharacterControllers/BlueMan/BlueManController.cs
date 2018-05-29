using Game3D.Interfaces;
using Game3D.SceneObjects.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.SceneObjects.Controllers
{

    [RequireComponent(typeof(AIMoveToController))]
    [RequireComponent(typeof(AIPatrolController))]
    public class BlueManController : BaseCharacterController, ITrapable
    {

        [Tooltip("Относительная скорость патрулирования")]
        [SerializeField]
        private float _patrolSpeedMultiplier = 1;

        [Tooltip("Относительная скорость преследования")]
        [SerializeField]
        private float _pursuitSpeedMultiplier = 2;

        [SerializeField]
        private AIVisionController _vision;

        [Saveable]
        public float maxTargetDistance = 1000f;
        

        private AIPatrolController _patrol;
        private AIMoveToController _move;
        private AIStateMachine _stateMachine;
        private Animator _animator;

        private int _visionLayerMask;
        private Trap _trap;
        private float _speedMultiplier = 1;

        private void Start()
        {
            _patrol = GetComponent<AIPatrolController>();
            _move = GetComponent<AIMoveToController>();
            //_vision = new AIVisionController(transform);
            _stateMachine = new AIStateMachine(this);
            _animator = GetComponent<Animator>();

            SetVisionLayerMask();
            InitSMStates();
        }



        private void InitSMStates()
        {
            _stateMachine.AddState("patrol", ProcessPatrol);
            _stateMachine.AddState("pursuit", ProcessPursuit);

            _stateMachine.AddState("trappedGravitationMine", ProcessGravitationTrap);


            _stateMachine.Start("patrol");
        }


        private void Update()
        {
            ProcessAnimation();
        }


        private IEnumerator ProcessPatrol()
        {
            WaitForSeconds patrolCheckPeriod = new WaitForSeconds(0.5f);

            string nextState = String.Empty;

            _speedMultiplier = _patrolSpeedMultiplier;
            _move.StartMovement();

            while (true)
            {
                if (_trap != null)
                {
                    nextState = GetTrappedState();
                    break;
                }

                if (SearchEnemies())
                {
                    nextState = "pursuit";

                    // TODO: тут разумеется нужно убрать жесткую привязку к игроку и определять конкретного врага через SearchEnemies
                    _move.SetTarget(Main.Instance.Player.transform.position);

                    break;
                }
                else if (_move.TargetReached())
                {
                    _move.SetTarget(_patrol.NextPoint());
                    _move.Agent.stoppingDistance = 0;
                }

                yield return patrolCheckPeriod;
            }

            yield return nextState;
        }


        private IEnumerator ProcessPursuit()
        {
            WaitForSeconds pursuitCheckPeriod = new WaitForSeconds(0.2f);

            string nextState = String.Empty;

            _speedMultiplier = _pursuitSpeedMultiplier;
            _move.StartMovement();

            while (true)
            {
                if (SearchEnemies())
                {
                    // TODO: И тут тоже надо убрать жесткую привязку
                    _move.SetTarget(Main.Instance.Player.transform.position);
                }
                else if (_move.TargetReached())
                {
                    nextState = "patrol";
                    break;
                }

                yield return pursuitCheckPeriod;
            }

            yield return nextState;
        }


        private IEnumerator ProcessGravitationTrap()
        {
            yield return (_trap as GravitationMine).ActionTimer;

            _move.NavEnable();

            yield return _stateMachine.PrevState;
        }


        private void SetVisionLayerMask()
        {

            // TODO: Нужно отдельно задавать слои. Тут это делать плохо.
            int includeLayers = int.MaxValue;

            int excludeLayers = 1 << LayerMask.NameToLayer("Player") |
                                1 << LayerMask.NameToLayer("Enemies");

            _visionLayerMask = includeLayers ^ excludeLayers;
        }


        private bool SearchEnemies()
        {
            return _vision.HasInVisionRange(Main.Instance.Player.transform, _visionLayerMask);
        }


        private string GetTrappedState()
        {
            return "trapped" + _trap.GetType().Name;
        }


        private void ProcessAnimation()
        {
            float speed = _move.Agent.velocity.magnitude * _speedMultiplier;
            _animator.SetFloat("Speed", speed);
        }




        public void SetTrapped(Trap trap)
        {
            _move.NavDisable();
            _trap = trap;
        }

    }

}