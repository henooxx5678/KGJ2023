using UnityEngine;
using UnityEngine.Audio;

namespace DoubleHeat.Utilities {

    public static class AudioTools {
        
        public static float ConvertVolumeRateToDB (float volumeRate) {
            // Debug.Log("volumeRate: " + volumeRate + " | " + Mathf.Log(Mathf.Clamp(volumeRate, 0.001f, 1f)) * 20f);
            return Mathf.Log(Mathf.Clamp(volumeRate, 0.001f, 1f)) * 20f;
        }

        public static float ConvertDBToVolumeRate (float dB) {
            // Debug.Log("dB: " + dB + " | " + Mathf.Clamp(Mathf.Pow(10f, dB / 20f), 0f, 1f));
            return Mathf.Clamp(Mathf.Pow(10f, dB / 20f), 0f, 1f);
        }

    }
}