﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunningBladeSpell : SpellMonoBehavior
{
    public Vector3 direction;
    public GameObject stunningBladePrefab;
    public float effectDuration = 1;
    public bool hasContacted = false;
    public EntityMonoBehaviour target;
    private float stunDuration;
    private GameObject stunningBlade;
    private bool stunHasBegun;
    readonly string audioName = "Rogue Stunning Blade";
    public void Cast()
    {
        Play(audioName);
        var spawnPosition = transform.position + 1.5f * direction;
        stunningBlade = Instantiate(stunningBladePrefab, spawnPosition, transform.rotation);
        stunningBlade.GetComponent<StunningBladeCollision>().stunningBladeSpell = this;
    }

    void Update()
    {
        if (stunHasBegun)
            stunDuration += Time.deltaTime;
        if (hasContacted)
        {
            if (target)
                target.IsStunned = true;
            stunHasBegun = true;
            hasContacted = false;
        }
        if (currentLifeTime >= cooldown)
        {
            if (target && target.IsStunned)
                target.IsStunned = false;
            Destroy(stunningBlade);
            Destroy(this);
        }
        if (stunDuration > effectDuration)
        {
            if(target)
                target.IsStunned = false;
        }
        currentLifeTime += Time.deltaTime;
    }
}
