using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day05
{
    public class PuzzleInput
    {
        public List<Segment> Segments =
            new List<Segment>();

        public int MaxX = 0;
        public int MaxY = 0;


        public PuzzleInput(string filename)
        {
            ReadFile(filename);


        }

        private void ReadFile(string filename)
        {
            var lines = File.ReadLines(
                $"../../../{filename}");

            foreach (var line in lines)
            {
                string[] parts = line.Split(" -> ");
                Point p1 = new Point(parts[0]);
                Point p2 = new Point(parts[1]);

                Segment s = new Segment(p1, p2);
                Segments.Add(s);

                if (p1.x > MaxX) MaxX = p1.x;
                if (p2.x > MaxX) MaxX = p2.x;

                if (p1.y > MaxY) MaxY = p1.y;
                if (p2.y > MaxY) MaxY = p2.y;
            }
        }
    }
}
