using System;
namespace day05
{
    public class Point
    {
        public int x;
        public int y;

        public Point(string s)
        {
            string[] parts = s.Split(",");
            x = int.Parse(parts[0]);
            y = int.Parse(parts[1]);
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
