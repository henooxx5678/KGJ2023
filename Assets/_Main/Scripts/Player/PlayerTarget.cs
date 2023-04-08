using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour {
    
    public static List<PlayerTarget> Instances { get; private set; } = new List<PlayerTarget>();

    public int TargetIndex = -1;

    void OnEnable () {
        Instances.Add(this);
    }

    void OnDisable () {
        Instances.Remove(this);
    }

}