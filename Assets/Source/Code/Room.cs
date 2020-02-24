using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//Room is a 17x11 space (including walls)
public class Room
{
   int[,] roomLayout;
   Transform map;

   public const int Width = 17,
                    Length = 11;
   const int TileNumber = Width * Length;

   const string PatternsPath = "Assets\\Patterns",
                 BossPath = "BossRooms";

   public Pattern Pattern { get; }

   public int this[int x, int y] { get => roomLayout[x, y]; }

   public Room(SpaceState spaceShape, bool noNewDoors)
   {
      Pattern = new Pattern(spaceShape, noNewDoors);
      GenerateRoomLayout(spaceShape.IsBossRoomSpace);
   }

   void GenerateRoomLayout(bool isBossRoom)
   {
      roomLayout = new int[Width, Length];

      string fileData;
      using (var sr = new StreamReader(RandomFile(isBossRoom)))
         fileData = sr.ReadToEnd();

      var roomData = fileData.Replace("\r\n", "").TrimEnd('\t').Split('\t');

      for (byte b = 0; b < TileNumber; b++)
         roomLayout[b % Width, b / Width] = int.Parse(roomData[b]);
   }

   string RandomFile(bool isBossRoom) //Pattern must be generated first
   {
      string path1 = PatternsPath;

      if (isBossRoom)
         path1 = Path.Combine(path1, BossPath);

      path1 = Path.Combine(path1, Pattern.ToString());

      int fileCount = Directory.GetFiles(path1).Where(s => s[s.Length - 1] != 'a').ToArray().Length;

      string path2 = Random.Range(0, fileCount).ToString();

      return Path.Combine(path1, path2);
   }

   public override string ToString()
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