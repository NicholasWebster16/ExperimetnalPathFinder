using System.Collections.Generic;

namespace ExperimetnalPathFinder
{
    class Intersection
    {
        public Road road;
        public List<KeyValuePair<Intersection, int>> linkedNorthIntersectinos;
        public List<KeyValuePair<Intersection, int>> linkedSouthIntersectinos;
        public List<KeyValuePair<Intersection, int>> linkedEastIntersectinos;
        public List<KeyValuePair<Intersection, int>> linkedWestIntersectinos;
        public Street horizontalStreet;
        public Street verticalStreet;
        public Intersection()
        {
            linkedNorthIntersectinos = new List<KeyValuePair<Intersection, int>>();
            linkedSouthIntersectinos = new List<KeyValuePair<Intersection, int>>();
            linkedEastIntersectinos = new List<KeyValuePair<Intersection, int>>();
            linkedWestIntersectinos = new List<KeyValuePair<Intersection, int>>();
        }
    }
}
