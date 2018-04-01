using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExperimetnalPathFinder
{
    class Map
    {
        public enum Direction { North, South, East, West };
        public static int mapHeight;
        public static int mapWidth;

        public static int[,] baseMap;
        public static Road[,] roadMap;
        public static Street[,] streetMap;
        public static List<Road> RoadList = new List<Road>();

        public static void GenDefaultMap()
        {
            baseMap = new int[,]{
                { 0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,0 },
                { 1,0,0,0,0,1,0,0,1,0 },
                { 1,1,1,1,0,1,0,0,1,0 },
                { 0,1,0,1,1,1,0,0,1,0 },
                { 0,1,0,0,1,0,0,0,1,0 },
                { 0,1,1,1,1,1,1,1,1,0 },
                { 0,1,0,0,1,0,0,0,0,0 },
                { 0,1,1,1,1,0,0,0,0,0 },
                { 0,0,0,0,1,1,1,1,1,1 },
            };

            mapHeight = baseMap.GetLength(0);
            mapWidth = baseMap.GetLength(1);

        }
        public static void GenRoadMap()
        {
            roadMap = new Road[mapHeight, mapWidth];
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (baseMap[y, x] == 1)
                    {
                        Road newRoad = new Road(new Vector2(x, y));
                        roadMap[y, x] = newRoad;
                    }
                }
            }
        }
        public static void GenHorizontalStreets()
        {
            int lastHorizontalStreetNumber = 0;
            int lastHorizontalStreetAddress = 0;
            Street workingStreet = null;

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (roadMap[y, x] == null)
                    {
                        lastHorizontalStreetAddress = 0;
                        workingStreet = null;
                    }
                    else if (workingStreet != null)
                    {
                        workingStreet.roads.Add(roadMap[y, x]);
                        workingStreet.length++;
                        streetMap[y, x] = workingStreet;
                        roadMap[y, x].HStreet = workingStreet;
                        roadMap[y, x].StreetAddress = lastHorizontalStreetAddress++;
                    }
                    else if (x + 1 < mapWidth)
                    {
                        if (roadMap[y, x + 1] != null)
                        {
                            workingStreet = new Street();
                            workingStreet.roads = new List<Road>();
                            workingStreet.isHorizontal = true;
                            workingStreet.name = GetStreetName(true, lastHorizontalStreetNumber++);
                            workingStreet.roads.Add(roadMap[y, x]);
                            streetMap[y, x] = workingStreet;
                            roadMap[y, x].HStreet = workingStreet;
                            roadMap[y, x].StreetAddress = lastHorizontalStreetAddress++;
                        }
                    }
                }
            }
        }
        public static void GenVerticalStreets()
        {
            int lastVerticalStreetNumber = 0;
            int lastVerticalStreetAddress = 0;
            Street workingStreet = null;

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (roadMap[y, x] == null)
                    {
                        lastVerticalStreetAddress = 0;
                        workingStreet = null;
                    }
                    else if (workingStreet != null)
                    {
                        workingStreet.roads.Add(roadMap[y, x]);
                        workingStreet.length++;
                        streetMap[y, x] = workingStreet;
                        roadMap[y, x].VStreet = workingStreet;
                        roadMap[y, x].StreetAddress = lastVerticalStreetAddress++;
                    }
                    else if (y + 1 < mapHeight)
                    {
                        if (roadMap[y + 1, x] != null)
                        {
                            workingStreet = new Street();
                            workingStreet.roads = new List<Road>();
                            workingStreet.isHorizontal = false;
                            workingStreet.name = GetStreetName(false, lastVerticalStreetNumber++);
                            workingStreet.roads.Add(roadMap[y, x]);
                            streetMap[y, x] = workingStreet;
                            lastVerticalStreetNumber++;
                            roadMap[y, x].VStreet = workingStreet;
                            roadMap[y, x].StreetAddress = lastVerticalStreetAddress++;
                        }
                    }
                }
            }
        }
        public static void MarkAllIntersections()
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (roadMap[y, x] != null)
                    {
                        if (roadMap[y, x].HStreet != null && roadMap[y, x].VStreet != null)
                        {
                            roadMap[y, x].IsIntersection = true; ;
                            roadMap[y, x].StreetAddress = -1;
                        }
                    }
                }
            }
        }
        public static void BuildAllIntersections()
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (roadMap[y, x] != null)
                    {
                        roadMap[y, x].BuildIntersectionLinkLists();
                    }
                }
            }
        }    

        /*
        public static bool[] AdjacentRoadCheck(Road inputRoad)
        {
            bool[] returnChecks = new bool[4];
            if ((int)inputRoad.Loc.Y + 1 < mapHeight)
            {
                returnChecks[(int)Direction.North] = (roadMap[(int)inputRoad.Loc.Y + 1, (int)inputRoad.Loc.X] != null);
            }
            if ((int)inputRoad.Loc.Y - 1 >= 0)
            {
                returnChecks[(int)Direction.South] = (roadMap[(int)inputRoad.Loc.Y - 1, (int)inputRoad.Loc.X] != null);
            }
            if ((int)inputRoad.Loc.X + 1 < mapWidth)
            {
                returnChecks[(int)Direction.East] = (roadMap[(int)inputRoad.Loc.Y, (int)inputRoad.Loc.X + 1] != null);
            }
            if ((int)inputRoad.Loc.X - 1 >= 0)
            {
                returnChecks[(int)Direction.West] = (roadMap[(int)inputRoad.Loc.Y, (int)inputRoad.Loc.X - 1] != null);
            }
            return returnChecks;
        }
        */

        public static string GetStreetName(bool isHorizontal, int streetNumber)
        {
            string returnName = "";
            if (isHorizontal)
            {
                returnName = streetNumber.ToString();
            }
            else
            {
                switch (streetNumber)
                {
                    case 0:
                        returnName = "A";
                        break;
                    case 1:
                        returnName = "B";
                        break;
                    case 2:
                        returnName = "C";
                        break;
                    case 3:
                        returnName = "D";
                        break;
                    case 4:
                        returnName = "E";
                        break;
                    case 5:
                        returnName = "F";
                        break;
                    case 6:
                        returnName = "G";
                        break;
                    case 7:
                        returnName = "H";
                        break;
                    case 8:
                        returnName = "I";
                        break;
                    case 9:
                        returnName = "J";
                        break;
                    case 10:
                        returnName = "K";
                        break;
                    case 11:
                        returnName = "L";
                        break;
                    case 12:
                        returnName = "M";
                        break;
                    case 13:
                        returnName = "N";
                        break;
                    case 14:
                        returnName = "O";
                        break;
                    case 15:
                        returnName = "P";
                        break;
                    case 16:
                        returnName = "Q";
                        break;
                    case 17:
                        returnName = "R";
                        break;
                    case 18:
                        returnName = "S";
                        break;
                    case 19:
                        returnName = "T";
                        break;
                    case 20:
                        returnName = "U";
                        break;
                    case 21:
                        returnName = "V";
                        break;
                    case 22:
                        returnName = "W";
                        break;
                    case 23:
                        returnName = "X";
                        break;
                    case 24:
                        returnName = "Y";
                        break;
                    case 26:
                        returnName = "Z";
                        break;
                    default:
                        returnName = "Vertical - " + streetNumber;
                        break;
                }
            }
            return returnName;
        }

        public static void PrintBaseMap()
        {
            System.Console.WriteLine("Base Map:");
            string closingLines = "--";
            for (int x = 0; x < baseMap.GetLength(1); x++)
            {
                closingLines += "---";
            }
            System.Console.WriteLine(closingLines);
            string outputString = "|";
            for (int y = 0; y < baseMap.GetLength(0); y++)
            {
                for (int x = 0; x < baseMap.GetLength(1); x++)
                {
                    if (baseMap[y, x] != 0)
                    {
                        outputString += " " + baseMap[y, x] + " ";
                    }
                    else
                    {
                        outputString += "   ";
                    }
                }
                System.Console.WriteLine(outputString += "|");
                outputString = "|";
            }
            System.Console.WriteLine(closingLines);
        }
        public static void PrintStreetAndIntersectionMap()
        {
            System.Console.WriteLine("Street and Intersection:");
            string closingLines = "--";
            for (int x = 0; x < mapWidth; x++)
            {
                closingLines += "---";
            }
            System.Console.WriteLine(closingLines);
            string outputString = "|";
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if(roadMap[y,x] != null)
                    {
                        if(roadMap[y, x].IsIntersection)
                        {
                            outputString += " % ";
                        }
                        else if (streetMap[y, x] != null)
                        {
                            outputString += " " + streetMap[y, x].name + " ";
                        }
                    }
                    else
                    {
                        outputString += "   ";
                    }
                }
                System.Console.WriteLine(outputString += "|");
                outputString = "|";
            }
            System.Console.WriteLine(closingLines);
        }
        public static void PrintPath(Path pathToPrint)
        {
            System.Console.WriteLine("Path:");
            string closingLines = "--";
            for (int x = 0; x < mapWidth; x++)
            {
                closingLines += "---";
            }
            System.Console.WriteLine(closingLines);
            string outputString = "|";
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (roadMap[y, x] != null)
                    {
                        bool isWaypoint = false;
                        foreach (Road waypoint in pathToPrint.waypoints) {
                            if (roadMap[y, x] == waypoint)
                            {
                                isWaypoint = true;
                            }
                        }
                        if (isWaypoint)
                        {
                            outputString += " X ";
                        }
                        else
                        {
                            outputString += " - ";
                        }
                    }
                    else
                    {
                        outputString += "   ";
                    }
                }
                System.Console.WriteLine(outputString += "|");
                outputString = "|";
            }
            System.Console.WriteLine(closingLines);
        }

        public static void TestPathFinding()
        {
            System.Console.WriteLine("Testing Pathfinding");
            /*
            Road source = roadMap[1, 0];
            Road dest = roadMap[9, 9];
            */

            Road source = roadMap[1, 0];
            Road dest = roadMap[5, 4];
            Path newPath = Path.FindPath(source, dest);
            System.Console.WriteLine("Pathfinding Complete");
            PrintPath(newPath);

        }
    }
}
