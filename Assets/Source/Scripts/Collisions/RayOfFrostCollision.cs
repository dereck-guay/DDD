using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfFrostCollision : MonoBehaviour
{
    public int[] collisionLayers;
    public ParticleSystem collideEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (CollidesWithApropriateLayer(collision.collider.gameObject.layer))
            Explode(collision.collider.gameObject);
    }
    private void Explode(GameObject gameObject)
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
}
