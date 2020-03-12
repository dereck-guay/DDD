using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMonoBehaviour : MonoBehaviour
{
    public int[] collisionLayers;

    protected bool CollidesWithAppropriateLayer(int GOLayer, int[] layers)
    {
        foreach (var layer in layers)
            if (layer == GOLayer)
                return true;
        return false;
    }
}
