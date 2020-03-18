using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IgnoreLayerCollisionData
{
   public int layer1;
   public int layer2;
}

public class GameManagerComponent : MonoBehaviour
{
   [Range(0, 120)]
   public int framerate;
   public IgnoreLayerCollisionData[] ignoreLayersCollisions;

   void Start()
   {
      QualitySettings.vSyncCount = 0;
      Application.targetFrameRate = framerate;

      foreach (var layers in ignoreLayersCollisions)
         Physics.IgnoreLayerCollision(layers.layer1, layers.layer2);
   }
}
