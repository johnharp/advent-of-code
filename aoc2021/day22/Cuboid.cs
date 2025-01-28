using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace day22
{
    public class Cuboid
    {
        public long x1;
        public long x2;
        public long y1;
        public long y2;
        public long z1;
        public long z2;

        public long NumIntersectingCuboids = 0;

        public bool value = false;
        public bool Finalized = false;

        public long Total
        {
            get
            {
                return
                    (x2 - x1 + 1) *
                    (y2 - y1 + 1) *
                    (z2 - z1 + 1);

            }
        }

        public Cuboid(string str)
        {
            Regex rx = new Regex(
                @"^([onf]+)\s+x=(\-?\d+)\.\.(\-?\d+),y=(\-?\d+)\.\.(\-?\d+),z=(\-?\d+)..(\-?\d+)$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);


            MatchCollection matches = rx.Matches(str);

            if (matches.Count == 1)
            {
                GroupCollection groups = matches[0].Groups;

                string onoff = groups[1].Value;

                if (onoff == "on") value = true;
                else if (onoff == "off") value = false;
                else throw new Exception($"Invalid cuboid string: {str}");

                x1 = long.Parse(groups[2].Value);
                x2 = long.Parse(groups[3].Value);
                y1 = long.Parse(groups[4].Value);
                y2 = long.Parse(groups[5].Value);
                z1 = long.Parse(groups[6].Value);
                z2 = long.Parse(groups[7].Value);

                if (x1 > x2) throw new Exception("didn't expect out of order x's");
                if (y1 > y2) throw new Exception("didn't expect out of order y's");
                if (z1 > z2) throw new Exception("didn't expect out of order z's");
            }
            else
            {
                throw new Exception($"Invalid player string: {str}");
            }
        }

        public Cuboid(
            bool v,
            long nx1, long nx2,
            long ny1, long ny2,
            long nz1, long nz2)
        {
            value = v;

            x1 = nx1;
            x2 = nx2;
            y1 = ny1;
            y2 = ny2;
            z1 = nz1;
            z2 = nz2;
        }


        public bool Contains(long x, long y, long z)
        {
            return (
                x >= x1 && x <= x2 &&
                y >= y1 && y <= y2 &&
                z >= z1 && z <= z2);
        }

        // Returns true if "other" is completely contained
        // by this Cuboid
        public bool Contains(Cuboid other)
        {
            return (
                other.x1 >= x1 &&
                other.x2 <= x2 &&
                other.y1 >= y1 &&
                other.y2 <= y2 &&
                other.z1 >= z1 &&
                other.z2 <= z2
            );
        }

        // Returns true if "other" intersects
        // this Cuboid
        public bool Intersects(Cuboid other)
        {
            return (
                other.x1 <= x2 && other.x2 >= x1 &&
                other.y1 <= y2 && other.y2 >= y1 &&
                other.z1 <= z2 && other.z2 >= z1
            );
        }

        public List<Cuboid> Split(Cuboid s)
        {
           var results = new List<Cuboid>();

            foreach (var r1 in SplitX(s.x1, s.x2))
            {
                foreach (var r2 in r1.SplitY(s.y1, s.y2))
                {
                    foreach (var r3 in r2.SplitZ(s.z1, s.z2))
                    {
                        results.Add(r3);
                    }
                }
            }

            return results;
        }

        public List<Cuboid> SplitX(long sx1, long sx2)
        {
            var splits = SplitTuples(p: (x1, x2), s: (sx1, sx2));
            var results = new List<Cuboid>();
            foreach(var split in splits)
            {
                results.Add(new Cuboid(
                    value,
                    split.Item1, split.Item2,
                    y1, y2,
                    z1, z2
                    ));
            }
            return results;
        }

        public List<Cuboid> SplitY(long sy1, long sy2)
        {
            var splits = SplitTuples(p: (y1, y2), s: (sy1, sy2));
            var results = new List<Cuboid>();
            foreach (var split in splits)
            {
                results.Add(new Cuboid(
                    value,
                    x1, x2,
                    split.Item1, split.Item2,
                    z1, z2
                    ));
            }
            return results;
        }

        public List<Cuboid> SplitZ(long sz1, long sz2)
        {
            var splits = SplitTuples(p: (z1, z2), s: (sz1, sz2));
            var results = new List<Cuboid>();
            foreach (var split in splits)
            {
                results.Add(new Cuboid(
                    value,
                    x1, x2,
                    y1, y2,
                    split.Item1, split.Item2
                    ));
            }
            return results;
        }
        public static List<(long, long)> SplitTuples((long, long) p, (long, long) s)
        {
            var results = new List<(long, long)>();

            if (s.Item1 <= p.Item1 && s.Item2 >= p.Item2)
            {
                AddTuple(results, (p.Item1, p.Item2));
            }
            else if (p.Item1 < s.Item1 && p.Item2 <= s.Item2)
            {
                // overlap only left side
                AddTuple(results, (p.Item1, s.Item1 - 1));
                AddTuple(results, (s.Item1, p.Item2));
            }
            else if (p.Item1 >= s.Item1 && p.Item2 > s.Item2)
            {
                // overlap only right side
                AddTuple(results, (p.Item1, s.Item2));
                AddTuple(results, (s.Item2 + 1, p.Item2));
            }
            else
            {
                // overlaps on both
                AddTuple(results, (p.Item1, s.Item1 - 1));
                AddTuple(results, (s.Item1, s.Item2));
                AddTuple(results, (s.Item2+1, p.Item2));
            }

            return results;
        }

        private static void AddTuple(List<(long, long)> l, (long, long) t)
        {
            if (t.Item1 <= t.Item2) l.Add(t);
        }

        public override string ToString()
        {
            string onoff = value ? "on" : "off";

            return $"{onoff} x[{x1},{x2}] y[{y1},{y2}] z[{z1},{z2}]   #{Total}";
        }
    }
}
