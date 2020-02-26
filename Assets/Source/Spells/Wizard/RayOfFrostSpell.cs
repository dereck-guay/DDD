using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfFrostSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject rayOfFrostPrefab;
    public GameObject icePatchPrefab;
    public float lengthOfPatch;

    private readonly float[] cooldowns = { 6f, 5f, 4f };
    public float currentLifeTime;
    private int spellLevel;
    private float timeSinceLastIcePatch;
    private float timeBetweenIcePatches;
    public void Cast(int level)
    {
        var spawnPosition = transform.position + 1.5f * direction;
        var rayOfFrost = Instantiate(rayOfFrostPrefab, spawnPosition, Quaternion.identity);
        rayOfFrost.GetComponent<StraightProjectile>().direction = direction;
        spellLevel = level;
        timeBetweenIcePatches = lengthOfPatch / rayOfFrost.GetComponent<StraightProjectile>().speed;
    }

    void Update()
    {
        if (timeSinceLastIcePatch > timeBetweenIcePatches)
        {
            Instantiate(icePatchPrefab, transform.position, Quaternion.identity);
            timeSinceLastIcePatch -= timeBetweenIcePatches;
        }
        if (currentLifeTime >= cooldowns[spellLevel - 1])
            Destroy(this);

        timeSinceLastIcePatch += Time.deltaTime;
        currentLifeTime += Time.deltaTime;
    }
}
