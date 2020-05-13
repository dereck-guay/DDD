using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellMonoBehavior : MonoBehaviour
{
    public float cooldown = 1;
    protected float currentLifeTime;
    protected void Play(string audioName) =>
        FindObjectOfType<AudioManager>().Play(audioName);
}
