using System;
using System.Collections.Generic;

namespace day19
{
    public class Calculator
    {
        public List<Orientation> Orientations;

        public Calculator()
        {
            Orientations = Orientation.PossibleOrientations();
        }

        public List<Vector3> LookForMatches(Scanner s1, Scanner s2)
        {
            var s1points = s1.PointsInWorldCoordinates();
            s1points.Sort();

            // assume s1 location and orientation line up
            // with the world origin and orientation
            foreach (var o in Orientations)
            {
                // rotate the points in s2 to each orientation
                var s2points = ApplyOrientation(o, s2.Points);

                var translations = PossibleTranslations(s1points, s2points);
                foreach(var translation in translations)
                {
                    List<Vector3> translatedPoints;
                    translatedPoints = ApplyTranslation(translation, s2points);
                    translatedPoints.Sort();
                    var matches = Match(s1points, translatedPoints);
                    if (matches.Count >= 12)
                    {
                        s2.IsLocated = true;
                        s2.Orientation = o;
                        s2.Origin = translation;
                        return matches;
                    }
                }

            }

            return new List<Vector3>();
        }

        public List<Vector3> ApplyOrientation(Orientation o, List<Vector3> points)
        {
            List<Vector3> newPoints = new List<Vector3>();

            foreach(var point in points)
            {
                Vector3 newPoint = o.Rotate(point);
                newPoints.Add(newPoint);
            }

            return newPoints;
        }


        public List<Vector3> ApplyTranslation(Vector3 trans, List<Vector3> points)
        {
            List<Vector3> newPoints = new List<Vector3>();

            foreach (var point in points)
            {
                Vector3 newPoint = trans.Add(point);
                newPoints.Add(newPoint);
            }

            return newPoints;
        }

        public List<Vector3> PossibleTranslations(List<Vector3> l1, List<Vector3> l2)
        {
            List<Vector3> translations = new List<Vector3>();

            foreach(Vector3 p1 in l1)
            {
                foreach(Vector3 p2 in l2)
                {
                    translations.Add(p1.Subtract(p2));
                }
            }

            return translations;
        }

        public List<Vector3> Match(List<Vector3> l1, List<Vector3> l2)
        {
            int i1 = 0;
            int i2 = 0;

            List<Vector3> matches = new List<Vector3>();

            while (i1 < l1.Count && i2 < l2.Count)
            {
                Vector3 v1 = l1[i1];
                Vector3 v2 = l2[i2];

                int order = v1.CompareSortOrder(v2);
                if (order == 0)
                {
                    matches.Add(v1);
                    i1++;
                    i2++;
                }
                else if (order < 0) i1++;
                else if (order > 0) i2++;
            }

            return matches;
        }
    }
}
