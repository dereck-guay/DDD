using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct SpaceState
{
   int[] shapeNWES; //Stands for North, West, East, South
   public bool IsBossRoomSpace { get; private set; }

   public int this[int x]
   {
      get
      {
         if (x < 0 || x >= 4)
            new IndexOutOfRangeException("The index for Shape must be between 0 and 3.");

         return shapeNWES[x];
      }
      set
      {
         if (value >= -1 && value <= 1)
            shapeNWES[x] = value;
      }
   }

   public SpaceState(int[] shapeI, bool isBossRoomSpaceI)
   {
      IsBossRoomSpace = isBossRoomSpaceI;
      shapeNWES = new int[4];

      for (byte b = 0; b < 4; b++)
         shapeNWES[b] = shapeI[b];
   }

   //"Opens" this space. Used when generating the boss room.
   public void Open() => shapeNWES = new int[] { 1, 1, 1, 1 };

   public override string ToString()
   {
      return shapeNWES[0].ToString() + shapeNWES[1] + shapeNWES[2] + shapeNWES[3];
   }
}
