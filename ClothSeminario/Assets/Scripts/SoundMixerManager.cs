using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour{
    [SerializeField] private AudioMixer _audioMixer;

    public void SetSFXVolume(float volume) {
        _audioMixer.SetFloat("SFXVolume", volume);
    }
}
