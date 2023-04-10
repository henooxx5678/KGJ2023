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
        PlayWithPitch(1f, fromStart);
    }
    
    public void PlayBackward (bool fromStart = false) {
        PlayWithPitch(-1f, fromStart);
    }

    void PlayWithPitch (float pitch, bool fromStart = false) {

        float currentTime = AudioSource.time;

        if (fromStart) {
            currentTime = 0f;
        }

        AudioSource.Stop();
        AudioSource.pitch = pitch;
        AudioSource.time = currentTime;
        AudioSource.Play();
    }

}