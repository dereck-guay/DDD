using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject fireballPrefab;

    private float[] cooldowns = { 3f, 2f, 1f };
    public float currentLifeTime;
    private int playerLevel;

    public void Cast()
    {
        var spawnPosition = transform.position + 1.5f * direction;
        var fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        fireball.GetComponent<MoveTowardsDirection>().direction = direction;
        playerLevel = GetComponent<XPComponent>().Level;
    }

    void Update()
    {
        if (currentLifeTime >= cooldowns[playerLevel])
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
