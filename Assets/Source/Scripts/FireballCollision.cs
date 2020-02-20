using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollision : MonoBehaviour
{
    public int[] collisionLayers;
    public int[] triggerLayers;
    public ParticleSystem collideEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (TriggersApropriateLayer(other.gameObject.layer))
            Explode();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (CollidesWithApropriateLayer(collision.collider.gameObject.layer))
            Explode();
    }
    private void Explode()
    {
        Destroy(gameObject);
        Instantiate(collideEffect as ParticleSystem, transform.position, Quaternion.identity);
    }

    private bool CollidesWithApropriateLayer(int GOLayer)
    {
        foreach (var layer in collisionLayers)
            if (layer == GOLayer)
                return true;

        return false;
    }
    private bool TriggersApropriateLayer(int GOLayer)
    {
        foreach (var layer in triggerLayers)
            if (layer == GOLayer)
                return true;

        return false;
    }

}
