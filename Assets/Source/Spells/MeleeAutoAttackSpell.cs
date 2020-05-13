using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAutoAttackSpell : AutoAttackMonoBehaviour
{
    public GameObject autoAttackPrefab;
    public float damage;

    public void Cast(float atkSpeedI, Vector3 casterPosition, Vector3 directionToLookAt, Transform parent, PlayerMonoBehaviour caster, string audioName)
    {
        Play(audioName);
        var spawnPosition = casterPosition;
        var autoAttack = Instantiate(autoAttackPrefab, spawnPosition, Quaternion.identity, parent);
        directionToLookAt.y = 0;
        autoAttack.transform.LookAt(casterPosition + directionToLookAt);
        autoAttack.GetComponentInChildren<AutoAttackCollision>().damage = damage;
        autoAttack.GetComponentInChildren<AutoAttackCollision>().caster = caster;
        atkSpeed = atkSpeedI;
    }
}
