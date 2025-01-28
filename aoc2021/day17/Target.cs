using System;

namespace day17
{
    public class Target
    {
        public long mintargetx = long.MaxValue;
        public long maxtargetx = long.MinValue;
        public long mintargety = long.MaxValue;
        public long maxtargety = long.MinValue;

        // str is of format:
        // target area: x=128..160, y=-142..-88
        public Target(string str)
        {
            str = str.Replace("target area: ", "");
            str = str.Replace("x=", "");
            str = str.Replace(" y=", "");
            string[] xyparts = str.Split(",");
            string[] xparts = xyparts[0].Split("..");
            string[] yparts = xyparts[1].Split("..");

            mintargetx = long.Parse(xparts[0]);
            maxtargetx = long.Parse(xparts[1]);

            mintargety = long.Parse(yparts[0]);
            maxtargety = long.Parse(yparts[1]);

            if (mintargetx > maxtargetx ||
                mintargety > maxtargety)
            {
                throw new Exception("Error initializing target");
            }
        }

        public bool IsHitX(ProbeState s)
        {
            return s.x >= mintargetx &&
                s.x <= maxtargetx;
        }

        public bool IsHitY(ProbeState s)
        {
            return s.y >= mintargety &&
                s.y <= maxtargety;
        }

        public bool IsHit(ProbeState s)
        {
            return IsHitX(s) && IsHitY(s);
        }

    }
}
