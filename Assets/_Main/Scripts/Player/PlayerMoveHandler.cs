using UnityEngine;



public class PlayerMoveHandler : MonoBehaviour {

    [SerializeField] float _walkSpeed = 10f;
    [SerializeField] float _sprintSpeed = 16f;


    CharacterController _characterController;
    public CharacterController characterController => _characterController ?? GetComponent<CharacterController>();


    public Vector2 CurrentMoveDirection {get; private set;} = Vector2.zero;
    public bool IsMoving => CurrentMoveDirection != Vector2.zero;
    public bool IsSprintOn {get; private set;} = false;


    void Update () {
        if (characterController.enabled && CurrentMoveDirection != Vector2.zero) {
            characterController.SimpleMove(transform.rotation * new Vector3(CurrentMoveDirection.x, 0, CurrentMoveDirection.y) * (IsSprintOn ? _sprintSpeed : _walkSpeed));
        }
    }


    public void On () {
        characterController.enabled = true;
    }

    public void Off () {
        SetMoveDirection(Vector2.zero);
        SetSprint(false);
        characterController.SimpleMove(Vector3.zero);
        characterController.enabled = false;
    }

    public void SetMoveDirection (Vector2 dir) {
        CurrentMoveDirection = dir;
    }

    public void SetSprint (bool isSprintOn) {
        IsSprintOn = isSprintOn;
    }

    public void ToggleSprint () {
        IsSprintOn = !IsSprintOn;
    }


}

