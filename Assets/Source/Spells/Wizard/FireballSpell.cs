using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject fireballPrefab;

    private readonly float[] cooldowns = { 3f, 2f, 1f };
    private readonly float[] damage = { 4f, 5f, 7f };
    public float currentLifeTime;
    private int spellLevel;

    public void Cast(int level)
    {
        spellLevel = level;
        var spawnPosition = transform.position + 1.5f * direction;
        var fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<StraightProjectile>().direction = direction;
        //fireball.GetComponent<FireballCollision>().damage = damage[spellLevel - 1];
    }

    void Update()
    {
        if (currentLifeTime >= cooldowns[spellLevel - 1])
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
