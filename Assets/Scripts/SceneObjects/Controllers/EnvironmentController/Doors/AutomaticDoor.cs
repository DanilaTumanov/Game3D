using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D {

	public class AutomaticDoor : BaseObjectScene {


        private Animator _animator;


        private void Start()
        {
            _animator = GetComponent<Animator>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                _animator.SetBool("character_nearby", true);
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                _animator.SetBool("character_nearby", false);
        }

    }
	
}