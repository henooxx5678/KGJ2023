using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour{

    [SerializeField] Camera _cam;


    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
    public Quaternion CamRotation => _cam.transform.rotation;


    public void SetStatus (Vector3 position, Quaternion rotation, Quaternion camRotation) {
        transform.position = position;
        transform.rotation = rotation;
        _cam.transform.rotation = camRotation;
    }

}
