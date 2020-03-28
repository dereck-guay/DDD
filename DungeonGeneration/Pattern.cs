using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGeneration
{
   struct Pattern
   {
      bool north, west, east, south;

      public Pattern(SpaceState spaceShape, bool noNewDoors)
      {
         //Random() should be initiated elsewhere
         var r = new Random();

         var pattern = new int[4];

         for (byte b = 0; b < 4; ++b)
         {
            if (!noNewDoors)
            {
               if (spaceShape[b] == -1)
                  pattern[b] = r.Next(2); //Assigns either 0 or 1;
               else
                  pattern[b] = spaceShape[b];
            }
            else
            {
               if (spaceShape[b] == -1)
                  pattern[b] = 0;
               else
                  pattern[b] = spaceShape[b];
            }
         }

         north = pattern[0] == 1;
         west = pattern[1] == 1;
         east = pattern[2] == 1;
         south = pattern[3] == 1;
      }

      //turns the 4 bools in a 4 digits-long string
      public override string ToString()
      {
         return (north ? "1" : "0") + (west ? "1" : "0") + (east ? "1" : "0") + (south ? "1" : "0");
      }
   }
}
