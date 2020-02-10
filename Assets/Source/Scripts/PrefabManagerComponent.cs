using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PrefabManagerComponent : MonoBehaviour
{
   [SerializeField]
   List<string> prefabSubFoldersNames;

   [SerializeField]
   string prefabsFolderPath;

   // Start is called before the first frame update
   void Start()
   {
      //Resources.LoadAll()
   }

   void Awake()
   {
      Debug.Log(Directory.GetFiles(Path.Combine(prefabsFolderPath, prefabSubFoldersNames[0])).Length);
   }

   // Update is called once per frame
   void Update()
   {
      
   }
}
