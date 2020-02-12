using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
   Object testWall;
   // Start is called before the first frame update
   void Start()
   {
      testWall = Resources.Load("Prefabs\\MapPrefabs\\Wall1");
      Instantiate(testWall, new Vector3(0, 0, 0), Quaternion.identity);
   }

   // Update is called once per frame
   void Update()
   {
      
   }
}
