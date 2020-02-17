using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpell : MonoBehaviour
{
    public GameObject target;
    public float slowValue;

    private float[] cooldowns = { 10f, 9f, 8f };
    private float[] effectDurations = { 2f, 4f, 6f };
    private float currentLifeTime;
    private int playerLevel;
    private bool isActive;

    public void Cast()
    {
        Debug.Log(target.name);
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
