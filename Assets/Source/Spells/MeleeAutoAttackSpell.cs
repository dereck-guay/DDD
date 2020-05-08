using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAutoAttackSpell : MonoBehaviour
{
    public GameObject autoAttackPrefab;

    public float currentLifeTime = 0;
    float atkSpeed;
    public float damage;

    public void Cast(float atkSpeedI, Vector3 casterPosition, Vector3 directionToLookAt, Transform parent, PlayerMonoBehaviour caster)
    {
        var spawnPosition = casterPosition;
        var autoAttack = Instantiate(autoAttackPrefab, spawnPosition, Quaternion.identity, parent);
        directionToLookAt.y = 0;
        autoAttack.transform.LookAt(casterPosition + directionToLookAt);
        autoAttack.GetComponentInChildren<AutoAttackCollision>().damage = damage;
        autoAttack.GetComponentInChildren<AutoAttackCollision>().caster = caster;
        atkSpeed = atkSpeedI;
    }

    void Update()
    {
        if (currentLifeTime >= atkSpeed / 60f)
            Destroy(this);

        currentLifeTime += Time.deltaTime;
    }
}
