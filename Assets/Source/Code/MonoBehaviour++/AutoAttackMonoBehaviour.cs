using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoAttackMonoBehaviour : MonoBehaviour
{
    public float currentLifeTime = 0;
    protected float atkSpeed;
    void Update()
    {
        if (currentLifeTime >= atkSpeed / 60f)
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
    protected void Play(string audioName) =>
        FindObjectOfType<AudioManager>().Play(audioName);
}
