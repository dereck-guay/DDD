using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
   const string MapPath = "Prefabs\\MapPrefabs";

   public Room roomModel;
   
   Object[] prefabs;

   void Awake() => prefabs = Resources.LoadAll(MapPath);

   public void Instantiate()
   {
      GameObject tile;

      for (int i = 0; i < Room.Length; i++)
         for (int j = 0; j < Room.Width; j++)
         {
            tile = (GameObject)Instantiate(prefabs[roomModel[j, i]], transform);
            tile.transform.localPosition = new Vector3(j - Room.Width / 2, 0, -i + Room.Length / 2);
         }
   }
   /*
   public void Instantiate(int posX, int posY)
   {
      GameObject tile;

      for (int i = 0; i < Room.Length; i++)
         for (int j = 0; j < Room.Width; j++)
         {
            tile = (GameObject)Instantiate(prefabs[roomModel[j, i]], new Vector3(-i - posY, 0, -j - posX), Quaternion.identity, transform);
         }
   }
   */
}
