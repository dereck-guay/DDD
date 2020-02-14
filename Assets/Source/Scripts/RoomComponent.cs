using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
   public Room roomModel;

   Object testWall;

   void Awake()
   {
      testWall = (GameObject)Resources.Load("Prefabs\\MapPrefabs\\Wall1");  //TEMP
   }

   // Start is called before the first frame update
   void Start()
   {
      
   }

   public void Instantiate(int posX, int posY)
   {
      for (int i = 0; i < Room.Length; i++)
         for (int j = 0; j < Room.Width; j++)
            Instantiate(testWall, new Vector3(i - posY, 0, j - posX), Quaternion.identity, transform);
   }
}
