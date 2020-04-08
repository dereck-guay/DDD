using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject fireballPrefab;

    public float cooldown;
    public float damage;
    public float currentLifeTime;
    private GameObject fireball;

    public void Cast()
    {
        var spawnPosition = transform.position + 1.5f * direction;
        fireball = Instantiate(fireballPrefab, spawnPosition, transform.rotation);
        fireball.GetComponent<FireballCollision>().damage = damage;
    }

    void Update()
    {
        if (currentLifeTime >= cooldown)
        {
            Destroy(this);
            Destroy(fireball);
        }

        currentLifeTime += Time.deltaTime;
    }
}
