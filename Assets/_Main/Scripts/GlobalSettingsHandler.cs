using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using DoubleHeat.Utilities;

public class GlobalSettingsHandler : MonoBehaviour {
    
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] Slider _volumeSlider;
    [SerializeField] Slider _lookSensitivitySlider;


    void OnEnable () {
        _volumeSlider.value = PlayerPrefs.GetFloat("MasterVolumeRate", 1f);
        _lookSensitivitySlider.value = PlayerPrefs.GetFloat("LookSensitivity", 1087f);
    }

    public void SetVolume (float volumeRate) {
        PlayerPrefs.SetFloat("MasterVolumeRate", volumeRate);
        _audioMixer.SetFloat("Master", AudioTools.ConvertVolumeRateToDB(volumeRate));
    }

    public void SetLookSensitivity (float LookSensitivity) {
        PlayerPrefs.SetFloat("LookSensitivity", LookSensitivity);
        Global.OnLookSensitivityChanged();
    }

    [ContextMenu("Reset Look Sensitivity")]
    public void ResetLookSensitivity () {
        PlayerPrefs.DeleteKey("LookSensitivity");
        Global.OnLookSensitivityChanged();
    }

}