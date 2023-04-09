using UnityEngine;



public class PlayerLookHandler : MonoBehaviour {

    const float MIN_VERTICAL_ANGLE = -90f;
    const float MAX_VERTICAL_ANGLE = 90f;

    public Vector2 CurrentLookAngularSpeeds {get; private set;} = Vector2.zero;



    [Header("REFS")]
    [SerializeField] Camera _cam;



    public float CurrentVerticalAngle {
        get {
            float eulerX = _cam.transform.localRotation.eulerAngles.x;
            return -(eulerX < 180f ? eulerX : eulerX - 360);
        }
        set => _cam.transform.localRotation = Quaternion.AngleAxis(value, Vector3.left);
    }


    void Update () {
        SetLookDirection(CurrentLookAngularSpeeds.x * Time.deltaTime, CurrentLookAngularSpeeds.y * Time.deltaTime);
    }


    public void SetLookAngularSpeeds (Vector2 deltaAngles) {
        CurrentLookAngularSpeeds = deltaAngles;
    }


    protected void SetLookDirection (float horizontalDeltaAngle, float verticalDeltaAngle) {

        if (horizontalDeltaAngle != 0) {
            transform.rotation = Quaternion.AngleAxis(horizontalDeltaAngle, Vector3.up) * transform.rotation;
        }

        if (verticalDeltaAngle != 0) {
            CurrentVerticalAngle = Mathf.Clamp(CurrentVerticalAngle + verticalDeltaAngle, MIN_VERTICAL_ANGLE, MAX_VERTICAL_ANGLE);
        }
    }



}
