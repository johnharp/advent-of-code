using System;
namespace day25
{
    public class Grid
    {
        public int Width;
        public int Height;

        public char[,] Values;

        public Grid()
        {
        }

        public Grid(Grid g)
        {
            Width = g.Width;
            Height = g.Height;

            Values = new char[Width, Height];
        }

        public void Init(string[] lines)
        {
            Width = lines[0].Length;
            Height = lines.Length;

            Values = new char[Width, Height];

            for(int y = 0; y<Height; y++)
            {
                string line = lines[y];
                for (int x = 0; x<Width; x++)
                {
                    char c = line[x];

                    Values[x, y] = c;
                }
            }
        }

        public char Get(int x, int y)
        {
            return Values[x, y];
        }

        public char Get((int, int) coord)
        {
            return Values[coord.Item1, coord.Item2];
        }

        public void Set(int x, int y, char v)
        {
            Values[x, y] = v;
        }

        public void Set((int, int) xy, char v)
        {
            Values[xy.Item1, xy.Item2] = v;
        }

        public (int, int) LeftOf(int x, int y)
        {
            int x1 = (x == 0 ? Width - 1 : x - 1);
            int y1 = y;

            return (x1, y1);
        }

        public (int, int) Above(int x, int y)
        {
            int x1 = x;
            int y1 = y == 0 ? Height - 1 : y - 1;

            return (x1, y1);
        }

        public void Dump()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(Get(x, y));
                }
                Console.WriteLine();
            }
        }
    }
}
