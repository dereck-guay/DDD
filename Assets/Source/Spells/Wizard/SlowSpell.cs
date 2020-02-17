using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpell : MonoBehaviour
{
    public GameObject target;
    public float slowValue;

    private float[] cooldowns = { 3f, 2f, 1f };
    private float[] effectDurations = { 1f, 2f, 3f };
    private float currentLifeTime;
    private int playerLevel;
    private bool isActive;

    private void Awake()
    {
        target.GetComponent<SpeedComponent>().ApplyModifier(slowValue);
        playerLevel = GetComponent<XPComponent>().Level;
        isActive = true;
    }

    private void Update()
    {
        if (currentLifeTime >= cooldowns[playerLevel])
            Destroy(this);
        else if(currentLifeTime >= effectDurations[playerLevel] && isActive)
        {
            target.GetComponent<SpeedComponent>().EndModifier(slowValue);
            isActive = false;
        }

        currentLifeTime += Time.deltaTime;
    }
}
