using UnityEngine;

namespace DoubleHeat.Common.GlobalSettings {
    
    public class TimeScaleSetter : MonoBehaviour {
        
        public void SetTimeScale (float value) {
            Time.timeScale = value;
        }

    }

}