using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LaserCollisionComponent : CollisionMonoBehaviour
{
    public LayerMask staticObjectLayer;
    public float maxDistance;
    public float lifeSpan = 1;
    public int[] damageLayers;

    RaycastHit hit;
    float laserLength;
    float currentLifeTime = 0;
    float width = 1;

    [HideInInspector]
    public float damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, staticObjectLayer))
            laserLength = hit.distance;
        else
            laserLength = maxDistance;

        transform.localScale = Vector3.forward * laserLength;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= lifeSpan)
            Destroy(gameObject);
        else
        {
            width = (lifeSpan - currentLifeTime) / lifeSpan;
            transform.localScale = new Vector3(width, width, laserLength);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (CollidesWithAppropriateLayer(other.gameObject.layer, damageLayers))
            other.GetComponent<Stats>().HP.TakeDamage(damage * Time.deltaTime, null);
    }
}
