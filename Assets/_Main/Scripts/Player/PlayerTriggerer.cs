using System;
using UnityEngine;

public class PlayerTriggerer : MonoBehaviour {

    [SerializeField] float _targetValidDistance = 2.6f;

    public Action<int> EnteredTarget;
    public Action<int> ExitedTarget;
    public Action ExitedLevel;


    int _currentInTargetIndex = -1;
    public int CurrentInTargetIndex {
        get => _currentInTargetIndex;
        protected set {
            if (_currentInTargetIndex != value) {
                int oldIndex = _currentInTargetIndex;
                _currentInTargetIndex = value;

                if (_currentInTargetIndex != -1) {
                    EnteredTarget?.Invoke(_currentInTargetIndex);
                } else {
                    ExitedTarget?.Invoke(oldIndex);
                }
            }
        }
    }

    void Update () {
        bool isAnyTargetInRange = false;
        foreach (PlayerTarget target in PlayerTarget.Instances) {
            if (Vector3.Distance(transform.position, target.transform.position) <= _targetValidDistance) {
                CurrentInTargetIndex = target.TargetIndex;
                isAnyTargetInRange = true;
                break;
            }
        }
        if (!isAnyTargetInRange) {
            CurrentInTargetIndex = -1;
        }
    }


    void OnTriggerEnter (Collider other) {
        if (other.tag == "Exit") {
            ExitedLevel?.Invoke();
        }
    }

}