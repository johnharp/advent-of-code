using System;
using System.Collections.Generic;

namespace day13
{
    public class DotTracker
    {
        int minx = int.MaxValue;
        int maxx = int.MinValue;

        int miny = int.MaxValue;
        int maxy = int.MinValue;

        public List<Dot> Dots = new List<Dot>();

        public DotTracker()
        {

        }

        public void CreateDots(PuzzleInput input)
        {
            foreach(var line in input.DotLines)
            {
                CreateDot(line);
            }
        }

        public void DoFolds(PuzzleInput input)
        {
            foreach(var line in input.FoldLines)
            {
                DoFold(line);
            }
        }

        public void DoFold(string line)
        {
            string[] parts = line.Split("=");
            int value = int.Parse(parts[1]);

            if (parts[0] == "fold along y")
            {
                FoldAroundY(value);
            }
            else if (parts[0] == "fold along x")
            {
                FoldAroundX(value);
            }
            else
            {
                throw new Exception("problem with input file -- fold along not found");
            }
        }

        public void CreateDot(string s)
        {
            string[] parts = s.Split(",");
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);

            if (x < minx) minx = x;
            if (x > maxx) maxx = x;

            if (y < miny) miny = y;
            if (y > maxy) maxy = y;

            Dot newdot = new Dot(x, y);
            Dots.Add(newdot);
        }

        public List<Dot> DotsWithXGreaterThan(int xval)
        {
            List<Dot> l = new List<Dot>();

            foreach(var dot in Dots)
            {
                if (dot.x > xval)
                {
                    l.Add(dot);
                }
            }

            return l;
        }

        public List<Dot> DotsWithYGreaterThan(int yval)
        {
            List<Dot> l = new List<Dot>();

            foreach (var dot in Dots)
            {
                if (dot.y > yval)
                {
                    l.Add(dot);
                }
            }

            return l;
        }

        public void FoldDotAroundY(Dot d, int yfoldval)
        {
            int newy = (2 * yfoldval) - d.y;

            if (CountDotsWithXY(d.x, newy) > 0)
            {
                Dots.Remove(d);
            }
            else
            {
                d.y = newy;
            }
        }

        public void FoldDotAroundX(Dot d, int xfoldval)
        {
            int newx = (2*xfoldval) -  d.x;

            if (CountDotsWithXY(newx, d.y) > 0)
            {
                Dots.Remove(d);
            }
            else
            {
                d.x = newx;
            }
        }

        public void DumpDotCount()
        {
            Console.WriteLine($"There are {Dots.Count} dots");
        }

        public void RecomputeMaxValues()
        {
            maxx = int.MinValue;
            maxy = int.MinValue;
            minx = int.MaxValue;
            miny = int.MaxValue;

            foreach (var d in Dots)
            {
                if (d.x > maxx) maxx = d.x;
                if (d.y > maxy) maxy = d.y;
                if (d.x < minx) minx = d.x;
                if (d.y < miny) miny = d.y;
            }
        }

        public int CountDotsWithXY(int x, int y)
        {
            int count = 0;

            foreach(Dot d in Dots)
            {
                if (d.x == x && d.y == y) count++;
            }

            return count;
        }

        public void FoldAroundY(int yfoldval)
        {
            List<Dot> l;

            l = DotsWithYGreaterThan(yfoldval);
            foreach (Dot d in l)
            {
                FoldDotAroundY(d, yfoldval);
            }

            RecomputeMaxValues();
        }

        public void FoldAroundX(int xfoldval)
        {
            List<Dot> l;

            l = DotsWithXGreaterThan(xfoldval);
            foreach (Dot d in l)
            {
                FoldDotAroundX(d, xfoldval);
            }

            RecomputeMaxValues();
        }

        public void DumpAllDots()
        {
            DumpDotList(Dots);
        }

        public void Print()
        {
            char[][] buffer = new char[maxy + 1][];
            for (int r = 0; r<= maxy; r++)
            {
                buffer[r] = new char[maxx + 1];
            }

            for (int r = 0; r < buffer.Length; r++)
            {
                for (int c = 0; c < buffer[r].Length; c++)
                {
                    buffer[r][c] = '.';
                }
            }

            foreach (Dot d in Dots)
            {
                buffer[d.y][d.x] = '#';
            }


            for (int r = 0; r < buffer.Length; r++)
            {
                for (int c = 0; c < buffer[r].Length; c++)
                {
                    Console.Write(buffer[r][c]);
                }
                Console.WriteLine();
            }
        }

        public void DumpDotList(List<Dot> l)
        {
            foreach (var dot in l)
            {
                Console.WriteLine($"{dot.x}, {dot.y}");
            }
        }
    }
}
