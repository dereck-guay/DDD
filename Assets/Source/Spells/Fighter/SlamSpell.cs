using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamSpell : MonoBehaviour
{
    public float range;
    public Vector3 position;

    private float[] cooldowns = { 10f, 9f, 8f };
    public float currentLifeTime;
    private int playerLevel;
    private bool isActive;

    public void Cast()
    {
        Debug.Log("ÉPIC SLAM DUNK");

        if (position.magnitude >= range)
            position = position.normalized * range;

        playerLevel = GetComponent<XP>().Level;
        isActive = true;
    }

    private void Update()
    {
        if (currentLifeTime >= cooldowns[playerLevel])
            Destroy(this);

        // Mouvement projectile vers la position

        // Quand l'animation est fini
        // GetComponent<FighterComponent>().spellLocked = false;

        currentLifeTime += Time.deltaTime;
    }
}
