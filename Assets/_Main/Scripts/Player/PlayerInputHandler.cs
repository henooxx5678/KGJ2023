using UnityEngine;



public class PlayerInputHandler : MonoBehaviour {

    // REFs
    [SerializeField] PlayerMoveHandler _moveHandler;
    [SerializeField] PlayerLookHandler _lookHandler;

    // Configs
    [SerializeField] float _lookSensitivity = 100f;


    public bool IsInputBlocked = false;


    void Update () {

        if (IsInputBlocked)  {
            return;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            _moveHandler.SetSprint(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            _moveHandler.SetSprint(false);
        }

        _moveHandler.SetMoveDirection(Input.GetAxis("Horizontal") * Vector2.right + Input.GetAxis("Vertical") * Vector2.up);
        _lookHandler.SetLookAngularSpeeds(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * _lookSensitivity);
    }


    public void BlockInput () {
        IsInputBlocked = true;

        _moveHandler.Off();
        _lookHandler.SetLookAngularSpeeds(Vector2.zero);
    }

    public void UnblockInput () {
        IsInputBlocked = false;
        _moveHandler.On();
    }

}