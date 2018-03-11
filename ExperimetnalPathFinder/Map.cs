using System;
using System.Collections.Generic;
using System.Linq;
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
        public static Intersection[,] intersectionMap;
        public static List<Intersection> intersectionList = new List<Intersection>();

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
                        roadMap[y, x].horizontalStreet = workingStreet;
                        roadMap[y, x].streetAddress = lastHorizontalStreetAddress++;
                    }
                    else if (x + 1 < mapWidth)
                    {
                        if (roadMap[y, x + 1] != null)
                        {
                            workingStreet = new Street();
                            workingStreet.roads = new List<Road>();
                            workingStreet.intersections = new List<Intersection>();
                            workingStreet.isHorizontal = true;
                            workingStreet.name = GetStreetName(true, lastHorizontalStreetNumber++);
                            workingStreet.roads.Add(roadMap[y, x]);
                            streetMap[y, x] = workingStreet;
                            roadMap[y, x].horizontalStreet = workingStreet;
                            roadMap[y, x].streetAddress = lastHorizontalStreetAddress++;
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
                        roadMap[y, x].verticalStreet = workingStreet;
                        roadMap[y, x].streetAddress = lastVerticalStreetAddress++;
                    }
                    else if (y + 1 < mapHeight)
                    {
                        if (roadMap[y + 1, x] != null)
                        {
                            workingStreet = new Street();
                            workingStreet.intersections = new List<Intersection>();
                            workingStreet.roads = new List<Road>();
                            workingStreet.isHorizontal = false;
                            workingStreet.name = GetStreetName(false, lastVerticalStreetNumber++);
                            workingStreet.roads.Add(roadMap[y, x]);
                            streetMap[y, x] = workingStreet;
                            lastVerticalStreetNumber++;
                            roadMap[y, x].verticalStreet = workingStreet;
                            roadMap[y, x].streetAddress = lastVerticalStreetAddress++;
                        }
                    }
                }
            }
        }
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


        public static void GenIntersectionMap()
        {
            intersectionMap = new Intersection[mapHeight, mapWidth];
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (roadMap[y, x] != null)
                    {
                        if (roadMap[y, x].horizontalStreet != null && roadMap[y, x].verticalStreet != null)
                        {
                            Intersection newIntersection = new Intersection();
                            newIntersection.road = roadMap[y, x];
                            newIntersection.horizontalStreet = roadMap[y, x].horizontalStreet;
                            newIntersection.verticalStreet = roadMap[y, x].verticalStreet;
                            intersectionMap[y, x] = newIntersection;
                            intersectionList.Add(newIntersection);
                            streetMap[y, x] = null;
                        }
                    }
                }
            }
        }
        public static void LinkIntersections()
        {
            foreach(Intersection intersection in intersectionList)
            {
                intersection.linkedNorthIntersectinos = BuildIntersectionLinkList(intersection, Direction.North);
                intersection.linkedSouthIntersectinos = BuildIntersectionLinkList(intersection, Direction.South);
                intersection.linkedEastIntersectinos = BuildIntersectionLinkList(intersection, Direction.East);
                intersection.linkedWestIntersectinos = BuildIntersectionLinkList(intersection, Direction.West);
            }
        }
        public static List<KeyValuePair<Intersection, int>> BuildIntersectionLinkList(Intersection source, Direction direction)
        {

            int sourceX = source.road.locX;
            int sourceY = source.road.locY;
            int checkX = sourceX;
            int checkY = sourceY;
            List<KeyValuePair<Intersection, int>> returnList = new List<KeyValuePair<Intersection, int>>();
            switch (direction)
            {
                case Direction.West:
                    checkX = sourceX - 1;
                    while (checkX >= 0)
                    {
                        if(intersectionMap[checkY, checkX] != null)
                        {
                            returnList.Add(new KeyValuePair<Intersection, int>(intersectionMap[checkY, checkX], Math.Abs(checkX - sourceX)));
                        }
                        checkX--;
                    }
                    break;
                case Direction.East:
                    checkX = sourceX + 1;
                    while (checkX < mapWidth)
                    {
                        if (intersectionMap[checkY, checkX] != null)
                        {
                            returnList.Add(new KeyValuePair<Intersection, int>(intersectionMap[checkY, checkX], Math.Abs(checkX - sourceX)));
                        }
                        checkX++;
                    }
                    break;
                case Direction.North:
                    checkY = sourceY + 1;
                    while (checkY < mapHeight)
                    {
                        if (intersectionMap[checkY, checkX] != null)
                        {
                            returnList.Add(new KeyValuePair<Intersection, int>(intersectionMap[checkY, checkX], Math.Abs(checkY - sourceY)));
                        }
                        checkY++;
                    }
                    break;
                case Direction.South:
                    checkY = sourceY - 1;
                    while (checkY >= 0)
                    {
                        if (intersectionMap[checkY, checkX] != null)
                        {
                            returnList.Add(new KeyValuePair<Intersection, int>(intersectionMap[checkY, checkX], Math.Abs(checkY - sourceY)));
                        }
                        checkY--;
                    }
                    break;
            }
            return returnList;
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
                        Road newRoad = new Road();
                        newRoad.locX = x;
                        newRoad.locY = y;
                        roadMap[y, x] = newRoad;
                    }
                }
            }
        }


        public static bool[] AdjacentRoadCheck(Road inputRoad)
        {
            bool[] returnChecks = new bool[4];
            if (inputRoad.locY + 1 < mapHeight)
            {
                returnChecks[(int)Direction.North] = (roadMap[inputRoad.locY + 1, inputRoad.locX] != null);
            }
            if (inputRoad.locY - 1 >= 0)
            {
                returnChecks[(int)Direction.South] = (roadMap[inputRoad.locY - 1, inputRoad.locX] != null);
            }
            if (inputRoad.locX + 1 < mapWidth)
            {
                returnChecks[(int)Direction.East] = (roadMap[inputRoad.locY, inputRoad.locX + 1] != null);
            }
            if (inputRoad.locX - 1 >= 0)
            {
                returnChecks[(int)Direction.West] = (roadMap[inputRoad.locY, inputRoad.locX - 1] != null);
            }
            return returnChecks;
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
            System.Console.WriteLine("Street and Intersection Map:");
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
                    if (streetMap[y, x] != null)
                    {
                        outputString += " " + streetMap[y, x].name + " ";
                    }
                    else if (intersectionMap[y, x] != null)
                    {
                        outputString += " % ";
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
    }
}
