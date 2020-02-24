using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackCollision : MonoBehaviour
{
    public int[] collsionLayers;
    private void OnTriggerEnter(Collider other)
    {
        if (CollidesWithAppropriateLayer(other.gameObject.layer))
        {
            Destroy(gameObject);
            other.GetComponentInParent<Stats>().HP.TakeDamage(4f);
            Debug.Log(other.GetComponentInParent<Stats>().gameObject.GetComponent<PlayerMonoBehaviour>().name);
        }
    }
    private bool CollidesWithAppropriateLayer(int GOLayer)
    {
        foreach (var layer in collsionLayers)
            if (layer == GOLayer)
                return true;
        return false;
    }
}
