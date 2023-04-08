using UnityEngine;

public class LevelClearRunner : MonoBehaviour {
    
    [SerializeField] Animator[] _animators;


    void OnEnable () {
        foreach (Animator animator in _animators) {
            animator.SetTrigger("win");
        }
    }

}