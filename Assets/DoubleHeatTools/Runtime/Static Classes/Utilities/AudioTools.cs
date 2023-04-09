using UnityEngine;
using UnityEngine.Audio;

namespace DoubleHeat.Utilities {

    public static class AudioTools {
        
        public static float ConvertVolumeRateToDB (float volumeRate) {
            return Mathf.Log(Mathf.Clamp(volumeRate, 0.001f, 1f)) * 20f;
        }


    }
}