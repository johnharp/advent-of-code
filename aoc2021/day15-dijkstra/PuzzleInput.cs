using System;
using System.Collections.Generic;
using System.IO;

namespace day15_dijkstra
{
    public class PuzzleInput
    {
        public string[] Lines;
        public int NumCols
        {
            get { return Lines[0].Length; }
        }

        public int NumRows
        {
            get { return Lines.Length; }
        }

        public PuzzleInput(string filename)
        {
            ReadInputFile($"../../../{filename}");
        }

        private void ReadInputFile(string fullpath)
        {
            Lines = File.ReadAllLines(fullpath);
        }

        public void CreateNodes(int numTiles = 1)
        {
            Node.NodeIndex = new Dictionary<string, Node>();
            Node.NumCols = Lines[0].Length * numTiles;
            Node.NumRows = Lines.Length * numTiles;

            for (int col = 0; col < Lines[0].Length; col++)
            {
                for (int row = 0; row < Lines.Length; row++)
                {
                    int value = int.Parse(new string(Lines[row][col], 1));

                    for(int i = 0; i < numTiles; i++)
                    {
                        for (int j = 0; j<numTiles; j++)
                        {
                            int tvalue = value;

                            for (int k = 0; k < i + j; k++) {
                                tvalue++;
                                if (tvalue > 9) tvalue = 1;
                            }

                            int c = (i * Lines[0].Length) + col;
                            int r = (j * Lines.Length) + row;

                            new Node(c, r, tvalue);
                        }
                    }
                }
            }
        }

    }
}
