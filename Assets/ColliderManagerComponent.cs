using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManagerComponent : MonoBehaviour
{
    public BoxCollider triggerZone;
    public float multiplier = 2;
    private Vector3 oldPos;
    private bool bulletIsStillActive;
    public GameObject projectile;
    public Transform plane;
    private void Start()
    {
        triggerZone = gameObject.AddComponent<BoxCollider>();
        plane = GetComponentInChildren<Transform>();
    }
    private void Update()
    {
        if (projectile)
        {
            var newPos = projectile.transform.position;
            var deltaPos = newPos - oldPos;
            triggerZone.center = new Vector3(triggerZone.center.x, triggerZone.center.y, triggerZone.center.z + deltaPos.z / 2);
            triggerZone.size = new Vector3(triggerZone.size.x, triggerZone.size.y, triggerZone.size.z + deltaPos.z);
            oldPos = newPos;
            plane.position = newPos;
        }
        
    }
}
