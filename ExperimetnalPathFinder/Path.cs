using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimetnalPathFinder
{
    class Path
    {
        List<Waypoint> waypoints;
        Road source;
        Road dest;
        int distActual;

        public class PathOpen : Path, ICloneable
        {
            public int distActualWorking;
            public int distEstimateRemaining;
            public Map.Direction lastDirection;
            public int GetDistWorkingTotal()
            {
                return distActualWorking + distEstimateRemaining;
            }


            public PathOpen(Road _source, Road _dest)
            {
                source = _source;
                dest = _dest;
                distActualWorking = 0;
                distEstimateRemaining = EstimateDistance(source, dest);
                Waypoint initalWaypoint = new Waypoint(source);
                waypoints = new List<Waypoint>();
                waypoints.Add(initalWaypoint);
            }

            public object Clone()
            {
                PathOpen clone = (PathOpen)this.MemberwiseClone();
                Waypoint[] clonedWaypoints = new Waypoint[this.waypoints.Count];
                this.waypoints.CopyTo(clonedWaypoints);
                clone.waypoints = clonedWaypoints.OfType<Waypoint>().ToList();
                return clone;

            }
        }

        public static Path FindPath(Road source, Road dest)
        {
            List<PathOpen> openPaths = new List<PathOpen>();
            int shortestDistFound = Map.mapHeight * Map.mapWidth;
            Path shortestPathFound = new Path();
            PathOpen initialPath = new PathOpen(source, dest);
            openPaths.Add(initialPath);
            
            while (openPaths.Count > 0)
            {
                PathOpen workingPath = openPaths[0];
                openPaths.RemoveAt(0);
                // Checks if the working path is on the same street as the destination.
                if (AreSameStreet(workingPath.waypoints.Last().Road, dest))
                {
                    // If it's on the same street finalize the working path
                    Path finalizedPath = FinalizePathOpen(workingPath);
                    // Check if the newly finialized path is shorter than the shortest found.
                    if (finalizedPath.distActual < shortestDistFound)
                    {
                        // Then Update the shortest path.
                        shortestPathFound = finalizedPath;
                        shortestDistFound = finalizedPath.distActual;
                        for (int i = 0; i < openPaths.Count; i++)
                        {
                            // Cull openPaths
                            if (openPaths[i].GetDistWorkingTotal() > shortestDistFound)
                            {
                                openPaths.Remove(openPaths[i]);
                            }
                        }
                    }
                }
                else
                {
                    List<PathOpen> branchedPath = BranchPath(workingPath, shortestDistFound);
                    openPaths.AddRange(branchedPath);
                    openPaths.OrderBy(x => x.GetDistWorkingTotal()).ToList();
                }
            }
            return shortestPathFound;
        }

        public static Path FinalizePathOpen(PathOpen pathOpen)
        {
            Path finalizedPath = new Path();
            finalizedPath.waypoints = pathOpen.waypoints;
            int finalDist = pathOpen.GetDistWorkingTotal();
            finalizedPath.distActual = finalDist;
            Waypoint newWaypoint = new Waypoint(pathOpen.dest);
            pathOpen.waypoints.Last().NextWaypoint = newWaypoint;
            pathOpen.waypoints.Add(newWaypoint);
            return pathOpen;
        }

        public static List<PathOpen> BranchPath(PathOpen root, int maxDist)
        {
            List<PathOpen> pathBranches = new List<PathOpen>();
            Map.Direction[] directions = (Map.Direction[])Enum.GetValues(typeof(Map.Direction));

            foreach(Map.Direction dir in directions)
            {
                if (root.waypoints.Last().Road.GetIntersectionLink(dir) != null)
                {
                    PathOpen newPath = (PathOpen)root.Clone();
                    int addDist = root.waypoints.Last().Road.GetLinkDistance(dir);
                    newPath.distActual += addDist;
                    Waypoint newWaypoint = new Waypoint(root.waypoints.Last().Road.GetIntersectionLink(dir));
                    newPath.waypoints.Last().NextWaypoint = newWaypoint;
                    newWaypoint.PreviousWaypoint = root.waypoints.Last();
                    newPath.waypoints.Add(newWaypoint);
                    newPath.distEstimateRemaining = EstimateDistance(newWaypoint.Road, newPath.dest);
                    pathBranches.Add(newPath);
                }
            }
            return pathBranches;
            
        }
        public static bool AreSameStreet(Road r1, Road r2)
        {
            if (r1.HStreet == r2.HStreet || r1.VStreet == r2.VStreet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int EstimateDistance(Road r1, Road r2)
        {
            return (int)Math.Abs(r2.Loc.X - r1.Loc.X) + (int)Math.Abs(r2.Loc.Y - r1.Loc.Y);
        }

        public static void TestCloning()
        {
            Road testSource = new Road();
            testSource.StreetAddress = 42;
            Road testDest = new Road();
            testDest.StreetAddress = 100;
            Road testRoad = new Road();
            testRoad.StreetAddress = 66;
            Waypoint testWaypoint1 = new Waypoint(testSource);
            Waypoint testWaypoint2 = new Waypoint(testDest);
            Waypoint testWaypoint3 = new Waypoint(testRoad);



            PathOpen originalPath = new PathOpen(testSource, testDest);
            originalPath.lastDirection = Map.Direction.North;
            originalPath.waypoints.Add(testWaypoint1);

            PathOpen clonedPath = (PathOpen)originalPath.Clone() ;
            PathOpen clonedPath2 = (PathOpen)originalPath.Clone();
            clonedPath.waypoints.Add(testWaypoint2);
            clonedPath.dest = testRoad;
            clonedPath.distActual = 6000;
            clonedPath.distEstimateRemaining = 2000;
            clonedPath.source = testRoad;
            
        }
    }
}
