using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Delay
{
    public float min, max;
    [HideInInspector]
    public float currentDelay;
}

public class IcePatchComponent : CollisionMonoBehaviour
{
    public BoxCollider triggerZone;
    public GameObject projectile;
    public PlayerMonoBehaviour player;
    public float slowValue;

    public Delay delayBetweenPatches;
    public GameObject icePatch;
    public float groundLevel = 0;
    float icePatchCooldown = 0;

    private Collider[] playerColliders;
    public float yPos;
    public float xPos;
    private const float xAndYSize = 1;
    private Vector3 oldPos;
    private Vector3 newPos;
    private List<Vector3> averageDeltaPos;
    private Vector3 newDeltaPos;
    private bool needToCaculateAverage = true;
    private void Start()
    {
        playerColliders = player.GetComponents<Collider>();
        triggerZone = gameObject.AddComponent<BoxCollider>();
        triggerZone.isTrigger = true;
        averageDeltaPos = new List<Vector3>();
        oldPos = projectile.transform.position;
        delayBetweenPatches.currentDelay = Random.Range(delayBetweenPatches.max, delayBetweenPatches.min);
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
            
            icePatchCooldown += Time.deltaTime;
            if (icePatchCooldown >= delayBetweenPatches.currentDelay)
            {
                icePatchCooldown -= delayBetweenPatches.currentDelay;
                
                var currentIcePatch = Instantiate(icePatch, new Vector3(projectile.transform.position.x, groundLevel, projectile.transform.position.z), Quaternion.identity);

                delayBetweenPatches.currentDelay = Random.Range(delayBetweenPatches.max, delayBetweenPatches.min);
            }
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
            if (triggerZone.size.z < 0.001f)
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
    void Recenter(Vector3 deltaPos) => triggerZone.center = new Vector3(xPos, yPos, triggerZone.center.z + Mathf.Sqrt(Mathf.Pow(deltaPos.x, 2) + Mathf.Pow(deltaPos.z, 2)) / 2);
    void Enlarge(Vector3 deltaPos) => triggerZone.size = new Vector3(xAndYSize, xAndYSize, triggerZone.size.z + Mathf.Sqrt(Mathf.Pow(deltaPos.x, 2) + Mathf.Pow(deltaPos.z, 2)));
    void Shrink() => triggerZone.size = new Vector3(xAndYSize, xAndYSize, triggerZone.size.z - Mathf.Sqrt(Mathf.Pow(newDeltaPos.x, 2) + Mathf.Pow(newDeltaPos.z, 2)));
    private void OnTriggerExit(Collider other)
    {
        if (CollidesWithAppropriateLayer(other.gameObject.layer, collisionLayers) && !playerColliders.Contains(other))
            Effect(false, other);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (CollidesWithAppropriateLayer(other.gameObject.layer, collisionLayers) && !playerColliders.Contains(other))
            Effect(true, other);
    }
    private void Effect(bool activate, Collider other)
    {
        if(activate)
            other.gameObject.GetComponentInParent<EffectHandlerComponent>().ApplyEffect((int)ModifiableStats.Speed, slowValue);
        else
            other.gameObject.GetComponentInParent<EffectHandlerComponent>().EndEffect((int)ModifiableStats.Speed, slowValue);
    }
}
