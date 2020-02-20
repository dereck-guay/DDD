using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackCollision : MonoBehaviour
{
    public int[] triggerLayers;
    private void OnTriggerEnter(Collider other)
    {
        if (TriggersApropriateLayer(other.gameObject.layer))
            Destroy(gameObject);
    }
    private bool TriggersApropriateLayer(int GOLayer)
    {
        foreach (var layer in triggerLayers)
            if (layer == GOLayer)
                return true;

        return false;
    }
}
