using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenuComponent : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterVolumeControl;
    //private float currentVol;

    //private void Start()
    //{
    //    SetVolume(.75f);
    //}
    //private void Update()
    //{
    //}

    public void SetVolume(float volume)
    {
        //float volume = masterVolumeControl.value;
        audioMixer.SetFloat("masterVol", Mathf.Log10(volume) * 20);
        //masterVolumeControl.GetComponentInChildren<Text>().text = (Mathf.Log10(volume) * 20).ToString();
        //PlayerPrefs.SetFloat("masterVol", volume);
    }
}
