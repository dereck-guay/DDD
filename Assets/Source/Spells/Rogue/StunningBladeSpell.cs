using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunningBladeSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject stunningBladePrefab;

    public float cooldown = 1;
    public float effectDuration = 1;
    private float currentLifeTime;
    public bool hasContacted = false;
    public EntityMonoBehaviour target;
    private GameObject stunningBlade;
    private bool stunHasBegun;
    public float stunDuration;
    public void Cast()
    {
        var spawnPosition = transform.position + 1.5f * direction;
        stunningBlade = Instantiate(stunningBladePrefab, spawnPosition, transform.rotation);
        stunningBlade.GetComponent<StunningBladeCollision>().stunningBladeSpell = this;
    }

    void Update()
    {
        if (stunHasBegun)
            stunDuration += Time.deltaTime;
        if (hasContacted)
        {
            if (target)
                target.IsStunned = true;
            stunHasBegun = true;
            hasContacted = false;
        }
        if (currentLifeTime >= cooldown)
        {
            if (target && target.IsStunned)
                target.IsStunned = false;
            Destroy(stunningBlade);
            Destroy(this);
        }
        if (stunDuration > effectDuration)
        {
            if(target)
                target.IsStunned = false;
        }
        currentLifeTime += Time.deltaTime;
    }
}
