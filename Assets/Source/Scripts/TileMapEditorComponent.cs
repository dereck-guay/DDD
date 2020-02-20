using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TileMapEditorComponent : MonoBehaviour
{
   public const int Width = Room.Width;
   public const int Length = Room.Length;

   float tileHeight, tileWidth;

   int[,] roomLayout = new int[Width, Length];

   public GameObject buttonPrefab;
   public RoomEditorComponent roomEditor;
   Button[] tileButtons;
   
   void Start()
   {
      tileHeight = GetComponent<RectTransform>().rect.height / Length;
      tileWidth = GetComponent<RectTransform>().rect.width / Width;

      InstantiateButtons();
      ArrangeTiles(GetComponentsInChildren<Button>());
   }

   void InstantiateButtons()
   {
      for (byte i = 0; i < Length; i++)
         for (byte j = 0; j < Width; j++)
         {
            var newTile = Instantiate(buttonPrefab);
            newTile.transform.SetParent(transform);
            newTile.transform.localScale = new Vector3(tileWidth, tileHeight);
            var i1 = i;
            var j1 = j;

            var button = newTile.GetComponent<Button>();

            button.onClick.AddListener(delegate { roomLayout[j1, i1] = roomEditor.CurrentTile.Value; });
            button.onClick.AddListener(delegate { Debug.Log("I got clicked! "); });
         }
   }

   void ArrangeTiles(Button[] tiles)
   {

      for (int i = 0; i < Length; ++i)
         for (int j = 0; j < Width; ++j)
            tiles[i * Width + j].GetComponent<RectTransform>().localPosition =
               new Vector3(j * tileWidth, -i * tileHeight);
   }

   public string SerializeLayout()
   {
      StringBuilder sb = new StringBuilder();

      for (byte b = 0; b < Length; ++b)
      {
         for (byte c = 0; c < Width; ++c)
            sb.Append(roomLayout[c, b]);
         sb.AppendLine();
      }
      return sb.ToString();
   }
}
