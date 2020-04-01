using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfFrostSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject rayOfFrostPrefab;
    public GameObject icePatchManagerPrefab;
    public float slowValue;

    private readonly float[] cooldowns = { 6f, 5f, 4f };
    public float currentLifeTime;
    private int spellLevel = 1;
    private GameObject rayOfFrost;
    public void Cast(int level, PlayerMonoBehaviour player)
    {
        var spawnPosition = transform.position + 1.5f * direction;
        rayOfFrost = Instantiate(rayOfFrostPrefab, spawnPosition, player.transform.rotation); //Changed Quaternion.identity for player rotation  ~Yan
        var icePatchManager = Instantiate(icePatchManagerPrefab, new Vector3(transform.position.x + 1.4f * direction.x, spawnPosition.y, transform.position.z + 1.4f * direction.z), Quaternion.identity);
        icePatchManager.transform.LookAt(rayOfFrost.transform);
        IcePatchManagerComponent icePatchManagerComponent = icePatchManager.GetComponentInChildren<IcePatchManagerComponent>();
        icePatchManagerComponent.projectile = rayOfFrost;
        icePatchManagerComponent.player = player;
        icePatchManagerComponent.slowValue = slowValue;
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
