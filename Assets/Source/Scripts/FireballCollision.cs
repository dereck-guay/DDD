using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollision : MonoBehaviour
{
    public ParticleSystem collideEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            Destroy(gameObject);
            var deathAnimation = Instantiate(collideEffect as ParticleSystem, transform.position, Quaternion.identity);
        }
    }
}
