﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ColliderManagerComponent : MonoBehaviour
{
    public BoxCollider triggerZone;
    public GameObject projectile;

    private float yPos;
    private float xPos;
    private const float xAndYSize = 1;
    private Vector3 oldPos;
    private Vector3 newPos;
    private List<Vector3> averageDeltaPos;
    private Vector3 newDeltaPos;
    private bool needToCaculateAverage = true;
    private void Start()
    {
        triggerZone = gameObject.AddComponent<BoxCollider>();
        yPos = projectile.transform.position.y;
        xPos = projectile.transform.position.x;
        gameObject.transform.parent.LookAt(projectile.GetComponent<StraightProjectile>().direction);
        triggerZone.isTrigger = true;
        averageDeltaPos = new List<Vector3>();
    }
    private void Update()
    {
        if (projectile)
        {
            newPos = projectile.transform.localPosition;
            var deltaPos = newPos - oldPos;
            Recenter(deltaPos);
            Enlarge(deltaPos);
            oldPos = newPos;
            averageDeltaPos.Add(deltaPos);
        }
        else
        {
            if (needToCaculateAverage)
            {
                CalculateAverage();
                needToCaculateAverage = false;
            }
            Recenter(newDeltaPos);
            Shrink();
            if (IsTooSmall(triggerZone.size, 0.001f))
                Destroy(gameObject);
        }
    }
    void CalculateAverage()
    {
        //https://stackoverflow.com/questions/33170643/finding-the-average-of-vectors-in-a-list
        //---------------------------------------
        newDeltaPos = new Vector3(averageDeltaPos.Average(e => e.x), averageDeltaPos.Average(e => e.y), averageDeltaPos.Average(e => e.z));
        //---------------------------------------
    }
    bool IsTooSmall(Vector3 v, float f) => v.z < f;
    void Recenter(Vector3 deltaPos) => triggerZone.center = new Vector3(xPos, yPos, triggerZone.center.z + Mathf.Sqrt(Mathf.Pow(deltaPos.x, 2) + Mathf.Pow(deltaPos.z, 2)) / 2);
    void Enlarge(Vector3 deltaPos) => triggerZone.size = new Vector3(xAndYSize, xAndYSize, triggerZone.size.z + Mathf.Sqrt(Mathf.Pow(deltaPos.x, 2) + Mathf.Pow(deltaPos.z, 2)));
    void Shrink() => triggerZone.size = new Vector3(xAndYSize, xAndYSize, triggerZone.size.z - Mathf.Sqrt(Mathf.Pow(newDeltaPos.x, 2) + Mathf.Pow(newDeltaPos.z, 2)));
}
