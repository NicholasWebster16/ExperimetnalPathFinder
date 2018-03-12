using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExperimetnalPathFinder
{
    class Path
    {
        public Road source;
        public Road destination;
        public int distanceCurrentActual;
        public int distanceRemainingEstimate;
        public List<Waypoint> waypoints;

        public static Path CloneAndExtendPath(Path pathToClone, Waypoint nextWaypoint)
        {
            Path newPath = new Path();
            newPath.source = pathToClone.source;
            newPath.destination = pathToClone.destination;
            newPath.waypoints = pathToClone.waypoints;
            newPath.waypoints.Add(nextWaypoint);
            newPath.distanceCurrentActual = pathToClone.distanceCurrentActual + nextWaypoint.distanceFromLastWaypoint;
            newPath.distanceRemainingEstimate = EstimateDistance(nextWaypoint.waypointRoad, newPath.destination);

            return newPath;
        }

        public static int EstimateDistance(Road road0, Road road1)
        {
            int distX = (int)Math.Abs(road1.loc.X - road0.loc.X);
            int distY = (int)Math.Abs(road1.loc.Y - road0.loc.Y);
            return distX + distY;
        }



        public static Path FindPath(Road source, Road destination)
        {
            List<Path> openPaths = new List<Path>();



            Path finalPath;
        }

        public static List<Path> BuildNextPathLayer(Path inputPath)
        {
            Waypoint lastWaypoint = inputPath.waypoints.Last();


        }

        public static bool IsSameStreet(Road road0, Road road1)
        {
            if (road0.horizontalStreet != null && road1.horizontalStreet != null)
            {
                if(road0.horizontalStreet == road1.horizontalStreet) { return true; }
                else{ return false; }
            }
            if (road0.verticalStreet != null && road1.verticalStreet != null)
            {
                if(road0.verticalStreet == road1.verticalStreet) { return true; }
                else { return false; }
            }
            else { return false; }
        }



    }
}
