using UnityEngine;
using DG.Tweening;

public class LevelClearRunner : MonoBehaviour {

    [SerializeField] GameObject[] _levelClearLaunchedGameObjects;
    [SerializeField] Animator[] _animators;


    [Header("Camera Shake")]
    [SerializeField] float _shakeDuration = 2f;
    [SerializeField] float _shakeStrength = 1f;
    [SerializeField] int _shakeVibrato = 10;
    [SerializeField] float _shakeRandomness = 90f;
    [SerializeField] bool _shakeFadeOut = true;


    void OnEnable () {
        foreach (GameObject levelClearLaunchedGameObject in _levelClearLaunchedGameObjects) {
            if (levelClearLaunchedGameObject) {
                levelClearLaunchedGameObject.SetActive(true);
            }
        }

        foreach (Animator animator in _animators) {
            if (animator) {
                animator.SetTrigger("win");
            }
        }

        Camera.main.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness, _shakeFadeOut);
    }

}