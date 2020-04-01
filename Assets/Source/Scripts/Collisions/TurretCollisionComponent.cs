using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TurretCollisionComponent : CollisionMonoBehaviour
{
    public int[] damageLayers;
    public float damage;
    public float maxDuration;

    float currentTime;
    Vector3 baseScale;

    private void Start() => baseScale = gameObject.transform.localScale;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;

        if (CollidesWithAppropriateLayer(target.layer, collisionLayers))
        {
            Destroy(gameObject);
            if (CollidesWithAppropriateLayer(target.layer, damageLayers))
                target.GetComponent<Stats>().HP.TakeDamage(damage);
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        gameObject.transform.localScale = baseScale * Mathf.Sqrt(Mathf.Abs(maxDuration - currentTime) / maxDuration);

        if (currentTime >= maxDuration)
            Destroy(gameObject);
    }
}
