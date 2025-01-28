using System;
using System.Collections.Generic;

namespace day19
{
    public class Scanner
    {
        public int Id = -1;
        public List<Vector3> Points = new List<Vector3>();

        public bool IsLocated = false;
        public Orientation Orientation = null;
        public Vector3 Origin;

        public Scanner()
        {
        }

        public List<Vector3> PointsInWorldCoordinates()
        {
            List<Vector3> l = new List<Vector3>();

            foreach(var point in Points)
            {
                var rotatedPoint = Orientation.Rotate(point);
                var translatedPoint = Origin.Add(rotatedPoint);
                l.Add(translatedPoint);
            }

            return l;
        }
    }
}
