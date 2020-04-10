using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenuComponent : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterVolumeControl;
    public Slider effectsVolumeControl;
    public Slider musicVolumeControl;

    private void Start()
    {
        masterVolumeControl.value = PlayerPrefs.GetFloat("MasterVolume");
        effectsVolumeControl.value = PlayerPrefs.GetFloat("EffectsVolume");
        musicVolumeControl.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeControl.value);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeControl.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeControl.value);
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVol", Mathf.Log10(volume) * 20);
        masterVolumeControl.GetComponentInChildren<Text>().text = (Mathf.FloorToInt(volume * 100) + "%").ToString();
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVol", Mathf.Log10(volume) * 20);
        effectsVolumeControl.GetComponentInChildren<Text>().text = (Mathf.FloorToInt(volume * 100) + "%").ToString();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVol", Mathf.Log10(volume) * 20);
        musicVolumeControl.GetComponentInChildren<Text>().text = (Mathf.FloorToInt(volume * 100) + "%").ToString();
    }
}
