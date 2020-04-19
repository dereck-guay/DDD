using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ShieldSpell : SpellMonoBehavior
{
    public float effectiveTime = 1;
    public GameObject player;
    public GameObject shield;
    public void Cast()
    {
        player.GetComponent<Stats>().HP.IsInvulnerable = true;
        shield.SetActive(true);
    }

    private void Update()
    {
        if (currentLifeTime >= effectiveTime && shield.activeSelf)
        {
            player.GetComponent<Stats>().HP.IsInvulnerable = false;
            shield.SetActive(false);
        }
        if (currentLifeTime >= cooldown)
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
