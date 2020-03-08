using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePatchCreatorComponent : MonoBehaviour
{
    public GameObject icePatchPrefab;
    public float timeBetweenIcePatches;
    private float timeSinceLastIcePatch;
    private void Update()
    {
        if (timeSinceLastIcePatch > timeBetweenIcePatches)
        {
            Instantiate(icePatchPrefab, transform.position, Quaternion.identity);
            timeSinceLastIcePatch = 0;
        }
        timeSinceLastIcePatch += Time.deltaTime;
    }
}
