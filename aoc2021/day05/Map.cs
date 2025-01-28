using System;
namespace day05
{
    public class Map
    {
        public int MaxX;
        public int MaxY;

        public int[][] Values;

        public Map(int maxx, int maxy)
        {
            MaxX = maxx;
            MaxY = maxy;

            Values = new int[maxx+1][];
            for(int x = 0; x < maxx+1; x++)
            {
                Values[x] = new int[maxy+1];
            }
        }

        public void AddSegment(Segment s)
        {
            int dx = 0;
            if (s.P2.x > s.P1.x) dx = 1;
            else if (s.P2.x < s.P1.x) dx = -1;

            int dy = 0;
            if (s.P2.y > s.P1.y) dy = 1;
            if (s.P2.y < s.P1.y) dy = -1;

            // for part 1:
            // only consider horizontal or vertical
            // lines -- bail if changing in both x and y
            //if (dx != 0 && dy != 0) return;

            Point p = new Point(s.P1.x, s.P1.y);

            while (p.x != s.P2.x ||
                p.y != s.P2.y)
            {
                Values[p.x][p.y]++;
                p.x += dx;
                p.y += dy;
            }

            Values[s.P2.x][s.P2.y]++;
        }

        public int CountValuesGreaterThan(int v)
        {
            int count = 0;

            for (int x = 0; x < MaxX+1; x++)
            {
                for (int y = 0; y < MaxY+1; y++)
                {
                    if (Values[x][y] > v) count++;
                }
            }

            return count;
        }
    }
}
