using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunningBladeCollision : CollisionMonoBehaviour
{
    public int[] stunLayers;
    public ParticleSystem collideEffect;
    public StunningBladeSpell stunningBladeSpell;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (CollidesWithAppropriateLayer(collision.gameObject.layer, collisionLayers))
            Explode(collision.gameObject);
    }

    private void Explode(GameObject target)
    {
        stunningBladeSpell.hasContacted = true;
        Instantiate(collideEffect as ParticleSystem, transform.position, Quaternion.identity);
        if (CollidesWithAppropriateLayer(target.layer, stunLayers))
            stunningBladeSpell.target = target.GetComponent<EntityMonoBehaviour>();
        Destroy(gameObject);
    }
}
