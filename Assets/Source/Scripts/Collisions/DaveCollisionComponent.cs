using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScaleDownComponent))]
public class DaveCollisionComponent : CollisionMonoBehaviour
{
    public int[] damageLayers;
    public new MeshRenderer renderer;
    public new Light light;

    [HideInInspector]
    public float damage;

    public float Duration { get; private set; }

    void Start()
    {
        Duration = GetComponent<ScaleDownComponent>().maxDuration;

        Color c = Random.ColorHSV(0, 1, .6f, .8f, .5f, .5f);

        renderer.material.color = c;
        renderer.material.SetColor("_EmissionColor", c);
        light.color = c;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;

        if (CollidesWithAppropriateLayer(target.layer, collisionLayers))
        {
            Destroy(gameObject);
            if (CollidesWithAppropriateLayer(target.layer, damageLayers))
                target.GetComponent<Stats>().HP.TakeDamage(damage, null);
        }
    }
}
