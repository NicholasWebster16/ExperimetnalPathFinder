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



        class PathOpen : Path
        {
            int distActualWorking;
            int distEstimateWorking;
            Map.Direction lastDirection;
            Waypoint lastWaypoint;
            public int GetDistWorkingTotal()
            {
                return distActualWorking + distEstimateWorking;
            }
        }
    }
}
