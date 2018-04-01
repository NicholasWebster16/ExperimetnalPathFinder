using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimetnalPathFinder
{
    class Path
    {
        public List<Road> waypoints;
        public int distActual;

        public class PathOpen : Path, ICloneable
        {
            public Road destination;
            public int distActualWorking;
            public int GetDistWorkingTotal()
            {
                return distActualWorking + EstimateDistance(waypoints.Last(), destination);
            }

            public object Clone()
            {
                PathOpen newPath = (PathOpen)this.MemberwiseClone();
                newPath.waypoints = new List<Road>(this.waypoints);
                return newPath;
            }

            public PathOpen(Road source, Road dest)
            {
                waypoints = new List<Road>();
                destination = dest;
                distActualWorking = 0;
                waypoints.Add(source);
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
                if (AreSameStreet(workingPath.waypoints.Last(), dest))
                {
                    // If it's on the same street finalize the working path
                    Path finalizedPath = FinalizePathOpen(workingPath);
                    // Check if the newly finialized path is shorter than the shortest found.
                    if (finalizedPath.distActual < shortestDistFound)
                    {
                        // Then Update the shortest path.
                        shortestPathFound = finalizedPath;
                        shortestDistFound = finalizedPath.distActual;
                        openPaths = CullOpenPaths(openPaths, shortestDistFound);
                    }
                }
                else
                {
                    List<PathOpen> branchedPath = BranchPath(workingPath, shortestDistFound);
                    openPaths.AddRange(branchedPath);
                    openPaths = CullOpenPaths(openPaths, shortestDistFound);
                    openPaths.OrderBy(x => x.GetDistWorkingTotal()).ToList();
                }
            }
            return shortestPathFound;
        }

        public static List<PathOpen> CullOpenPaths(List<PathOpen> openPaths, int cutoff)
        {
            for (int i = 0; i < openPaths.Count; i++)
            {
                // Cull openPaths
                if (openPaths[i].GetDistWorkingTotal() >= cutoff)
                {
                    openPaths.Remove(openPaths[i]);
                }
            }
            return openPaths;
        }

        public static Path FinalizePathOpen(PathOpen pathOpen)
        {
            Path finalizedPath = new Path();
            finalizedPath.waypoints = pathOpen.waypoints;
            int finalDist = pathOpen.GetDistWorkingTotal();
            finalizedPath.distActual = finalDist;
            finalizedPath.waypoints.Add(pathOpen.destination);
            return finalizedPath;
        }

        public static List<PathOpen> BranchPath(PathOpen root, int maxDist)
        {
            List<PathOpen> pathBranches = new List<PathOpen>();
            Map.Direction[] directions = (Map.Direction[])Enum.GetValues(typeof(Map.Direction));

            foreach(Map.Direction dir in directions)
            {
                bool isBackwardMove;
                if(root.waypoints.Count - 2 >= 0)
                {
                    isBackwardMove = root.waypoints.Last().GetIntersectionLink(dir) == root.waypoints[root.waypoints.Count - 2];
                }
                else
                {
                    isBackwardMove = false;
                }
                if (root.waypoints.Last().GetIntersectionLink(dir) != null && !isBackwardMove)
                {
                    PathOpen newPath = (PathOpen)root.Clone();
                    int addDist = root.waypoints.Last().GetLinkDistance(dir);
                    newPath.distActualWorking += addDist;
                    newPath.waypoints.Add(root.waypoints.Last().GetIntersectionLink(dir));
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
    }
}
