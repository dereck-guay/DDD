using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfFrostSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject rayOfFrostPrefab;
    public GameObject icePatchPrefab;
    public float lengthOfPatch = 1;

    private readonly float[] cooldowns = { 6f, 5f, 4f };
    public float currentLifeTime;
    private int spellLevel;
    private GameObject rayOfFrost;
    public void Cast(int level)
    {
        var spawnPosition = transform.position + 1.5f * direction;
        rayOfFrost = Instantiate(rayOfFrostPrefab, spawnPosition, Quaternion.identity);
        rayOfFrost.GetComponent<IcePatchManagerComponent>().timeBetweenIcePatches = 1;
        rayOfFrost.GetComponent<StraightProjectile>().direction = direction;
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
