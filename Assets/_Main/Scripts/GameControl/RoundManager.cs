using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour {
    
    [Header("Round Settings")]
    public int LevelNumber = -1;
    public int TargetCount = 4;

    [Header("REFs")]
    [SerializeField] GameObject _levelClearLaunched;
    [SerializeField] RewindingHandler _rewindingHandler;

    [Header("REFs - Player")]
    [SerializeField] PlayerStatusManager _playerStatusManager;
    [SerializeField] PlayerInputHandler _playerInputHandler;
    [SerializeField] PlayerTriggerer _playerTriggerer;

    [Header("Configs")]
    [SerializeField] float _validInTargetDuration = 1.5f;

    [Header("Restart Key")]
    [SerializeField] KeyCode _restartKey = KeyCode.R;
    [SerializeField] float _restartKeyHoldingDuration = 1f;


    public float RestartKeyHoldedProgress => Mathf.Clamp((Time.realtimeSinceStartup - _restartKeyStartHoldTime) / _restartKeyHoldingDuration, 0f, 1f);

    float _restartKeyStartHoldTime = Mathf.Infinity;


    Vector3 _playerInitPosition;
    Quaternion _playerInitRotation;
    Quaternion _playerInitCamRotation;


    Coroutine _currentInTargetEventCoroutine = null;
    protected Coroutine currentInTargetEventCoroutine {
        get => _currentInTargetEventCoroutine;
        set {
            if (_currentInTargetEventCoroutine != null) {
                StopCoroutine(_currentInTargetEventCoroutine);
            }
            _currentInTargetEventCoroutine = value;
        }
    }

    List<int> _validTargets = new List<int>();


    void Awake() {
        _playerInitPosition = _playerStatusManager.Position;
        _playerInitRotation = _playerStatusManager.Rotation;
        _playerInitCamRotation = _playerStatusManager.CamRotation;
    }

    void OnEnable () {
        if (_playerTriggerer != null) {
            _playerTriggerer.EnteredTarget += OnPlayerEnteredTarget;
            _playerTriggerer.ExitedTarget += OnPlayerExitedTarget;
            _playerTriggerer.ExitedLevel += OnPlayerExitLevel;
        }
        if (_rewindingHandler != null) {
            _rewindingHandler.RewindStarted += OnRewindStarted;
            _rewindingHandler.RewindStopped += OnRewindStopped;
        }
    }

    void OnDisable () {
        if (_playerTriggerer != null) {
            _playerTriggerer.EnteredTarget -= OnPlayerEnteredTarget;
            _playerTriggerer.ExitedTarget -= OnPlayerExitedTarget;
            _playerTriggerer.ExitedLevel -= OnPlayerExitLevel;
        }
        if (_rewindingHandler != null) {
            _rewindingHandler.RewindStarted -= OnRewindStarted;
            _rewindingHandler.RewindStopped -= OnRewindStopped;
        }
    }

    void Start () {
        NewRound(); //temp
    }

    void Update () {
        if (Input.GetKeyDown(_restartKey)) {
            _restartKeyStartHoldTime = Time.realtimeSinceStartup;
        }
        if (Input.GetKeyUp(_restartKey)) {
            _restartKeyStartHoldTime = Mathf.Infinity;
        }

        if (RestartKeyHoldedProgress >= 1f) {
            _restartKeyStartHoldTime = Mathf.Infinity;
            NewRound();
        }
    }

    public void NewRound () {
        _playerInputHandler.BlockInput();

        _rewindingHandler.OnRoundStart();
        AudioSourceController.PlayAllBackward(true);

        // Reset player status.
        _playerStatusManager.SetStatus(_playerInitPosition, _playerInitRotation, _playerInitCamRotation);

        _playerInputHandler.UnblockInput();

        print("New Round");
    }


    IEnumerator InTargetEvent (int targetIndex) {
        if (!_rewindingHandler.IsRewinding) {
            yield break;
        }

        yield return new WaitForSeconds(_validInTargetDuration);
        RecordValidTarget(targetIndex);
    }

    void RecordValidTarget (int targetIndex) {
        print("Valid Target: " + targetIndex);

        _validTargets.Add(targetIndex);
        if (_validTargets.Count > TargetCount) {
            _validTargets.RemoveAt(0);
        }

        if (_validTargets.SequenceEqual(Enumerable.Range(0, TargetCount))) {
            OnLevelClear();
        }
    }

    void OnPlayerEnteredTarget (int targetIndex) {
        print("Player Entered Target " + targetIndex);
        currentInTargetEventCoroutine = StartCoroutine(InTargetEvent(targetIndex));
    }

    void OnPlayerExitedTarget (int targetIndex) {
        print("Player Exited Target " + targetIndex);
        currentInTargetEventCoroutine = null;
    }

    void OnRewindStarted () {
        if (_playerTriggerer.CurrentInTargetIndex != -1) {
            currentInTargetEventCoroutine = StartCoroutine(InTargetEvent(_playerTriggerer.CurrentInTargetIndex));
        }
    }

    void OnRewindStopped () {
        currentInTargetEventCoroutine = null;
    }

    void OnLevelClear () {
        print("== Level Clear! ==");
        _levelClearLaunched.SetActive(true);
    }

    void OnPlayerExitLevel () {
        print("== Player Exit Level! ==");
        Global.OnPlayerExitLevel(LevelNumber);
    }

}