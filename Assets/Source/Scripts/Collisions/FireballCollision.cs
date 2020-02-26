using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollision : MonoBehaviour
{
    public int[] collisionLayers;
    public float damage;
    public ParticleSystem collideEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (CollidesWithApropriateLayer(collision.collider.gameObject.layer))
            Explode(collision.collider.gameObject);
    }
    private void Explode(GameObject target)
    {
        Destroy(gameObject);
        Instantiate(collideEffect as ParticleSystem, transform.position, Quaternion.identity);
        if (target.transform.parent.gameObject)
            target.GetComponentInParent<Stats>().HP.TakeDamage(damage);
    }

    private bool CollidesWithApropriateLayer(int GOLayer)
    {
        foreach (var layer in collisionLayers)
            if (layer == GOLayer)
                return true;

        return false;
    }

}
