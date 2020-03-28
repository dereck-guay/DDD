using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfFrostCollision : CollisionMonoBehaviour
{
    public ParticleSystem collideEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (CollidesWithAppropriateLayer(collision.collider.gameObject.layer, collisionLayers))
            Explode();
    }
    private void Explode()
    {
        Destroy(gameObject);
        Instantiate(collideEffect as ParticleSystem, transform.position, Quaternion.identity);
    }
}
