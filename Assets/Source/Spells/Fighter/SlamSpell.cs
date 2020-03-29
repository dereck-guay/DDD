using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamSpell : MonoBehaviour
{
    public float range;
    public Vector3 position;
    public Vector3 direction;

    private float[] cooldowns = { 10f, 9f, 8f };
    public float currentLifeTime;
    private int playerLevel = 1;
    private bool isActive;
    private bool hasLanded = false;
    private bool hasShockWaved = false;

    public void Cast(int level)
    {
        // Locks player movement.
        GetComponent<FighterComponent>().spellLocked = false;
        if (position.magnitude >= range)
            position = position.normalized * range;

        playerLevel = level;
        isActive = true;
    }

    private void Update()
    {
        if (currentLifeTime >= cooldowns[playerLevel])
            Destroy(this);

        // Mouvement projectile vers la position
        HandleJump();

        // GetComponent<FighterComponent>().spellLocked = false;
        if (hasShockWaved)
            GetComponent<FighterComponent>().spellLocked = false;
        // Quand le jump est fini
        else if (hasLanded)
            ShockWave();

        currentLifeTime += Time.deltaTime;
    }

    private void HandleJump()
    {
        // Calculate next frame position with direction.
    }

    private void ShockWave()
    {
        // Creates a shockwave that deals damage and knocks back enemies.
    }
}
