using System;
namespace day09
{
    public class Map
    {
        public long[][] v;
        public long Width;
        public long Height;

        public Map(string[] lines)
        {
            string firstLine = lines[0].Trim();

            Width = firstLine.Length;
            Height = lines.Length;

            // map values are arranged in v[row][col]
            v = new long[Height][];


            for (int row = 0; row<lines.Length; row++)
            {
                string line = lines[row].Trim();
                v[row] = new long[Width];

                int col = 0;
                foreach(char c in line)
                {
                    long value = long.Parse(new String(c, 1));
                    v[row][col] = value;

                    col++;
                }
            }

        }

        public void FindBasins()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    long risk = RiskLevelOfPoint(row, col);

                    // only low points have risk level > 0
                    if (risk > 0)
                    {
                        new Basin(row, col);
                    }
                }
            }
        }

        public void MeasureBasinSizes()
        {
            for(long row = 0; row < Height; row++)
            {
                for (long col = 0; col < Width; col++)
                {
                    // points of height 9 aren't part of any basin
                    if (v[row][col] == 9) continue;

                    // if point is already the low point of a basin
                    // skip it also
                    if (Basin.Get(row, col) != null) continue;

                    // Otherwise this point needs to be added to an existing
                    // basin
                    Basin b = FlowToBasin(row, col);
                    if (b == null) throw new Exception("Error: couldn't find a basin");
                    b.size++;
                }
            }
        }

        private Basin FlowToBasin(long row, long col)
        {
            // keep moving to the lowest neighboring point until
            // a basin is reached

            long r = row;
            long c = col;

            while (true)
            {
                var deltas = new[]{(-1, 0), (1, 0), (0, -1), (0, 1)};

                foreach (var delta in deltas)
                {
                    long deltaRow = delta.Item1;
                    long deltaCol = delta.Item2;

                    // avoid going off the map
                    if (r + deltaRow < 0 ||
                        r + deltaRow > Height - 1 ||
                        c + deltaCol < 0 ||
                        c + deltaCol > Width - 1) continue;

                    if (v[r + deltaRow][c + deltaCol] < v[r][c])
                    {
                        r = r + deltaRow;
                        c = c + deltaCol;

                        Basin b = Basin.Get(r, c);
                        if (b != null) return b;
                    }

                }
            }
        }

        public long SumRiskLevels()
        {
            long sum = 0;

            for(long row = 0; row < Height; row++)
            {
                for (long col=0; col<Width; col++)
                {
                    sum += RiskLevelOfPoint(row, col);
                }
            }

            return sum;
        }

        private long RiskLevelOfPoint(long row, long col)
        {
            long value = v[row][col];
            long riskLevel = 0;

            if ((row == 0 || v[row - 1][col] > value) &&
                (col == Width - 1 || v[row][col + 1] > value) &&
                (row == Height - 1 || v[row + 1][col] > value) &&
                (col == 0 || v[row][col - 1] > value))
            {
                riskLevel = value + 1;
            }

            return riskLevel;
        }

        public void Dump()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    long value = v[row][col];
                    Console.Out.Write(value);
                }
                Console.Out.WriteLine();
            }
        }
    }
}
