using System;
using System.Collections.Generic;

namespace day19
{
    public class Orientation
    {
        public Vector3 x;
        public Vector3 y;
        public Vector3 z;

        public Orientation(Vector3 nx, Vector3 ny, Vector3 nz) =>
            (x, y, z) = (nx, ny, nz);

        public Vector3 Rotate(Vector3 v)
        {
            return new Vector3(
                (v.x * x.x) + (v.y * y.x) + (v.z * z.x),
                (v.x * x.y) + (v.y * y.y) + (v.z * z.y),
                (v.x * x.z) + (v.y * y.z) + (v.z * z.z));
        }

        public static List<Orientation> PossibleOrientations()
        {
            List<Orientation> orientations = new List<Orientation>();

            foreach(var xdir in PossibleDirections())
            {
                foreach(var ydir in PossibleDirections())
                {
                    if (ydir.Equals(xdir) ||
                        ydir.Equals(xdir.TimesScalar(-1)))
                    {
                        // y cannot point along the same axis
                        // as x
                        continue;
                    }

                    // There will only be one possible z direction
                    // once x and y are pinned down
                    var zdir = xdir.CrossProduct(ydir);
                    orientations.Add(new Orientation(xdir, ydir, zdir));
                }
            }

            return orientations;
        }

        public static List<Vector3> PossibleDirections()
        {
            List<Vector3> l = new List<Vector3>()
            {
                new Vector3(1, 0, 0),
                new Vector3(-1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, -1, 0),
                new Vector3(0, 0, 1),
                new Vector3(0, 0, -1)
            };

            return l;
        }

    }
}
