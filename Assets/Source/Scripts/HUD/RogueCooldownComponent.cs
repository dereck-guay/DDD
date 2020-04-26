using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RogueCooldownComponent : MonoBehaviour
{
    private float nextDashTime = 0;
    private float nextStunningTime = 0;
    //private float nextHealTime = 0;
    //private float nextSlowTime = 0;
    public Image dashMask;
    public Image stunningMask;
    //public Image healMask;
    //public Image slowMask;
    private RogueComponent rogue;

    private void Start()
    {
        rogue = GetComponentInParent<RogueComponent>();
    }

    void Update()
    {
        //if (rogue.GetComponent<>() != null)
        //    DashCooldown(rogue..cooldowns[0]);
        if (rogue.GetComponent<StunningBladeSpell>() != null)
            StunningCooldown(rogue.stunningBlade.cooldowns[0]);
        //if (rogue.GetComponent<>() != null)
        //    HealCooldown(rogue..cooldowns[0]);
        //if (rogue.GetComponent<>() != null)
        //    SlowCooldown(rogue..cooldowns[0]);
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

    //public void HealCooldown(float cooldownTime)
    //{
    //    stunningMask.fillAmount = (nextHealTime / cooldownTime);

    //    if (nextHealTime == 0)
    //        nextHealTime = cooldownTime;
    //    if (nextHealTime <= 0)
    //        nextHealTime = 0;
    //    else if (nextHealTime > 0)
    //        nextHealTime -= Time.deltaTime;
    //}

    //public void SlowCooldown(float cooldownTime)
    //{
    //    stunningMask.fillAmount = (nextSlowTime / cooldownTime);

    //    if (nextSlowTime == 0)
    //        nextSlowTime = cooldownTime;
    //    if (nextSlowTime <= 0)
    //        nextSlowTime = 0;
    //    else if (nextSlowTime > 0)
    //        nextSlowTime -= Time.deltaTime;
    //}
}
