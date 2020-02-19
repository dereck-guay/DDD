using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public struct Tile
{
   public int Value { get; }
   public string Name { get => texture.name; }
   Texture texture;

   public Tile(int valueI, Texture textureI)
   {
      Value = valueI;
      texture = textureI;
   }
}

public class RoomEditorComponent : MonoBehaviour
{
   int[,] roomLayout;
   Tile[] tiles;
   Tile currentTile;
   public string patternsPath, tilesPath;

   public Button saveButton;
   public Toggle bossRoomToggle,
                 northToggle,
                 westToggle,
                 eastToggle,
                 southToggle;
   public Dropdown tileSelector;

   void Awake()
   {
      GenerateTiles(Resources.LoadAll<Texture>(tilesPath));
   }

   void Start()
   {
      tileSelector.onValueChanged.AddListener(delegate { currentTile = tiles[tileSelector.value]; });
      tileSelector.onValueChanged.AddListener(delegate { Debug.Log(currentTile.Value); });

      tileSelector.AddOptions(tiles.Select(tile => tile.Name).ToList());
   }

   // Update is called once per frame
   void Update()
   {
      
   }

   void GenerateTiles(Texture[] sprites)
   {
      tiles = new Tile[sprites.Length];

      for (int i = 0; i < tiles.Length; ++i)
         tiles[i] = new Tile(i, sprites[i]);
   }
}
