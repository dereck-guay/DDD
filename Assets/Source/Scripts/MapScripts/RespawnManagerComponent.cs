using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManagerComponent : MonoBehaviour
{
    public float respawnDelay;
    List<Vector3> respawnPoints;
    
    void Awake() => respawnPoints = new List<Vector3>(4);


    public void AddRespawnPoint(Vector3 position) => respawnPoints.Add(position);

    public Vector3 GetRandomRespawnPoint() => respawnPoints[Random.Range(0, respawnPoints.Count)];
}
