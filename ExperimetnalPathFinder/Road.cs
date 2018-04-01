using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static ExperimetnalPathFinder.Map;

namespace ExperimetnalPathFinder
{
    class Road
    {
        private Street hStreet = null;
        private Street vStreet = null;
        private bool isIntersection = false;
        private int streetAddress = -1;
        private Vector2 loc;
        private KeyValuePair<Road, int>[] intersectionLinks;
        

        public Street HStreet { get => hStreet; set => hStreet = value; }
        public Street VStreet { get => vStreet; set => vStreet = value; }
        public int StreetAddress { get => streetAddress; set => streetAddress = value; }
        public Vector2 Loc { get => loc; set => loc = value; }
        public bool IsIntersection { get => isIntersection; set =>  isIntersection = value; }

        public Road() { }
        public Road(Vector2 _loc) { loc = _loc; }

        public Road GetIntersectionLink(Direction direction) { return intersectionLinks[(int)direction].Key; }
        public int GetLinkDistance(Direction direction) { return intersectionLinks[(int)direction].Value; }
        public void SetIntersectionLinks(KeyValuePair<Road, int>[] newLinks) { intersectionLinks = newLinks; }


        private KeyValuePair<Road, int> GetNearestIntersection(Direction direction)
        {
            int checkX = (int)loc.X;
            int checkY = (int)loc.Y;
            switch (direction)
            {
                case Direction.East:
                    checkX = (int)loc.X + 1;
                    while (checkX < mapWidth)
                    {
                        if (roadMap[checkY, checkX] != null)
                        {
                            if(roadMap[checkY, checkX].isIntersection)
                                {
                                    return new KeyValuePair<Road, int>(roadMap[checkY, checkX], Math.Abs(checkX - (int)loc.X));
                                }
                            }
                        checkX++;
                    }
                    return new KeyValuePair<Road, int>();

                case Direction.West:
                    checkX = (int)loc.X - 1;
                    while (checkX >= 0)
                    {
                        if (roadMap[checkY, checkX] != null)
                        {
                            if (roadMap[checkY, checkX].isIntersection)
                            {
                                return new KeyValuePair<Road, int>(roadMap[checkY, checkX], Math.Abs(checkX - (int)loc.X));
                            }
                        }
                        checkX--;
                    }
                    return new KeyValuePair<Road, int>();

                case Direction.North:
                    checkY = (int)loc.Y - 1;
                    while (checkY >= 0)
                    {
                        if (roadMap[checkY, checkX] != null)
                        {
                            if (roadMap[checkY, checkX].isIntersection)
                            {
                                return new KeyValuePair<Road, int>(roadMap[checkY, checkX], Math.Abs(checkY - (int)loc.Y));
                            }
                        }
                        checkY--;
                    }
                    return new KeyValuePair<Road, int>();

                case Direction.South:
                    checkY = (int)loc.Y + 1;
                    while (checkY < mapHeight)
                    {
                        if ( roadMap[checkY, checkX] != null)
                        {
                            if (roadMap[checkY, checkX].isIntersection)
                            {
                                return new KeyValuePair<Road, int>(roadMap[checkY, checkX], Math.Abs(checkY - (int)loc.Y));
                            }
                        }
                        checkY++;
                    }
                    return new KeyValuePair<Road, int>();
            }
            return new KeyValuePair<Road, int>();
        }
        public void BuildIntersectionLinkLists()
        {
            intersectionLinks = new KeyValuePair<Road, int>[4];

            KeyValuePair<Road, int> north = GetNearestIntersection(Direction.North);
            KeyValuePair<Road, int> south = GetNearestIntersection(Direction.South);
            KeyValuePair<Road, int> east = GetNearestIntersection(Direction.East);
            KeyValuePair<Road, int> west = GetNearestIntersection(Direction.West);

            if (north.Key != null) { intersectionLinks[(int)Direction.North] = north; }
            if (south.Key != null) { intersectionLinks[(int)Direction.South] = south; }
            if (east.Key != null) { intersectionLinks[(int)Direction.East] = east; }
            if (west.Key != null) { intersectionLinks[(int)Direction.West] = west; }

        }
    }
}
