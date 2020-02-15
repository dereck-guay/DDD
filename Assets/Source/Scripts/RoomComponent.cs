using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
   const string MapPath = "Prefabs\\MapPrefabs";

   public Room roomModel;

   //Object testWall;
   Object[] prefabs;

   void Awake()
   {
      prefabs = Resources.LoadAll(MapPath);
      //testWall = (GameObject)Resources.Load("Prefabs\\MapPrefabs\\Wall");  //TEMP
   }

   // Start is called before the first frame update
   void Start()
   {
      
   }

   public void Instantiate(int posX, int posY)
   {
      for (int i = 0; i < Room.Length; i++)
         for (int j = 0; j < Room.Width; j++)
            Instantiate(prefabs[roomModel[j, i]], new Vector3(-i - posY, 0, -j - posX), Quaternion.identity, transform);
   }
}
