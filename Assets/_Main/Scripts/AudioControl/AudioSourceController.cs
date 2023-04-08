using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour {
    
    public static List<AudioSourceController> Instances { get; private set; } = new List<AudioSourceController>();


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