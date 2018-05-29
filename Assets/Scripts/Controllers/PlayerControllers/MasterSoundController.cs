using Game3D.SceneObjects;
using Game3D.SceneObjects.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Game3D.Controllers
{
    /// <summary>
    /// TODO: В этом контроллере все неправильно. Надо переделывать. Быстро писал.
    /// </summary>
    public class MasterSoundController : BaseController
    {

        const float AREA_GROUND_CHECK = 20f;

        private AudioMixer _audioMixer;

        private AudioMixerSnapshot _indoorSound;
        private AudioMixerSnapshot _outdoorSound;



        private void Start()
        {
            _audioMixer = Resources.Load<AudioMixer>("Audio/GameSceneAudio");
            _outdoorSound = _audioMixer.FindSnapshot("Outdoor");
            _indoorSound = _audioMixer.FindSnapshot("Indoor");
        }


        private void Update()
        {
            RaycastHit hit;

            if(Physics.Raycast(
                PlayerController.Instance.transform.position,
                Vector3.down,
                out hit,
                AREA_GROUND_CHECK,
                1 << LayerMask.NameToLayer("Ground") |
                1 << LayerMask.NameToLayer("Buildings"))
            )
            {
                switch (LayerMask.LayerToName(hit.transform.gameObject.layer))
                {
                    case "Ground":
                        _outdoorSound.TransitionTo(0.1f);
                        break;
                    case "Buildings":
                        _indoorSound.TransitionTo(0.1f);
                        break;
                }
            }

            
        }

    }

}