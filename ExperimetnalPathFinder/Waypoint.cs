using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExperimetnalPathFinder
{
    class Waypoint
    {
        private Road road;
        private Waypoint prevWaypoint;
        private Waypoint nextWaypoint;
        public Road Road { get => road; set => road = value; }
        internal Waypoint PrevWaypoint { get => prevWaypoint; set => prevWaypoint = value; }
        internal Waypoint NextWaypoint { get => nextWaypoint; set => nextWaypoint = value; }
    }
}
