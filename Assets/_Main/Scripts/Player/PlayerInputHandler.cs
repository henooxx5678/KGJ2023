using UnityEngine;



public class PlayerInputHandler : MonoBehaviour {

    // REFs
    [SerializeField] PlayerMoveHandler _moveHandler;
    [SerializeField] PlayerLookHandler _lookHandler;

    // Configs
    [SerializeField] float _defaultLookSensitivity = 1087f;

    float _currentLookSensitive = 1087f;

    public bool IsInputBlocked = false;


    void Awake () {
        OnLookSensitivityChanged();
    }

    void OnEnable () {
        Global.LookSensitivityChanged += OnLookSensitivityChanged;
    }

    void OnDisable () {
        Global.LookSensitivityChanged -= OnLookSensitivityChanged;
    }

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
        _lookHandler.SetLookAngularSpeeds(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * _currentLookSensitive);
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

    void OnLookSensitivityChanged () {
        if (PlayerPrefs.HasKey("LookSensitivity")) {
            _currentLookSensitive = PlayerPrefs.GetFloat("LookSensitivity");
        }
        else {
            _currentLookSensitive = _defaultLookSensitivity;
        }
    }

}