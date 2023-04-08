using UnityEngine;
using UnityEngine.InputSystem;

namespace FirstPerson {

    public class PlayerInputHandler : MonoBehaviour {

        // REFs
        [SerializeField] PlayerMoveHandler _moveHandler;
        [SerializeField] PlayerLookHandler _lookHandler;

        // Configs
        [SerializeField] float _lookSensitivity = 100f;


        void Update () {

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                _moveHandler.SetSprint(true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift)) {
                _moveHandler.SetSprint(false);
            }

            _moveHandler.SetMoveDirection(Input.GetAxis("Horizontal") * Vector2.right + Input.GetAxis("Vertical") * Vector2.up);
            _lookHandler.SetLookAngularSpeeds(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * _lookSensitivity);
        }

    }
}