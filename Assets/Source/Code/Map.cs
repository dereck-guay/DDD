using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Map
{
   int maxRoomCount;
   public int MapSize { get; }
   public int MapMiddle { get => MapSize / 2; }

   readonly int[] Adjacents = { 0, -1, 1, 0 };

   int currentRoomCount;
   Room[,] rooms;
   SpaceState[,] spaceStates;
   List<Tuple<int, int>> undefinedRooms;

   public Room this[int x, int y] { get => rooms[x, y]; }
   void CreateEmptyMap()
   {
      for (int i = 0; i < MapSize; i++)
         for (int j = 0; j < MapSize; j++)
            //To prevent room generation outside of the 
            //2D array, a "wall" (0) is created on the map border.
            spaceStates[j, i] = new SpaceState(new int[] {
                  i == 0 ? 0 : -1,
                  j == 0 ? 0 : -1,
                  j == MapSize - 1 ? 0 : -1,
                  i == MapSize - 1 ? 0 : -1
               }, IsBossRoomSpace(j, i));
   }
   //The boss room is a 3x3 space and is
   //ALWAYS in the middle of the map.
   bool IsBossRoomSpace(int x, int y) =>
      CoordsInBetween(x, y, MapMiddle - 1, MapMiddle + 1);
   void GenerateBossRoom()
   {
      int x, y;

      //The boss room is "opened" to create a large area.
      spaceStates[MapMiddle, MapMiddle].Open();

      //Same thing for the adjacent rooms.
      for (byte b = 0; b < 4; b++)
      {                                            //  11      
         x = MapMiddle + Adjacents[b];             //220033    x: 0 -1 1 0
         y = MapMiddle + Adjacents[(b + 2) % 4];   //  44      y: 1 0 0 -1

         spaceStates[x, y].Open();
      }

      //Sets the first undefined room in the middle of the map.
      //The other rooms will be generated from that point.
      undefinedRooms.Add(new Tuple<int, int>(MapMiddle, MapMiddle));
   }
   void GenerateMap()
   {
      Tuple<int, int> pos;

      while (undefinedRooms.Count > 0)
      {
         pos = undefinedRooms.ElementAt(0);

         rooms[pos.Item1, pos.Item2] =
            new Room(spaceStates[pos.Item1, pos.Item2], SizeReached());

         currentRoomCount++;
         UpdateAdjacentSpaces(pos);

         undefinedRooms.RemoveAt(0);
      }
   }
   void TryGenerateMap()
   {
      currentRoomCount = 0;

      rooms = new Room[MapSize, MapSize];
      spaceStates = new SpaceState[MapSize, MapSize];
      undefinedRooms = new List<Tuple<int, int>>(4);

      CreateEmptyMap();
      GenerateBossRoom();
      GenerateMap();

      Debug.Log(currentRoomCount);
   }
   public Map(int mapScale)
   {
      if (mapScale < 0)
         mapScale = 0;

      //mapSize includes a 3x3 area for the boss room.
      MapSize = 2 * mapScale + 3;

      maxRoomCount = MapSize * MapSize / 2;  //Might have to change... (Game design)
      
      while (!SizeReached())
         TryGenerateMap();
   }
   void UpdateAdjacentSpaces(Tuple<int, int> currentPosition)
   {
      int x, y, doorState;
      int xInit = currentPosition.Item1;
      int yInit = currentPosition.Item2;
      var currentRoom = rooms[xInit, yInit];
      Tuple<int, int> newRoomPosition;

      for (byte b = 0; b < 4; b++)             // x >
      {                                        //y   00      
         x = xInit + Adjacents[b];             //v 11##22    x: 0 -1 1 0
         y = yInit - Adjacents[(b + 2) % 4];   //    33      y: -1 0 0 1

         if (CoordsInBetween(x, y, 0, MapSize - 1))
         {
            doorState = int.Parse(currentRoom.Pattern.ToString()[b].ToString());  //Not very clean...
            spaceStates[x, y][3 - b] = doorState;

            if (doorState == 1)
            {
               newRoomPosition = new Tuple<int, int>(x, y);

               if (!undefinedRooms.Contains(newRoomPosition) &&
               rooms[newRoomPosition.Item1, newRoomPosition.Item2] == null)
                  undefinedRooms.Add(new Tuple<int, int>(x, y));
            }
         }
      }
   }
   bool SizeReached() => currentRoomCount >= maxRoomCount;
   bool CoordsInBetween(int x, int y, int minValue, int maxValue) =>
      x <= maxValue && y <= maxValue && x >= minValue && y >= minValue;
   public string ToString(int x, int y) => rooms[x, y].ToString();
   public bool HasRoom(int x, int y) => rooms[x, y] != null;
}
