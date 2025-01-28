using System;
namespace day17
{
    public class ProbeState
    {
        public long x; // Current X Position
        public long y; // Current Y Position

        public long vx; // Current X Velocity
        public long vy; // Current Y Velocity

        public long vxi; // Initial X Velocity
        public long vyi; // Initial Y Velocity

        public long drag = 1;
        public long gravity = 1;

        public long t = 0;

        public long maxAttainedY = long.MinValue;

        public (long, long) hitIntervalX = (-1, -1);
        public (long, long) hitIntervalY = (-1, -1);

        public ProbeState(long initialXvel, long initialYvel)
        {
            x = 0;
            y = 0;

            vxi = initialXvel;
            vyi = initialYvel;

            vx = vxi;
            vy = vyi;
        }

        public void Step()
        {
            y += vy;
            vy -= gravity;

            x += vx;
            if (vx > 0) vx -= drag;
            if (vx < 0) vx += drag;

            if (maxAttainedY < y) maxAttainedY = y;

            t++;
        }
    }
}
