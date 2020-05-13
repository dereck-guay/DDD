using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAutoAttackSpell : AutoAttackMonoBehaviour
{
    public GameObject target;
    public GameObject autoAttackPrefab;
    public float damage;

    public void Cast(float atkSpeedI, Vector3 casterPosition, PlayerMonoBehaviour caster, string audioName)
    {
        Play(audioName);
        var targetDirection = target.transform.position - casterPosition;
        var spawnPosition = transform.position + 0.2f * targetDirection; 
        var autoAttack = Instantiate(autoAttackPrefab, spawnPosition, Quaternion.identity);
        autoAttack.GetComponent<FollowProjectile>().target = target;
        autoAttack.GetComponent<AutoAttackCollision>().damage = damage;
        autoAttack.GetComponent<AutoAttackCollision>().caster = caster;
        atkSpeed = atkSpeedI;
    }
}
