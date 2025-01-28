using System;
namespace day15
{
    public class Map
    {
        // Values[col][row]
        public long[][] Values;
        // LPS = Least Path Sum
        public long[][] LPS;


        public long NumCols;
        public long NumRows;

        public Map(string[] initialValues)
        {
            NumRows = initialValues.Length;
            NumCols = initialValues[0].Length;

            Values = new long[NumCols][];
            LPS = new long[NumCols][];

            for (long col=0; col<NumCols; col++)
            {
                Values[col] = new long[NumRows];
                LPS[col] = new long[NumRows];
            }

            for(long row = 0; row<NumRows; row++)
            {
                char[] colChars = initialValues[row].ToCharArray();
                for (long col = 0;  col < colChars.Length; col++)
                {
                    long value = long.Parse(new string(colChars[col], 1));

                    Values[col][row] = value;
                }
            }
        }


        public Map(Map map, long numTiles)
        {
            NumCols = map.NumCols * numTiles;
            NumRows = map.NumRows * numTiles;

            Values = new long[NumCols][];
            LPS = new long[NumCols][];

            for (long col = 0; col < NumCols; col++)
            {
                Values[col] = new long[NumRows];
                LPS[col] = new long[NumRows];
            }

            for (long rowtile = 0; rowtile < numTiles; rowtile++)
            {
                for (long coltile = 0; coltile < numTiles; coltile++)
                {
                    for (long row = 0; row < map.NumRows; row++)
                    {
                        for (long col = 0; col < map.NumCols; col++)
                        {
                            long value = map.Values[col][row];
                            for(int i = 0; i<rowtile+coltile; i++)
                            {
                                value += 1;
                                if (value > 9) value = 1;
                            }

                            Values
                                [col + (coltile * map.NumCols)]
                                [row + (rowtile * map.NumRows)] = value;
                        }
                    }
                }
            }

        }

        // The Least Path Sum (LPS) at a point is the minimum
        // cost to reach the goal from that point (if you follow
        // one of the optimal paths from that point to the goal).
        public long LeastPathSumAtPoint(long col, long row)
        {
            // If you start at the goal, there is no cost to reach
            // it.
            if (col == NumCols - 1 && row == NumRows - 1) return 0;

            if (LPS[col][row] > 0) return LPS[col][row];

            return -1;
        }

        public void ComputeLeastPathSums()
        {
            for (long row = NumRows - 1; row >= 0; row--)
            {
                for (long col = NumCols - 1; col >= 0; col--)
                {
                    long lps;

                    if (col == NumCols - 1 && row == NumRows - 1)
                    {
                        // we're at the goal
                        lps = 0;
                    }
                    else
                    {
                        long rightLps = col == NumCols - 1 ? long.MaxValue :
                            Values[col + 1][row] + LPS[col + 1][row];

                        long downLps = row == NumRows - 1 ? long.MaxValue :
                            Values[col][row + 1] + LPS[col][row + 1];

                        lps = Math.Min(rightLps, downLps);
                    }

                    LPS[col][row] = lps;
                }
            }
        }

        public void DumpLeastPathSums()
        {
            DumpGrid(LPS, "D4", spaces: true);
        }

        public void DumpValues()
        {
            DumpGrid(Values, "D1", spaces: false);
        }

        public void DumpGrid(long[][] g, string format, bool spaces)
        {
            for (int row = 0; row < NumRows && row < 10; row++)
            {
                for (int col = 0; col < NumCols && col < 10; col++)
                {
                    Console.Write(g[col][row].ToString(format));
                    if (spaces) Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

    }
}
