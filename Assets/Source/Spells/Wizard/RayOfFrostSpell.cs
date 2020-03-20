using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfFrostSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject rayOfFrostPrefab;
    public GameObject icePatchPrefab;
    public float slowValue;

    private readonly float[] cooldowns = { 6f, 5f, 4f };
    public float currentLifeTime;
    private int spellLevel = 1;
    private GameObject rayOfFrost;
    public void Cast(int level, PlayerMonoBehaviour player)
    {
        var spawnPosition = transform.position + 1.5f * direction;
        rayOfFrost = Instantiate(rayOfFrostPrefab, spawnPosition, Quaternion.identity);
        rayOfFrost.GetComponent<StraightProjectile>().direction = direction;
        var icePatch = Instantiate(icePatchPrefab, transform.position + 1.4f * direction, Quaternion.identity);
        icePatch.transform.LookAt(rayOfFrost.transform);
        IcePatchComponent icePatchComponent = icePatch.GetComponentInChildren<IcePatchComponent>();
        icePatchComponent.projectile = rayOfFrost;
        icePatchComponent.player = player;
        icePatchComponent.slowValue = slowValue;
        spellLevel = level;
    }
    void Update()
    {
        if (currentLifeTime >= cooldowns[spellLevel - 1])
        {
            Destroy(this);
            Destroy(rayOfFrost);
        }
        currentLifeTime += Time.deltaTime;
    }
}
