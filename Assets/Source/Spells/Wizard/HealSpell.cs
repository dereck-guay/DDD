using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class HealSpell : MonoBehaviour
{
    public GameObject target;
    public float[] healValue;

    private float[] cooldowns = { 10f, 9f, 8f };
    public float currentLifeTime;
    private int playerLevel = 1;

    public void Cast(int level)
    {
        playerLevel = level;
        Debug.Log(playerLevel);
        target.GetComponent<PlayerMonoBehaviour>().entityStats.HP.Heal(healValue[playerLevel - 1]);
        Debug.Log($"Character has been healed for {healValue[playerLevel - 1]}");
    }

    private void Update()
    {
        if (currentLifeTime >= cooldowns[playerLevel - 1])
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
