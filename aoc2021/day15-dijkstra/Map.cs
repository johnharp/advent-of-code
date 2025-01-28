using System;
using System.Collections.Generic;

namespace day15_dijkstra
{
    public class Map
    {
        // Values[col][row]
        public long[,] Risk;
        public long[,] RiskSum;
        public bool[,] IsFinal;

        public long NumCols;
        public long NumRows;

        

        public void Eval(long col, long row)
        {
            var neighbors = NonFinalNeighbors(col, row);

            foreach (var neighbor in neighbors)
            {
                long risk = RiskSum[col, row] +
                    Risk[neighbor.Item1, neighbor.Item2];

                if (risk < RiskSum[neighbor.Item1, neighbor.Item2])
                {
                    RiskSum[neighbor.Item1, neighbor.Item2] = risk; 
                }
            }

            IsFinal[col, row] = true;
        }


        public List<(long, long)> NonFinalNeighbors(long col, long row)
        {
            List<(long, long)> locs = new List<(long, long)>();

            TryNeighbor(locs, col-1,  row);
            TryNeighbor(locs, col, row-1);
            TryNeighbor(locs, col+1,  row);
            TryNeighbor(locs,  col,  row+1);

            return locs;
        }

        private void TryNeighbor(List<(long, long)> locs, long c, long r)
        {
            if (c >= 0 && r >= 0 &&
                c < NumCols && r < NumRows &&
                !IsFinal[c, r])
            {
                locs.Add((c, r));
            }
        }



        public Map(long numCols, long numRows)
        {
            NumCols = numCols;
            NumRows = numRows;

            Risk = new long[NumCols, NumRows];
            RiskSum = new long[NumCols, NumRows];
            IsFinal = new bool[NumCols, NumRows];

            for (int c = 0; c < NumCols; c++)
            {
                for (int r = 0; r < NumRows; r++)
                {
                    RiskSum[c, r] = long.MaxValue;
                }
            }

            RiskSum[0, 0] = 0;
        }

        public void InitializeRiskValues(string[] initialValues, int numTiles = 1)
        {

            for (long rowtile = 0; rowtile < numTiles; rowtile++)
            {
                for (long coltile = 0; coltile < numTiles; coltile++)
                {
                    for (int row = 0; row < initialValues.Length; row++)
                    {
                        for (int col = 0; col < initialValues[0].Length; col++)
                        {
                            long value = long.Parse(new string(initialValues[row][col], 1));

                            for (int i = 0; i < rowtile + coltile; i++)
                            {
                                value += 1;
                                if (value > 9) value = 1;
                            }

                            Risk
                                [col + (coltile * initialValues[0].Length),
                                row + (rowtile * initialValues.Length)] = value;
                        }
                    }
                }
            }
        }

        public void DumpValues()
        {
            DumpGrid(Risk, "D1", spaces: false);
        }

        private void DumpGrid(long[,] g, string format, bool spaces)
        {
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    Console.Write(g[col,row].ToString(format));
                    if (spaces) Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

    }
}
