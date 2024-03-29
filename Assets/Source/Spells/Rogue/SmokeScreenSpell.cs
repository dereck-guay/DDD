﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class SmokeScreenSpell : SpellMonoBehavior
{
    public ParticleSystem smoke;
    public GameObject player;
    public float smokeDuration = 5;
    private bool smokeIsOn;
    readonly string audioName = "Rogue Smoke Screen";
    public void Cast()
    {
        Play(audioName);
        Instantiate(smoke as ParticleSystem, player.transform);
        player.GetComponent<Stats>().HP.IsInvulnerable = true;
        smokeIsOn = true;
    }

    private void Update()
    {
        if (currentLifeTime >= smokeDuration && smokeIsOn)
        {
            player.GetComponent<Stats>().HP.IsInvulnerable = false;
            smokeIsOn = false;
        }
        if (currentLifeTime >= cooldown)
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
