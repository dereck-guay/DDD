using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ShieldSpell : MonoBehaviour
{
    public float cooldown = 1;
    public float effectiveTime = 1;
    public GameObject bodyToChange;
    public GameObject shield;
    private float currentLifeTime;
    public void Cast()
    {
        bodyToChange.layer = (int)Layers.Invulnerable;
        shield.SetActive(true);
    }

    private void Update()
    {
        if (currentLifeTime >= effectiveTime)
        {
            bodyToChange.layer = (int)Layers.Players;
            shield.SetActive(false);
        }
        if (currentLifeTime >= cooldown)
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
