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
      tileButtons = new Button[Width * Length];

      for (byte i = 0; i < Length; i++)
         for (byte j = 0; j < Width; j++)
         {
            var newTile = Instantiate(buttonPrefab);
            newTile.transform.SetParent(transform);
            newTile.transform.localScale = new Vector3(tileWidth, tileHeight);

            var tileImage = newTile.GetComponent<Image>();
            tileImage.overrideSprite = roomEditor.CurrentTile.Sprite;

            var i1 = i;
            var j1 = j;

            var tileButton = newTile.GetComponent<Button>();

            tileButtons[i * Width + j] = tileButton;
            tileButton.onClick.AddListener(delegate { SetTile(j1, i1, roomEditor.CurrentTile, tileImage); });
         }
   }

   void ArrangeTiles(Button[] tiles)
   {
      for (int i = 0; i < Length; ++i)
         for (int j = 0; j < Width; ++j)
            tiles[i * Width + j].GetComponent<RectTransform>().localPosition =
               new Vector3(j * tileWidth, -i * tileHeight);
   }

   public void Fill(Tile tileToFill, bool borderOnly)
   {
      for (int i = 0; i < tileButtons.Length; i++)
         if (borderOnly)
         {
            if (i % Width == 0 || i % Width == Width - 1 || i / Width == 0 || i / Width == Length - 1)
               SetTile(i % Width, i / Width, tileToFill, tileButtons[i].GetComponent<Image>());
         }            
         else
            SetTile(i % Width, i / Width, tileToFill, tileButtons[i].GetComponent<Image>());
   }

   void SetTile(int x, int y, Tile tileToSet, Image imageToModify)
   {
      roomLayout[x, y] = tileToSet.Value;
      imageToModify.overrideSprite = tileToSet.Sprite;
   }

   public string SerializeLayout()
   {
      StringBuilder sb = new StringBuilder();

      for (byte b = 0; b < Length; ++b)
      {
         for (byte c = 0; c < Width; ++c)
            sb.Append(roomLayout[c, b] + "\t");
         sb.AppendLine();
      }
      return sb.ToString();
   }
}
