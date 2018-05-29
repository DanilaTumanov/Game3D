using Game3D.SceneObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game3D.Controllers
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioLoopController : BaseController
    {
        public bool randomStart = false;

        [SerializeField]
        private AudioClip[] _audioClips;

        private AudioSource _audioSource;
        private int _currentClipIndex = 0;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _currentClipIndex = randomStart ? (new System.Random()).Next(_audioClips.Length) : _currentClipIndex;
            StartCoroutine(PlayClips());
        }



        private void PlayClip(int clipIndex)
        {
            _audioSource.clip = _audioClips[clipIndex];
            _audioSource.Play();
        }

        private void NextClip()
        {
            _currentClipIndex = ++_currentClipIndex % _audioClips.Length;
            PlayClip(_currentClipIndex);
        }

        private IEnumerator PlayClips()
        {
            PlayClip(_currentClipIndex);
            while (true)
            {
                yield return new WaitForSeconds(_audioClips[_currentClipIndex].length);
                NextClip();
            }
        }

    }

}