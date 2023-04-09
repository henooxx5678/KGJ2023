using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DoubleHeat.Common {

    public class DelayedEventsEmitter : MonoBehaviour {

        [SerializeField] bool _emitDelayedEventsWhenEnabled = true;
        [SerializeField] DelayedEventInfo[] _delayedEventInfos;


        
        protected virtual void OnEnable () {
            if (_emitDelayedEventsWhenEnabled) {
                EmitDelayedEvents();
            }
        }


        [ContextMenu("Emit Delayed Events")]
        public void EmitDelayedEvents() {
            foreach (DelayedEventInfo info in _delayedEventInfos) {
                StartCoroutine(info.DelayingToEmitEvent());
            }
        }



        [Serializable]
        class DelayedEventInfo {
            [SerializeField] float _delay;
            [SerializeField] UnityEvent _targetEvent;
            
            public float Delay => _delay;


            public IEnumerator DelayingToEmitEvent() {
                yield return new WaitForSeconds(Delay);
                _targetEvent?.Invoke();
            }
        }

    }
}