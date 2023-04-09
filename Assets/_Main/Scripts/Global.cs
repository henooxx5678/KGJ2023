using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public static class Global {

    public static Action LookSensitivityChanged;

    public static void OnPlayerExitLevel (int levelNumber) {
        
    }

    public static void OnLookSensitivityChanged () {
        LookSensitivityChanged?.Invoke();
    }

}