﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardCooldownComponent : MonoBehaviour
{
    private float nextFireTime = 0;
    private float nextFrostTime = 0;
    private float nextHealTime = 0;
    private float nextSlowTime = 0;
    public Image fireballMask;
    public Image frostMask;
    public Image healMask;
    public Image slowMask;
    private WizardComponent wizard;

    private void Start()
    {
        wizard = GetComponentInParent<WizardComponent>();
    }

    void Update()
    {
        SetFillToZero();
        if (wizard.GetComponent<FireballSpell>() != null)
            FireballCooldown(wizard.fireball.cooldowns[wizard.entityStats.XP.Level - 1]);
        if (wizard.GetComponent<RayOfFrostSpell>() != null)
            FrostCooldown(wizard.rayOfFrost.cooldowns[wizard.entityStats.XP.Level - 1]);
        if (wizard.GetComponent<HealSpell>() != null)
            HealCooldown(wizard.heal.cooldowns[wizard.entityStats.XP.Level - 1]);
        if (wizard.GetComponent<SlowSpell>() != null)
            SlowCooldown(wizard.slow.cooldowns[wizard.entityStats.XP.Level - 1]);
    }

    public void FireballCooldown(float cooldownTime)
    {
        fireballMask.fillAmount = (nextFireTime / cooldownTime);

        if (nextFireTime == 0)
            nextFireTime = cooldownTime;
        if (nextFireTime <= 0)
            nextFireTime = 0;
        else if (nextFireTime > 0)
            nextFireTime -= Time.deltaTime;
    }

    public void FrostCooldown(float cooldownTime)
    {
        frostMask.fillAmount = (nextFrostTime / cooldownTime);

        if (nextFrostTime == 0)
            nextFrostTime = cooldownTime;
        if (nextFrostTime <= 0)
            nextFrostTime = 0;
        else if (nextFrostTime > 0)
            nextFrostTime -= Time.deltaTime;
    }

    public void HealCooldown(float cooldownTime)
    {
        healMask.fillAmount = (nextHealTime / cooldownTime);

        if (nextHealTime == 0)
            nextHealTime = cooldownTime;
        if (nextHealTime <= 0)
            nextHealTime = 0;
        else if (nextHealTime > 0)
            nextHealTime -= Time.deltaTime;
    }

    public void SlowCooldown(float cooldownTime)
    {
        slowMask.fillAmount = (nextSlowTime / cooldownTime);

        if (nextSlowTime == 0)
            nextSlowTime = cooldownTime;
        if (nextSlowTime <= 0)
            nextSlowTime = 0;
        else if (nextSlowTime > 0)
            nextSlowTime -= Time.deltaTime;
    }

    public void SetFillToZero()
    {
        fireballMask.fillAmount = 0;
        frostMask.fillAmount = 0;
        healMask.fillAmount = 0;
        slowMask.fillAmount = 0;
    }
}
