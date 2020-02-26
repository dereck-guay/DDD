using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGeneration
{
   class Program
   {
      static void Main(string[] args)
      {
         

         int mapSize = 9;

         var map = new Map(mapSize);
         int x, y;

         while (true)
         {
            Console.Write($"x [0,{mapSize - 1}]: ");
            x = int.Parse(Console.ReadLine());
            Console.Write($"y [0,{mapSize - 1}]: ");
            y = int.Parse(Console.ReadLine());

            if (x >= 0 && x < mapSize && y >= 0 && y < mapSize)
            {
               if (map.HasRoom(x, y))
                  Console.WriteLine(map.ToString(x, y));
               else
                  Console.WriteLine("This space is empty.");
            }
            else
               Console.WriteLine("Index out of range.");

            Console.WriteLine();
         }
      }
   }
}
