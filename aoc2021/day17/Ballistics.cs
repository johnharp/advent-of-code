using System;
using System.Collections.Generic;
namespace day17
{
    public class Ballistics
    {
        public Target Target;

        public Ballistics(Target t)
        {
            Target = t;
        }

        public bool WillHit(long vxi, long vyi)
        {
            ProbeState state = new ProbeState(vxi, vyi);
            if (Target.IsHit(state)) return true;

            while (MightStillHit(state))
            {
                state.Step();

                if (Target.IsHit(state))
                {
                    return true;
                }
            }
            return false;
        }

        public bool MightStillHit(ProbeState s)
        {
            return (s.y > Target.mintargety);
        }

        public long MaxAchievedY(long vxi, long vyi)
        {
            var s = new ProbeState(vxi, vyi);

            long max = s.y;

            while (s.y >= max)
            {
                s.Step();
                if (max < s.y) max = s.y;
            }

            return max;
        }

        public long MinReasonableVX { get { return 1;  } }
        public long MinReasonableVY { get { return Target.mintargety - 1; } }
        public long MaxReasonableVX { get { return Target.maxtargetx + 1; } }
        public long MaxReasonableVY { get { return -Target.mintargety + 1; } }

        public (long, long) DetermineBestInitialVelocity()
        {
            (long, long) bestv = (0, 0);
            long maxYfound = long.MinValue;

            long minxv = MinReasonableVX;
            long minyv = MinReasonableVY;
            long maxxv = MaxReasonableVX;
            long maxyv = MaxReasonableVY;

            for (long vyi = minyv; vyi <= maxyv; vyi++)
            {
                for (long vxi = minxv; vxi <= maxxv; vxi++)
                {
                    if (WillHit(vxi, vyi))
                    {
                        long maxy = MaxAchievedY(vxi, vyi);
                        if (maxy > maxYfound)
                        {
                            maxYfound = maxy;
                            bestv = (vxi, vyi);
                        }
                    }
                }
            }

            return bestv;
        }

        public List<(long, long)> DetermineAllSuccessfulVelocities()
        {
            List<(long, long)> velocities = new List<(long, long)>();

            long minxv = MinReasonableVX;
            long minyv = MinReasonableVY;
            long maxxv = MaxReasonableVX;
            long maxyv = MaxReasonableVY;

            for (long vyi = minyv; vyi <= maxyv; vyi++)
            {
                for (long vxi = minxv; vxi <= maxxv; vxi++)
                {
                    if (WillHit(vxi, vyi))
                    {
                        velocities.Add((vxi, vyi));
                    }
                }
            }

            return velocities;
        }
    }
}
