﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct Pattern
{
   bool north, west, east, south;

   public Pattern(SpaceState spaceShape, bool noNewDoors)
   {
      var pattern = new int[4];

      for (byte b = 0; b < 4; ++b)
      {
         if (!noNewDoors)
         {
            if (spaceShape[b] == -1)
               pattern[b] = Random.Range(0, 2); //Assigns either 0 or 1;
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
   public override string ToString() => ToString(north, west, east, south);

   public static string ToString(bool north, bool west, bool east, bool south)
   {
      return (north ? "1" : "0") + (west ? "1" : "0") + (east ? "1" : "0") + (south ? "1" : "0");
   }
}
