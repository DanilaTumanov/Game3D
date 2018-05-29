using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game3D {

	public class PendulumController : BaseObjectScene {


        [SerializeField]
        private float _changeLengthStep = 0.3f;

        private GameObject _joint;
        private GameObject _weight;
        private GameObject _root;
        private Vector3 _changeScaleVector = Vector3.zero;

        private void Start()
        {
            _joint = transform.Find("Stick/Joint").gameObject;
            _weight = transform.Find("Stick/Weight").gameObject;
            _root = transform.Find("Root").gameObject;
        }


        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                IncreaseLength(_changeLengthStep);
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                DecreaseLength(_changeLengthStep);
            }


            _weight.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(_root.transform.position - _weight.transform.position, _root.transform.up));

        }


        private void IncreaseLength(float num)
        {
            ChangeLength(num);
        }


        private void DecreaseLength(float num)
        {
            ChangeLength(-num);
        }


        private void ChangeLength(float num)
        {
            _changeScaleVector.y = num;
            _joint.transform.localScale += _changeScaleVector;
        }
    }
	
}