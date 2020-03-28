using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ShieldSpell : MonoBehaviour
{
    private readonly float[] cooldowns = { 10f, 9f, 8f };
    private readonly float[] effectiveTimes = { 2f, 3f, 4f };
    public float currentLifeTime;
    private int playerLevel = 1;
    public GameObject bodyToChange;
    public GameObject shield;
    public void Cast(int level)
    {
        playerLevel = level;
        bodyToChange.layer = (int)Layers.Invulnerable;
        shield.SetActive(true);
    }

    private void Update()
    {
        if (currentLifeTime >= effectiveTimes[playerLevel - 1])
        {
            bodyToChange.layer = (int)Layers.Players;
            shield.SetActive(false);
        }
        if (currentLifeTime >= cooldowns[playerLevel - 1])
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
