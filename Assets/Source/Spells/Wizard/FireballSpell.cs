using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject fireballPrefab;
    public Collider colliderToAvoid;

    private float[] cooldowns = { 3f, 2f, 1f };
    public float currentLifeTime;
    private int spellLevel;

    public void Cast(int level)
    {
        var spawnPosition = transform.position + 1.5f * direction;
        var fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<FireballCollision>().colliderToAvoid = colliderToAvoid;
        fireball.GetComponent<MoveTowardsDirection>().direction = direction;
        spellLevel = level;
    }

    void Update()
    {
        if (currentLifeTime >= cooldowns[spellLevel])
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
