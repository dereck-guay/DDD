using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackCollision : MonoBehaviour
{
    public int[] collsionLayers;
    private void OnCollisionEntre(Collider other)
    {
        if (CollidesWithAppropriateLayer(other.gameObject.layer))
            Destroy(gameObject);
    }
    private bool CollidesWithAppropriateLayer(int GOLayer)
    {
        foreach (var layer in collsionLayers)
            if (layer == GOLayer)
            {
                Debug.Log("Allo esit de mongole?");
                return true;
            }

        return false;
    }
}
