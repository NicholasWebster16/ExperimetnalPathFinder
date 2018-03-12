using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimetnalPathFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Map.GenDefaultMap();
            Map.GenRoadMap();
            Map.streetMap = new Street[Map.mapHeight, Map.mapWidth];
            Map.GenHorizontalStreets();
            Map.GenVerticalStreets();
            Map.MarkAllIntersections();
            Map.BuildAllIntersections();
            Map.PrintBaseMap();
            System.Console.WriteLine();
            Map.PrintStreetAndIntersectionMap();
            System.Console.Read();
        }
    }

}
