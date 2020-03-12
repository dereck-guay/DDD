using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
   public GameObject enemy;
   public float weight;
}

[RequireComponent(typeof(SphereCollider))]
public class SpawnerComponent : MonoBehaviour
{
   public EnemyData[] enemies;
   public float delay;
   public int maxEnemyCount;
   public int enemyLayer;
   public int playerLayer;

   float totalWeight;
   float elapsedTime;
   int nbPlayersInArea = 0;

   SphereCollider area;
   List<GameObject> enemiesInArea;

   void Start() 
   { 
      totalWeight = enemies.Sum(e => e.weight);
      elapsedTime = Random.Range(0, delay);
      area = GetComponent<SphereCollider>();
      area.isTrigger = true;
      enemiesInArea = new List<GameObject>();
   }

   void Update()
   {
      elapsedTime += Time.deltaTime;

      if (elapsedTime >= delay)
      {
         elapsedTime -= delay;
         TrySpawn();
      }
   }

   void Spawn()
   {
      Debug.Log("Enemy spawned");
   }

   void TrySpawn()
   {
      if (enemiesInArea.Count < maxEnemyCount && nbPlayersInArea == 0)
         Spawn();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer == enemyLayer)
         enemiesInArea.Add(other.gameObject);

      if (other.gameObject.layer == playerLayer)
         nbPlayersInArea++;
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.layer == enemyLayer && enemiesInArea.Contains(other.gameObject))
         enemiesInArea.Remove(other.gameObject);

      if (other.gameObject.layer == playerLayer)
         nbPlayersInArea--;
   }
}
