using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackSpell : MonoBehaviour
{
    public GameObject target;
    public GameObject autoAttackPrefab;

    public float currentLifeTime = 0;
    float atkSpeed;

    public void Cast(float atkSpeedI, Vector3 casterPosition)
    {
        var targetDirection = target.transform.position - casterPosition;
        var spawnPosition = transform.position + 0.2f * targetDirection; 
        var autoAttack = Instantiate(autoAttackPrefab, spawnPosition, Quaternion.identity);
        autoAttack.GetComponent<FollowProjectile>().target = target;
        atkSpeed = atkSpeedI;
    }

    void Update()
    {
        if (currentLifeTime >= atkSpeed/60f)
            Destroy(this);
        
        currentLifeTime += Time.deltaTime;
    }
}
