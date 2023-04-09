using UnityEngine;

public class LevelClearRunner : MonoBehaviour {

    [SerializeField] GameObject[] _levelClearLaunchedGameObjects;
    [SerializeField] Animator[] _animators;


    void OnEnable () {
        foreach (GameObject levelClearLaunchedGameObject in _levelClearLaunchedGameObjects) {
            levelClearLaunchedGameObject.SetActive(true);
        }

        foreach (Animator animator in _animators) {
            animator.SetTrigger("win");
        }
    }

}