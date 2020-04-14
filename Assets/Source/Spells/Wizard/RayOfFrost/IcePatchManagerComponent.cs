using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IcePatchManagerComponent : CollisionMonoBehaviour
{
    public BoxCollider triggerZone;
    public GameObject projectile;
    public PlayerMonoBehaviour player;
    public float slowValue;
    public float lifeSpan;

    //Ice patches generation
    //------------------------------------
    public FloatInterval spacingBetweenPatches;
    public GameObject icePatch;
    public float groundLevel = 0;
    float distanceCovered = 0;
    Vector3 startingPosition,
            rayOrientation;
    //------------------------------------

    private CapsuleCollider playerCollider;
    private float currentLifeTime = 0;
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
        playerCollider = player.gameObject.GetComponentInChildren<CapsuleCollider>();
        triggerZone = gameObject.AddComponent<BoxCollider>();
        triggerZone.isTrigger = true;
        averageDeltaPos = new List<Vector3>();

        startingPosition = projectile.transform.position;
        rayOrientation = projectile.transform.forward;

        oldPos = startingPosition;
        startingPosition.y = groundLevel;
    }
    private void Update()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime < lifeSpan)
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

            while (distanceCovered + spacingBetweenPatches.NewRandomValue() < (newPos - startingPosition).magnitude)
            {
                distanceCovered += spacingBetweenPatches.Current;

                Instantiate(icePatch, startingPosition + rayOrientation * distanceCovered, Quaternion.identity).GetComponent<IcePatchComponent>().lifeSpan = lifeSpan;
            }
        }
        else
        {
            if (needToCaculateAverage && averageDeltaPos.Count != 0)
            {
                CalculateAverage();
                needToCaculateAverage = false;
            }
            else if (newDeltaPos == Vector3.zero)
                triggerZone.size = Vector3.zero;
            Recenter(newDeltaPos);
            Shrink();
            if (triggerZone.size.z < 0.001f)
                Destroy(gameObject.transform.parent.gameObject);
        }
    }
    void CalculateAverage()
    {
        //https://stackoverflow.com/questions/33170643/finding-the-average-of-vectors-in-a-list
        //---------------------------------------
        newDeltaPos = new Vector3(averageDeltaPos.Average(e => e.x), averageDeltaPos.Average(e => e.y), averageDeltaPos.Average(e => e.z));
        //---------------------------------------
    }
    void Recenter(Vector3 deltaPos) => triggerZone.center = new Vector3(xPos, yPos, triggerZone.center.z + Mathf.Sqrt(Mathf.Pow(deltaPos.x, 2) + Mathf.Pow(deltaPos.z, 2)) / 2);
    void Enlarge(Vector3 deltaPos) => triggerZone.size = new Vector3(xAndYSize, xAndYSize, triggerZone.size.z + Mathf.Sqrt(Mathf.Pow(deltaPos.x, 2) + Mathf.Pow(deltaPos.z, 2)));
    void Shrink() => triggerZone.size = new Vector3(xAndYSize, xAndYSize, triggerZone.size.z - Mathf.Sqrt(Mathf.Pow(newDeltaPos.x, 2) + Mathf.Pow(newDeltaPos.z, 2)));
    private void OnTriggerExit(Collider other)
    {
        if (CollidesWithAppropriateLayer(other.gameObject.layer, collisionLayers) && playerCollider != other)
            Effect(false, other);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (CollidesWithAppropriateLayer(other.gameObject.layer, collisionLayers) && playerCollider != other)
        {
            Effect(true, other);
        }
    }
    private void Effect(bool activate, Collider other)
    {
        if(activate)
            other.gameObject.GetComponentInParent<EffectHandlerComponent>().ApplyEffect((int)ModifiableStats.Speed, slowValue);
        else
            other.gameObject.GetComponentInParent<EffectHandlerComponent>().EndEffect((int)ModifiableStats.Speed, slowValue);
    }

}
