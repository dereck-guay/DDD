using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterCooldownComponent : MonoBehaviour
{
    private float nextSlamTime = 0;
    private float nextRageTime = 0;
    private float nextBreatherCooldown = 0;
    private float nextShieldTime = 0;
    public Image slamMask;
    public Image rageMask;
    public Image breatherMask;
    public Image shieldMask;
    private FighterComponent fighter;

    private void Start()
    {
        fighter = GetComponentInParent<FighterComponent>();
    }

    void Update()
    {
        SetFillToZero();
        if (fighter.GetComponent<SlamSpell>() != null)
            SlamCooldown(fighter.slam.cooldowns[fighter.entityStats.XP.Level - 1]);
        if (fighter.GetComponent<RageSpell>() != null)
            RageCooldown(fighter.rage.cooldowns[fighter.entityStats.XP.Level - 1]);
        if (fighter.GetComponent<TakeABreatherSpell>() != null)
            BreatherCooldown(fighter.takeABreather.cooldowns[fighter.entityStats.XP.Level - 1]);
        if (fighter.GetComponent<ShieldSpell>() != null)
            ShieldCooldown(fighter.shield.cooldowns[fighter.entityStats.XP.Level - 1]);
    }

    public void SlamCooldown(float cooldownTime)
    {
        slamMask.fillAmount = (nextSlamTime / cooldownTime);

        if (nextSlamTime == 0)
            nextSlamTime = cooldownTime;
        if (nextSlamTime <= 0)
            nextShieldTime = 0;
        else if (nextSlamTime > 0)
            nextSlamTime -= Time.deltaTime;
    }

    public void RageCooldown(float cooldownTime)
    {
        rageMask.fillAmount = (nextRageTime / cooldownTime);

        if (nextRageTime == 0)
            nextRageTime = cooldownTime;
        if (nextRageTime <= 0)
            nextRageTime = 0;
        else if (nextRageTime > 0)
            nextRageTime -= Time.deltaTime;
    }

    public void BreatherCooldown(float cooldownTime)
    {
        breatherMask.fillAmount = (nextBreatherCooldown / cooldownTime);

        if (nextBreatherCooldown == 0)
            nextBreatherCooldown = cooldownTime;
        if (nextBreatherCooldown <= 0)
            nextBreatherCooldown = 0;
        else if (nextBreatherCooldown > 0)
            nextBreatherCooldown -= Time.deltaTime;
    }

    public void ShieldCooldown(float cooldownTime)
    {
        shieldMask.fillAmount = (nextShieldTime / cooldownTime);

        if (nextShieldTime == 0)
            nextShieldTime = cooldownTime;
        if (nextShieldTime <= 0)
            nextShieldTime = 0;
        else if (nextShieldTime > 0)
            nextShieldTime -= Time.deltaTime;
    }

    public void SetFillToZero()
    {
        slamMask.fillAmount = 0;
        rageMask.fillAmount = 0;
        breatherMask.fillAmount = 0;
        shieldMask.fillAmount = 0;
    }
}
