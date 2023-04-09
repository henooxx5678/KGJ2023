using UnityEngine;
using LoopEscapeGame.Common.Audio;

public class PlayerFootstepsHandler : MonoBehaviour {
    
    [SerializeField] PlayerMoveHandler _playerMoveHandler;
    [SerializeField] MultiAudioClipRandomPlayer _footstepPlayer;
    [SerializeField] float _walkingFootstepInterval = 1f;
    [SerializeField] float _runningFootstepInterval = 0.5f;


    public enum State {
        Idle,
        Walking,
        Running
    }

    public State CurrentState { get; private set; } = State.Idle;

    float _lastFootstepTime = Mathf.NegativeInfinity;

    void Update () {
        if (_playerMoveHandler.IsMoving) {
            if (_playerMoveHandler.IsSprintOn) {
                CurrentState = State.Running;
            }
            else {
                CurrentState = State.Walking;
            }
        }
        else {
            CurrentState = State.Idle;
        }

        if (CurrentState == State.Walking) {
            if (Time.time - _lastFootstepTime >= _walkingFootstepInterval) {
                _lastFootstepTime = Time.time;
                _footstepPlayer.PlayOneShot();
            }
        }
        else if (CurrentState == State.Running) {
            if (Time.time - _lastFootstepTime >= _runningFootstepInterval) {
                _lastFootstepTime = Time.time;
                _footstepPlayer.PlayOneShot();
            }
        }
    }

}