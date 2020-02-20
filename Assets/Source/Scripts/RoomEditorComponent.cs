using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
   Tile[] tiles;

   Tile currentTile;
   public Tile CurrentTile { get => currentTile; }

   public string patternsPath, tilesPath;

   public Button saveButton;
   public Toggle bossRoomToggle,
                 northToggle,
                 westToggle,
                 eastToggle,
                 southToggle;
   public Dropdown tileSelector;

   public TileMapEditorComponent tileMapEditor;

   void Awake() => GenerateTiles(Resources.LoadAll<Texture>(tilesPath));

   void Start()
   {
      tileSelector.onValueChanged.AddListener(delegate { currentTile = tiles[tileSelector.value]; });
      tileSelector.onValueChanged.AddListener(delegate { Debug.Log(CurrentTile.Value); });
      tileSelector.AddOptions(tiles.Select(tile => tile.Name).ToList());

      saveButton.onClick.AddListener(Save);
   }

   void GenerateTiles(Texture[] sprites)
   {
      tiles = new Tile[sprites.Length];

      for (int i = 0; i < tiles.Length; ++i)
         tiles[i] = new Tile(i, sprites[i]);
   }

   void Save()
   {
      using (var sw = new StreamWriter(SerializePath()))
      {
         sw.Write(tileMapEditor.SerializeLayout());
         sw.Flush();
      }
   }

   string SerializePath()
   {
      var path = patternsPath;
      if (bossRoomToggle.isOn)
         path = Path.Combine(path, "BossRooms");
      return Path.Combine(path, Pattern.ToString(northToggle.isOn, westToggle.isOn, eastToggle.isOn, southToggle.isOn));
   }
}
