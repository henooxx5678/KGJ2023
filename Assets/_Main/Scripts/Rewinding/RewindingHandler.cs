using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public class RewindingHandler : MonoBehaviour {
    
    [SerializeField] KeyCode _rewindKey = KeyCode.Q;

    // Configs
    [SerializeField] float _timelineLogGap = 0.1f;
    [SerializeField] int _maxTimelineFrameCount = 100;  // 10 seconds

    // REFs
    [SerializeField] PlayerStatusManager _playerStatusManager;
    [SerializeField] PlayerInputHandler _playerInputHandler;


    public Action RewindStarted;
    public Action RewindStopped;
    public UnityEvent RewindStartedEvent;
    public UnityEvent RewindStoppedEvent;
    

    float _roundTime = 0f;
    List<PlayerTimelineFrame> _timelineFrames = new List<PlayerTimelineFrame>();
    float _lastLoggedRoundTime = Mathf.NegativeInfinity;


    bool _isRewinding = false;
    public bool IsRewinding => _isRewinding;

    bool _isLerpingBetweenFrames = false;
    Tween _currentLerpTween = null;



    public void OnRoundStart () {
        if (_currentLerpTween != null && _currentLerpTween.IsActive()) {
            _currentLerpTween.Kill(false);
        }

        _roundTime = 0f;
        _timelineFrames.Clear();
        _lastLoggedRoundTime = Mathf.NegativeInfinity;
        _isRewinding = false;
        _isLerpingBetweenFrames = false;
    }

    void Update () {
        if (Input.GetKeyDown(_rewindKey)) {
            TryStartRewinding();
        }
        if (Input.GetKeyUp(_rewindKey)) {
            TryStopRewinding();
        }


        // Rewind.
        if (!_isRewinding) {
            // Log timeline frame.
            _roundTime += Time.deltaTime;
            if (_roundTime - _lastLoggedRoundTime >= _timelineLogGap) {
                Log(_playerStatusManager.Position, _playerStatusManager.Rotation, _playerStatusManager.CamRotation);
                _lastLoggedRoundTime = _roundTime;
            }
        }
        else {
            if (!_isLerpingBetweenFrames) {
                if (_timelineFrames.Any()) {

                    var thisFrame = _timelineFrames.Last();
                    _timelineFrames.RemoveAt(_timelineFrames.Count - 1);

                    LerpToFrame(thisFrame, _roundTime - _lastLoggedRoundTime);
                }
                else {
                    TryStopRewinding();
                }
            }
        }

    }

    void Log (Vector3 position, Quaternion rotation, Quaternion camRotation) {
        if (_timelineFrames.Count >= _maxTimelineFrameCount) {
            _timelineFrames.RemoveAt(0);
        }
        _timelineFrames.Add(new PlayerTimelineFrame(position, rotation, camRotation));
        // print("logged");
    }

    void TryStartRewinding () {
        
        if (_isRewinding) {
            return;
        }

        _isRewinding = true;
        _playerInputHandler.BlockInput();
        SpeakerAudioSourceController.PlayAllForward();
        RewindStarted?.Invoke();
        RewindStartedEvent.Invoke();
    }

    void TryStopRewinding () {
        if (!_isRewinding) {
            return;
        }

        _isRewinding = false;
        _playerInputHandler.UnblockInput();
        SpeakerAudioSourceController.PlayAllBackward();
        RewindStopped?.Invoke();
        RewindStoppedEvent.Invoke();
    }


    void LerpToFrame (PlayerTimelineFrame frame, float duration) {
        float progress = 0f;

        // print("start lerping");

        Vector3 startPos = _playerStatusManager.Position;
        Quaternion startRot = _playerStatusManager.Rotation;
        Quaternion startCamRot = _playerStatusManager.CamRotation;


        _currentLerpTween = DOTween.To(
            () => progress,
            x => {
                progress = x;
                _playerStatusManager.SetStatus(
                    Vector3.Lerp(startPos, frame.position, progress),
                    Quaternion.Lerp(startRot, frame.rotation, progress),
                    Quaternion.Lerp(startCamRot, frame.camRotation, progress)
                );
            },
            1f,
            duration
        )
        .SetEase(Ease.Linear)
        .OnStart(() => {
            _isLerpingBetweenFrames = true;
        })
        .OnComplete(() => {
            _roundTime -= duration;
            if (_timelineFrames.Any()) {
                _lastLoggedRoundTime = _roundTime - _timelineLogGap;
            }
            else {
                _lastLoggedRoundTime = Mathf.NegativeInfinity;
            }
            _isLerpingBetweenFrames = false;
        });
    }

}