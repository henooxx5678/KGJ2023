using System.Collections.Generic;
using UnityEngine;

namespace LoopEscapeGame.Common.Audio {
    
    public class MultiAudioClipRandomPlayer : MonoBehaviour {
        
        [SerializeField] float _pitchRandomRangeMin = 1f;
        [SerializeField] float _pitchRandomRangeMax = 1f;

        [Header("REFS")]
        [SerializeField] MultiAudioClipRandomPicker _audioClipPicker;
        [SerializeField] AudioSource _targetAudioSource;


        public void Play () {
            if (_targetAudioSource) {
                var clip = GetNextAudioClip();
                if (clip != null) {
                    _targetAudioSource.clip = clip;
                    SetRandomPitch();
                    _targetAudioSource.Play();
                }
            }
        }

        public void PlayDelayed (float delay) {
            if (_targetAudioSource) {
                var clip = GetNextAudioClip();
                if (clip != null) {
                    _targetAudioSource.clip = clip;
                    SetRandomPitch();
                    _targetAudioSource.PlayDelayed(delay);
                }
            }
        }

        public void PlayOneShot () {
            if (_targetAudioSource) {
                var clip = GetNextAudioClip();
                if (clip != null) {
                    SetRandomPitch();
                    _targetAudioSource.PlayOneShot(clip);
                }
            }
        }


        protected void SetRandomPitch () {
            if (_targetAudioSource) {
                _targetAudioSource.pitch = Random.Range(_pitchRandomRangeMin, _pitchRandomRangeMax);
            }
        }

        protected void ResetPitch () {
            if (_targetAudioSource) {
                _targetAudioSource.pitch = 1f;
            }
        }

        protected AudioClip GetNextAudioClip () {
            if (_audioClipPicker) {
                return _audioClipPicker.PickNext();
            }
            else {
                Debug.LogWarning("_audioClipPicker not assigned!");
            }
            return null;
        }

    }

}