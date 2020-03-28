using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
<<<<<<< HEAD
using UnityEngine.UI;
=======
>>>>>>> ca977158dae0d27f8cbb56efe67450a78aebb4fd

public class AudioMenuComponent : MonoBehaviour
{
    public AudioMixer audioMixer;
<<<<<<< HEAD
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
=======

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVol", volume);
>>>>>>> ca977158dae0d27f8cbb56efe67450a78aebb4fd
    }
}
