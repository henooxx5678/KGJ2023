using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public static class Global {

    public static Action LookSensitivityChanged;

    public static void OnPlayerExitLevel (int levelNumber) {
        
        if (levelNumber == 1) {
            SceneManager.LoadScene("Apartment");
        }

    }

    public static void OnLookSensitivityChanged () {
        LookSensitivityChanged?.Invoke();
    }

}