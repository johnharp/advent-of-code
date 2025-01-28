using System;
using System.Collections.Generic;
using System.Linq;

namespace day19
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filename = "input-example.txt";
            string filename = "input.txt";
            var input = new PuzzleInput(filename);
            var calc = new Calculator();

            var scanners = input.Scanners;


            List<Scanner> DoneScanners = new List<Scanner>();
            List<Scanner> LocatedScanners = new List<Scanner>();
            List<Scanner> UnlocatedScanners = new List<Scanner>();

            LocatedScanners.Add(scanners[0]);
            for(int i = 1; i<scanners.Count; i++)
            {
                UnlocatedScanners.Add(scanners[i]);
            }

            while (UnlocatedScanners.Count > 0)
            {
                var s1 = LocatedScanners[0];
                List<Scanner> moveFromUnlocatedToLocated = new List<Scanner>();

                foreach (Scanner s2 in UnlocatedScanners)
                {
                    var matches = calc.LookForMatches(s1, s2);
                    if (matches.Count >= 12)
                    {
                        moveFromUnlocatedToLocated.Add(s2);
                    }
                }

                DoneScanners.Add(s1);
                LocatedScanners.Remove(s1);

                foreach (var s in moveFromUnlocatedToLocated)
                {
                    LocatedScanners.Add(s);
                    UnlocatedScanners.Remove(s);
                }

                Console.WriteLine($"{DoneScanners.Count}/{scanners.Count}");
            }

            foreach (Scanner s in LocatedScanners)
            {
                DoneScanners.Add(s);
            }
            LocatedScanners.Clear();
            Console.WriteLine($"{DoneScanners.Count}/{scanners.Count}");

            if (UnlocatedScanners.Count > 0) throw new Exception("failed to locate some scanners");

            //var matches = calc.LookForMatches(scanners[0], scanners[8]);
            //DumpPoints(matches);

            List<string> allPointNames = new List<string>();
            foreach(Scanner s in scanners)
            {
                var points = s.PointsInWorldCoordinates();
                foreach(var point in points)
                {
                    string pointName = point.ToString();
                    allPointNames.Add(pointName);
                }
            }

            allPointNames = allPointNames.Distinct().ToList();
            allPointNames.Sort();
            foreach(string name in allPointNames)
            {
                Console.WriteLine(name);
            }

            Console.Out.WriteLine($"There are {allPointNames.Count} distinct points");
            FindMaxManhattanDistance(scanners);
            Console.Out.WriteLine("stop");

            //Console.Out.WriteLine("---- scanner 0 ----");
            //DumpPoints(scanners[0].PointsInWorldCoordinates());

            //Console.Out.WriteLine("---- scanner 1 ----");
            //DumpPoints(scanners[1].PointsInWorldCoordinates());
        }

        public static void DumpPoints(List<Vector3> points)
        {
            foreach (var p in points)
            {
                Console.WriteLine(p);
            }
        }


        public static void FindMaxManhattanDistance(List<Scanner> scanners)
        {
            int max = int.MinValue;

            for (int i = 0; i < scanners.Count; i++)
            {
                for (int j = 0; j < scanners.Count; j++)
                {
                    if (i == j) continue;

                    Vector3 a = scanners[i].Origin;
                    Vector3 b = scanners[j].Origin;

                    Vector3 c = a.Subtract(b);
                    int distance = Math.Abs(c.x) +
                        Math.Abs(c.y) +
                        Math.Abs(c.z);

                    if (distance > max) max = distance;
                }
            }

            Console.WriteLine($"Max Manhattan distance is {max}");
        }
    }


}
