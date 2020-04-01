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
    private int spellLevel = 1;
    private GameObject fireball;

    public void Cast(int level)
    {
        spellLevel = level;
        var spawnPosition = transform.position + 1.5f * direction;
        fireball = Instantiate(fireballPrefab, spawnPosition, transform.rotation);
        fireball.GetComponent<FireballCollision>().damage = damage[spellLevel - 1];
    }

    void Update()
    {
        if (currentLifeTime >= cooldowns[spellLevel - 1])
        {
            Destroy(this);
            Destroy(fireball);
        }

        currentLifeTime += Time.deltaTime;
    }
}
