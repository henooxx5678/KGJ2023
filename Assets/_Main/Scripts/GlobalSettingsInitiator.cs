using UnityEngine;
using UnityEngine.Audio;
using DoubleHeat.Utilities;

public class GlobalSettingsInitiator : MonoBehaviour {
    
    [SerializeField] AudioMixer _masterMixer;


    void OnEnable () {
        _masterMixer.SetFloat("Master", AudioTools.ConvertVolumeRateToDB(PlayerPrefs.GetFloat("MasterVolumeRate", 1f)));
    }

}