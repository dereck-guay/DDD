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
   public string Name { get => Sprite.name; }
   public Sprite Sprite { get; }

   public Tile(int valueI, Sprite spriteI)
   {
      Value = valueI;
      Sprite = spriteI;
   }
}

public class RoomEditorComponent : MonoBehaviour
{
   Tile[] tiles;
   public Tile CurrentTile { get; private set; }

   public string patternsPath, tilesPath;

   public Button saveButton, clearButton;
   public Toggle bossRoomToggle,
                 northToggle,
                 westToggle,
                 eastToggle,
                 southToggle;
   public Dropdown tileSelector;

   public TileMapEditorComponent tileMapEditor;

   void Awake()
   {
      GenerateTiles(Resources.LoadAll<Sprite>(tilesPath));
      CurrentTile = tiles[0];
   }

   void Start()
   {
      tileSelector.onValueChanged.AddListener(delegate { CurrentTile = tiles[tileSelector.value]; });
      tileSelector.AddOptions(tiles.Select(tile => tile.Name).ToList());

      saveButton.onClick.AddListener(Save);
      clearButton.onClick.AddListener(Clear);

      Clear();
   }

   void GenerateTiles(Sprite[] sprites)
   {
      tiles = new Tile[sprites.Length];

      for (int i = 0; i < tiles.Length; ++i)
         tiles[i] = new Tile(i, sprites[i]);
   }

   void Save()
   {
      var folder = SerializePath();
      var fileName = Directory.GetFiles(folder).Where(s => s[s.Length - 1] != 'a').ToArray().Length.ToString();

      using (var sw = new StreamWriter(Path.Combine(folder, fileName)))
      {
         sw.Write(tileMapEditor.SerializeLayout());
         sw.Flush();
      }
   }

   void Clear()
   {
      tileMapEditor.Fill(tiles[0], false);
      if (!bossRoomToggle.isOn)
         tileMapEditor.Fill(tiles[1], true);
   }

   string SerializePath()
   {
      var path = patternsPath;
      if (bossRoomToggle.isOn)
         path = Path.Combine(path, "BossRooms");
      return Path.Combine(path, Pattern.ToString(northToggle.isOn, westToggle.isOn, eastToggle.isOn, southToggle.isOn));
   }
}
