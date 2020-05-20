using UnityEngine.Audio;
using UnityEngine;
// ----------------
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;
    [Range(0f, 1f)]
    public float volume;
    [HideInInspector]
    public AudioSource source;
}
//Vient de https://youtu.be/6OT43pvUyfY
// -----------------