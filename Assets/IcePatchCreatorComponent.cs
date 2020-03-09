using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IcePatchCreatorComponent : MonoBehaviour
{
    public GameObject icePatchPrefab;
    public float timeBetweenIcePatches;
    private float timeSinceLastIcePatch;
    protected List<Collider> playersInContact;
    private void Awake() => playersInContact = new List<Collider>();
    private void Update()
    {
        if (timeSinceLastIcePatch > timeBetweenIcePatches)
        {
            var icePatch = Instantiate(icePatchPrefab, transform.position, Quaternion.identity);
            //icePatch.GetComponent<IcePatchComponent>().playersInContact = playersInContact;
            timeSinceLastIcePatch = 0;
        }
        timeSinceLastIcePatch += Time.deltaTime;
    }
}
