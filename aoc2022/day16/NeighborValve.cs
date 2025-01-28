using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day16
{
    public class NeighborValve
    {
        public Valve Valve { get; set; }
        public int TraversalCost { get; set; }

        public NeighborValve(Valve v, int traversalCost)
        {
            Valve = v;
            TraversalCost = traversalCost;
        }
    }
}
