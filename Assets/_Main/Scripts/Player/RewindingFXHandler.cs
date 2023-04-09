using UnityEngine;

public class RewindingFXHandler : MonoBehaviour {
    
    [SerializeField] AudioSource _audioSource;


    void OnEnable () {
        _audioSource.time = 0f;
        _audioSource.Play();
    }

    void OnDisable () {
        _audioSource.Stop();
    }

}