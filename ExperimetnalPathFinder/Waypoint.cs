using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimetnalPathFinder
{
    class Waypoint
    {
        private Road road;
        private Waypoint previousWaypoint;
        private Waypoint nextWaypoint;

        public Waypoint(Road _road)
        {
            road = _road;
        }

        public Road Road { get => road; set => road = value; }
        public Waypoint PreviousWaypoint { get => previousWaypoint; set => previousWaypoint = value; }
        public Waypoint NextWaypoint { get => nextWaypoint; set => nextWaypoint = value; }
    }
}
