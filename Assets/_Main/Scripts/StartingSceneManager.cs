using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSceneManager : MonoBehaviour {
    
    public string NextSceneName = "MainScene";
    public float LifeTimeRemaining = 4f;

    void Update () {
        if (Input.anyKeyDown) {
            // Load the next scene
            LoadNextScene();
        }
        
        LifeTimeRemaining -= Time.deltaTime;

        if (LifeTimeRemaining <= 0) {
            // Load the next scene
            LoadNextScene();
        }
    }

    void LoadNextScene() {
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName);
    }

}
