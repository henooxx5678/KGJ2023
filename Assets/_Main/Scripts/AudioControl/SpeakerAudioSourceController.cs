using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerAudioSourceController : MonoBehaviour {
    
    public static List<SpeakerAudioSourceController> Instances { get; private set; } = new List<SpeakerAudioSourceController>();


    AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource ? _audioSource : _audioSource = GetComponent<AudioSource>();


    public static void PlayAllForward (bool fromStart = false) {
        foreach (var instance in Instances) {
            instance.PlayForward(fromStart);
        }
    }

    public static void PlayAllBackward (bool fromStart = false) {
        foreach (var instance in Instances) {
            instance.PlayBackward(fromStart);
        }
    }

    public static void StopAll () {
        foreach (var instance in Instances) {
            instance.AudioSource.Stop();
        }
    }


    void OnEnable () {
        Instances.Add(this);
    }

    void OnDisable () {
        Instances.Remove(this);
    }

    public void PlayForward (bool fromStart = false) {
        if (fromStart) {
            AudioSource.time = 0f;
        }
        AudioSource.pitch = 1f;
        AudioSource.Play();
    }
    
    public void PlayBackward (bool fromStart = false) {
        if (fromStart) {
            AudioSource.time = 0f;
        }
        AudioSource.pitch = -1f;
        AudioSource.Play();
    }

}