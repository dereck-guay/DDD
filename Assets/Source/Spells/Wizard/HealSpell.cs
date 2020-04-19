using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class HealSpell : SpellMonoBehavior
{
    public GameObject target;
    public float healValue;

    public void Cast() =>
        target.GetComponent<PlayerMonoBehaviour>().entityStats.HP.Heal(healValue);

    private void Update()
    {
        if (currentLifeTime >= cooldown)
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
