using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RogueCooldownComponent : MonoBehaviour
{
    private float nextDashTime = 0;
    private float nextStunningTime = 0;
    private float nextFanTime = 0;
    private float nextSmokeTime = 0;
    public Image dashMask;
    public Image stunningMask;
    public Image fanMask;
    public Image smokeMask;
    private RogueComponent rogue;

    private void Start()
    {
        rogue = GetComponentInParent<RogueComponent>();
    }

    void Update()
    {
        if (rogue.GetComponent<DashSpell>() != null)
            DashCooldown(rogue.dash.cooldowns[rogue.entityStats.XP.Level - 1]);
        if (rogue.GetComponent<StunningBladeSpell>() != null)
            StunningCooldown(rogue.stunningBlade.cooldowns[rogue.entityStats.XP.Level - 1]);
        if (rogue.GetComponent<FanOfKnivesSpell>() != null)
            FanCooldown(rogue.fanOfKnives.cooldowns[rogue.entityStats.XP.Level - 1]);
        if (rogue.GetComponent<SmokeScreenSpell>() != null)
            SmokeCooldown(rogue.smokeScreen.cooldowns[rogue.entityStats.XP.Level - 1]);
    }

    public void DashCooldown(float cooldownTime)
    {
        dashMask.fillAmount = (nextDashTime / cooldownTime);

        if (nextDashTime == 0)
            nextDashTime = cooldownTime;
        if (nextDashTime <= 0)
        {
            nextDashTime = 0;
            dashMask.fillAmount = 0;
        }
        else if (nextDashTime > 0)
            nextDashTime -= Time.deltaTime;
    }

    public void StunningCooldown(float cooldownTime)
    {
        stunningMask.fillAmount = (nextStunningTime / cooldownTime);

        if (nextStunningTime == 0)
            nextStunningTime = cooldownTime;
        if (nextStunningTime <= 0)
        {
            nextStunningTime = 0;
            stunningMask.fillAmount = 0;
        }
        else if (nextStunningTime > 0)
            nextStunningTime -= Time.deltaTime;
    }

    public void SmokeCooldown(float cooldownTime)
    {
        smokeMask.fillAmount = (nextFanTime / cooldownTime);

        if (nextFanTime == 0)
            nextFanTime = cooldownTime;
        if (nextFanTime <= 0)
            nextFanTime = 0;
        else if (nextFanTime > 0)
            nextFanTime -= Time.deltaTime;
    }

    public void FanCooldown(float cooldownTime)
    {
        fanMask.fillAmount = (nextSmokeTime / cooldownTime);

        if (nextSmokeTime == 0)
            nextSmokeTime = cooldownTime;
        if (nextSmokeTime <= 0)
            nextSmokeTime = 0;
        else if (nextSmokeTime > 0)
            nextSmokeTime -= Time.deltaTime;
    }
}
