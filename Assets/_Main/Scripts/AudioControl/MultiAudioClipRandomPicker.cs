using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LoopEscapeGame.Common.Audio {

    public class MultiAudioClipRandomPicker : MonoBehaviour {
        
        public PickingMethod CurrentPickingMethod => _pickingMethod;

        [SerializeField] PickingMethod _pickingMethod;
        [SerializeField] AudioClip[] _audioClips;
        

        int _prevPickedIndex = -1;
        List<int> _remainedIndexInARound;


        public AudioClip PickNext () {
            int pickedIndex = PickNextIndex();
            if (pickedIndex >= 0 && pickedIndex < _audioClips.Length) {
                return _audioClips[pickedIndex];
            }
            return null;
        }

        public int PickNextIndex () {

            int pickedIndex = -1;

            if (_audioClips.Length > 0) {
                switch (CurrentPickingMethod) {
                    case PickingMethod.RoundRobin: {
                        pickedIndex = (_prevPickedIndex + 1) % _audioClips.Length;
                        break;
                    }
                    case PickingMethod.Random: {
                        pickedIndex = Random.Range(0, _audioClips.Length);
                        break;
                    }
                    case PickingMethod.NonRepeatRandom: {
                        if (_audioClips.Length > 1) {
                            pickedIndex = Random.Range(0, _audioClips.Length - 1);
                            if (pickedIndex >= _prevPickedIndex) {
                                pickedIndex += 1;
                            }
                        }
                        else {
                            pickedIndex = 0;
                        }
                        break;
                    }
                    case PickingMethod.RandomInARound: {
                        if (_remainedIndexInARound == null || _remainedIndexInARound.Count == 0) {
                            _remainedIndexInARound = new List<int>();
                            for (int i = 0 ; i < _audioClips.Length ; i++) {
                                _remainedIndexInARound.Add(i);
                            }
                        }

                        if (_remainedIndexInARound.Count > 0) {
                            int randomIndexOfList = Random.Range(0, _remainedIndexInARound.Count);

                            pickedIndex = _remainedIndexInARound[randomIndexOfList];
                            _remainedIndexInARound.RemoveAt(randomIndexOfList);
                        }
                        break;
                    }
                }
            }

            _prevPickedIndex = pickedIndex;
            return pickedIndex;
        }

        
        public enum PickingMethod {
            RoundRobin,
            Random,
            NonRepeatRandom,
            RandomInARound
        }

    }
}