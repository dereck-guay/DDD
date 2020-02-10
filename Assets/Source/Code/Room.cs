using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonGeneration
{
   //Room is a 11x9 space (including walls)
   class Room
   {
      int[,] roomLayout;

      const int Width = 11,
                Length = 9,
                TileNumber = Width * Length;

      const string PatternsPath = "..\\..\\patterns",
                   BossPath = "boss_rooms";

      public Pattern Pattern { get; }

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

         var roomData = fileData.Replace('\r', ' ').Split(new char[] { '\t', '\n' });

         for (byte b = 0; b < TileNumber; b++)
            roomLayout[b % Width, b / Width] = int.Parse(roomData[b]);
      }

      string RandomFile(bool isBossRoom) //Pattern must be generated first
      {
         string path1 = PatternsPath;

         if (isBossRoom)
            path1 = Path.Combine(path1, BossPath);

         path1 = Path.Combine(path1, Pattern.ToString());

         int fileCount = Directory.GetFiles(path1).Length;

         string path2 = Random.Range(0, fileCount).ToString();

         return Path.Combine(path1, path2);
      }

      public void GenerateRoom()
      {
         //Initializes the required gameObjects in Unity
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
}