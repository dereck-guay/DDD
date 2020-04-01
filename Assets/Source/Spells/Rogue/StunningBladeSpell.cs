using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunningBladeSpell : MonoBehaviour
{
    public Vector3 direction;
    public GameObject stunningBladePrefab;

    private readonly float[] cooldowns = { 7f, 5f, 4f };
    private readonly float[] stunDurations = { 2f, 3f, 3.5f };
    public float currentLifeTime;
    private int spellLevel = 1;
    private GameObject stunningBlade;
    public bool hasContacted = false;
    public EntityMonoBehaviour target;
    private bool stunHasBegun;
    public float stunDuration;
    public void Cast(int level)
    {
        spellLevel = level;
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
        if (currentLifeTime >= cooldowns[spellLevel - 1])
        {
            if (target && target.IsStunned)
                target.IsStunned = false;
            Destroy(stunningBlade);
            Destroy(this);
        }
        if (stunDuration > stunDurations[spellLevel - 1])
        {
            if(target)
                target.IsStunned = false;
        }
        currentLifeTime += Time.deltaTime;
    }
}
